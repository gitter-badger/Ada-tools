using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace AdaPrj {
	internal static class Clean {
		internal static void All() {
			Compiler.Clean();
		}

		internal static void FullHelp() {
			Console.WriteLine("clean — Clean up this project");
			Console.WriteLine();
		}

		internal static void Help() {
			Console.WriteLine("  clean — Clean up this project");
		}

		internal static CleanFlags ParseCleanFlags(List<String> Args) {
			CleanFlags Result = CleanFlags.Clean;
			foreach (String Arg in Args) {
				switch (Arg.ToUpper()) {
				case "--HELP":
					return CleanFlags.Help;
				default:
					break;
				}
			}
			return Result;
		}

	}
}
