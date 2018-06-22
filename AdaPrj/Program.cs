using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;

namespace AdaPrj {
	public static class Program {
		static void Main(String[] args) {
			String Verb = args[0].ToUpper();
			List<String> Args = new List<String>(args.Skip(1).Take(args.Length - 1));

			switch (Verb) {
			case "BUILD":
			case "COMPILE":
			case "MAKE":
				switch (Build.ParseBuildFlags(Args)) {
				case BuildFlags.Flags | BuildFlags.Plan:
					Build.PlanWithFlags();
					return;
				case BuildFlags.Flags:
					Build.Flags();
					return;
				case BuildFlags.Help:
					Build.FullHelp();
					return;
				case BuildFlags.Plan:
					Build.Plan();
					return;
				case BuildFlags.Build:
				default:
					Build.Simple();
					return;
				}
			case "CLEAN":
				switch (Clean.ParseCleanFlags(Args)) {
				case CleanFlags.Help:
					Clean.FullHelp();
					return;
				case CleanFlags.Clean:
				default:
					Clean.All();
					return;

				}
			case "CONFIG":
				switch (Config.ParseConfigFlags(Args)) {
				case ConfigFlags.Help:
					Config.FullHelp();
					return;
				case ConfigFlags.Config:
				default:
					Config.Interactive();
					return;
				}
			case "TYPE":
			case "TYPES":
				switch (Types.ParseTypesFlags(Args)) {
				case TypesFlags.Help:
					Types.FullHelp();
					return;
				case TypesFlags.Info:
					Types.Info(Args);
					return;
				case TypesFlags.Table:
					Types.Table();
					return;
				case TypesFlags.Types:
				default:
					Types.Each();
					return;
				}
			case "UNIT":
			case "UNITS":
				switch (Units.ParseUnitsFlags(Args)) {
				default:
					Units.Each();
					return;
				}
			case "HELP":
			case "--HELP":
			default:
				// Print the help menu so people know what they are doing
				Program.Help();
				return;
			}
		}

		static void Help() {
			Console.WriteAscii("AdaPrj", Color.Green);
			Console.WriteLine("\tCopyright 2018 - Entomy");
			Console.WriteLine();
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
			Build.Help();
			Clean.Help();
			Types.Help();
			Units.Help();
		}

	}
}
