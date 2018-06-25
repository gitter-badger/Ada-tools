using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;
using AdaTools;
using System.Security;
using CommandLine;
using CommandLine.Text;

namespace AdaPrj {
	public static class Program {
		static void Main(String[] args) {
			try {
				Parser.Default.ParseArguments<BuildOptions, CleanOptions, ConfigOptions, TypesOptions, UnitsOptions>(args)
					.WithParsed<BuildOptions>(opts => Build.Run(opts, args))
					.WithParsed<CleanOptions>(opts => Clean.Run(opts, args))
					.WithParsed<ConfigOptions>(opts => Config.Run(opts, args))
					.WithParsed<TypesOptions>(opts => Types.Run(opts, args))
					.WithParsed<UnitsOptions>(opts => Units.Run(opts, args));
			} catch (MissingGNATProgramException Exception) {
				Console.Write("Missing necessary program: ", Color.Red);
				Console.WriteLine(Exception.Message);
				Console.Write("See: ");
				Console.WriteLine("https://en.wikibooks.org/wiki/Ada_Programming/Installing");
			} catch (SecurityException) {
				Console.Write("Need administrative permissions", Color.Red);
			}
		}

		static void Help() {
			Console.WriteAscii("AdaPrj", Color.Green);
			Console.WriteLine("\tCopyright 2018 - Entomy");
			Console.WriteLine();
			Console.WriteLine(AppDomain.CurrentDomain.FriendlyName);
		}

	}
}
