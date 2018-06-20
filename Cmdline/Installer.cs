using System;
using System.IO;
using System.IO.Compression;

using AdaTools;

namespace Cmdline {
	static class Installer {

		public static void FullHelp() {
			Console.WriteLine("\t" + "install <package>+ — Install the specified packages globally");
		}

		public static void Help() {
			Console.WriteLine("\t" + "install <package>+ — Install the specified packages");
		}

		/// <summary>
		/// Install the specified package
		/// </summary>
		/// <param name="Package">Package to install</param>
		public static void Global(Package Package) {
			if (!Directory.Exists(Settings.SourceSearchPath[1])) Directory.CreateDirectory(Settings.SourceSearchPath[1]);
			Package.Archive.GetEntry(Package.Info.Name + ".ads").ExtractToFile(Settings.SourceSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".ads", true);
			File.SetAttributes(Settings.SourceSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".ads", FileAttributes.ReadOnly);
			if (!(Package.Archive.GetEntry(Package.Info.Name + ".adb") is null)) {
				Package.Archive.GetEntry(Package.Info.Name + ".adb").ExtractToFile(Settings.SourceSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".adb", true);
				File.SetAttributes(Settings.SourceSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".adb", FileAttributes.ReadOnly);
			}
			if (!Directory.Exists(Settings.ObjectSearchPath[1])) Directory.CreateDirectory(Settings.ObjectSearchPath[1]);
			switch (Environment.OSVersion.Platform) {
			case (PlatformID)1:
			case (PlatformID)2:
			case (PlatformID)3:
				Package.Archive.GetEntry(Package.Info.Name + ".dll").ExtractToFile(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".dll", true);
				File.SetAttributes(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".dll", FileAttributes.ReadOnly);
				break;
			case PlatformID.Unix:
			default:
				Package.Archive.GetEntry(Package.Info.Name + ".so").ExtractToFile(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".so", true);
				File.SetAttributes(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".so", FileAttributes.ReadOnly);
				break;
			}
			Package.Archive.GetEntry(Package.Info.Name + ".ali").ExtractToFile(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".ali", true);
			File.SetAttributes(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Info.Name + ".ali", FileAttributes.ReadOnly);
			if (!Directory.Exists(Settings.PackageDatabasePath)) Directory.CreateDirectory(Settings.PackageDatabasePath);
			Package.WriteInfo(new FileStream(Settings.PackageDatabasePath + Path.DirectorySeparatorChar + Package.Info.Name, FileMode.Create));
			File.SetAttributes(Settings.PackageDatabasePath + Path.DirectorySeparatorChar + Package.Info.Name, FileAttributes.Normal);
		}

		/// <summary>
		/// List all installed packages
		/// </summary>
		public static void ListInstalled() {
			if (!Directory.Exists(Settings.PackageDatabasePath)) return;
			foreach (String FileName in Directory.GetFiles(Settings.PackageDatabasePath)) {
				PackageInfo Package = new PackageInfo(FileName);
				if (String.IsNullOrEmpty(Package.Variant)) {
					Console.Write(Package.Name);
				} else {
					Console.Write(Package.Name + ":" + Package.Variant);
				}
				Console.WriteLine(" [" + Package.Version + "]");
			}
		}

		public static void Uninstall(String Name) {
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

	}
}