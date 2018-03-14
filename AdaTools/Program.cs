using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {

	/// <summary>
	/// Represents an Ada program
	/// </summary>
	/// <remarks>
	/// Holds traits about the program for easy analysis
	/// </remarks>
	public sealed class Program : Unit {

		/// <summary>
		/// The type of program
		/// </summary>
		public readonly ProgramType Type;

		/// <summary>
		/// The full list of package names this package depends on
		/// </summary>
		public readonly List<String> Dependencies = new List<String>();

		/// <summary>
		/// Get all associated files of this program
		/// </summary>
		/// <returns>An array of the file names</returns>
		public override String[] GetFiles() => new String[] { this.Name + Extension };

		/// <summary>
		/// Initialize a program with the specified <paramref name="Name"/>
		/// </summary>
		/// <remarks>
		/// </remarks>
		/// <param name="Name">The name of the program</param>
		public Program(String Name) : base(Name) {
			Source ProgSource = new Source(Name + Extension);
			String ProgName = ProgSource.TryParseName();
			if (this.Name != ProgName) throw new ProgramNameDoesNotMatchException();
			this.Type = ProgSource.TryParseProgramType();
			this.Dependencies.AddRange(ProgSource.TryParseDependencies());
		}

		/// <summary>
		/// The file extension to use for programs
		/// </summary>
		public static String Extension = ".adb";
	}
}
