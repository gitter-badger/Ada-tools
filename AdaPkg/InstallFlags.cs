using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPkg {
	[Flags]
	internal enum InstallFlags {
		Install = 0,
		Global = 1,
		Help = 2,
		List = 4,
	}
}
