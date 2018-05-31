using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an array index of a signed integer type
	/// </summary>
	public sealed class SignedIndex : Index {

		private readonly SignedType Type;

		private readonly Range<Int64>? Range;

		public override String ToString() {
			if (this.Range is null) {
				return this.Type.Name;
			} else {
				return this.Type.Name + " range " + this.Range.ToString();
			}
		}

		public SignedIndex(SignedType Type) {
			this.Type = Type;
		}

		public SignedIndex(SignedType Type, Int64 Lower, Int64 Upper) : this(Type, new Range<Int64>(Lower, Upper)) {

		}

		public SignedIndex(SignedType Type, Range<Int64> Range) : this(Type) {
			this.Range = Range;
		}

	}
}
