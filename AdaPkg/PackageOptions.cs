using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace AdaPkg {
	[Verb("package", HelpText = "Package all found units.")]
	internal class PackageOptions : Options {

		[Option("info", HelpText = "List the package info for the specified archives.")]
		internal Boolean Info { get; set; }

		[Option("validate", HelpText = "Validate the packages.")]
		internal Boolean Validate { get; set; }

	}
}
