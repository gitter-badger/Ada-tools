using System;
using System.Collections.Generic;
using System.Text;

namespace Cmdline {
	public static class Build {

		/// <summary>
		/// Attempt a simple build, in which no target specific stuff needs to be done
		/// </summary>
		/// <remarks>
		/// <para>This is a simple build in which there are no target specific folders to also consider, rather, everything must be flatly organized.</para>
		/// </remarks>
		internal static void Simple() {

		}

		internal static void FullHelp() {
			Console.WriteLine("\t" + "(build|compile|make) — Build all sources within this directory");
		}

		internal static void Help() {
			Console.WriteLine("\t" + "(build|compile|make) — Build all sources within this directory");
		}

	}
}
