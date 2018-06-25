using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace AdaPrj {
	[Verb("types", HelpText = "Get the types within this project")]
	internal class TypesOptions : Options {

		[Option("info", HelpText = "Get the info on the specified types", Separator = ' ')]
		public IEnumerable<String> Info { get; set; }

		[Option("table", HelpText = "Present the types as a detailed table")]
		public Boolean Table { get; set; }

	}
}
