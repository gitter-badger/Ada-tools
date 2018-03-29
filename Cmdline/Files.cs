using System;
using System.Collections.Generic;
using System.Linq;
using AdaTools;

namespace Cmdline {
	internal static class Files {

		internal static void Each() {
			List<String> Files = new List<String>();
			foreach (Unit U in new Project().Units) {
				Files.AddRange(U.GetFiles());
			}
			foreach (String FileName in Files.Distinct()) {
				Console.Write(FileName + "  ");
			}
			Console.WriteLine();
		}

		internal static void Each(Stack<String> Arguments) {
			foreach (String Name in Arguments) {
				PackageUnit Package = new PackageUnit(Name);
				Console.Write(Package.Name + ": ");
				foreach (String FileName in Package.GetFiles()) {
					Console.Write(FileName + "  ");
				}
				Console.WriteLine();
			}
		}

		internal static void FullHelp() {
			Console.WriteLine("\t" + "files");
			Console.WriteLine("\t\t" + "— Lists the associated files for the specified package");
			Console.WriteLine("\t\t" + "«Packages»+ — Lists the associated files for the specified package");
		}

		internal static void Help() {
			Console.WriteLine("\t" + "files [«Packages»+] — Lists the associated files");
		}

	}
}
