using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;

namespace AdaPkg {
	public static class Program {
		static void Main(String[] args) {
			String Verb = args[0].ToUpper();
			List<String> Args = new List<String>(args.Skip(1).Take(args.Length - 1));

			switch (Verb) {
			case "INSTALL":
				switch (Install.ParseInstallFlags(Args)) {
				case InstallFlags.Help:
					Install.FullHelp();
					break;
				case InstallFlags.List:
					Install.ListInstalled();
					break;
				case InstallFlags.Global | InstallFlags.Install:
				default:
					Install.GlobalInstall(Args);
					break;
				}
				break;
			case "PACKAGE":
				switch (Package.ParsePackageFlags(Args)) {
				case PackageFlags.Help:
					Package.FullHelp();
					break;
				case PackageFlags.Info:
					Package.Info(Args);
					break;
				case PackageFlags.Validate:
					Package.Validate(Args);
					break;
				case PackageFlags.Package:
				default:
					Package.Each();
					break;
				}
				break;
			case "UNINSTALL":
				switch (Uninstall.ParseUninstallFlags(Args)) {
				case UninstallFlags.Help:
					break;
				case UninstallFlags.Global | UninstallFlags.Uninstall:
				default:
					Install.GlobalInstall(Args);
					break;
				}
				break;
			case "HELP":
			case "--HELP":
			default:
				// Print the help menu so people know what they are doing
				Program.Help();
				break;
			}
		}

		static void Help() {
			Console.WriteAscii("AdaPkg", Color.Green);
			Console.WriteLine("\tCopyright 2018 - Entomy");
			Console.WriteLine();
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
			Install.Help();
			Package.Help();
			Uninstall.Help();
		}
	}
}
