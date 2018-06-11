using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents the source type, since Ada has multiple representable units
	/// </summary>
	public enum SourceType {
		/// <summary>
		/// Signifies an Ada package, a collection of reusable components
		/// </summary>
		Package,
		/// <summary>
		/// Signifies a reusable Ada subroutine
		/// </summary>
		Subroutine,
		/// <summary>
		/// Signifies an Ada program, which is not reusable but rather built into an executable
		/// </summary>
		Program,
	}
}
