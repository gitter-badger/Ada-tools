using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPkg {
	[Flags]
	internal enum InstallFlags {
		Install = 1,
		Global = 2,
		Help = 4,
		List = 8,
	}
}
