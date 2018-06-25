using AdaTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;
using CommandLine;
using CommandLine.Text;
using System.Security;

namespace AdaPkg {
	public static class Program {
		static void Main(String[] args) {
			try {
				Parser.Default.ParseArguments<InstallOptions, PackageOptions, UninstallOptions>(args)
					.WithParsed<InstallOptions>(opts => Install.Run(opts, args))
					.WithParsed<PackageOptions>(opts => Package.Run(opts, args))
					.WithParsed<UninstallOptions>(opts => Uninstall.Run(opts, args));
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
