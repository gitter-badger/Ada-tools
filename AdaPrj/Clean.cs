using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace AdaPrj {
	internal static class Clean {
		internal static void All() {
			Compiler.Clean();
		}

		internal static void Run(CleanOptions otps, Span<String> args) {
			Clean.All();
		}

	}
}
