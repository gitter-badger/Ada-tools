using AdaTools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdaPkg {
	public static class Program {
		static void Main(String[] args) {
			try {
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
					case InstallFlags.Global:
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
			} catch (MissingGNATProgramException Exception) {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Missing necessary program: ");
				Console.ResetColor();
				Console.WriteLine(Exception.Message);
				Console.Write("See: ");
				Console.WriteLine("https://en.wikibooks.org/wiki/Ada_Programming/Installing");
			}
		}

		static void Help() {
			Console.WriteLine("AdaPkg");
			Console.WriteLine("Copyright 2018 - Entomy");
			Console.WriteLine();
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
			Install.Help();
			Package.Help();
			Uninstall.Help();
		}
	}
}
