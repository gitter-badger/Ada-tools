using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Interface wrapping for the GNAT compiler command line arguments
	/// </summary>
	public static class Compiler {

		public static void Clean(Unit Unit) {
			Process GnatClean = Process.Start("gnatclean", String.Join(' ', Unit.GetFiles()));
			GnatClean.WaitForExit(); // We wait only because concurrent processes do terrible things to the command line text. This could be fixed by buffering the output then oneshot printing it.
			GnatClean.Dispose();
		}

		public static void Compile(Unit Unit) {
			Process GnatMake = Process.Start("gnatmake", String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
			GnatMake.WaitForExit(); // We need to wait here, because otherwise units will be compiled before their dependencies are finished compiling. That's bad.
			GnatMake.Dispose();
			if (Environment.OSVersion.Platform <= (PlatformID)3) {
				Process CreateLibrary = Process.Start("gnatdll", "-d " + Unit.Name + ".dll " + Unit.Name + ".ali");
				CreateLibrary.WaitForExit();
				CreateLibrary.Dispose();
			} else if (Environment.OSVersion.Platform == PlatformID.Unix) {
				Process CreateLibrary = Process.Start("ld", "-shared " + Unit.Name + ".o " + Unit.LinkerArguments + " -o " + Unit.Name + ".so");
				CreateLibrary.WaitForExit();
				CreateLibrary.Dispose();
			}
		}
	}
}
