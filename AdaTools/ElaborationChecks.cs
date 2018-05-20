using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents the configured Elaboration Checks
	/// </summary>
	public enum ElaborationChecks {
		/// <summary>
		/// Dynamic Elaboration Model, which is the default in the ARM.
		/// </summary>
		Dynamic,
		/// <summary>
		/// Static Elaboration Model, which is the default for GNAT.
		/// </summary>
		Static,
	}
}
