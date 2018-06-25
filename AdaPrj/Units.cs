using System;
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

		internal static void Run(UnitsOptions opts, Span<String> args) {
			if (opts.Table) {
				Units.Table();
			} else {
				Units.Each();
			}
		}

	}
}
