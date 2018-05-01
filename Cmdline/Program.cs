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
				// If they didn't specify something, they likely didn't know the operations, so print out the help.
				Program.Help();
				return;
			}
			// Do whatever the operation is
			switch (Operation.ToLower()) {
				case "help":
					Program.Help();
					break;
				case "build":
				case "compile":
				case "make":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToLower()) {
							case "flags":
								Build.Flags();
								return;
							case "help":
								Build.FullHelp();
								return;
							case "plan":
								Build.Plan();
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						Build.Simple();
						return;
					}
				case "config":
				case "configure":
				case "configuration":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToLower()) {
							case "help":
								Config.FullHelp();
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						Config.Interactive();
						return;
					}
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
								return;
							case "help":
								Dependencies.FullHelp();
								return;
							default:
								Arguments.Push(Mode);
								throw new NotImplementedException();
						}
					} else {
						Dependencies.Each();
						return;
					}
				case "file":
				case "files":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToLower()) {
							case "help":
								Files.FullHelp();
								return;
							default:
								Arguments.Push(Mode);
								Files.Each(Arguments);
								return;
						}
					} else {
						Files.Each();
						return;
					}
				case "list":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToLower()) {
							case "help":
								List.FullHelp();
								return;
							case "table":
								List.Table();
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						List.Each();
						return;
					}
				default:
					Console.Error.WriteLine("The operation '" + Operation + "' isn't a known operation");
					Program.Help();
					return;
			}
		}

		static void Help() {
			Console.WriteLine("adatool");
			Build.Help();
			Config.Help();
			Dependencies.Help();
			Files.Help();
			List.Help();

		}
	}
}
