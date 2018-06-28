using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a separate part of another unit
	/// </summary>
	/// <remarks>
	/// <para>As only subroutines are seperatable, this inherits from SubroutineUnit and modifies the necessary parts</para>
	/// </remarks>
	public sealed class SeparateUnit : SubroutineUnit {

		public SeparateUnit(String Name) : base(Name) {

		}

	}
}
