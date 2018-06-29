using System;
using System.Collections.Generic;
using System.ComponentModel;
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
		/// <param name="Architecture">Target architecture</param>
		/// <param name="WindowsSubsystem">Windows Subsystem to build for</param>
		public static void Compile(Unit Unit, Architecture Architecture = Architecture.Generic, WindowsSubsystem WindowsSubsystem = WindowsSubsystem.Console) {
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
			try {
				Process GnatMake = new Process();
				ProcessStartInfo StartInfo = new ProcessStartInfo();
				StartInfo.FileName = "gnatmake";
				StartInfo.EnvironmentVariables["ADA_INCLUDE_PATH"] = "";
				StartInfo.EnvironmentVariables["ADA_OBJECTS_PATH"] = "";
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					switch (WindowsSubsystem) {
					case WindowsSubsystem.Windows:
						StartInfo.Arguments = "-shared -shared-libgcc -fPIC -mwindows " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments;
						break;
					case WindowsSubsystem.Console:
					default:
						StartInfo.Arguments = "-shared -shared-libgcc -fPIC -mconsole " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments;
						break;
					}
					break;
				case PlatformID.Unix:
					StartInfo.Arguments = "-shared -fPIC " + arch + " " + String.Join(' ', Unit.GetFiles()) + Unit.LinkerArguments;
					break;
				default:
					throw new PlatformNotSupportedException("Unable to determine what the Operating System is");
				}
				GnatMake.StartInfo = StartInfo;
				GnatMake.Start();
				GnatMake.WaitForExit(); // We need to wait here, because otherwise units will be compiled before their dependencies are finished compiling. That's bad.
				GnatMake.Dispose();
			} catch (Win32Exception) {
				throw new MissingGNATProgramException("gnatmake");
			}
			if (Unit is PackageUnit || Unit is SubroutineUnit) {
				Process CreateLibrary;
				// Linking is a very different procedure on different systems, so figure out what we're supposed to do
				switch (Environment.OSVersion.Platform) {
				case (PlatformID)1:
				case (PlatformID)2:
				case (PlatformID)3:
					CreateLibrary = Process.Start("gcc", "-shared " + Unit.Name + ".o " + Unit.LinkerArguments + " -o " + Unit.Name + ".dll -Wl,--export-all-symbols");
					break;
				case PlatformID.Unix:
					CreateLibrary = Process.Start("ld", "-shared " + Unit.Name + ".o " + Unit.LinkerArguments + " -o " + Unit.Name + ".so");
					break;
				default:
					throw new PlatformNotSupportedException("Unable to determine what the Operating System is");
				}
				CreateLibrary.WaitForExit(); //? We might not need to wait here
				CreateLibrary.Dispose();
			}
		}

		public static String GNATLibrary {
			get {
				if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-2018.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-2018.so")) {
					return "gnat-2018";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-2017.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-2017.so")) {
					return "gnat-2017";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-2016.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-2016.so")) {
					return "gnat-2016";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-8.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-8.so")) {
					return "gnat-8";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-7.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-7.so")) {
					return "gnat-7";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-6.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnat-6.so")) {
					return "gnat-6";
				}
				return "gnat";
			}
		}

		public static String GNARLLibrary {
			get {
				if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-2018.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-2018.so")) {
					return "gnarl-2018";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-2017.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-2017.so")) {
					return "gnarl-2017";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-2016.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-2016.so")) {
					return "gnarl-2016";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-8.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-8.so")) {
					return "gnarl-8";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-7.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-7.so")) {
					return "gnarl-7";
				} else if (File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-6.dll") || File.Exists(Settings.GNATObjectsPath + Path.DirectorySeparatorChar + "libgnarl-6.so")) {
					return "gnarl-6";
				}
				return "gnarl";
			}
		}

		/// <summary>
		/// Call gnatkrunch to "krunch" the <paramref name="Name"/> to <paramref name="Length"/>
		/// </summary>
		/// <param name="Name">Name to krunch</param>
		/// <param name="Length">Krunched length, defaults to 8</param>
		/// <returns>The krunched name</returns>
		/// <see cref="http://docs.adacore.com/live/wave/gnat_ugn/html/gnat_ugn/gnat_ugn/the_gnat_compilation_model.html#file-name-krunching-with-gnatkr"/>
		public static String Krunch(String Name, Int32 Length = 8) {
			try {
				Process GnatKrunch = new Process();
				GnatKrunch.StartInfo.FileName = "gnatkr";
				GnatKrunch.StartInfo.Arguments = Name + " " + Length;
				GnatKrunch.StartInfo.RedirectStandardOutput = true;
				GnatKrunch.Start();
				GnatKrunch.WaitForExit();
				String Result = GnatKrunch.StandardOutput.ReadToEnd().Trim();
				GnatKrunch.Dispose();
				return Result;
			} catch (Win32Exception) {
				throw new MissingGNATProgramException("gnatkr");
			}
		}

	}
}
