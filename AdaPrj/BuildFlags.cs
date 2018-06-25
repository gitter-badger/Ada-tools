using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPrj {
	[Flags]
	internal enum BuildFlags {
		Build = 0,
		Flags = 1,
		Help = 2,
		Plan = 4,
	}
}
