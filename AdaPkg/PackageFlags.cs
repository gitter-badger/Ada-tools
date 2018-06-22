using System;
using System.Collections.Generic;
using System.Text;

namespace AdaPkg {
	[Flags]
	internal enum PackageFlags {
		Package = 1,
		Help = 2,
		Info = 4,
		Validate = 8,
	}
}
