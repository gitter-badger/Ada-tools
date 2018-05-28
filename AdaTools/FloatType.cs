using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a floating-point numeric type
	/// </summary>
	public sealed class FloatType : RealType {

		public UInt16? Digits { get; private set; }

		public Range<Double>? Range { get; private set; }

		public Boolean? Contains(Single Value) => this.Range?.Contains(Value);

		public Boolean? Contains(Double Value) => this.Range?.Contains(Value);

		public Boolean? Contains(Range<Single> Range) => this.Range?.Contains(new Range<Double>(Range.Lower, Range.Upper));

		public Boolean? Contains(Range<Double> Range) => this.Range?.Contains(Range);

		public override void Join(Type Type) {
			if (!(Type is FloatType)) throw new TypeMismatchException();
			base.Join(Type);
			if (this.Digits is null) this.Digits = (Type as FloatType).Digits;
			if (this.Range is null) this.Range = (Type as FloatType).Range;
		}

		public override String ToString() {
			if (this.Range is null) {
				return "type " + this.Name + " is digits " + this.Digits + ";";
			} else {
				return "type " + this.Name + " is digits " + this.Digits + " range " + this.Range + ";";
			}
		}

		public override Boolean Equals(Object obj) {
			if (!(obj is FloatType)) return false;
			return this.Digits == (obj as FloatType).Digits
				&& this.Range == (obj as FloatType).Range;
		}

		public override Int32 GetHashCode() => base.GetHashCode();

		public FloatType(String Name, UInt16 Digits) : base(Name) {
			this.Digits = Digits;
		}

		public FloatType(String Name, UInt16 Digits, Double Lower, Double Upper) : this(Name, Digits, new Range<Double>(Lower, Upper)) {

		}

		public FloatType(String Name, UInt16 Digits, Range<Double> Range) : this(Name, Digits) {
			this.Range = Range;
		}
	}
}
