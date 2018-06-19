using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace Cmdline {
	internal static class Settings {

		internal static void FullHelp() {
			Console.WriteLine("\t" + "settings — Interactively modify the Ada-tools settings");
		}

		internal static void Help() {
			Console.WriteLine("\t" + "settings — Manage the Ada-tools settings");
		}

		private static void WriteChoices(params Choice[] Choices) {
			foreach (Choice Choice in Choices) {
				Console.Write(Choice);
			}
			Console.WriteLine();
		}

		private static void WriteChoices(params SettingChoice[] Choices) {
			foreach (SettingChoice Choice in Choices) {
				Console.WriteLine(" [{0,1}] {1,25} := {2} ", Choice.Code, Choice.Setting, Choice.Value);
			}
		}

		/// <summary>
		/// Enter the Settings REPL
		/// </summary>
		internal static void Interactive() {
			String Choice;
			ListSettings:
			Settings.WriteChoices(
				new SettingChoice("1", "Build Packages On Install", BuildPackagesOnInstall),
				new SettingChoice("2", "Source Search Path", String.Join("  ", SourceSearchPath)),
				new SettingChoice("3", "Object Search Path", String.Join("  ", ObjectSearchPath)),
				new SettingChoice("4", "Package Database Path", PackageDatabasePath),
				new SettingChoice("5", "Package Repository Path", PackageRepositoryPath));
			Settings.WriteChoices(
				new Choice("Q", "Quit"));
			EnterChoice:
			Console.Write(" Enter Choice: ");
			Choice = Console.ReadLine().ToUpper();
			switch (Choice.ToUpper()) {
			case "Q":
				return;
			case "1": // Build Packages On Install
				Settings.WriteChoices(
					new Choice("1", "Yes"),
					new Choice("2", "No"),
					new Choice("C", "Cancel"));
				EnterBuildOnInstallChoice:
				Console.Write(" Enter Choice: ");
				Choice = Console.ReadLine();
				switch (Choice.ToUpper()) {
				case "C":
					goto ListSettings;
				case "1": // Yes
					Settings.BuildPackagesOnInstall = true;
					break;
				case "2": // No
					Settings.BuildPackagesOnInstall = false;
					break;
				default:
					goto EnterBuildOnInstallChoice;
				}
				goto ListSettings;
			case "2": // Source Search Path
				Settings.WriteChoices(
					new Choice("N", "New Paths"),
					new Choice("C", "Cancel"));
				EnterSourcePathChoice:
				Console.Write(" Enter Choice: ");
				Choice = Console.ReadLine();
				switch (Choice.ToUpper()) {
				case "N": // New Paths
					List<String> Paths = new List<String>();
					Console.WriteLine(" Enter each path on a new line");
					Console.WriteLine(" The first path is used for installations");
					Console.WriteLine(" Enter \"done\" on a line to finish");
					String Path;
					while (true) {
						Console.Write(" > ");
						Path = Console.ReadLine();
						if (Path.ToUpper() == "DONE") break;
						Paths.Add(Path);
					}
					SourceSearchPath = Paths;
					break;
				case "C":
					break;
				default:
					goto EnterSourcePathChoice;
				}
				goto ListSettings;
			case "3": // Object Search Path
				Settings.WriteChoices(
					new Choice("N", "New Paths"),
					new Choice("C", "Cancel"));
				EnterObjectPathChoice:
				Console.Write(" Enter Choice: ");
				Choice = Console.ReadLine();
				switch (Choice.ToUpper()) {
				case "N": // New Paths
					List<String> Paths = new List<String>();
					Console.WriteLine(" Enter each path on a new line");
					Console.WriteLine(" The first path is used for installations");
					Console.WriteLine(" Enter \"done\" on a line to finish");
					String Path;
					while (true) {
						Console.Write(" > ");
						Path = Console.ReadLine();
						if (Path.ToUpper() == "DONE") break;
						Paths.Add(Path);
					}
					ObjectSearchPath = Paths;
					break;
				case "C":
					break;
				default:
					goto EnterObjectPathChoice;
				}
				goto ListSettings;
			case "4": // Package Database Path
				Settings.WriteChoices(
					new Choice("N", "New Path"),
					new Choice("C", "Cancel"));
				EnterPkgDBPath:
				Console.Write(" Enter Choice: ");
				Choice = Console.ReadLine();
				switch (Choice.ToUpper()) {
				case "N":
					Console.Write(" Package Database Path: ");
					PackageDatabasePath = Console.ReadLine();
					break;
				case "C":
					break;
				default:
					goto EnterPkgDBPath;
				}
				goto ListSettings;
			case "5": // Package Repository Path
				Settings.WriteChoices(
					new Choice("N", "New Path"),
					new Choice("C", "Cancel"));
				EnterPkgRepoPath:
				Console.Write(" Enter Choice: ");
				Choice = Console.ReadLine();
				switch (Choice.ToUpper()) {
				case "N":
					Console.Write(" Package Repository Path: ");
					PackageRepositoryPath = Console.ReadLine();
					break;
				case "C":
					break;
				default:
					goto EnterPkgRepoPath;
				}
				goto ListSettings;
			default:
				goto EnterChoice;
			}
		}

		#region Wrappings
		// This whole region just wraps the AdaTools.Settings settings so that the syntax in the command line interface is cleaner

		public static Boolean BuildPackagesOnInstall {
			get => AdaTools.Settings.BuildPackagesOnInstall;
			set => AdaTools.Settings.BuildPackagesOnInstall = value;
		}

		public static List<String> SourceSearchPath {
			get => AdaTools.Settings.SourceSearchPath;
			set => AdaTools.Settings.SourceSearchPath = value;
		}

		public static List<String> ObjectSearchPath {
			get => AdaTools.Settings.ObjectSearchPath;
			set => AdaTools.Settings.ObjectSearchPath = value;
		}

		public static String PackageDatabasePath {
			get => AdaTools.Settings.PackageDatabasePath;
			set => AdaTools.Settings.PackageDatabasePath = value;
		}

		public static String PackageRepositoryPath {
			get => AdaTools.Settings.PackageRepositoryPath;
			set => AdaTools.Settings.PackageRepositoryPath = value;
		}

		#endregion

	}
}
