using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an integer type
	/// </summary>
	/// <remarks>
	/// <para>Ada refers to any whole number, whether it follows integer or modular semantics as an integer, and as such, is an abstract type here. "Signed" is the type that actually follows integer semantics, even though it's possible to have unsigned integer semantics.</para>
	/// </remarks>
	public abstract class IntegerType : DiscreteType {
		protected IntegerType(String Name) : base(Name) {

		}
	}
}
