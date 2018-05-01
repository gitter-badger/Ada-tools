using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Interface wrapping for the GNAT compiler command line arguments
	/// </summary>
	public static class Compiler {
		public static void Compile() {
			Process.Start("gnatmake");
		}
	}
}
