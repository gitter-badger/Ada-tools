using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;
using AdaTools;
using CommandLine;
using CommandLine.Text;
using System.Security;

namespace AdaTool {
	public static class Program {
		public static void Main(String[] args) {
			try {
				Parser.Default.ParseArguments<SettingsOptions>(args)
					.WithParsed(opts => Settings.Run(opts));
			} catch (MissingGNATProgramException Exception) {
				Console.Write("Missing necessary program: ", Color.Red);
				Console.WriteLine(Exception.Message);
				Console.Write("See: ");
				Console.WriteLine("https://en.wikibooks.org/wiki/Ada_Programming/Installing");
			} catch (SecurityException) {
				Console.Write("Need administrative permissions", Color.Red);
			}
		}

	}
}
