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
			Package.Archive.GetEntry(Package.Name + ".ads").ExtractToFile(Settings.SourceSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".ads", true);
			File.SetAttributes(Settings.SourceSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".ads", FileAttributes.ReadOnly);
			if (!(Package.Archive.GetEntry(Package.Name + ".adb") is null)) {
				Package.Archive.GetEntry(Package.Name + ".adb").ExtractToFile(Settings.SourceSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".adb", true);
				File.SetAttributes(Settings.SourceSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".adb", FileAttributes.ReadOnly);
			}
			if (!Directory.Exists(Settings.ObjectSearchPath[1])) Directory.CreateDirectory(Settings.ObjectSearchPath[1]);
			switch (Environment.OSVersion.Platform) {
			case (PlatformID)1:
			case (PlatformID)2:
			case (PlatformID)3:
				Package.Archive.GetEntry(Package.Name + ".dll").ExtractToFile(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".dll", true);
				File.SetAttributes(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".dll", FileAttributes.ReadOnly);
				break;
			case PlatformID.Unix:
			default:
				Package.Archive.GetEntry(Package.Name + ".so").ExtractToFile(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".so", true);
				File.SetAttributes(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".so", FileAttributes.ReadOnly);
				break;
			}
			Package.Archive.GetEntry(Package.Name + ".ali").ExtractToFile(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".ali", true);
			File.SetAttributes(Settings.ObjectSearchPath[1] + Path.DirectorySeparatorChar + Package.Name + ".ali", FileAttributes.ReadOnly);
			if (!Directory.Exists(Settings.PackageDatabasePath)) Directory.CreateDirectory(Settings.PackageDatabasePath);
			Package.WriteInfo(new FileStream(Settings.PackageDatabasePath + Path.DirectorySeparatorChar + Package.Name, FileMode.Create));
			File.SetAttributes(Settings.PackageDatabasePath + Path.DirectorySeparatorChar + Package.Name, FileAttributes.ReadOnly);
		}

	}
}