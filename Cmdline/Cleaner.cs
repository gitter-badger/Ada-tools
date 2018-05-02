using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace Cmdline {
	internal static class Cleaner {
		internal static void Clean() {
			foreach (Unit Unit in new Project().Units) {
				Compiler.Clean(Unit);
			}
		}
	}
}
