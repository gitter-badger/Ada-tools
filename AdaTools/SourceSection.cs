using System;

namespace AdaTools {
	[Flags]
	public enum SourceSection {
		Imports = 1,
		Generic = 2,
		Public = 4,
		Private = 8,
	}
}
