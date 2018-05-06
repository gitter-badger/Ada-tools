using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a floating-point numeric type
	/// </summary>
	public sealed class FloatType : RealType {

		private readonly UInt16 Digits;

		private readonly Range<Double> Range;

		public FloatType(String Name, UInt16 Digits) : base(Name) {
			this.Digits = Digits;
		}

		public FloatType(String Name, UInt16 Digits, Range<Double> Range) : this(Name, Digits) {
			this.Range = Range;
		}
	}
}
