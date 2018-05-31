using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an array index of a signed integer type
	/// </summary>
	public sealed class ModularIndex : Index {

		private readonly ModularType Type;

		private readonly Range<UInt64>? Range;

		public override String ToString() {
			if (this.Range is null) {
				return this.Type.Name;
			} else {
				return this.Type.Name + " range " + this.Range.ToString();
			}
		}

		public ModularIndex(ModularType Type) {
			this.Type = Type;
		}

		public ModularIndex(ModularType Type, UInt64 Lower, UInt64 Upper) : this(Type, new Range<UInt64>(Lower, Upper)) {

		}

		public ModularIndex(ModularType Type, Range<UInt64> Range) : this(Type) {
			this.Range = Range;
		}

	}
}
