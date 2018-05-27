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

		/// <summary>
		/// Call the compiler with appropriate arguments for compiling the <paramref name="Unit"/>
		/// </summary>
		/// <remarks>
		/// This actually calls the linker as well, so that a shared library is built rather than just object code
		/// </remarks>
		/// <param name="Unit">Unit to compile</param>
		/// <param name="march">-march flag setting</param>
		/// <param name="WindowsSubsystem">Windows Subsystem to build for</param>
		public static void Compile(PackageUnit Unit, Architecture Architecture = Architecture.Generic, WindowsSubsystem WindowsSubsystem = WindowsSubsystem.Console) {
			String arch;
			switch (Architecture) {
				case Architecture.Generic:
					arch = "-mtune=generic";
					break;
				case Architecture.Native:
				default:
					arch = "-march=native";
					break;
			}
			Process GnatMake;
			if (Environment.OSVersion.Platform <= (PlatformID)3) {
				switch (WindowsSubsystem) {
					case WindowsSubsystem.Windows:
						GnatMake = Process.Start("gnatmake", "-mwindows " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
						break;
					case WindowsSubsystem.Console:
					default:
						GnatMake = Process.Start("gnatmake", "-mconsole " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
						break;
				}
			} else {
				GnatMake = Process.Start("gnatmake", arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
			}
			GnatMake.WaitForExit(); // We need to wait here, because otherwise units will be compiled before their dependencies are finished compiling. That's bad.
			GnatMake.Dispose();
			Process CreateLibrary;
			// Linking is a very different procedure on different systems, so figure out what we're supposed to do
			if (Environment.OSVersion.Platform <= (PlatformID)3) {
				CreateLibrary = Process.Start("gnatdll", "-d " + Unit.Name + ".dll " + Unit.Name + ".ali");
			} else if (Environment.OSVersion.Platform == PlatformID.Unix) {
				CreateLibrary = Process.Start("ld", "-shared " + Unit.Name + ".o " + Unit.LinkerArguments + " -o " + Unit.Name + ".so");
			} else {
				throw new Exception("Unable to determine what the Operating System is");
			}
			CreateLibrary.WaitForExit(); //? We might not need to wait here
			CreateLibrary.Dispose();
		}

		/// <summary>
		/// Call the compiler with appropriate arguments for compiling the <paramref name="Unit"/>
		/// </summary>
		/// <param name="Unit">Program Unit to compile</param>
		/// <param name="march">-march flag setting</param>
		/// <param name="WindowsSubsystem">Windows Subsystem to build for</param>
		public static void Compile(ProgramUnit Unit, Architecture Architecture = Architecture.Generic, WindowsSubsystem WindowsSubsystem = WindowsSubsystem.Console) {
			String arch;
			switch (Architecture) {
				case Architecture.Generic:
					arch = "-mtune=generic";
					break;
				case Architecture.Native:
				default:
					arch = "-march=native";
					break;
			}
			Process GnatMake;
			if (Environment.OSVersion.Platform <= (PlatformID)3) {
				switch (WindowsSubsystem) {
					case WindowsSubsystem.Windows:
						GnatMake = Process.Start("gnatmake", "-mwindows " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
						break;
					case WindowsSubsystem.Console:
					default:
						GnatMake = Process.Start("gnatmake", "-mconsole " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
						break;
				}
			} else {
				GnatMake = Process.Start("gnatmake", arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
			}
			GnatMake.WaitForExit();
			GnatMake.Dispose();
		}
	}
}
