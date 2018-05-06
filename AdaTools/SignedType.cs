using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a signed integer type
	/// </summary>
	public sealed class SignedType : IntegerType {

		private readonly Range<Int64> Range;

		public SignedType(String Name, Range<Int16> Range): base(Name) {
			this.Range = new Range<Int64>(Range.Lower, Range.Upper);
		}

		public SignedType(String Name, Range<Int32> Range): base(Name) {
			this.Range = new Range<Int64>(Range.Lower, Range.Upper);
		}

		public SignedType(String Name, Range<Int64> Range) : base(Name) {
			this.Range = Range;
		}
	}
}
