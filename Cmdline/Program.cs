﻿using System;
using System.Linq;
using AdaTools;

namespace Cmdline {
	public static class Program {
		static void Main(String[] args) {
			/// <summary>The operation that decides how this tool runs</summary>
			String Operation;
			// Attempt to read the operation
			if (args.Length >= 1) {
				Operation = args[0].ToLower();
			} else {
				Error();
				return;
			}
			/// <summary>The current project being analyzed</summary>
			Project Project;
			/// <summary>The current package being analyzed</summary>
			Package Package;
			// Do whatever the operation is
			switch (Operation) {
				case "help":
					Help();
					break;
				case "dep":
				case "deps":
				case "depend":
				case "depends":
				case "dependency":
				case "dependencies":
					if (args.Length == 1) {
						Project = new Project();
						foreach (String Dep in Project.Dependencies) {
							Console.Write(Dep + ' ');
						}
					} else {
						foreach (String Name in args.Skip(1).Take(args.Length - 1)) {
							Package = new Package(Name);
							Console.Write(Package.Name + ": ");
							foreach (String Dep in Package.Dependencies) {
								Console.Write(Dep + ' ');
							}
							Console.WriteLine();
						}
					}
					Console.WriteLine();
					break;
				case "file":
				case "files":
					foreach (String Name in args.Skip(1).Take(args.Length - 1)) {
						Package = new Package(Name);
						Console.Write(String.Join(' ', Package.GetFiles()) + ' ');
					}
					Console.WriteLine();
					break;
				case "list":
					if (args.Length >= 2 && args[1].ToLower() == "table") {
						Project = new Project();
						Console.WriteLine(String.Format("{0,4}  {1,4}", "Kind", "Name"));
						Console.WriteLine(String.Format("{0,4}  {1,4}", "----", "----"));
						foreach (Unit U in Project.Units) {
							String KindFormat = "";
							if (U is AdaTools.Package) {
								if ((U as Package).HasBody && (U as Package).HasSpec) {
									KindFormat = "SpBd";
								} else if ((U as Package).HasSpec) {
									KindFormat = "Sp  ";
								} else if ((U as Package).HasBody) {
									KindFormat = "  Bd";
								}
								Console.WriteLine(String.Format("{0,4}  {1,4}", KindFormat, U.Name));
							} else if (U is AdaTools.Program) {
								switch ((U as AdaTools.Program).Type) {
									case ProgramType.Function:
										KindFormat = " Fn ";
										break;
									case ProgramType.Procedure:
										KindFormat = " Pr ";
										break;
								}
								Console.WriteLine(String.Format("{0,4}  {1,4}", KindFormat, U.Name));
							}
						}
					} else {
						foreach (Unit U in new Project().Units) {
							Console.Write(U.Name + ' ');
						}
					}
					Console.WriteLine();
					break;
				default:
					Error();
					return;
			}
		}

		static void Error() {
			Console.WriteLine("You have used this program incorrectly, correct usage is as follows");
			Help();
		}

		static void Help() {
			Console.WriteLine("adatool");
			Console.WriteLine("\t" + "(deps|depends|dependencies) «Packages»+ — Lists the dependent packages for each specified package");
			Console.WriteLine("\t" + "files «Packages»+ — Lists the associated files for the specified package");
			Console.WriteLine("\t" + "list [table] [«Directory»] — Lists the Ada units within the directory, defaults to the current directory");

		}
	}
}
