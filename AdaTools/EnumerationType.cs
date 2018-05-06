using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an enumeration type
	/// </summary>
	public sealed class EnumerationType : DiscreteType {

		public readonly String[] Values;

		public EnumerationType(String Name, params String[] Values) : base(Name) {
			this.Values = Values;
		}
	}
}
