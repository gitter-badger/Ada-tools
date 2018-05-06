using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public struct Range<Type> where Type : IComparable {

		internal readonly Type Lower;

		internal readonly Type Upper;

		public Boolean Contains(Type Value) {
			return Value.CompareTo(this.Lower) >= 0 && Value.CompareTo(this.Upper) <= 0;
		}

		public Boolean Contains(Range<Type> Range) {
			return Range.Lower.CompareTo(this.Lower) >= 0 && Range.Upper.CompareTo(this.Upper) <= 0;
		}

		public Range(Type Lower, Type Upper) {
			if (Lower.CompareTo(Upper) >= 0) throw new InvalidRangeException();
			this.Lower = Lower;
			this.Upper = Upper;
		}

	}
}
