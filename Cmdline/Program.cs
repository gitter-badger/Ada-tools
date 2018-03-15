using System;
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
			/// <summary>
			/// The current project being analyzed
			/// </summary>
			/// <remarks>
			/// This is Lazy so that we can "initialize" it once, but only actually initialize it if we actually need it. Quite a few operations don't need it, so it's a waste of resources to initialize an entire project (a quite expensive operation) only to never use it, but it's also incredibly tedious to initialize it ever time it is needed. This introduces slightly awkward syntax but overcomes both issues.
			/// </remarks>
			Lazy<Project> Project = new Lazy<Project>(new Project());
			/// <summary>The current package being analyzed</summary>
			PackageUnit Package;
			// Do whatever the operation is
			switch (Operation) {
				case "help":
					Program.Help();
					break;
				case "dep":
				case "deps":
				case "depend":
				case "depends":
				case "dependency":
				case "dependencies":
					if (args.Length == 1) {
						foreach (Unit U in Project.Value.Units) {
							Console.Write(U.Name + ": ");
							foreach (String Dep in U.Dependencies) {
								Console.Write(Dep + ' ');
							}
							Console.WriteLine();
						}
					} else {
						if (args.Length == 2 && (args[1].ToLower() == "all" || args[1].ToLower() == "project")) {
							foreach (String Dep in Project.Value.Dependencies) {
								Console.Write(Dep + ' ');
							}
						} else {
							foreach (String Name in args.Skip(1).Take(args.Length - 1)) {
								Package = new PackageUnit(Name);
								Console.Write(Package.Name + ": ");
								foreach (String Dep in Package.Dependencies) {
									Console.Write(Dep + ' ');
								}
								Console.WriteLine();
							}
						}
					}
					Console.WriteLine();
					break;
				case "file":
				case "files":
					foreach (String Name in args.Skip(1).Take(args.Length - 1)) {
						Package = new PackageUnit(Name);
						Console.Write(String.Join(' ', Package.GetFiles()) + ' ');
					}
					Console.WriteLine();
					break;
				case "list":
					if (args.Length >= 2 && args[1].ToLower() == "table") {
						Console.WriteLine(String.Format("{0,4}  {1,4}  {2,6}  {3,4}", "Kind", "Pure", "Remote", "Name"));
						Console.WriteLine(String.Format("{0,4}  {1,4}  {2,6}  {3,4}", "----", "----", "------", "----"));
						foreach (Unit U in Project.Value.Units) {
							String KindFormat = "";
							String Purity = "";
							String Remote = "";
							if (U is PackageUnit) {
								if (U.HasBody && U.HasSpec) {
									KindFormat = "SpBd";
								} else if (U.HasSpec) {
									KindFormat = "Sp  ";
								} else if (U.HasBody) {
									KindFormat = "  Bd";
								}
								if (U.IsPure) {
									Purity = "Pure";
								}
								if (U.IsRemoteCallInterface) {
									Remote = "Intrfc";
								} else if (U.IsAllCallsRemote) {
									Remote = "Calls";
								}
								Console.WriteLine(String.Format("{0,4}  {1,4}  {2,6}  {3,4}", KindFormat, Purity, Remote, U.Name));
							} else if (U is ProgramUnit) {
								switch ((U as ProgramUnit).Type) {
									case ProgramType.Function:
										KindFormat = " Fn ";
										break;
									case ProgramType.Procedure:
										KindFormat = " Pr ";
										break;
								}
								Console.WriteLine(String.Format("{0,4}  {1,4}  {2,6}  {3,4}", KindFormat, Purity, Remote, U.Name));
							}
						}
					} else {
						foreach (Unit U in Project.Value.Units) {
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
