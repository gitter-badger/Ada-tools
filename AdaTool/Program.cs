using System;
using System.Collections.Generic;
using System.Linq;
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
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Missing necessary program: ");
				Console.ResetColor();
				Console.WriteLine(Exception.Message);
				Console.Write("See: ");
				Console.WriteLine("https://en.wikibooks.org/wiki/Ada_Programming/Installing");
			}
		}

		public static void Help() {
			Console.WriteLine("AdaTools");
			Console.WriteLine("Copyright 2018 - Entomy");
			Console.WriteLine();
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
			Settings.Help();
		}
	}
}
