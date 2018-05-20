using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AdaTools {
	public static class Settings {

		/// <summary>
		/// The search path for source files
		/// </summary>
		/// <returns>A list of each source path</returns>
		public static List<String> SourceSearchPath {
			get => new List<String>(("./:" + Environment.GetEnvironmentVariable("ADA_INCLUDE_PATH")).Split(':'));
			set => Environment.SetEnvironmentVariable("ADA_INCLUDE_PATH", String.Join(':', value));
		}

		/// <summary>
		/// The search path for object files
		/// </summary>
		/// <returns>A list of each object path</returns>
		public static List<String> ObjectSearchPath {
			get => new List<String>(("./:" + Environment.GetEnvironmentVariable("ADA_OBJECTS_PATH")).Split(':'));
			set => Environment.SetEnvironmentVariable("ADA_OBJECTS_PATH", String.Join(':', value));
		}

		/// <summary>
		/// The path to the package database
		/// </summary>
		/// <returns>A single path entry</returns>
		public static String PackageDatabasePath {
			get {
				if (Environment.OSVersion.Platform <= (PlatformID)3) {
					//TODO: Windows packagedb path
					throw new NotImplementedException();
				} else if (Environment.OSVersion.Platform == PlatformID.Unix) {
					return "/var/lib/adatools"; //TODO: This probably shouldn't be hardcoded, but this is the most sensible location
				} else {
					throw new NotImplementedException();
				}
			}
		}

		/// <summary>
		/// Print out settings to console
		/// </summary>
		public static void Print() {
			Console.WriteLine("Source Search Path: " + String.Join("  ", SourceSearchPath));
			Console.WriteLine("Object Search Path: " + String.Join("  ", ObjectSearchPath));
			Console.WriteLine("Package Database Path: " + PackageDatabasePath);
		}

	}
}