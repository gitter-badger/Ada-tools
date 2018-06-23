using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace AdaTools {
	public static class Settings {

		/// <summary>
		/// Whether packages are built upon install
		/// </summary>
		/// <remarks>
		/// If this setting is true when a package is installed, instead of using the prebuilt generic library, it is build specifically for the native architecture. This is an optimization, but increases install times.
		/// </remarks>
		public static Boolean BuildPackagesOnInstall {
			get {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					Boolean Result;
					if (Boolean.TryParse((String)Registry.LocalMachine.OpenSubKey("SOFTWARE")?.OpenSubKey("AdaTools")?.GetValue("BuildPackages"), out Result)) {
						return Result;
					} else {
						return false;
					}
				case PlatformID.Unix:
				default:
					return Boolean.Parse(Environment.GetEnvironmentVariable("ADA_BUILD_PACKAGES", EnvironmentVariableTarget.Machine));
				}
			}
			set {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("AdaTools").SetValue("BuildPackages", value);
					return;
				case PlatformID.Unix:
				default:
					Environment.SetEnvironmentVariable("ADA_BUILD_PACKAGES", value.ToString(), EnvironmentVariableTarget.Machine);
					return;
				}
			}
		}

		internal static String GNATSourcePath {
			get {
				try {
					Process GnatLS = new Process();
					GnatLS.StartInfo.FileName = "gnatls";
					GnatLS.StartInfo.Arguments = "-v";
					GnatLS.StartInfo.RedirectStandardOutput = true;
					GnatLS.Start();
					Match Match;
					while (!GnatLS.StandardOutput.EndOfStream) {
						Match = new Regex(@"^.*(gcc|gnat).*adainclude", RegexOptions.IgnoreCase).Match(GnatLS.StandardOutput.ReadLine());
						if (Match.Value != "") return Match.Value.Trim();
					}
					return "";
				} catch (Win32Exception) {
					throw new MissingGNATProgramException("gnatls");
				}
			}
		}

		internal static String GNATObjectsPath {
			get {
				try {
					Process GnatLS = new Process();
					GnatLS.StartInfo.FileName = "gnatls";
					GnatLS.StartInfo.Arguments = "-v";
					GnatLS.StartInfo.RedirectStandardOutput = true;
					GnatLS.Start();
					Match Match;
					while (!GnatLS.StandardOutput.EndOfStream) {
						Match = new Regex(@"^.*(gcc|gnat).*adalib", RegexOptions.IgnoreCase).Match(GnatLS.StandardOutput.ReadLine());
						if (Match.Value != "") return Match.Value.Trim();
					}
					return "";
				} catch (Win32Exception) {
					throw new MissingGNATProgramException("gnatls");
				}
			}
		}

		/// <summary>
		/// The search path for source files
		/// </summary>
		/// <returns>A list of each source path</returns>
		public static List<String> SourceSearchPath {
			get {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					return new List<String>((".\\;" + Environment.GetEnvironmentVariable("ADA_INCLUDE_PATH", EnvironmentVariableTarget.Machine) + ";" + GNATSourcePath).Split(';'));
				case PlatformID.Unix:
				default:
					return new List<String>(("./:" + Environment.GetEnvironmentVariable("ADA_INCLUDE_PATH", EnvironmentVariableTarget.Machine) + ":" + GNATSourcePath).Split(':'));
				}
			}
			set {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					Environment.SetEnvironmentVariable("ADA_INCLUDE_PATH", String.Join(';', value), EnvironmentVariableTarget.Machine);
					break;
				case PlatformID.Unix:
				default:
					Environment.SetEnvironmentVariable("ADA_INCLUDE_PATH", String.Join(':', value), EnvironmentVariableTarget.Machine);
					break;
				}
			}
		}


		/// <summary>
		/// The search path for object files
		/// </summary>
		/// <returns>A list of each object path</returns>
		public static List<String> ObjectSearchPath {
			get {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					return new List<String>((".\\;" + Environment.GetEnvironmentVariable("ADA_OBJECTS_PATH", EnvironmentVariableTarget.Machine) + ";" + GNATObjectsPath).Split(';'));
				case PlatformID.Unix:
				default:
					return new List<String>(("./:" + Environment.GetEnvironmentVariable("ADA_OBJECTS_PATH", EnvironmentVariableTarget.Machine) + ":" + GNATObjectsPath).Split(':'));
				}
			}
			set {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					Environment.SetEnvironmentVariable("ADA_OBJECTS_PATH", String.Join(';', value), EnvironmentVariableTarget.Machine);
					break;
				case PlatformID.Unix:
				default:
					Environment.SetEnvironmentVariable("ADA_OBJECTS_PATH", String.Join(':', value), EnvironmentVariableTarget.Machine);
					break;
				}
			}
		}

		/// <summary>
		/// The path to the package database
		/// </summary>
		/// <returns>A single path entry</returns>
		public static String PackageDatabasePath {
			get {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					return (String)Registry.LocalMachine.OpenSubKey("SOFTWARE")?.OpenSubKey("AdaTools")?.GetValue("PackageDatabase") ?? "";
				case PlatformID.Unix:
				default:
					return Environment.GetEnvironmentVariable("ADA_PACKAGEDB_PATH");
				}
			}
			set {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("AdaTools").SetValue("PackageDatabase", value);
					break;
				case PlatformID.Unix:
				default:
					Environment.SetEnvironmentVariable("ADA_PACKAGEDB_PATH", value);
					break;
				}
			}
		}

		/// <summary>
		/// The path to the package repository
		/// </summary>
		/// <remarks>
		/// This isn't currently used and won't be for a while, but the implementation will not need to change for when it is used
		/// </remarks>
		public static String PackageRepositoryPath {
			get {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					return (String)Registry.LocalMachine.OpenSubKey("SOFTWARE")?.OpenSubKey("AdaTools")?.GetValue("PackageRepository") ?? "";
				case PlatformID.Unix:
				default:
					return Environment.GetEnvironmentVariable("ADA_REPOSITORY_PATH");
				}
			}
			set {
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey("AdaTools").SetValue("PackageRepository", value);
					break;
				case PlatformID.Unix:
				default:
					Environment.SetEnvironmentVariable("ADA_REPOSITORY_PATH", value);
					break;
				}
			}
		}

		/// <summary>
		/// Print out settings to console
		/// </summary>
		public static void Print() {
			Console.WriteLine("Build Packages On Install: " + BuildPackagesOnInstall ?? "");
			Console.WriteLine("Source Search Path: " + String.Join("  ", SourceSearchPath));
			Console.WriteLine("Source Install Directory " + SourceSearchPath[1]);
			Console.WriteLine("Object Search Path: " + String.Join("  ", ObjectSearchPath));
			Console.WriteLine("Object Install Directory " + ObjectSearchPath[1]);
			Console.WriteLine("Package Database Path: " + PackageDatabasePath);
			Console.WriteLine("Package Repository Path: " + PackageRepositoryPath);
		}

	}
}