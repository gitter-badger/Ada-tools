using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a fixed-point numeric type
	/// </summary>
	public abstract class FixedType : RealType {
		protected FixedType(String Name) : base(Name) {
		}
	}
}
