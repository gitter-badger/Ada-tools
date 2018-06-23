using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;
using AdaTools;

namespace AdaTool {
	public static class Program {
		public static void Main(String[] args) {
			try {
				String Verb = args[0].ToUpper();
				List<String> Args = new List<String>(args.Skip(1).Take(args.Length - 1));

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
			} catch (MissingGNATProgramException Exception) {
				Console.Write("Missing necessary program: ", Color.Red);
				Console.WriteLine(Exception.Message);
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
