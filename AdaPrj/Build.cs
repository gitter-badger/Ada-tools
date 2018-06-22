using System;
using System.Collections.Generic;
using AdaTools;

namespace AdaPrj {
	internal static class Build {

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

		/// <summary>
		/// Print the build plan for the units, instead of actually building them
		/// </summary>
		internal static void Plan() {
			foreach (Unit Unit in new BuildPlan(new Project())) {
				Console.WriteLine(Unit.Name);
			}
		}

		internal static void PlanWithFlags() {
			foreach (Unit Unit in new BuildPlan(new Project())) {
				Console.WriteLine(Unit.Name + ": " + Unit.LinkerArguments);
			}
		}

		internal static void FullHelp() {
			Console.WriteLine("build — Build all sources within this directory");
			Console.WriteLine("  --flags — Print the build flags instead of building");
			Console.WriteLine("  --plan — Print the build plan instead of building");
		}

		internal static void Help() {
			Console.WriteLine("  (build|compile|make) — Build all sources within this directory");
		}

		internal static BuildFlags ParseBuildFlags(List<String> Args) {
			BuildFlags Result = BuildFlags.Build;
			foreach (String Arg in Args) {
				switch (Arg.ToUpper()) {
				case "--FLAGS":
					Result |= BuildFlags.Flags;
					break;
				case "--HELP":
					return BuildFlags.Help;
				case "--PLAN":
					Result |= BuildFlags.Plan;
					break;
				default:
					break;
				}
				Args.Remove(Arg);
			}
			return Result;
		}

	}
}
