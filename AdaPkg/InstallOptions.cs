using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;
using CommandLine.Text;

namespace AdaPkg {
	[Verb("install", HelpText = "Install the specified packages.")]
	internal class InstallOptions : Options {

		[Option("list", HelpText = "List all installed packages.")]
		internal Boolean List { get; set; }

	}
}
