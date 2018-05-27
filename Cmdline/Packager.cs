using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace Cmdline {
	public static class Packager {

		internal static void FullHelp() {
			Console.WriteLine("\t" + "package — Package all found units");
			Console.WriteLine("\t" + "package info <Archive>+ — List the package info for the specified archives");
		}

		internal static void Help() {
			Console.WriteLine("\t" + "package — Package all found units");
		}

		internal static void Info(Stack<String> Arguments) {
			foreach (String Name in Arguments) {
				try {
					new Package(Name).Info();
				} catch (NotInstallPackageException) {
					Console.WriteLine("\"Name\" doesn't appear to be an install package");
				}
			}
		}

		internal static void Each() {
			foreach (PackageUnit PackageUnit in new Project().Packages) {
				try {
					new Package(PackageUnit).Create();
				} catch (PackageNotBuildException) {
					Console.WriteLine(PackageUnit.Name + ": Has not been built, so it cannot be packaged");
				}
			}
		}

	}
}
