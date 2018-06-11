using System;
using System.IO;

namespace AdaTools {
	/// <summary>
	/// Represents an Ada subroutine
	/// </summary>
	/// <remarks>
	/// Holds traits about the subroutine for easy analysis
	/// </remarks>
	public sealed class SubroutineUnit : Unit {

		/// <summary>
		/// Whether a spec file for this subroutine was found
		/// </summary>
		public override Boolean HasSpec { get; protected set; }

		/// <summary>
		/// Whether a body file for this subroutine was found
		/// </summary>
		public override Boolean HasBody { get; protected set; }

		/// <summary>
		/// Whether the subroutine uses entirely remote calls
		/// </summary>
		public override Boolean IsAllCallsRemote { get; }

		/// <summary>
		/// Whether the subroutine is pure
		/// </summary>
		public override Boolean IsPure { get; }

		/// <summary>
		/// Whether the subroutine is a remote call interface
		/// </summary>
		/// <remarks>
		/// This is intentionally a false getter because a subroutine can not be a remote call interface
		/// </remarks>
		public override Boolean IsRemoteCallInterface { get => false; }

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
		/// Get all associated files of this subroutine
		/// </summary>
		/// <returns>An array of the file names</returns>
		public override String[] GetFiles() {
			if (this.HasSpec && this.HasBody) {
				if (!this.Krunched) {
					return new String[] { this.Name + BodyExtension, this.Name + SpecExtension };
				} else {
					return new String[] { Compiler.Krunch(this.Name + BodyExtension), Compiler.Krunch(this.Name + SpecExtension) };
				}
			} else if (this.HasSpec) {
				if (!this.Krunched) {
					return new String[] { this.Name + SpecExtension };
				} else {
					return new String[] { Compiler.Krunch(this.Name + SpecExtension) };
				}
			} else if (this.HasBody) {
				if (!this.Krunched) {
					return new String[] { this.Name + BodyExtension };
				} else {
					return new String[] { Compiler.Krunch(this.Name + BodyExtension) };
				}
			} else {
				return Array.Empty<String>();
			}
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
		/// Initialize a subroutine with the specified <paramref name="Name"/>
		/// </summary>
		/// <remarks>
		/// This attempts to find source files associated with the subroutine. If files with the appropriate name are found, they are then parsed for certain traits and validated against, then additional traits are retrieved. If no source files are found, the subroutine is still initialized, but only with the given name.
		/// </remarks>
		/// <param name="Name">The name of the subroutine</param>
		public SubroutineUnit(String Name) : base(Name) {
			Console.WriteLine("new SubroutineUnit(" + Name + ")");

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

		public static String BodyExtension = ".adb";

		public static String SpecExtension = ".ads";

	}
}
