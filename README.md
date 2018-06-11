# Ada-tools
Collection of tooling for Ada projects

This tooling was developed on top of [.NET Core](https://dotnet.github.io/) for ease of development. It may be ported to Ada later, but this is not a priority.

# Goals
The majority of software projects are very straightforward and shouldn't require extensive configuration. In fact, I would argue they shouldn't require _any_ configuration. Because Ada follows very consistent practices, it's possible to, for many project scenarios, operate without any configuration at all.

We tend to structure projects predictable ways. Tooling should recognize that.

Act as a package/dependency manager, a much needed and otherwise non-existant too for Ada.

# Features
## Types
Runtime representation of types, to enable:
 * Listing all type definitions within a project or unit
 * Finding a type, including where it is located
## Units
Runtime representation of all units, to enable:
### Packages & Reusable Subroutines
 * Determining dependencies of a package, even if the body and spec have different source level dependencies.
 * Understanding whether a package is implemented with only a spec, both a spec and body, or has a body but is missing a spec.
### Programs
 * Knowing what needs to be built without explicitly telling the project manager
 * Knowing the difference between a program to be built into an executable and a reusable subroutine to the built into a library
## Projects
Automatic configuration of projects
   * The project is the directory, a convention shared with modern editors.
   * Upon initialization, the directory is scanned for Ada files, and determines what is what, initializing the representations, and so on.
   * Total project dependencies are understood, but still tracked at the unit level, which allows finer grained understanding, which allows finer grained linking. Shared objects are good, and we should be taking full advantage of them
## Settings
 * Library level settings are accessable through the cmdline interface or integratable with other tools. As much as possible consistancy is maintained with gnat tooling, so setting a path for gnat will also set it for this.
## gnat.adc
 * Interactive editor for GNAT configuration pragma files. This isn't project configuration, but rather a list of configuration pragmas that should be applied to every file instead of writing them to each and every file. GNAT made this; I just made an interactive editor.

While much of this might seem complicated or complex, it's actually just saying "Hey, I can recognize most cases of Ada projects without needing you to tell me what things are".