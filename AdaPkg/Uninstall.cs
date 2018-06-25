using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using AdaTools;

namespace AdaPkg {
	internal static class Uninstall {

		internal static void FullHelp() {
			Console.WriteLine("uninstall [--global] <package>+ — Uninstall the specified packages globally");
		}

		internal static void Help() {
			Console.WriteLine("  uninstall <package>+ — Uninstall the specified packages");
		}

		public static void GlobalUninstall(String Name) {
			if (!Directory.Exists(Settings.PackageDatabasePath)) return;
			Boolean IsInstalled = false;
			foreach (String FileName in Directory.GetFiles(Settings.PackageDatabasePath)) {
				if (Path.GetFileName(FileName) == Name) {
					IsInstalled = true;
					File.SetAttributes(FileName, FileAttributes.Normal);
					File.Delete(FileName);
				}
			}
			if (IsInstalled) {
				foreach (String FileName in Directory.GetFiles(Settings.SourceSearchPath[1])) {
					if (Path.GetFileName(FileName) == Name + ".ads") {
						File.SetAttributes(FileName, FileAttributes.Normal);
						File.Delete(FileName);
					} else if (Path.GetFileName(FileName) == Name + ".adb") {
						File.SetAttributes(FileName, FileAttributes.Normal);
						File.Delete(FileName);
					}
				}
				foreach (String FileName in Directory.GetFiles(Settings.ObjectSearchPath[1])) {
					if (Path.GetFileName(FileName) == Name + ".ali") {
						File.SetAttributes(FileName, FileAttributes.Normal);
						File.Delete(FileName);
					} else if (Path.GetFileName(FileName) == Name + ".dll") {
						File.SetAttributes(FileName, FileAttributes.Normal);
						File.Delete(FileName);
					} else if (Path.GetFileName(FileName) == Name + ".so") {
						File.SetAttributes(FileName, FileAttributes.Normal);
						File.Delete(FileName);
					}
				}
			} else {
				Console.WriteLine("Not Installed");
			}
		}

		public static void GlobalUninstall(List<String> Names) {
			foreach (String Name in Names) {
				GlobalUninstall(Name);
			}
		}

		internal static UninstallFlags ParseUninstallFlags(List<String> Args) {
			UninstallFlags Result = UninstallFlags.Uninstall;
			foreach (String Arg in Args) {
				switch (Arg.ToUpper()) {
				case "--HELP":
					return UninstallFlags.Help;
				case "--GLOBAL":
				default:
					Result |= UninstallFlags.Global;
					break;
				}
			}
			return Result;
		}

	}
}
