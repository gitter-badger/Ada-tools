using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a base-2 fixed-point numeric type
	/// </summary>
	public sealed class OrdinaryType : FixedType {
		public OrdinaryType(String Name, Double Delta) : base(Name, Delta) {

		}

		public OrdinaryType(String Name, Double Delta, Range<Double> Range) : base(Name, Delta, Range) {

		}
	}
}
