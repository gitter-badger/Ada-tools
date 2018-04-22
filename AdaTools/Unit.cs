using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdaTools {
	/// <summary>
	/// Represents any of the various Ada units that can exist
	/// </summary>
	public abstract class Unit {

		/// <summary>
		/// The name of this unit
		/// </summary>
		public readonly String Name;

		/// <summary>
		/// The full list of package names this unit depends on
		/// </summary>
		public readonly List<String> Dependencies = new List<String>();

		/// <summary>
		/// Return the linker argument section for this unit
		/// </summary>
		public String LinkerArguments {
			get {
				String Result = "";
				foreach (String Dependency in this.Dependencies) {
					// Ada standard libraries are included anyways, so don't even bother
					if (new Regex(@"^(ada|interfaces|system)\b", RegexOptions.IgnoreCase).IsMatch(Dependency)) continue;
					// Add the linker dependency argument
					Result += "-l" + Dependency + " ";
				}
				return Result.Trim();
			}
		}

		/// <summary>
		/// Whether the unit has a specification
		/// </summary>
		public abstract Boolean HasSpec { get; protected set; }

		/// <summary>
		/// Whether the unit has a body
		/// </summary>
		public abstract Boolean HasBody { get; protected set; }
		
		/// <summary>
		/// Whether the unit makes entirely remote calls
		/// </summary>
		public abstract Boolean IsAllCallsRemote { get; protected set; }

		/// <summary>
		/// Whether the unit is pure
		/// </summary>
		public abstract Boolean IsPure { get; protected set; }

		/// <summary>
		/// Whether the unit is a remote call interface
		/// </summary>
		public abstract Boolean IsRemoteCallInterface { get; protected set; }

		/// <summary>
		/// Get all associated files of this unit
		/// </summary>
		/// <returns>An array of the file names</returns>
		public abstract String[] GetFiles();

		protected Unit(String Name) {
			this.Name = Name;
		}
	}
}
