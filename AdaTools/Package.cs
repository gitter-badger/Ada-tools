using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdaTools {
	/// <summary>
	/// Represents an Ada package
	/// </summary>
	/// <remarks>
	/// Holds traits about the package for easy analysis
	/// </remarks>
	public sealed class Package : Unit {

		/// <summary>
		/// The full list of package names this package depends on
		/// </summary>
		public readonly List<String> Dependencies = new List<String>();

		/// <summary>
		/// Whether a spec file for this package was found
		/// </summary>
		public readonly Boolean HasSpec;

		/// <summary>
		/// Whether a body file for this package was found
		/// </summary>
		public readonly Boolean HasBody;

		/// <summary>
		/// Get all associated files of this package
		/// </summary>
		/// <returns>An array of the file names</returns>
		public override String[] GetFiles() {
			if (this.HasSpec && this.HasBody) {
				return new String[] { this.Name + SpecExtension, this.Name + BodyExtension };
			} else if (this.HasSpec) {
				return new String[] { this.Name + SpecExtension };
			} else if (this.HasBody) {
				return new String[] { this.Name + BodyExtension };
			} else {
				return Array.Empty<String>();
			}
		}

		/// <summary>
		/// Initialize a package with the specified <paramref name="Name"/>
		/// </summary>
		/// <remarks>
		/// This attempts to find source files associated with the package. If files with the appropriate name are found, they are then parsed for certain traits and validated against, then additional traits are retrieved. If no source files are found, the package is still initialized, but only with the given name.
		/// </remarks>
		/// <param name="Name">The name of the package</param>
		public Package(String Name) : base(Name) {

			// We need to tollerate missing specs or bodies, as long as at least one is found.
			Source SpecSource;
			String SpecName = null;
			try {
				SpecSource = new Source(Name + SpecExtension);
				SpecName = SpecSource.TryParseName();
				this.HasSpec = true;
				this.Dependencies.AddRange(SpecSource.TryParseDependencies());
			} catch {
				SpecSource = null;
				this.HasSpec = false;
			}
			Source BodySource;
			String BodyName = null;
			try {
				BodySource = new Source(Name + BodyExtension);
				BodyName = BodySource.TryParseName();
				this.HasBody = true;
				this.Dependencies.AddRange(BodySource.TryParseDependencies());
			} catch {
				BodySource = null;
				this.HasBody = false;
			}

			// Validate all the names match up
			if (SpecName != null && this.Name.ToLower() != SpecName.ToLower()) throw new PackageNameDoesNotMatchException();
			if (BodyName != null && this.Name.ToLower() != BodyName.ToLower()) throw new PackageNameDoesNotMatchException();
			if ((SpecName != null && BodyName != null) && SpecName.ToLower() != BodyName.ToLower()) throw new PackageNameDoesNotMatchException();
		}

		/// <summary>
		/// The file extension to use for package specifications
		/// </summary>
		public static String SpecExtension = ".ads";

		/// <summary>
		/// The file extension to use for package bodies
		/// </summary>
		public static String BodyExtension = ".adb";

	}
}
