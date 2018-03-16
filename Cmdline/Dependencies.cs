using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace Cmdline {
	internal static class Dependencies {

		internal static void Parse(Stack<String> Arguments) {
			if (Arguments.TryPop(out String Modifier)) {
				switch (Modifier.ToLower()) {
					case "help":
						Dependencies.FullHelp();
						break;
					case "all":
					case "project":
						foreach (String Dep in new Project().Dependencies) {
							Console.Write(Dep + "  ");
						}
						Console.WriteLine();
						break;
					default:
						// The argument wasn't a modifier, but there are arguments
						// Place the argument back onto the queue
						Arguments.Push(Modifier);
						// Then each remaining argument is interpreted as a package name
						foreach (String Name in Arguments) {
							PackageUnit Package = new PackageUnit(Name);
							Console.Write(Package.Name + ": ");
							foreach (String Dep in Package.Dependencies) {
								Console.Write(Dep + "  ");
							}
							Console.WriteLine();
						}
						break;
				}
			} else {
				// There are no arguments, so list the dependencies for each package within the project
				foreach (Unit U in new Project().Units) {
					Console.Write(U.Name + ": ");
					foreach (String Dep in U.Dependencies) {
						Console.Write(Dep + "  ");
					}
					Console.WriteLine();
				}
			}
		}

		internal static void Help() {
			Console.WriteLine("\t" + "(deps|depends|dependencies) [(all|project|«Packages»+)] — Lists the dependent packages");
		}

		internal static void FullHelp() {
			Console.WriteLine("\t" + "(deps|depends|dependencies)");
			Console.WriteLine("\t\t" + "— Lists the dependent packages for each package in this project");
			Console.WriteLine("\t\t" + "(all|project) — Lists the dependent packages for the project overall");
			Console.WriteLine("\t\t" + "«Packages»+ — Lists the dependent packages for all specified packages");
		}

	}
}
