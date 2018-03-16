using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdaTools;

namespace Cmdline {
	internal static class Files {

		internal static void Parse(Stack<String> Arguments) {
			if (Arguments.TryPop(out String Modifier)) {
				switch (Modifier.ToLower()) {
					case "help":
						Files.FullHelp();
						break;
					default:
						// The argument wasn't a modifier, but there are arguments
						// Place the argument back onto the stack
						Arguments.Push(Modifier);
						// Then each remaining argument is interpreted as a package name
						foreach (String Name in Arguments) {
							PackageUnit Package = new PackageUnit(Name);
							Console.Write(Package.Name + ": ");
							foreach (String FileName in Package.GetFiles()) {
								Console.Write(FileName + "  ");
							}
							Console.WriteLine();
						}
						break;
				}
			} else {
				// There are no arguments, so list the files for each package within the project
				List<String> Files = new List<String>();
				foreach (Unit U in new Project().Units) {
					Files.AddRange(U.GetFiles());
				}
				foreach (String FileName in Files.Distinct()) {
					Console.Write(FileName + "  ");
				}
				Console.WriteLine();
			}
		}

		internal static void Help() {
			Console.WriteLine("\t" + "files [«Packages»+] — Lists the associated files");
		}

		internal static void FullHelp() {
			Console.WriteLine("\t" + "files");
			Console.WriteLine("\t\t" + "— Lists the associated files for the specified package");
			Console.WriteLine("\t\t" + "«Packages»+ — Lists the associated files for the specified package");
		}

	}
}
