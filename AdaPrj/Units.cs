﻿using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace AdaPrj {
	internal static class Units {

		internal static void Each() {
			foreach (Unit U in new Project().Units) {
				Console.Write(U.Name + "  ");
			}
			Console.WriteLine();
		}

		internal static void Help() {
			Console.WriteLine("  (unit|units) — Lists the Ada units within the current directory");
		}

		internal static void FullHelp() {
			Console.WriteLine("(unit|units) — Lists the Ada units within the current directory");
			Console.WriteLine("  --table — Lists the Ada units within the current directory as a detailed table");
		}

		internal static void Table() {
			Console.WriteLine(String.Format("{0,4}  {1,4}  {2,6}  {3,4}", "Kind", "Pure", "Remote", "Name"));
			Console.WriteLine(String.Format("{0,4}  {1,4}  {2,6}  {3,4}", "----", "----", "------", "----"));
			foreach (Unit Unit in new Project().Units) {
				String Kind = "";
				String Pure = "";
				String Remote = "";
				switch (Unit) {
					case PackageUnit Package:
						if (Package.HasBody && Package.HasSpec) {
							Kind = "SpBd";
						} else if (Package.HasSpec) {
							Kind = "Sp  ";
						} else if (Package.HasBody) {
							Kind = "  Bd";
						}
						if (Package.IsPure) {
							Pure = "Pure";
						}
						if (Package.IsRemoteCallInterface) {
							Remote = "Intrfc";
						} else if (Package.IsAllCallsRemote) {
							Remote = "Calls";
						}
						Console.WriteLine(String.Format("{0,4}  {1,4}  {2,6}  {3,4}", Kind, Pure, Remote, Package.Name));
						break;
					case ProgramUnit Program:
						switch (Program.Type) {
							case ProgramType.Function:
								Kind = " Fn ";
								break;
							case ProgramType.Procedure:
								Kind = " Pr ";
								break;
						}
						Console.WriteLine(String.Format("{0,4}  {1,4}  {2,6}  {3,4}", Kind, Pure, Remote, Program.Name));
						break;
				}
			}
		}

		internal static UnitsFlags ParseUnitsFlags(List<String> Args) {
			UnitsFlags Result = UnitsFlags.Units;
			List<String> ToBeRemoved = new List<String>();
			foreach (String Arg in Args) {
				switch (Arg.ToUpper()) {
				case "--HELP":
					Result |= UnitsFlags.Units;
					break;
				case "--TABLE":
					Result |= UnitsFlags.Table;
					break;
				default:
					break;
				}
				if (Arg.StartsWith("--")) {
					ToBeRemoved.Add(Arg);
				}
			}
			foreach (String Arg in ToBeRemoved) {
				Args.Remove(Arg);
			}
			return Result;
		}

	}
}
