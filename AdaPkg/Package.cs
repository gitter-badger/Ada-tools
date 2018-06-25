using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace AdaPkg {
	internal static class Package {

		private static void Each(Boolean IncludeBody = true) {
			foreach (PackageUnit PackageUnit in new Project().Packages) {
				try {
					new AdaTools.Package(PackageUnit).Create(IncludeBody);
				} catch (PackageNotBuildException) {
					Console.WriteLine(PackageUnit.Name + ": Has not been built, so it cannot be packaged");
				}
			}
		}

		private static void Info(Span<String> Arguments) {
			Console.WriteLine("Package.Info()");
			foreach (String Name in Arguments) {
				try {
					new AdaTools.Package(Name).WriteInfo();
				} catch (NotInstallPackageException) {
					Console.WriteLine("\"" + Name + "\" doesn't appear to be an install package");
				}
			}
		}

		private static void Validate(Span<String> Arguments) {
			AdaTools.Package Package;
			foreach (String Name in Arguments) {
				Package = new AdaTools.Package(Name);
				Console.WriteLine(Package.Info.Name + ": ");
				Package.WriteValidation();
			}
		}

		internal static void Run(PackageOptions opts, String[] args) {
			if (opts.Info) {
				Package.Info(args);
			} else if (opts.Validate) {
				Package.Validate(args);
			}
		}

	}
}
