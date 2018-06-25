using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AdaPrj {
	[Verb("units", HelpText = "Get the units of this project")]
	internal class UnitsOptions {

		[Option("table", HelpText = "Present the units as a detailed table")]
		internal Boolean Table { get; set; }

	}
}
