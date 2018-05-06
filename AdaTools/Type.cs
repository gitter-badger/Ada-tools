using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an abstract type
	/// </summary>
	public abstract class Type {

		/// <summary>
		/// The name of the type
		/// </summary>
		public readonly String Name;

		protected Type(String Name) {
			this.Name = Name;
		}
	}
}
