using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a base-2 fixed-point numeric type
	/// </summary>
	public sealed class OrdinaryType : FixedType {

		public Double? Delta { get; private set; }

		public Range<Double>? Range { get; private set; }

		public Boolean? Contains(Single Value) => this.Range?.Contains(Value);

		public Boolean? Contains(Double Value) => this.Range?.Contains(Value);

		public Boolean? Contains(Range<Single> Range) => this.Range?.Contains(new Range<Double>(Range.Lower, Range.Upper));

		public Boolean? Contains(Range<Double> Range) => this.Range?.Contains(Range);

		public override void Join(Type Type) {
			if (!(Type is OrdinaryType)) throw new TypeMismatchException();
			base.Join(Type);
			if (this.Delta is null) this.Delta = (Type as OrdinaryType).Delta;
			if (this.Range is null) this.Range = (Type as OrdinaryType).Range;
		}

		public override String ToString() {
			if (this.Range is null) {
				return "type " + this.Name + " is delta " + this.Delta + ";";
			} else {
				return "type " + this.Name + " is delta " + this.Delta + " range " + this.Range + ";";
			}
		}

		public override Boolean Equals(Object obj) {
			if (!(obj is OrdinaryType)) return false;
			return this.Delta == (obj as OrdinaryType).Delta
				&& this.Range == (obj as OrdinaryType).Range;
		}

		public override Int32 GetHashCode() => this.Delta.GetHashCode() ^ this.Range.GetHashCode();

		public OrdinaryType(String Name, Double Delta) : base(Name) {
			this.Delta = Delta;
		}

		public OrdinaryType(String Name, Double Delta, Double Lower, Double Upper) : this(Name, Delta, new Range<Double>(Lower, Upper)) {

		}

		public OrdinaryType(String Name, Double Delta, Range<Double> Range) : this(Name, Delta) {
			this.Range = Range;

		}
	}
}
