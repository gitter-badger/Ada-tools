using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPkg {
	[Flags]
	internal enum UninstallFlags {
		Uninstall = 0,
		Global = 1,
		Help = 2,
	}
}
