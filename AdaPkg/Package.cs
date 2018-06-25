using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace AdaPkg {
	public static class Package {

		internal static void FullHelp() {
			Console.WriteLine("package — Package all found units");
			Console.WriteLine("  --info <Archive>+ — List the package info for the specified archives");
			Console.WriteLine("  --validate <Archive>+ — Validate the specified package archives");
		}

		internal static void Help() {
			Console.WriteLine("  package — Package all found units");
		}

		internal static void Each(Boolean IncludeBody = true) {
			foreach (PackageUnit PackageUnit in new Project().Packages) {
				try {
					new AdaTools.Package(PackageUnit).Create(IncludeBody);
				} catch (PackageNotBuildException) {
					Console.WriteLine(PackageUnit.Name + ": Has not been built, so it cannot be packaged");
				}
			}
		}

		internal static void Info(List<String> Arguments) {
			foreach (String Name in Arguments) {
				try {
					new AdaTools.Package(Name).WriteInfo();
				} catch (NotInstallPackageException) {
					Console.WriteLine("\"" + Name + "\" doesn't appear to be an install package");
				}
			}
		}

		internal static void Validate(List<String> Arguments) {
			AdaTools.Package Package;
			foreach (String Name in Arguments) {
				Package = new AdaTools.Package(Name);
				Console.WriteLine(Package.Info.Name + ": ");
				Package.WriteValidation();
			}
		}

		internal static PackageFlags ParsePackageFlags(List<String> Args) {
			PackageFlags Result = PackageFlags.Package;
			List<String> ToBeRemoved = new List<String>();
			foreach (String Arg in Args) {
				switch (Arg.ToUpper()) {
				case "--HELP":
					Result |= PackageFlags.Help;
					break;
				case "--INFO":
					Result |= PackageFlags.Info;
					break;
				case "--VALIDATE":
					Result |= PackageFlags.Validate;
					break;
				default:
					break;
				}
				if (Arg.StartsWith("--")) {
					ToBeRemoved.Add(Arg);
				}
			}
			foreach (String Arg in ToBeRemoved) {
				Args.Remove(Arg);
			}
			return Result;
		}

	}
}
