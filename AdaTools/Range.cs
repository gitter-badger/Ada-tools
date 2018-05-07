using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public struct Range<Type> where Type : IComparable, IEquatable<Type> {

		internal readonly Type Lower;

		internal readonly Type Upper;

		public Boolean Contains(Type Value) {
			return Value.CompareTo(this.Lower) >= 0 && Value.CompareTo(this.Upper) <= 0;
		}

		public Boolean Contains(Range<Type> Range) {
			return Range.Lower.CompareTo(this.Lower) >= 0 && Range.Upper.CompareTo(this.Upper) <= 0;
		}

		public override Boolean Equals(Object obj) => (obj is Range<Type>) && this == (Range<Type>)obj;

		public override Int32 GetHashCode() => base.GetHashCode();

		public static Boolean operator ==(Range<Type> Left, Range<Type> Right) => Left.Lower.Equals(Right.Lower) && Left.Upper.Equals(Right.Upper);

		public static Boolean operator !=(Range<Type> Left, Range<Type> Right) => !Left.Lower.Equals(Right.Lower) || !Left.Upper.Equals(Right.Upper);

		public Range(Type Lower, Type Upper) {
			if (Lower.CompareTo(Upper) >= 0) throw new InvalidRangeException();
			this.Lower = Lower;
			this.Upper = Upper;
		}

	}
}
