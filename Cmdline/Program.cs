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
			String Operation;
			String Mode;
			// Attempt to read the operation
			if (!Arguments.TryPop(out Operation)) {
				throw new MissingOperationException();
			}
			// Do whatever the operation is
			switch (Operation.ToLower()) {
				case "help":
					Program.Help();
					break;
				case "config":
				case "configure":
				case "configuration":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToLower()) {
							default:
								Arguments.Push(Mode);
								break;
						}
					} else {
						Config.Interactive();
					}
					break;
				case "dep":
				case "deps":
				case "depend":
				case "depends":
				case "dependency":
				case "dependencies":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToLower()) {
							case "all":
							case "project":
								Dependencies.Project();
								break;
							case "help":
								Dependencies.FullHelp();
								break;
							default:
								Arguments.Push(Mode);
								Dependencies.Each(Arguments);
								break;
						}
					} else {
						Dependencies.Each();
					}
					break;
				case "file":
				case "files":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToLower()) {
							case "help":
								Files.FullHelp();
								break;
							default:
								Arguments.Push(Mode);
								Files.Each(Arguments);
								break;
						}
					} else {
						Files.Each();
					}
					break;
				case "list":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToLower()) {
							case "help":
								List.FullHelp();
								break;
							case "table":
								List.Table();
								break;
							default:
								Arguments.Push(Mode);
								// TODO: Write a list method for specified packages
								break;
						}
					} else {
						List.Each();
					}
					break;
				default:
					throw new UnknownOperationException("Found: " + Operation);
			}
		}

		static void Help() {
			Console.WriteLine("adatool");
			Config.Help();
			Dependencies.Help();
			Files.Help();
			List.Help();

		}
	}
}
