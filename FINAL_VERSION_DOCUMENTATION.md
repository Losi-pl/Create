# Create Project - Final Version Documentation

**Last Commit:** `a4d4608cf851885bf1adcfad83ddeaea92d6cdaa` - "When doing then for everyone"  
**Date:** June 22, 2026  
**Branch:** `dev/cs-old/clear-bobwebs`

---

## Project Overview

**Create** is a C# game engine/framework built with OpenGL for Windows (x64 architecture). The project implements a modular architecture for game development with support for custom resources, shaders, and a mod system.

### Key Technologies
- **Language:** C# (C# 14 language features)
- **.NET Version:** net10.0 (upgraded from net8.0 in the final version)
- **Graphics API:** OpenGL
- **Platform:** Windows x64 (self-contained executable)
- **License:** MIT (changed from GPL v3)

---

## Architecture Overview

### Core Components

#### 1. **Assets System** (`Assets.*.cs`)
Manages game resources including fonts, interfaces, models, shaders, and textures.

**Key Classes:**
- `Assets.Font.cs` - Font asset loading and caching
- `Assets.Interface.cs` - XML-based UI interface loading
- `Assets.Models.cs` - Block model loading from XML resources
- `Assets.Shaders.cs` - Shader compilation and caching
- `Assets.Textures.cs` - 2D texture loading via SixLabors.ImageSharp
- `Assets.cs` - Central resource management and aggregation

**Resource Loading Flow:**
1. Resources are merged from multiple sources (mods and external)
2. Stored in a global `MergedResources` instance
3. Individual asset loaders fetch from the global resource package
4. Assets are cached to prevent redundant loading

**Note:** The final version includes a TODO comment to separate resources back out rather than merging them into a single source.

#### 2. **Registry System** (`Register.cs` + `Create/Registry/ElementRegister.cs`)
Handles registration and management of game elements through a console-based pattern.

**Registered Element Types:**
- **Blocks** - Basic building blocks in the game world
- **Dimensions** - World/dimension types (previously named "Dimentions")
- **Entities** - Game entities/actors
- **Items** - Inventory items
- **Creative Tabs** - UI organization tabs for creative mode

**Registry Architecture:**
- `ElementRegister<T>.Console` - Editable interface for adding elements
- `ElementRegister<T>` - Read-only public registry with caching
- Virtual dictionaries for fast lookup by name and ID
- Thread-safe registration with locks

**Key Methods:**
- `GetPrimaryResources()` - Loads core game resources
- `LoadIcon()` - Sets window icon
- `LoadMainShaders()` - Initializes renderer and GUI shaders
- `create_resource()` renamed to `GetPrimaryResources()` for clarity

#### 3. **Mod System** (`Mod.cs`)
Plugin architecture allowing mods to register game elements.

**Mod Features:**
- Named registration of elements with namespaced IDs (`modname:elementname`)
- Recipe system for crafting
- Custom user interface interpreters
- Version tracking

**API Changes:**
- `ArgumentNullException` calls updated to use new 4-parameter signature
- Register console references updated to PascalCase (e.g., `Register.BlocksConsole`)

#### 4. **Shader System** (`Create.OpenGL/Shader.Stream.cs`)
Custom shader format with XML configuration.

**Key Changes in Final Version:**
- Fixed XDocument parsing issue with resource files
- Changed from `XDocument.Load(stream)` to `XDocument.Parse(content)` 
- Reads stream content into string via `StreamReader` first
- Shader files support version and core profile specifications

**Shader Naming:**
- Changed from "bazic" to "basic" spelling
- Updated paths: `create:basic/item`, `create:basic/render-layer`

#### 5. **Resource Compilation** (`Create.Resource.Compiler/`)
Console application that packages game resources into a compressed format.

**Build Process:**
- Scans resource directories
- Compiles resources into a single compressed file (`create.resources`)
- In Debug mode: resources loaded from disk via folder scanning
- In Release mode: resources loaded from precompiled `create.resources` file

**Compiler Changes in Final Version:**
- Refactored argument parsing system (now semicolon-delimited)
- Removed hardcoded Release configuration check
- Simplified path resolution
- Updated to .net10.0

#### 6. **Initialization & Loading** (`Sceans/Loading.cs`)
Scene that handles startup resource loading and initialization.

**Loading Sequence:**
1. Loads primary resources from `Register.GetPrimaryResources()`
2. Loads all mod resources
3. Calls element initialization methods via ModIniters
4. Renders loading screen while async loading

**Rendering:**
- Red background color (RGB: 239, 39, 39)
- Default resolution: 1443 x 866 pixels

---

## Code Quality Improvements (Final Version)

### Naming Conventions
Updated to follow C# standards:
- Snake_case private fields → PascalCase with underscore prefix (e.g., `render_layer` → `_renderLayer`)
- Misspellings corrected:
  - "Dimentions" → "Dimensions"
  - "bazic" → "basic"
  - "Separating" → "Separating" (already fixed)
