using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a base-10 fixed-point numeric type
	/// </summary>
	public sealed class DecimalType : FixedType {

		public Decimal? Delta { get; private set; }

		public UInt16? Digits { get; private set; }

		public Range<Decimal>? Range { get; private set; }

		public Boolean? Contains(Decimal Value) => this.Range?.Contains(Value);

		public Boolean? Contains(Range<Decimal> Range) => this.Range?.Contains(Range);

		public override void Join(Type Type) {
			if (!(Type is DecimalType)) throw new TypeMismatchException();
			base.Join(Type);
			if (this.Delta is null) this.Delta = (Type as DecimalType).Delta;
			if (this.Digits is null) this.Digits = (Type as DecimalType).Digits;
			if (this.Range is null) this.Range = (Type as DecimalType).Range;
		}

		public DecimalType(String Name, Decimal Delta, UInt16 Digits) : base(Name) {
			this.Delta = Delta;
			this.Digits = Digits;
		}

		public DecimalType(String Name, Decimal Delta, UInt16 Digits, Range<Decimal> Range) : this(Name, Delta, Digits) {
			this.Range = Range;
		}
	}
}
