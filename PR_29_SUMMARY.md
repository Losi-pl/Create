# Textures & OpenGL Wrapper

## Modifications
- Converted main application entry point from Java (`Start.java`) to Kotlin (`App.kt`) for better consistency and modern language features.
- Moved application initialization logic into `App` object with cleaner Kotlin idioms.
- Updated `Version.kt` to simplify version management by removing the properties file dependency and using lazy evaluation for library versions.
- Modified `build.gradle.kts`:
  - Updated JOML from `1.10.8` to `1.10.9`
  - Removed `org.gradlex.extra-java-module-info` plugin
  - Added Apache Batik transcoder dependency (`1.19`) for SVG texture support
  - Removed complex module info configuration
  - Simplified JVM arguments and main class configuration
  - Added JAR manifest configuration

## Fixed
- Removed obsolete Java-based GL wrapper classes (`GL20C.java`, `GL21C.java`, `GL40C.java`) in favor of Kotlin wrappers.
- Removed `GLErrorHandler.java` static implementation in favor of functional approach in `ErrorHandler.kt`.

## Additions

### OpenGL Type System & Wrappers
- Created comprehensive OpenGL type system with Kotlin enums for type safety:
  - `GLSLVar` - Unified GLSL variable types (primitives, vectors, matrices, samplers, images)
  - `InternalFormat` - Texture internal format specifications (500+ variants)
  - `ShaderType`, `ShaderPart`, `ShaderProgram` - Shader compilation and linking
  - `TextureType`, `TextureObject` - Texture target and handle management
  - `PixelFormat`, `InternalFormat` - Format specifications
  - `TextureWrappingMode`, `WrappingDirection` - Texture wrapping behavior
  - `MinFilterMode`, `MagFilterMode` - Texture filtering modes
  - Filter, comparison, and debug message enums for complete OpenGL control

### Shader & Uniform Management
- Created `Shader` wrapper improvements:
  - New uniform and attribute location types (`UniformLocation`, `AttributeLocation`)
  - Enhanced uniform/attribute information retrieval
  - Support for all GLSL types (primitives, vectors, matrices, samplers, images)
  - Texture object binding system for samplers
  - `assignObjects()` method for automatic texture unit assignment
  - Complete error handling with shader compilation diagnostics

### Texture System
- `Texture` interface - Base for all GPU texture resources
- `Texture2D` class - 2D texture with:
  - Support for all `BufferedImage` types with automatic format detection
  - Wrapping mode configuration
  - Filtering parameter control
  - Proper resource cleanup via Cleaner mechanism
  - SVG support via Batik transcoder
  
- `Texture2DAtlas` class - Texture atlas for efficient sprite management:
  - 2D array texture storage (`GL_TEXTURE_2D_ARRAY`)
  - Builder pattern for configuration
  - Automatic mipmap and format handling
  
- `BlockTexture` - Specialized texture type for block textures:
  - `BlockAtlasProcessor` for automatic atlas generation
  - 16x16 texture size specification
  - Nearest-neighbor filtering for pixel-perfect appearance
  - NOT_FOUND fallback texture handling

### Mesh & Rendering
- Extended `Mesh` class with comprehensive attribute support:
  - Support for all primitive types (byte, short, int, long, float, double)
  - Vector types (2D, 3D, 4D for int, long, float, double)
  - Matrix types (2x2 to 4x4 for float and double)
  - Automatic type validation and size calculation
  - Shader object assignment during draw calls

### GL Function Wrappers
Created modular Kotlin GL wrapper functions organized by GL version:
- `GL11.kt` - Texture creation, deletion, binding, parameters, image upload
- `GL13.kt` - Active texture unit management
- `GL20.kt` - Shader programs, uniforms, attributes, shader creation/compilation (258 lines)
- `GL21.kt` - Additional matrix uniform functions
- `GL30.kt` - Fragment data location, extended unsigned uniforms
- `GL43.kt` - Debug message callbacks, depth/stencil texture modes
- `GL46.kt` - SPIR-V detection
- `GLDouble.kt` - Double precision uniform functions (75 lines)
- `GLLong.kt` - 64-bit integer uniform functions
- `GLTexture.kt` - Texture storage and sub-image update functions

### Utility Enhancements
- `MutablePair` - Mutable pair data structure for shader object tracking
- `Quad` - 4-tuple data structure
- Enhanced `CGeneral.kt`:
  - `mustRun()` - Safe non-null lambda execution
  - `splitCamelCase()` - String formatting for debug output
  - `require()` - Inline requirement checking
  - Sequence to MutableMap conversion
  - Enumeration forEach extension
- Extended `COverloads.kt` with ByteBuffer vector put functions (Vector2L, Vector3L, Vector4L)
- Enhanced `OnMainThread`:
  - Same-thread optimization for `query()`
  - Timeout protection (5 seconds)
  - Exception propagation from main thread

### Asset System Integration
- `BlockTexture.BlockAtlasProcessor` - Automatic block texture atlas creation during asset loading
- Integration with `AssetManager` for texture resource loading
- Support for texture atlas population from asset directories

### Graphics Wrapper
- `GLBound` interface moved to `graphics.gl` package for better organization
- Error handler refactored from static class to functional approach with `bindErrorCather()`
- Window integration with texture rendering and atlas assignment

### Shader Configuration
- Updated shader compilation with new type system
- Improved error reporting with `ShaderCompilationError` using `ShaderType` enums
- Fragment shader updated for texture sampling (image and atlas support)
- Vertex shader updated for UV coordinate generation

### Resources
- Added debug texture assets:
  - `debug3.svg` - 16x16 colored square texture with 4x4 grid pattern
  - `debug.bmp`, `debug2.bmp`, `dirt.png` - Additional test textures
- Updated shader sources with texture sampling logic

## Summary
This PR implements a complete OpenGL texture and type system with comprehensive Kotlin wrappers for all major OpenGL operations. The refactoring provides type-safe, modern API design while maintaining full compatibility with the existing rendering pipeline. The addition of texture atlas support enables efficient batch rendering of textures, and the comprehensive GL wrapper functions eliminate the need for raw OpenGL calls throughout the codebase.
