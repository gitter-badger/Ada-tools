using System;
using System.Collections.Generic;
using System.IO;
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
		/// Separate units associated with this package unit
		/// </summary>
		private readonly List<SeparateUnit> SeparateUnits = new List<SeparateUnit>();

		/// <summary>
		/// Whether a spec file for this package was found
		/// </summary>
		public override Boolean HasSpec { get; protected set; }

		/// <summary>
		/// Whether a body file for this package was found
		/// </summary>
		public override Boolean HasBody { get; protected set; }

		/// <summary>
		/// Whether the unit makes entirely remote calls
		/// </summary>
		/// <remarks>
		/// This implements lazy loading even though it doesn't use the Lazy class
		/// </remarks>
		public override Boolean IsAllCallsRemote {
			get {
				foreach (String FileName in this.GetFiles()) {
					if (new Source(FileName).ParseAllCallsRemote()) return true;
				}
				return false;
			}
		}

		/// <summary>
		/// Whether the unit is pure
		/// </summary>
		/// <remarks>
		/// This implements lazy loading even though it doesn't use the Lazy class
		/// </remarks>
		public override Boolean IsPure {
			get {
				foreach (String FileName in this.GetFiles()) {
					if (new Source(FileName).ParsePurity()) return true;
				}
				return false;
			}
		}

		/// <summary>
		/// Whether the unit is a remote call interface
		/// </summary>
		/// <remarks>
		/// This implements lazy loading even though it doesn't use the Lazy class
		/// </remarks>
		public override Boolean IsRemoteCallInterface {
			get {
				foreach (String FileName in this.GetFiles()) {
					if (new Source(FileName).ParseRemoteCallInterface()) return true;
				}
				return false;
			}
		}


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
			List<String> Result = new List<String>();
			if (this.HasSpec && this.HasBody) {
				if (!this.Krunched) {
					Result.Add(this.Name + BodyExtension);
					Result.Add(this.Name + SpecExtension);
				} else {
					Result.Add(Compiler.Krunch(this.Name + BodyExtension));
					Result.Add(Compiler.Krunch(this.Name + SpecExtension));
				}
			} else if (this.HasSpec) {
				if (!this.Krunched) {
					Result.Add(this.Name + SpecExtension);
				} else {
					Result.Add(Compiler.Krunch(this.Name + SpecExtension));
				}
			} else if (this.HasBody) {
				if (!this.Krunched) {
					Result.Add(this.Name + BodyExtension);
				} else {
					Result.Add(Compiler.Krunch(this.Name + BodyExtension));
				}
			}
			foreach (SeparateUnit Separate in this.SeparateUnits) {
				Result.AddRange(Separate.GetFiles());
			}
			return Result.ToArray();
		}

		/// <summary>
		/// Get the spec file if it exists
		/// </summary>
		/// <returns>The spec file if it exists, or null otherwise</returns>
		public String GetSpec() {
			if (this.HasSpec) {
				if (!this.Krunched) {
					return this.Name + SpecExtension;
				} else {
					return Compiler.Krunch(this.Name + SpecExtension);
				}
			} else {
				return null;
			}
		}

		/// <summary>
		/// Get the body file if it exists
		/// </summary>
		/// <returns>The body file if it exists, or null otherwise</returns>
		public String GetBody() {
			if (this.HasBody) {
				if (!this.Krunched) {
					return this.Name + BodyExtension;
				} else {
					return Compiler.Krunch(this.Name + BodyExtension);
				}
			} else {
				return null;
			}
		}

		/// <summary>
		/// Initialize the Standard package
		/// </summary>
		/// <remarks>
		/// This does not parse a source, but rather, is for setting up Standard, a package that doesn't actual exist as code
		/// </remarks>
		private PackageUnit() : base("Standard") {
			this.HasBody = false;
			this.HasSpec = false;
			this.Types.Add(
				new EnumerationType("Boolean", "False", "True"),
				new SignedType("Short_Short_Integer"),
				new SignedType("Short_Integer"),
				new SignedType("Integer"),
				new SignedType("Long_Integer"),
				new SignedType("Long_Long_Integer"),
				new FloatType("Short_Float"),
				new FloatType("Float"),
				new FloatType("Long_Float"),
				new FloatType("Long_Long_Float"),
				new EnumerationType("Character"),
				new EnumerationType("Wide_Character"),
				new EnumerationType("Wide_Wide_Character"),
				new OrdinaryType("Duration"));
			// This has to come after, as a separate add, so that the reference lookup actually finds what it needs to
			this.Types.Add(
				new ArrayType("String", this.Types["Character"]),
				new ArrayType("Wide_String", this.Types["Wide_Character"]),
				new ArrayType("Wide_Wide_String", this.Types["Wide_Wide_Character"]));
		}

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
				if (File.Exists(Name + SpecExtension)) {
					SpecSource = new Source(Name + SpecExtension);
				} else if (File.Exists(Compiler.Krunch(Name + SpecExtension))) {
					this.Krunched = true;
					SpecSource = new Source(Compiler.Krunch(Name + SpecExtension));
				} else {
					throw new FileNotFoundException();
				}
				SpecName = SpecSource.ParseName();
				this.HasSpec = true;
			} catch {
				SpecSource = null;
				this.HasSpec = false;
			}
			Source BodySource;
			String BodyName = null;
			try {
				if (File.Exists(Name + BodyExtension)) {
					BodySource = new Source(Name + BodyExtension);
				} else if (File.Exists(Compiler.Krunch(Name + BodyExtension))) {
					this.Krunched = true;
					BodySource = new Source(Compiler.Krunch(Name + BodyExtension));
				} else {
					throw new FileNotFoundException();
				}
				BodyName = BodySource.ParseName();
				this.HasBody = true;
			} catch {
				BodySource = null;
				this.HasBody = false;
			}

			// Validate all the names match up
			if (SpecName != null && this.Name.ToLower() != SpecName.ToLower()) throw new PackageNameDoesNotMatchException("The spec '" + SpecName + "' is different from the expected '" + this.Name + "'");
			if (BodyName != null && this.Name.ToLower() != BodyName.ToLower()) throw new PackageNameDoesNotMatchException("The body '" + BodyName + "' is different from the expected '" + this.Name + "'");
			if ((SpecName != null && BodyName != null && SpecName.ToLower() != BodyName.ToLower())) throw new PackageNameDoesNotMatchException("The spec '" + SpecName + "' does not match the body '" + BodyName + "'");
		}

		/// <summary>
		/// The file extension to use for package specifications
		/// </summary>
		public static String SpecExtension = ".ads";

		/// <summary>
		/// The file extension to use for package bodies
		/// </summary>
		public static String BodyExtension = ".adb";

		/// <summary>
		/// Get the Standard package
		/// </summary>
		/// <returns>A package unit representing Standard</returns>
		internal static PackageUnit Standard() {
			return new PackageUnit();
		}

	}
}
