using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

using AdaTools;

namespace AdaPkg {
	internal static class Uninstall {

		private static void GlobalUninstall(Span<String> Names) {
			foreach (String Name in Names) {
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
					Console.WriteLine(Name + " not installed");
				}
			}
		}

		internal static void Run(UninstallOptions opts, String[] args) {
			Uninstall.GlobalUninstall(args);
		}

	}
}
