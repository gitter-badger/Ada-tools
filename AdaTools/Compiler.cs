using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Interface wrapping for the GNAT compiler command line arguments
	/// </summary>
	public static class Compiler {

		/// <summary>
		/// Clean the entire current directory
		/// </summary>
		/// <remarks>
		/// This formerly called  gnatclean to do the work. As explained with Issue #3 <see cref="https://github.com/Entomy/Ada-tools/issues/3"/> the tool experiences some extremely serious bugs. So like quite a few other things, we're doing this ourselves.
		/// </remarks>
		public static void Clean() {
			// Only look through the current directory
			// This avoids the problem gnatclean had where it deleted its own libgnat
			foreach (String FileName in Directory.EnumerateFiles(Environment.CurrentDirectory)) {
				switch (Path.GetExtension(FileName).ToUpper()) {
				// Only delete files with specific extensions
				// This avoids the problem gnatclean had where it deleted project source files
				case ".ALI":
				case ".APKG":
				case ".DLL":
				case ".EXE":
				case ".O":
				case ".SO":
					File.Delete(FileName);
					Console.WriteLine("\"." + Path.DirectorySeparatorChar + Path.GetFileName(FileName) + "\" has been deleted");
					break;
				default:
					continue;
				}
			}
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
					GnatMake = Process.Start("gnatmake", "-shared -fPIC -mwindows " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
					break;
				case WindowsSubsystem.Console:
				default:
					GnatMake = Process.Start("gnatmake", "-shared -fPIC -mconsole " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
					break;
				}
			} else {
				GnatMake = Process.Start("gnatmake", "-shared -fPIC " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments);
			}
			GnatMake.WaitForExit(); // We need to wait here, because otherwise units will be compiled before their dependencies are finished compiling. That's bad.
			GnatMake.Dispose();
			Process CreateLibrary;
			// Linking is a very different procedure on different systems, so figure out what we're supposed to do
			if (Environment.OSVersion.Platform <= (PlatformID)3) {
				CreateLibrary = Process.Start("gcc", "-shared " + Unit.Name + ".o " + Unit.LinkerArguments + " -o " + Unit.Name + ".dll -Wl,--export-all-symbols");
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
		/// <remarks>
		/// This actually calls the linker as well, so that a shared library is built rather than just object code
		/// </remarks>
		/// <param name="Unit">Unit to compile</param>
		/// <param name="Architecture">-march flag setting</param>
		/// <param name="WindowsSubsystem">Windows Subsystem to build for</param>
		public static void Compile(SubroutineUnit Unit, Architecture Architecture = Architecture.Generic, WindowsSubsystem WindowsSubsystem = WindowsSubsystem.Console) {
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

		/// <summary>
		/// Call gnatkrunch to "krunch" the <paramref name="Name"/> to <paramref name="Length"/>
		/// </summary>
		/// <param name="Name">Name to krunch</param>
		/// <param name="Length">Krunched length, defaults to 8</param>
		/// <returns>The krunched name</returns>
		/// <see cref="http://docs.adacore.com/live/wave/gnat_ugn/html/gnat_ugn/gnat_ugn/the_gnat_compilation_model.html#file-name-krunching-with-gnatkr"/>
		public static String Krunch(String Name, Int32 Length = 8) {
			Process GnatKrunch = new Process();
			GnatKrunch.StartInfo.FileName = "gnatkr";
			GnatKrunch.StartInfo.Arguments = Name + " " + Length;
			GnatKrunch.StartInfo.RedirectStandardOutput = true;
			GnatKrunch.Start();
			GnatKrunch.WaitForExit();
			String Result = GnatKrunch.StandardOutput.ReadToEnd().Trim();
			GnatKrunch.Dispose();
			return Result;
		}

	}
}
