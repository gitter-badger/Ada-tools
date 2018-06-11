using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AdaTools {

	/// <summary>
	/// Represents an Ada program
	/// </summary>
	/// <remarks>
	/// Holds traits about the program for easy analysis
	/// </remarks>
	public sealed class ProgramUnit : Unit {

		/// <summary>
		/// The type of program
		/// </summary>
		public readonly ProgramType Type;

		/// <summary>
		/// Whether a spec file for this program was found
		/// </summary>
		/// <remarks>
		/// This intentionally is a false getter and a null setter because a program unit only involves a body.
		/// </remarks>
		public override Boolean HasSpec { get => false; protected set { } }

		/// <summary>
		/// Whether a body file for this program was found
		/// </summary>
		public override Boolean HasBody { get; protected set; }

		/// <summary>
		/// Whether the program uses entirely remote calls
		/// </summary>
		public override Boolean IsAllCallsRemote { get; }

		/// <summary>
		/// Whether the program is pure
		/// </summary>
		public override Boolean IsPure { get; }

		/// <summary>
		/// Whether the program is a remote call interface
		/// </summary>
		/// <remarks>
		/// This intentionally is a false getter because a program unit can not be a remote call interface
		/// </remarks>
		public override Boolean IsRemoteCallInterface { get => false; }

		public override String OutputArguments {
			get {
				if (Environment.OSVersion.Platform <= (PlatformID)3) {
					return base.OutputArguments.TrimEnd() + ".exe ";
				} else if (Environment.OSVersion.Platform == PlatformID.Unix) {
					return base.OutputArguments;
				} else {
					throw new PlatformNotSupportedException();
				}
			}
		}

		/// <summary>
		/// Get all associated files of this program
		/// </summary>
		/// <returns>An array of the file names</returns>
		public override String[] GetFiles() {
			if (!this.Krunched) {
				return new String[] { this.Name + Extension };
			} else {
				return new String[] { Compiler.Krunch(this.Name + Extension) };
			}
		}

		/// <summary>
		/// Initialize a program with the specified <paramref name="Name"/>
		/// </summary>
		/// <remarks>
		/// </remarks>
		/// <param name="Name">The name of the program</param>
		public ProgramUnit(String Name) : base(Name) {
			Source ProgSource = new Source(Name + Extension);
			String ProgName = ProgSource.ParseName();
			if (this.Name != ProgName) throw new ProgramNameDoesNotMatchException();
			this.Type = ProgSource.ParseProgramType();
		}

		/// <summary>
		/// The file extension to use for programs
		/// </summary>
		public static String Extension = ".adb";
	}
}
