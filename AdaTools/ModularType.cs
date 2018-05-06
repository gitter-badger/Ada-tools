using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a modular integer type
	/// </summary>
	public sealed class ModularType : IntegerType {

		public readonly UInt64 Modulus;

		public ModularType(String Name, UInt16 Modulus): base(Name) {
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
