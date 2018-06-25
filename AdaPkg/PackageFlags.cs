using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPkg {
	[Flags]
	internal enum PackageFlags {
		Package = 0,
		Help = 1,
		Info = 2,
		Validate = 4,
	}
}
