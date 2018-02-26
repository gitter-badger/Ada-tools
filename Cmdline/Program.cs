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
			/// <summary>The current package being analyzed</summary>
			Package Package;
			// Do whatever the operation is
			switch (Operation) {
				case "help":
					Help();
					break;
				case "files":
					foreach (String Name in args.Skip(1).Take(args.Length-1)) {
						Package = new Package(Name);
						Console.Write(String.Join(' ', Package.GetFiles()) + ' ');
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
			Console.WriteLine("\t" + "adatool files «Packages»+ — Lists the associated files for the specified package");
		}
	}
}
