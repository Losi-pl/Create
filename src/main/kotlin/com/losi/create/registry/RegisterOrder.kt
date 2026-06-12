package com.losi.create.registry

import com.losi.create.utility.exclude
import com.losi.create.utility.orElse
import com.losi.create.utility.toMutableMap
import java.util.UUID
import java.util.function.Consumer

/**Used to construct pipeline of resource processing*/
object RegisterOrder {
    /**All processes that take part in overall resource creation*/
    private val processes = mutableMapOf<UUID, Pair<Runnable, String>>()
    /**Marks which processes are dependent on others being finished already*/
    private val precessRequirements = mutableMapOf<UUID, List<UUID>>()
    /**Marks which processes are to be done just after others and not when their turn comes, exclusive with [Last][lastPrecesses]*/
    private val precessesJustAfter = mutableMapOf<UUID, MutableList<UUID>>()
    /**Marks which processes are to be done last, exclusive with [Just After][precessesJustAfter]*/
    private val lastPrecesses = mutableListOf<UUID>()

    /**Registers a new process to be done during resources loading and it's requirements if there are any
     * @param uuid Used to identify a specific process
     * @param action The process in question to be registered
     * @param name A name for the process for easier human identification
     * @param requirements The additional requirements of this process*/
    fun registerProcess(uuid: UUID, action: Runnable, name: String = uuid.toString(), requirements: Consumer<Requirements>? = null) {
        require(processes[uuid] == null) { "Process $uuid is already registered" }

        processes[uuid] = Pair(action, name)

        val dependency = mutableListOf<UUID>()
        var rightAfter: UUID? = null
        var doLast = false

        requirements?.accept(object : Requirements {
            override fun dependsOn(uuid: UUID) { dependency.add(uuid) }
            override fun justAfter(@Suppress("PARAMETER_NAME_CHANGED_ON_OVERRIDE") myUuid: UUID) {
                check(!doLast) { "This process was already marked to be done among the last" }
                require(!(precessesJustAfter[myUuid]?.contains(uuid)?: false)) { "This element is already requested" }
                rightAfter = myUuid
            }
            override fun doLast() {
                check(rightAfter == null) { "This process was already marked to be done just after $rightAfter" }
                doLast = true
            }
        })

        precessRequirements[uuid] = dependency

        rightAfter?.let { after ->
            precessesJustAfter[after]?.add(uuid).orElse { precessesJustAfter[after] = mutableListOf(uuid) }
        }
        if(doLast) lastPrecesses.add(uuid)
    }

    internal fun precesses(): Sequence<Pair<Runnable, String>> {
        val done = processes.keys.asSequence().map { it to false }.toMutableMap()
        val doneAfter = precessesJustAfter.values.flatten()

        return sequence {
            fun trackBack(uuid: UUID): Sequence<UUID> = sequence {
                precessRequirements[uuid]?.let { list ->
                    list.forEach {
                        if(done[it] == false)
                            yieldAll(trackBack(it))
                    }
                }
                yield(uuid)
                precessesJustAfter[uuid]?.let { list ->
                    list.forEach {
                        if(done[it] == false)
                            yieldAll(trackBack(it))
                    }
                }
            }

            processes.keys.asSequence().exclude(lastPrecesses).forEach {
                if(doneAfter.contains(it))
                    return@forEach
                if(done[it] == false)
                    yieldAll(trackBack(it))
            }
            lastPrecesses.forEach {
                if(done[it] == false)
                    yieldAll(trackBack(it))
            }

            processes.clear()
            precessRequirements.clear()
            precessesJustAfter.clear()
            lastPrecesses.clear()
        }.filter {
            if(done[it] == true)
                return@filter false
            done[it] = true
            return@filter true
        }.map { processes[it]!! }
    }

    /**Meant for [processes] ut is used to specify requirements of a process*/
    interface Requirements {
        fun dependsOn(uuid: UUID)
        fun justAfter(uuid: UUID)
        fun doLast()
    }
}