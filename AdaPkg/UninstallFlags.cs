using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPkg {
	[Flags]
	internal enum UninstallFlags {
		Uninstall = 1,
		Global = 2,
		Help = 4,
	}
}