- Class names: `ElementRegister` renamed register consoles to PascalCase
  - `blocks_console` → `BlocksConsole`
  - `dimentions_console` → `DimensionsConsole`
  - `entitys_console` → `EntitiesConsole`
  - `items_console` → `ItemsConsole`
  - `creativetab_console` → `CreativeTabsConsole`

### Code Refactoring
- Extracted `ElementRegister<T>` class to separate file: `Create/Registry/ElementRegister.cs`
- Restructured resource compiler argument parsing for better maintainability
- Improved string manipulation using new helper: `SubstringBeforeLast(char)`
- Removed debug-specific warning levels from project files
- Added `PackageLicenseUrl` to project metadata

### Documentation Improvements
- Updated XML comments from Polish to English in Registry class
- Added TODO for resource separation architecture
- Clarified method purposes with better English descriptions

---

## Resource Structure

### Directory Layout (in Resources/)
```
Resources/
├── Shaders/
│   ├── Basic/          (renamed from Bazic)
│   │   ├── Item.xml
│   │   └── Render-Layer.xml
│   └── Interface/
├── Textures/
├── Interfaces/
├── Models/
│   └── Blocks/
└── Fonts/
```

### Build Artifacts

**Debug Mode:**
- Resources loaded dynamically from disk
- Enables hot-reloading during development

**Release Mode:**
- `create.resources` - Compressed single-file package containing all assets
- Located in output directory alongside executable

---

## Project Configuration

### Target Frameworks
- Primary: `.net10.0`
- Output Type: Console Application
- Runtime Identifier: `win-x64`
- Self-contained: `true`
- Publish Ready-to-Run: `true`

### C# Language Features
- Language Version: `14` (updated from 13)
- Nullable Reference Types: `enabled`
- Implicit Usings: `enabled`
- Unsafe Code: `enabled`

### Related Projects
- `Create.OpenGL` - OpenGL graphics abstraction
- `Create.Resource` - Resource management and compression
- `Create.Resource.Compiler` - Compiler for resource packaging

---

## External Dependencies

### NuGet Packages
- `SixLabors.ImageSharp` - Image loading and processing
- `System.Private.Uri` - URI parsing utilities
- `OpenGL` libraries (via OpenGL abstraction layer)

---

## Known Issues & TODOs

### Open Tasks
1. **Resource Architecture:** Need to separate merged resources back into individual sources (noted in `Assets.cs`)
2. **XDocument Parser:** Workaround in place for resource file compatibility issues (see `Shader.Stream.cs`)

### Design Decisions
- Uses a global merged resource package for simplicity (currently in use, but flagged for refactoring)
- XML-based configuration for shaders, models, and interfaces
- Thread-safe element registration with lock-based synchronization

---

## License

Changed from GNU General Public License v3 to **MIT License** in final version.

MIT License - Copyright (c) 2026 Losi-pl

---

## Commit History Summary (Final 6 Commits)

1. **8bf6478** - "Make stronger project lock" (June 20, 16:06 UTC)
2. **584309e** - "For some reason XDocument parser does not mesh with my resource file" (June 20, 16:18 UTC)
3. **188bc46** - "Move the class out & tidy those names" (June 20, 16:54 UTC)
   - Extracted `ElementRegister<T>` to new file
4. **3e104c4** - "Some more renaming" (June 20, 17:10 UTC)
5. **7ec3ce35** - "Dotnet update" (June 20, 20:30 UTC)
   - Upgraded to .net10.0
6. **a4d4608** - "When doing then for everyone" (June 22, 23:59 UTC)
   - Final commit with last-minute cleanup

---

## File Changes Summary

**24 files modified**
- **Additions:** 322 lines
- **Deletions:** 975 lines
- **Net Change:** -653 lines (cleanup and consolidation)

### Major File Changes
- `Create.csproj` - Upgraded framework, removed debug warnings
- `Register.cs` - Major refactoring (193 deletions, 43 additions) - moved `ElementRegister<T>` to separate file
- `Create.App.Windows.csproj` - Formatting and build script updates
- `LICENSE` - GPL v3 → MIT
- `.csproj` files across projects - Framework updates

---

## How to Build & Run

### Debug Build
```bash
dotnet build -c Debug
dotnet run --project Create.App.Windows -c Debug
```
Resources will be loaded from disk in real-time.

### Release Build
```bash
dotnet build -c Release
dotnet publish -c Release --self-contained
```
- Triggers resource compilation via `Create.Resource.Compiler`
- Creates `create.resources` package
- Produces standalone Windows x64 executable

---

## Notes for Next Version

This documentation captures the state of the Create project before starting fresh. Key learnings:

1. **Architecture was sound** - The mod and registry systems are well-designed
2. **Code cleanliness matters** - Major refactor was needed for naming conventions
3. **Resource system needs rethinking** - Consider separating merged resources before scaling further
4. **Testing coverage unclear** - No test files found; consider TDD for new version
5. **Documentation was minimal** - This project would have benefited from inline docs earlier

The project shows good progression in cleaning up technical debt and improving code quality in the final months. Consider applying these lessons to the new version from the start.

---

*Documentation compiled from PR #34: "I think its time to start over"*  
*This represents the final production-ready state before the project restart.*
