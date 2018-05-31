using System;
using System.Text.RegularExpressions;

namespace AdaTools {
	/// <summary>
	/// Represents an Ada package
	/// </summary>
	/// <remarks>
	/// Holds traits about the package for easy analysis
	/// </remarks>
	public sealed class PackageUnit : Unit {

		/// <summary>
		/// Whether a spec file for this package was found
		/// </summary>
		public override Boolean HasSpec { get; protected set; }

		/// <summary>
		/// Whether a body file for this package was found
		/// </summary>
		public override Boolean HasBody { get; protected set; }

		/// <summary>
		/// Whether the package uses entirely remote calls
		/// </summary>
		public override Boolean IsAllCallsRemote { get; protected set; }

		/// <summary>
		/// Whether the package is pure
		/// </summary>
		public override Boolean IsPure { get; protected set; }

		/// <summary>
		/// Whether the package is a remote call interface
		/// </summary>
		public override Boolean IsRemoteCallInterface { get; protected set; }

		/// <summary>
		/// Return the output argument section for this unit
		/// </summary>
		public override String OutputArguments {
			get {
				if (Environment.OSVersion.Platform <= (PlatformID)3) {
					return base.OutputArguments.TrimEnd() + ".dll ";
				} else if (Environment.OSVersion.Platform == PlatformID.Unix) {
					return base.OutputArguments.TrimEnd() + ".so ";
				} else {
					throw new PlatformNotSupportedException();
				}
			}
		}

		/// <summary>
		/// Get all associated files of this package
		/// </summary>
		/// <returns>An array of the file names</returns>
		public override String[] GetFiles() {
			if (this.HasSpec && this.HasBody) {
				return new String[] { this.Name + BodyExtension, this.Name + SpecExtension };
			} else if (this.HasSpec) {
				return new String[] { this.Name + SpecExtension };
			} else if (this.HasBody) {
				return new String[] { this.Name + BodyExtension };
			} else {
				return Array.Empty<String>();
			}
		}

		/// <summary>
		/// Get the spec file if it exists
		/// </summary>
		/// <returns>The spec file if it exists, or null otherwise</returns>
		public String GetSpec() => (this.HasSpec) ? this.Name + SpecExtension : null;

		/// <summary>
		/// Get the body file if it exists
		/// </summary>
		/// <returns>The body file if it exists, or null otherwise</returns>
		public String GetBody() => (this.HasBody) ? this.Name + BodyExtension : null;

		/// <summary>
		/// Initialize a package with the specified <paramref name="Name"/>
		/// </summary>
		/// <remarks>
		/// This attempts to find source files associated with the package. If files with the appropriate name are found, they are then parsed for certain traits and validated against, then additional traits are retrieved. If no source files are found, the package is still initialized, but only with the given name.
		/// </remarks>
		/// <param name="Name">The name of the package</param>
		public PackageUnit(String Name) : base(Name) {

			// We need to tollerate missing specs or bodies, as long as at least one is found.
			Source SpecSource;
			String SpecName = null;
			try {
				SpecSource = new Source(Name + SpecExtension);
				SpecName = SpecSource.ParseName();
				this.HasSpec = true;
				this.IsAllCallsRemote = SpecSource.ParseAllCallsRemote();
				this.Dependencies.AddRange(SpecSource.ParseDependencies());
				this.IsPure = SpecSource.ParsePurity();
				this.IsRemoteCallInterface = SpecSource.ParseRemoteCallInterface();
			} catch {
				SpecSource = null;
				this.HasSpec = false;
				this.IsAllCallsRemote = false;
				this.IsPure = false;
				this.IsRemoteCallInterface = false;
			}
			Source BodySource;
			String BodyName = null;
			try {
				BodySource = new Source(Name + BodyExtension);
				BodyName = BodySource.ParseName();
				this.HasBody = true;
				this.Dependencies.AddRange(BodySource.ParseDependencies());
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
