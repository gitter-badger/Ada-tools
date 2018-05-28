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

		public override String ToString() => "type " + this.Name + " is (" + String.Join(", ", this.Values) + ");";

		public override Boolean Equals(Object obj) {
			if (!(obj is EnumerationType)) return false;
			return this.Values == (obj as EnumerationType).Values;
		}

		public override Int32 GetHashCode() => base.GetHashCode();

		public EnumerationType(String Name, params String[] Values) : base(Name) {
			this.Values = Values;
		}
	}
}
