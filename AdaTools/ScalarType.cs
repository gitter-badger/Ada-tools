using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a scalar type
	/// </summary>
	public abstract class ScalarType : Type {
		protected ScalarType(String Name) : base(Name) {

		}
	}
}
