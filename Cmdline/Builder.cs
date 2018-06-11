using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace Cmdline {
	public static class Builder {

		/// <summary>
		/// Attempt a simple build, in which no target specific stuff needs to be done
		/// </summary>
		/// <remarks>
		/// <para>This is a simple build in which there are no target specific folders to also consider, rather, everything must be flatly organized.</para>
		/// </remarks>
		internal static void Simple() {
			foreach (Unit Unit in new BuildPlan(new Project())) {
				switch (Unit) {
				case PackageUnit PackageUnit:
					Compiler.Compile(PackageUnit);
					break;
				case SubroutineUnit SubroutineUnit:
					Compiler.Compile(SubroutineUnit);
					break;
				case ProgramUnit ProgramUnit:
					Compiler.Compile(ProgramUnit);
					break;
				default:
					break;
				}
			}
		}

		/// <summary>
		/// Print the build flags for the units, instead of actually building them
		/// </summary>
		internal static void Flags() {
			foreach (Unit Unit in new Project().Units) {
				Console.WriteLine(Unit.Name + ": " + Unit.LinkerArguments);
			}
		}

		internal static void FullHelp() {
			Console.WriteLine("\t" + "(build|compile|make) — Build all sources within this directory");
		}

		internal static void Help() {
			Console.WriteLine("\t" + "(build|compile|make) — Build all sources within this directory");
		}

		/// <summary>
		/// Print the build plan for the units, instead of actually building them
		/// </summary>
		internal static void Plan() {
			foreach (Unit Unit in new BuildPlan(new Project())) {
				Console.WriteLine(Unit.Name);
			}
		}

	}
}
