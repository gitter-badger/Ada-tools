using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an enumeration type
	/// </summary>
	public sealed class EnumerationType : DiscreteType {

		public String[] Values { get; private set; }

		public Boolean? Contains(String Value) {
			foreach (String val in this.Values) {
				if (val.ToUpper() == Value.ToUpper()) return true;
			}
			return false;
		}

		public EnumerationType(String Name, params String[] Values) : base(Name) {
			this.Values = Values;
		}
	}
}
