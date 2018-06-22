using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;

namespace AdaTool {
	public static class Program {
		public static void Main(String[] args) {
			String Verb = args[0].ToUpper();
			List<String> Args = new List<string>(args.Skip(1).Take(args.Length - 1));

			switch (Verb) {
			case "SETTING":
			case "SETTINGS":
				Settings.Interactive();
				break;
			case "HELP":
			case "--HELP":
			default:
				Program.Help();
				break;
			}
		}

		public static void Help() {
			Console.WriteAscii("AdaTools", Color.Green);
			Console.WriteLine("\tCopyright 2018 - Entomy");
			Console.WriteLine();
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
			Settings.Help();
		}
	}
}
