@file:Suppress("unused")
package com.losi.create.registry

import com.losi.create.utility.exclude
import com.losi.create.utility.orElse
import com.losi.create.utility.toMutableMap
import java.util.UUID
import java.util.function.Consumer
import kotlin.uuid.ExperimentalUuidApi
import kotlin.uuid.Uuid
import kotlin.uuid.toKotlinUuid

/**Used to construct pipeline of resource processing*/
object RegisterOrder {
    /**All processes that take part in overall resource creation*/ @OptIn(ExperimentalUuidApi::class)
    private val processes = mutableMapOf<Uuid, Pair<Runnable, String>>()
    /**Marks which processes are dependent on others being finished already*/@OptIn(ExperimentalUuidApi::class)
    private val precessRequirements = mutableMapOf<Uuid, List<Uuid>>()
    /**Marks which processes are to be done just after others and not when their turn comes, exclusive with [Last][lastPrecesses]*/@OptIn(ExperimentalUuidApi::class)
    private val precessesJustAfter = mutableMapOf<Uuid, MutableList<Uuid>>()
    /**Marks which processes are to be done last, exclusive with [Just After][precessesJustAfter]*/@OptIn(ExperimentalUuidApi::class)
    private val lastPrecesses = mutableListOf<Uuid>()

    /**Registers a new process to be done during resources loading and it's requirements if there are any
     * @param process The process to be executed*/
    fun registerProcess(process: RegisterProcess) {
        @OptIn(ExperimentalUuidApi::class)
        registerProcess(process.uuid, process.action, process.name, null)
    }

    /**Registers a new process to be done during resources loading and it's requirements if there are any
     * @param process The process to be executed
     * @param requirements The additional requirements of this process*/
    fun registerProcess(process: RegisterProcess, requirements: Requirements.() -> Unit) {
        @OptIn(ExperimentalUuidApi::class)
        registerProcess(process.uuid, process.action, process.name, Consumer<Requirements> { it.requirements() })
    }

    /**Registers a new process to be done during resources loading and it's requirements if there are any
     * @param process The process to be executed
     * @param requirements The additional requirements of this process*/
    fun registerProcess(process: RegisterProcess, requirements: Consumer<Requirements>?) {
        @OptIn(ExperimentalUuidApi::class)
        registerProcess(process.uuid, process.action, process.name, requirements)
    }

    /**Registers a new process to be done during resources loading and it's requirements if there are any
     * @param uuid Used to identify a specific process
     * @param action The process in question to be registered
     * @param name A name for the process for easier human identification
     * @param requirements The additional requirements of this process*/
    @OptIn(ExperimentalUuidApi::class)
    fun registerProcess(uuid: Uuid, action: Runnable, name: String = uuid.toString(), requirements: Requirements.() -> Unit) {
        registerProcess(uuid, action, name, Consumer<Requirements> { it.requirements() })
    }

    /**Registers a new process to be done during resources loading and it's requirements if there are any
     * @param uuid Used to identify a specific process
     * @param action The process in question to be registered
     * @param name A name for the process for easier human identification
     * @param requirements The additional requirements of this process*/
    fun registerProcess(uuid: UUID, action: Runnable, name: String = uuid.toString(), requirements: Requirements.() -> Unit) {
        @OptIn(ExperimentalUuidApi::class)
        registerProcess(uuid.toKotlinUuid(), action, name, Consumer<Requirements> { it.requirements() })
    }

    /**Registers a new process to be done during resources loading and it's requirements if there are any
     * @param uuid Used to identify a specific process
     * @param action The process in question to be registered
     * @param name A name for the process for easier human identification
     * @param requirements The additional requirements of this process*/
    fun registerProcess(uuid: UUID, action: Runnable, name: String = uuid.toString(), requirements: Consumer<Requirements>? = null) {
        @OptIn(ExperimentalUuidApi::class)
        registerProcess(uuid.toKotlinUuid(), action, name, requirements)
    }

    /**Registers a new process to be done during resources loading and it's requirements if there are any
     * @param uuid Used to identify a specific process
     * @param action The process in question to be registered
     * @param name A name for the process for easier human identification
     * @param requirements The additional requirements of this process*/
    @OptIn(ExperimentalUuidApi::class)
    fun registerProcess(uuid: Uuid, action: Runnable, name: String = uuid.toString(), requirements: Consumer<Requirements>? = null) {
        require(processes[uuid] == null) { "Process $uuid is already registered" }

        processes[uuid] = Pair(action, name)

        val dependency = mutableListOf<Uuid>()
        var rightAfter: Uuid? = null
        var doLast = false

        requirements?.accept(object : Requirements {
            override fun dependsOn(uuid: Uuid) { dependency.add(uuid) }
            override fun justAfter(@Suppress("PARAMETER_NAME_CHANGED_ON_OVERRIDE") myUuid: Uuid) {
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

    internal fun precesses(): Sequence<Pair<Runnable, String>> = @OptIn(ExperimentalUuidApi::class) run {
        val done = processes.keys.asSequence().map { it to false }.toMutableMap()
        val doneAfter = precessesJustAfter.values.flatten()

        return sequence {
            fun trackBack(uuid: Uuid): Sequence<Uuid> = sequence {
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
        fun dependsOn(process: RegisterProcess) {
            @OptIn(ExperimentalUuidApi::class)
            dependsOn(process.uuid)
        }
        fun dependsOn(uuid: UUID) {
            @OptIn(ExperimentalUuidApi::class)
            dependsOn(uuid.toKotlinUuid())
        }
        @OptIn(ExperimentalUuidApi::class)
        fun dependsOn(uuid: Uuid)
        fun justAfter(process: RegisterProcess) {
            @OptIn(ExperimentalUuidApi::class)
            justAfter(process.uuid)
        }
        fun justAfter(uuid: UUID) {
            @OptIn(ExperimentalUuidApi::class)
            justAfter(uuid.toKotlinUuid())
        }
        @OptIn(ExperimentalUuidApi::class)
        fun justAfter(uuid: Uuid)

        fun doLast()
    }
}