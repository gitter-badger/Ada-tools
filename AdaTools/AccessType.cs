using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an access type
	/// </summary>
	public sealed class AccessType : ElementaryType {

		/// <summary>
		/// The actual type this accesses
		/// </summary>
		public readonly Type Accesses;

		public AccessType(String Name, Type Accesses) : base(Name) {
			this.Accesses = Accesses;
		}
	}
}
