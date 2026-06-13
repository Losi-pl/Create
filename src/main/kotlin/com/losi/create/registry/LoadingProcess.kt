package com.losi.create.registry

import java.util.UUID
import kotlin.uuid.ExperimentalUuidApi
import kotlin.uuid.Uuid
import kotlin.uuid.toJavaUuid
import kotlin.uuid.toKotlinUuid

class LoadingProcess {

    @OptIn(ExperimentalUuidApi::class)
    val uuid: Uuid
    val name: String
    internal val action: Runnable

    @Suppress("unused", "PropertyName")@get:JvmName("getUUID")
    val UUID: UUID get()
    {
        @OptIn(ExperimentalUuidApi::class)
        return uuid.toJavaUuid()
    }

    companion object {
        @OptIn(ExperimentalUuidApi::class)
        private fun UUID.toUuid() = this.toKotlinUuid()
    }

    @OptIn(ExperimentalUuidApi::class)@Suppress("unused")
    constructor(uuid: Uuid, name: String = uuid.toString(), action: Runnable)
    {
        this.uuid = uuid
        this.name = name
        this.action = action
    }

    @Suppress("unused")
    constructor(uuid: UUID, name: String = uuid.toString(), action: Runnable)
    {
        @OptIn(ExperimentalUuidApi::class)
        this.uuid = uuid.toUuid()
        this.name = name
        this.action = action
    }

    constructor(name: String, action: Runnable)
    {
        @OptIn(ExperimentalUuidApi::class)
        this.uuid = Uuid.generateV7()
        this.name = name
        this.action = action
    }

    override infix operator fun equals(other: Any?): Boolean {
        if(other !is LoadingProcess)
            return false

        @OptIn(ExperimentalUuidApi::class)
        return this === other
    }

    override fun hashCode(): Int {
        @OptIn(ExperimentalUuidApi::class)
        var result = uuid.hashCode()
        result = 31 * result + name.hashCode()
        result = 31 * result + action.hashCode()
        return result
    }
}