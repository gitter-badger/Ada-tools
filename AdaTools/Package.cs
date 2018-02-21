using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an Ada package
	/// </summary>
	/// <remarks>
	/// Holds traits about the package for easy analysis
	/// </remarks>
	public class Package {

		/// <summary>
		/// The file extension to use for package specifications
		/// </summary>
		public static String SpecExtension = ".ads";

		/// <summary>
		/// The file extension to use for package bodies
		/// </summary>
		public static String BodyExtension = ".adb";

		/// <summary>
		/// The name of the package
		/// </summary>
		public readonly String Name;

		/// <summary>
		/// The full list of packages this package depends on
		/// </summary>
		public readonly List<Package> Dependencies;

		public Package(String Name) {
			Source SpecSource;
			try {
				SpecSource = new Source(Name + SpecExtension);
			} catch {
				SpecSource = null;
			}
			Source BodySource;
			try {
				BodySource = new Source(Name + BodyExtension);
			} catch {
				BodySource = null;
			}
		}
	}
}
