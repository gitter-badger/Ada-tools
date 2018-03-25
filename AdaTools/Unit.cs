using System;
using System.Collections.Generic;
using System.Text;

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
		/// Whether the unit has a specification
		/// </summary>
		public abstract Boolean HasSpec { get; protected set; }

		/// <summary>
		/// Whether the unit has a body
		/// </summary>
		public abstract Boolean HasBody { get; protected set; }
		
		public abstract Boolean IsAllCallsRemote { get; protected set; }

		public abstract Boolean IsPure { get; protected set; }

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
