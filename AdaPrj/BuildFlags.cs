using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPrj {
	[Flags]
	internal enum BuildFlags {
		Build = 1,
		Flags = 2,
		Help = 4,
		Plan = 8,
	}
}
