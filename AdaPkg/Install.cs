using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using AdaTools;
using Console = Colorful.Console;

namespace AdaPkg {
	static class Install {

		public static void FullHelp() {
			Console.WriteLine("install [--global] <package>+ — Install the specified packages globally");
			Console.WriteLine("  --list — List the installed packages");
		}

		public static void Help() {
			Console.WriteLine("  install <package>+ — Install the specified packages");
		}

		/// <summary>
		/// Install the specified package
		/// </summary>
		/// <param name="Package">Package to install</param>
		public static void GlobalInstall(String Name) {
			AdaTools.Package Package = new AdaTools.Package(Name);
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

		public static void GlobalInstall(List<String> Names) {
			foreach (String Name in Names) {
				GlobalInstall(Name);
			}
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
					Console.Write(Package.Name + ":");
					Console.Write(Package.Variant, Color.Khaki);
				}
				Console.WriteLine(" [" + Package.Version + "]", Color.Indigo);
			}
		}

		internal static InstallFlags ParseInstallFlags(List<String> Args) {
			InstallFlags Result = InstallFlags.Install;
			foreach (String Arg in Args) {
				switch (Arg.ToUpper()) {
				case "--HELP":
					return InstallFlags.Help;
				case "--LIST":
					Result |= InstallFlags.List;
					break;
				case "--GLOBAL":
				default:
					Result |= InstallFlags.Global;
					break;
				}
			}
			return Result;
		}

	}
}