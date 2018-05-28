using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a modular integer type
	/// </summary>
	public sealed class ModularType : IntegerType {

		public UInt64? Modulus { get; private set; }

		public Boolean? Contains(UInt16 Value) => (this.Modulus is null) ? null : (Boolean?)(Value >= 0 && Value <= this.Modulus - 1);

		public Boolean? Contains(UInt32 Value) => (this.Modulus is null) ? null : (Boolean?)(Value >= 0 && Value <= this.Modulus - 1);

		public Boolean? Contains(UInt64 Value) => (this.Modulus is null) ? null : (Boolean?)(Value >= 0 && Value <= this.Modulus - 1);

		public override String ToString() => "type " + this.Name + " is mod " + this.Modulus + ";";

		public override Boolean Equals(Object obj) {
			if (!(obj is ModularType)) return false;
			return this.Modulus == (obj as ModularType).Modulus;
		}

		public override Int32 GetHashCode() => base.GetHashCode();

		public ModularType(String Name) : base(Name) {

		}

		public ModularType(String Name, UInt16 Modulus) : base(Name) {
			this.Modulus = Modulus;
		}

		public ModularType(String Name, UInt32 Modulus) : base(Name) {
			this.Modulus = Modulus;
		}

		public ModularType(String Name, UInt64 Modulus) : base(Name) {
			this.Modulus = Modulus;
		}
	}
}
