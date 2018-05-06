using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an abstract real numeral type
	/// </summary>
	public abstract class RealType : ScalarType {
		protected RealType(String Name) : base(Name) {

		}
	}
}
