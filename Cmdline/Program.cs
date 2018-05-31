using System;
using System.Collections.Generic;
using System.Linq;
using AdaTools;

namespace Cmdline {
	public static class Program {

		static void Main(String[] args) {
			/// <summary>
			/// Presents the arguments as a stack, which is useful for the parsing strategy taken here
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
			switch (Operation.ToUpper()) {
				case "HELP":
					Program.Help();
					break;
				case "BUILD":
				case "COMPILE":
				case "MAKE":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToUpper()) {
							case "FLAGS":
								Builder.Flags();
								return;
							case "HELP":
								Builder.FullHelp();
								return;
							case "PLAN":
								Builder.Plan();
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						Builder.Simple();
						return;
					}
				case "CLEAN":
					Cleaner.Clean();
					return;
				case "CONFIG":
				case "CONFIGURE":
				case "CONFIGURATION":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToUpper()) {
							case "HELP":
								Config.FullHelp();
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						Config.Interactive();
						return;
					}
				case "DEP":
				case "DEPS":
				case "DEPEND":
				case "DEPENDS":
				case "DEPENDENCY":
				case "DEPENDENCIES":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToUpper()) {
							case "ALL":
							case "PROJECT":
								Dependencies.Project();
								return;
							case "HELP":
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
				case "FILE":
				case "FILES":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToUpper()) {
							case "HELP":
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
				case "LIST":
				case "UNITS":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToUpper()) {
							case "HELP":
								List.FullHelp();
								return;
							case "TABLE":
								List.Table();
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						List.Each();
						return;
					}
				case "PKG":
				case "PACKAGE":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToUpper()) {
							case "HELP":
								Packager.FullHelp();
								return;
							case "INFO":
								Packager.Info(Arguments);
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						Packager.Each();
						return;
					}
				case "SETTING":
				case "SETTINGS":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToUpper()) {
							case "OBJ":
							case "OBJS":
							case "OBJECT":
							case "OBJECTS":
								if (Arguments.Count >= 1) {
									Settings.ObjectSearchPath = new List<String>(Arguments);
								} else {
									Console.WriteLine(String.Join(' ', Settings.ObjectSearchPath));
								}
								return;
							case "PKG":
							case "PACKAGE":
								if (Arguments.TryPop(out Mode)) {
									switch (Mode.ToUpper()) {
										case "DB":
										case "DATABASE":
											break;
										default:
											throw new UnknownOperationException();
									}
									goto case "PKGDB";
								} else {
									throw new UnknownOperationException();
								}
							case "PKGDB":
							case "PACKAGEDB":
							case "PACKAGEDATABASE":
								if (Arguments.Count >= 1) {
									Settings.PackageDatabasePath = String.Join(' ', Arguments);
								} else {
									Console.WriteLine(Settings.PackageDatabasePath);
								}
								return;
							case "REPO":
							case "REPOSITORY":
								if (Arguments.Count >= 1) {
									Settings.PackageRepositoryPath = String.Join(' ', Arguments);
								} else {
									Console.WriteLine(Settings.PackageRepositoryPath);
								}
								return;
							case "SOURCE":
							case "SOURCES":
								if (Arguments.Count >= 1) {
									Settings.SourceSearchPath = new List<String>(Arguments);
								} else {
									Console.WriteLine(String.Join(' ', Settings.SourceSearchPath));
								}
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						Settings.Print();
						return;
					}
				case "TYPE":
				case "TYPES":
					if (Arguments.TryPop(out Mode)) {
						switch (Mode.ToUpper()) {
							case "HELP":
								Types.FullHelp();
								return;
							case "INFO":
								if (Arguments.TryPop(out Mode)) {
									Types.Info(Mode);
									return;
								} else {
									throw new MissingOperationException();
								}
							case "TABLE":
								Types.Table();
								return;
							default:
								throw new NotImplementedException();
						}
					} else {
						Types.Each();
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
			Builder.Help();
			Config.Help();
			Dependencies.Help();
			Files.Help();
			List.Help();
			Packager.Help();
			Types.Help();
		}
	}
}
