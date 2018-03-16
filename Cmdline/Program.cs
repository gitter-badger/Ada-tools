using System;
using System.Collections.Generic;
using System.Linq;
using AdaTools;

namespace Cmdline {
	public static class Program {

		static void Main(String[] args) {
			/// <summary>
			/// Presents the arguments as a queue, which is useful for the parsing strategy taken here
			/// </summary>
			/// <remarks>
			/// <para>Arguments are read in left-to-right order, with operations and modifiers being removed. In some cases, a modifier is possible, but turns out to be part of a list, in which case it is popped back on. Once operations and modifiers are done, the remaining arguments, if any, are a list for that operation to operate upon.</para>
			/// <para>The args array has to be reversed before being converted, because otherwise it would be the "wrong" way</para>
			/// </remarks>
			Stack<String> Arguments = new Stack<String>(args.Reverse());
			// Attempt to read the operation
			if (!Arguments.TryPop(out String Operation)) {
				throw new MissingOperationException();
			}
			// Do whatever the operation is
			switch (Operation.ToLower()) {
				case "help":
					Program.Help();
					break;
				case "dep":
				case "deps":
				case "depend":
				case "depends":
				case "dependency":
				case "dependencies":
					Dependencies.Parse(Arguments);
					break;
				case "file":
				case "files":
					Files.Parse(Arguments);
					break;
				case "list":
					List.Parse(Arguments);
					break;
				default:
					throw new UnknownOperationException("Found: " + Operation);
			}
		}

		static void Help() {
			Console.WriteLine("adatool");
			Dependencies.Help();
			Files.Help();
			List.Help();

		}
	}
}
