using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace Cmdline {
	public static class Types {

		public static void Each() {
			Console.WriteLine("Types.Each()");
			foreach (AdaTools.Type T in new Project().Types) {
				Console.WriteLine(T.Name);
			}
		}

		public static void FullHelp() {

		}

		public static void Help() {

		}

	}
}
