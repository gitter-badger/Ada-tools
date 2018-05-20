using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents the license class of the source
	/// </summary>
	public enum License {
		/// <summary>
		/// Unrestricted license class, which doesn't need any checking
		/// </summary>
		Unrestricted,
		/// <summary>
		/// GPL license class, which infects other units to be under the same license
		/// </summary>
		GPL,
		/// <summary>
		/// Modified GPL license class, which is similar to the GPL but doesn't infect other units
		/// </summary>
		Modified_GPL,
		/// <summary>
		/// Restricted license class, which can't be linked with GPL units
		/// </summary>
		Restricted,
	}
}
