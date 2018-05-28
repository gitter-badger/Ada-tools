using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an Ada range
	/// </summary>
	public struct Range<Type> where Type : IComparable, IEquatable<Type> {

		/// <summary>
		/// Lower bound of the range
		/// </summary>
		internal readonly Type Lower;

		/// <summary>
		/// Upper bound of the range
		/// </summary>
		internal readonly Type Upper;


		/// <summary>
		/// Whether the range contains the specified <paramref name="Value"/>
		/// </summary>
		/// <param name="Value">Value to check</param>
		/// <returns>True if contained, false otherwise</returns>
		public Boolean Contains(Type Value) {
			return Value.CompareTo(this.Lower) >= 0 && Value.CompareTo(this.Upper) <= 0;
		}

		/// <summary>
		/// Whether the range contains the specified <paramref name="Range"/> as a subrange
		/// </summary>
		/// <remarks>
		/// The entirety of the range must be contained.
		/// </remarks>
		/// <param name="Range">Range to check</param>
		/// <returns>True if contained, false otherwise</returns>
		public Boolean Contains(Range<Type> Range) {
			return Range.Lower.CompareTo(this.Lower) >= 0 && Range.Upper.CompareTo(this.Upper) <= 0;
		}

		public override String ToString() => this.Lower + ".." + this.Upper;

		public override Boolean Equals(Object obj) => (obj is Range<Type>) && this == (Range<Type>)obj;

		public override Int32 GetHashCode() => this.Lower.GetHashCode() ^ this.Upper.GetHashCode();

		public static Boolean operator ==(Range<Type> Left, Range<Type> Right) => Left.Lower.Equals(Right.Lower) && Left.Upper.Equals(Right.Upper);

		public static Boolean operator !=(Range<Type> Left, Range<Type> Right) => !Left.Lower.Equals(Right.Lower) || !Left.Upper.Equals(Right.Upper);

		public Range(Type Lower, Type Upper) {
			if (Lower.CompareTo(Upper) >= 0) throw new InvalidRangeException();
			this.Lower = Lower;
			this.Upper = Upper;
		}

	}
}
