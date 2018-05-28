using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a signed integer type
	/// </summary>
	public sealed class SignedType : IntegerType {

		public Range<Int64>? Range { get; private set; }

		public Boolean? Contains(Int16 Value) => this.Range?.Contains(Value);

		public Boolean? Contains(Int32 Value) => this.Range?.Contains(Value);

		public Boolean? Contains(Int64 Value) => this.Range?.Contains(Value);

		public Boolean? Contains(Range<Int16> Range) => this.Range?.Contains(new Range<Int64>(Range.Lower, Range.Upper));

		public Boolean? Contains(Range<Int32> Range) => this.Range?.Contains(new Range<Int64>(Range.Lower, Range.Upper));

		public Boolean? Contains(Range<Int64> Range) => this.Range?.Contains(Range);

		public override void Join(Type Type) {
			if (!(Type is SignedType)) throw new TypeMismatchException();
			base.Join(Type);
			if (this.Range is null) this.Range = (Type as SignedType).Range;
		}

		public override String ToString() => "type " + this.Name + " is range " + this.Range + ";";

		public override Boolean Equals(Object obj) {
			if (!(obj is SignedType)) return false;
			return this.Range == (obj as SignedType).Range;
		}

		public override Int32 GetHashCode() => base.GetHashCode();

		public SignedType(String Name) : base(Name) {

		}

		public SignedType(String Name, Int16 Lower, Int16 Upper) : this(Name, new Range<Int16>(Lower, Upper)) {

		}

		public SignedType(String Name, Range<Int16> Range) : base(Name) {
			this.Range = new Range<Int64>(Range.Lower, Range.Upper);
		}

		public SignedType(String Name, Int32 Lower, Int32 Upper) : this(Name, new Range<Int32>(Lower, Upper)) {

		}

		public SignedType(String Name, Range<Int32> Range) : base(Name) {
			this.Range = new Range<Int64>(Range.Lower, Range.Upper);
		}

		public SignedType(String Name, Int64 Lower, Int64 Upper) : this(Name, new Range<Int64>(Lower, Upper)) {

		}

		public SignedType(String Name, Range<Int64> Range) : base(Name) {
			this.Range = Range;
		}
	}
}
