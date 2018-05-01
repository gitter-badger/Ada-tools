using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace Cmdline {
	internal static class Dependencies {

		internal static void Each() {
			foreach (Unit Unit in new Project().Units) {
				Console.Write(Unit.Name + ": ");
				foreach (String Dep in Unit.Dependencies) {
					Console.Write(Dep + "  ");
				}
				Console.WriteLine();
			}
		}

		internal static void FullHelp() {
			Console.WriteLine("\t" + "(deps|depends|dependencies)");
			Console.WriteLine("\t\t" + "— Lists the dependent packages for each package in this project");
			Console.WriteLine("\t\t" + "(all|project) — Lists the dependent packages for the project overall");
			Console.WriteLine("\t\t" + "«Packages»+ — Lists the dependent packages for all specified packages");
		}

		internal static void Help() {
			Console.WriteLine("\t" + "(deps|depends|dependencies) [(all|project|«Packages»+)] — Lists the dependent packages");
		}
		
		internal static void Project() {
			foreach (String Dep in new Project().Dependencies) {
				Console.Write(Dep + "  ");
				Console.WriteLine();
				break;
			}
		}

	}
}
