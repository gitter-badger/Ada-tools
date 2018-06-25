using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AdaPrj {
	[Verb("build", HelpText = "Build the project")]
	internal class BuildOptions : Options {

		[Option("flags", HelpText = "Get the build flags for each unit instead")]
		public Boolean Flags { get; set; }

		[Option("plan", HelpText = "Get the build plan instead")]
		public Boolean Plan { get; set; }

	}
}
