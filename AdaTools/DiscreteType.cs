using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a discrete type
	/// </summary>
	public abstract class DiscreteType : ScalarType {
		protected DiscreteType(String Name) : base(Name) {

		}
	}
}
