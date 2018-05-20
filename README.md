# Ada-tools
Collection of tooling for Ada projects

This tooling was developed on top of [.NET Core](https://dotnet.github.io/) for ease of development. It may be ported to Ada later, but this is not a priority.

# Goals
The majority of software projects are very straightforward and shouldn't require extensive configuration. In fact, I would argue they shouldn't require _any_ configuration. Because Ada follows very consistent practices, it's possible to, for many project scenarios, operate without any configuration at all.

We tend to structure projects predictable ways. Tooling should recognize that.

# Features
* Runtime representation of packages, to enable things like
	* Determining dependencies of a package, even if the body and spec have different source level dependencies.
	* Understanding whether a package is implemented with only a spec, both a spec and body, or has a body but is missing a spec.
* Runtime representation of programs, to enable things like
	* Knowing what needs to be built without explicitly telling the project manager
* Automatic configuration of projects
	* The project is the directory, a convention shared with modern editors.
	* Upon initialization, the directory is scanned for Ada files, and determines which are packages, initializing the package representations, and so on.
	* Total project dependencies are understood, but still tracked at the unit level, which allows finer grained understanding.
* Interactive editor for GNAT configuration pragma files. This isn't project configuration, but rather a list of configuration pragmas that should be applied to every file instead of writing them to each and every file. GNAT made this; I just made an interactive editor.

While much of this might seem complicated or complex, it's actually just saying "Hey, I can recognize most cases of Ada projects without needing you to tell me what things are".