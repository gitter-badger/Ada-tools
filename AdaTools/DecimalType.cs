using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a base-10 fixed-point numeric type
	/// </summary>
	public sealed class DecimalType : FixedType {

		private readonly UInt16 Digits;

		public DecimalType(String Name, Double Delta, UInt16 Digits) : base(Name, Delta) {
			this.Digits = Digits;
		}

		public DecimalType(String Name, Double Delta, UInt16 Digits, Range<Double> Range) : base(Name, Delta, Range) {
			this.Digits = Digits;
		}
	}
}
