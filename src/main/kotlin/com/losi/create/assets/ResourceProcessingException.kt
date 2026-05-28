package com.losi.create.assets

/**This exception is used when there is a problem with the data provided to process the resources*/
class ResourceProcessingException(message: String, cause: Throwable? = null) : RuntimeException(message, cause)