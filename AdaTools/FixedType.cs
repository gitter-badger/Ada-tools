using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a fixed-point numeric type
	/// </summary>
	public abstract class FixedType : RealType {

		private readonly Double Delta;

		private readonly Range<Double> Range;

		protected FixedType(String Name, Double Delta) : base(Name) {
			this.Delta = Delta;
		}

		protected FixedType(String Name, Double Delta, Range<Double> Range) : this(Name, Delta) {
			this.Range = Range;
		}
	}
}
