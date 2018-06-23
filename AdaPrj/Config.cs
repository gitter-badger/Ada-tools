using System;
using System.Collections.Generic;
using System.Drawing;
using AdaTools;
using Console = Colorful.Console;

namespace AdaPrj {
	internal static class Config {

		internal static void FullHelp() {
			Console.WriteLine("(config|configure|configuration) — Interactively modify the configuration pragmas");
		}

		internal static void Help() {
			Console.WriteLine("  (config|configure|configuration) — Manage the configuration pragmas");
		}

		/// <summary>
		/// Enter the Config REPL
		/// </summary>
		internal static void Interactive() {
			ConfigurationUnit Configuration = new ConfigurationUnit();
			String Choice;
		ListConfig:
			new PragmaChoice("1", "Ada Version", Configuration.AdaVersion).WriteLine();
			new PragmaChoice("2", "Assertion Policy", Configuration.AssertionPolicy).WriteLine();
			new PragmaChoice("3", "Assume No Invalid Values", Configuration.AssumeNoInvalidValues).WriteLine();
			new PragmaChoice("4", "Elaboration Checks", Configuration.ElaborationChecks).WriteLine();
			new PragmaChoice("5", "Extensions Allowed", Configuration.ExtensionsAllowed).WriteLine();
			new PragmaChoice("6", "Fast Math", Configuration.FastMath).WriteLine();
			new PragmaChoice("7", "Source File Name", Configuration.SourceFileNames).WriteLine();
			new PragmaChoice("8", "Wide Character Encoding", Configuration.WideCharacterEncoding).WriteLine();
			// No, License was not forgotten. Rather, it is modified through another command.
			new Choice("Q", "Quit").Write();
			new Choice("S", "Save").Write();
			new Choice("SQ", "Save & Quit").WriteLine();
		EnterChoice:
			Console.Write(" Enter Choice: ", Color.LimeGreen);
			Choice = Console.ReadLine();
			switch (Choice.ToUpper()) {
			case "Q":
				return;
			case "S":
			case "SQ":
				Configuration.Save();
				Console.WriteLine(" Configuration Saved ");
				if (Choice == "SQ") goto case "Q";
				goto EnterChoice;
			case "1": // Ada Version
				new Choice("1", "Ada1983").Write();
				new Choice("2", "Ada1995").Write();
				new Choice("3", "Ada2005").Write();
				new Choice("4", "Ada2012").Write();
				new Choice("C", "Cancel").WriteLine();
			EnterAdaChoice:
				Console.Write(" Enter Choice: ", Color.LimeGreen);
				Choice = Console.ReadLine().ToUpper();
				switch (Choice) {
				case "C":
					goto ListConfig;
				case "1":
					Configuration.AdaVersion = AdaVersion.Ada1983;
					break;
				case "2":
					Configuration.AdaVersion = AdaVersion.Ada1995;
					break;
				case "3":
					Configuration.AdaVersion = AdaVersion.Ada2005;
					break;
				case "4":
					Configuration.AdaVersion = AdaVersion.Ada2012;
					break;
				default:
					goto EnterAdaChoice;
				}
				goto ListConfig;
			case "2": // Assertion Policy
				new Choice("1", "Check Globally").Write();
				new Choice("2", "Disable Globally").Write();
				new Choice("3", "Ignore Globally").Write();
				new Choice("4", "Specific Policies").Write();
				new Choice("C", "Cancel").WriteLine();
			EnterAssertionPolicyChoice:
				Console.Write(" Enter Choice: ", Color.LimeGreen);
				Choice = Console.ReadLine().ToUpper();
				switch (Choice) {
				case "C":
					goto ListConfig;
				case "1":
					Configuration.AssertionPolicy = new AssertionPolicy(PolicyIdentifier.Check);
					break;
				case "2":
					Configuration.AssertionPolicy = new AssertionPolicy(PolicyIdentifier.Disable);
					break;
				case "3":
					Configuration.AssertionPolicy = new AssertionPolicy(PolicyIdentifier.Ignore);
					break;
				case "4":
					Dictionary<String, PolicyIdentifier> Policies;
					if (Configuration.AssertionPolicy.Policies != null) {
						Policies = new Dictionary<String, PolicyIdentifier>(Configuration.AssertionPolicy.Policies);
					} else {
						Policies = new Dictionary<String, PolicyIdentifier>();
					}
				ListAssertionMarks:
					foreach (KeyValuePair<String, PolicyIdentifier> policy in Policies) {
						Console.Write(" " + policy.Key + " => ");
						switch (policy.Value) {
						case PolicyIdentifier.Check:
							Console.WriteLine("Check");
							break;
						case PolicyIdentifier.Disable:
							Console.WriteLine("Disable");
							break;
						case PolicyIdentifier.Ignore:
							Console.WriteLine("Ignore");
							break;
						case PolicyIdentifier.Suppressible:
							Console.WriteLine("Suppressible");
							break;
						}
					}
				EnterAssertionMarkChoice:
					new Choice("V", "View").Write();
					new Choice("D", "Done").WriteLine();
					Console.Write(" Enter Choice or Policy Name: ");
					Choice = Console.ReadLine().ToUpper();
					switch (Choice) {
					case "V":
						goto ListAssertionMarks;
					case "D":
						Configuration.AssertionPolicy = new AssertionPolicy(Policies);
						goto ListConfig;
					default:
						// The choice is actually a Policy Mark, so create a new entry
						String PolicyMark = Choice;
						new Choice("1", "Check").Write();
						new Choice("2", "Disable").Write();
						new Choice("3", "Ignore").Write();
						new Choice("4", "Suppressible").Write();
						new Choice("R", "Remove").Write();
						new Choice("C", "Cancel").WriteLine();
						Console.Write(" Enter Choice: ", Color.LimeGreen);
						Choice = Console.ReadLine().ToUpper();
						switch (Choice) {
						case "C":
							goto ListAssertionMarks;
						case "R":
							Policies.Remove(PolicyMark);
							goto EnterAssertionMarkChoice;
						case "1":
							if (Policies.ContainsKey(PolicyMark)) {
								Policies[PolicyMark] = PolicyIdentifier.Check;
							} else {
								Policies.Add(PolicyMark, PolicyIdentifier.Check);
							}
							goto EnterAssertionMarkChoice;
						case "2":
							if (Policies.ContainsKey(PolicyMark)) {
								Policies[PolicyMark] = PolicyIdentifier.Disable;
							} else {
								Policies.Add(PolicyMark, PolicyIdentifier.Disable);
							}
							goto EnterAssertionMarkChoice;
						case "3":
							if (Policies.ContainsKey(PolicyMark)) {
								Policies[PolicyMark] = PolicyIdentifier.Ignore;
							} else {
								Policies.Add(PolicyMark, PolicyIdentifier.Ignore);
							}
							goto EnterAssertionMarkChoice;
						case "4":
							if (Policies.ContainsKey(PolicyMark)) {
								Policies[PolicyMark] = PolicyIdentifier.Suppressible;
							} else {
								Policies.Add(PolicyMark, PolicyIdentifier.Suppressible);
							}
							goto EnterAssertionMarkChoice;
						}
						goto EnterAssertionMarkChoice;
					}
				default:
					goto EnterAssertionPolicyChoice;
				}
				goto ListConfig;
			case "3": // Assume No Invalid Values
				new Choice("1", "On").Write();
				new Choice("2", "Off").Write();
				new Choice("C", "Cancel").WriteLine();
			EnterAssumeNoInvalidValuesChoice:
				Console.Write(" Enter Choice: ", Color.LimeGreen);
				Choice = Console.ReadLine().ToUpper();
				switch (Choice) {
				case "C":
					goto ListConfig;
				case "1":
					Configuration.AssumeNoInvalidValues = AssumeNoInvalidValues.On;
					break;
				case "2":
					Configuration.AssumeNoInvalidValues = AssumeNoInvalidValues.Off;
					break;
				default:
					goto EnterAssumeNoInvalidValuesChoice;
				}
				goto ListConfig;
			case "4": // Elaboration Checks
				new Choice("1", "Dynamic").Write();
				new Choice("2", "Static").Write();
				new Choice("C", "Cancel").WriteLine();
			EnterElaborationChecksChoice:
				Console.Write(" Enter Choice: ", Color.LimeGreen);
				Choice = Console.ReadLine().ToUpper();
				switch (Choice) {
				case "C":
					goto ListConfig;
				case "1":
					Configuration.ElaborationChecks = ElaborationChecks.Dynamic;
					break;
				case "2":
					Configuration.ElaborationChecks = ElaborationChecks.Static;
					break;
				default:
					goto EnterElaborationChecksChoice;
				}
				goto ListConfig;
			case "5": // Extensions Allowed
				new Choice("1", "On").Write();
				new Choice("2", "Off").Write();
				new Choice("C", "Cancel").WriteLine();
			EnterExtensionsAllowedChoice:
				Console.Write(" Enter Choice: ", Color.LimeGreen);
				Choice = Console.ReadLine().ToUpper();
				switch (Choice) {
				case "C":
					goto ListConfig;
				case "1":
					Configuration.ExtensionsAllowed = ExtensionsAllowed.On;
					break;
				case "2":
					Configuration.ExtensionsAllowed = ExtensionsAllowed.Off;
					break;
				default:
					goto EnterExtensionsAllowedChoice;
				}
				goto ListConfig;
			case "6": //Fast Math
				new Choice("1", "Proper").Write();
				new Choice("2", "Fast").Write();
				new Choice("C", "Cancel").WriteLine();
			EnterFastMathChoice:
				Console.Write(" Enter Choice: ", Color.LimeGreen);
				Choice = Console.ReadLine().ToUpper();
				switch (Choice) {
				case "C":
					goto ListConfig;
				case "1":
					Configuration.FastMath = false;
					break;
				case "2":
					Configuration.FastMath = true;
					break;
				default:
					goto EnterFastMathChoice;
				}
				goto ListConfig;
			case "7": //Source File Name
			ListSourceFileConfig:
				new Choice("1", "Spec File Name").Write();
				new Choice("2", "Body File Name").Write();
				new Choice("3", "Subunit File Name").Write();
				new Choice("D", "Done").WriteLine();
			EnterSourceFileNameChoice:
				Console.Write(" Enter Choice: ", Color.LimeGreen);
				Choice = Console.ReadLine().ToUpper();
				switch (Choice) {
				case "D":
					goto ListConfig;
				case "1":
					if (Configuration.SourceFileNames.SpecFileName is null) Configuration.SourceFileNames.SpecFileName = new SpecFileName();
					ListSpecFileChoice:
					new Choice("1", "Spec File Name").Write();
					new Choice("2", "Casing").Write();
					new Choice("3", "Dot Replacement").Write();
					new Choice("D", "Done").WriteLine();
				EnterSpecFileChoice:
					Console.Write(" Enter Choice: ", Color.LimeGreen);
					Choice = Console.ReadLine().ToUpper();
					switch (Choice) {
					case "D":
						goto ListSourceFileConfig;
					case "1":
						Console.Write(" Spec File Name: ");
						Configuration.SourceFileNames.SpecFileName.UnitFileName = Console.ReadLine();
						goto ListSpecFileChoice;
					case "2":
						new Choice("1", "Lowercase").Write();
						new Choice("2", "Uppercase").Write();
						new Choice("3", "Mixedcase").Write();
						new Choice("C", "Cancel").WriteLine();
					EnterSpecFileCasing:
						Console.Write(" Enter Choice: ", Color.LimeGreen);
						Choice = Console.ReadLine().ToUpper();
						switch (Choice) {
						case "C":
							goto ListSpecFileChoice;
						case "1":
							Configuration.SourceFileNames.SpecFileName.Casing = Casing.Lowercase;
							break;
						case "2":
							Configuration.SourceFileNames.SpecFileName.Casing = Casing.Uppercase;
							break;
						case "3":
							Configuration.SourceFileNames.SpecFileName.Casing = Casing.Mixedcase;
							break;
						default:
							goto EnterSpecFileCasing;
						}
						goto ListSpecFileChoice;
					case "3":
						Console.Write(" Dot Replacement: ");
						Configuration.SourceFileNames.SpecFileName.DotReplacement = Console.ReadLine();
						goto ListSpecFileChoice;
					default:
						goto EnterSpecFileChoice;
					}
				case "2":
					if (Configuration.SourceFileNames.BodyFileName is null) Configuration.SourceFileNames.BodyFileName = new BodyFileName();
					ListBodyFileChoice:
					new Choice("1", "Body File Name").Write();
					new Choice("2", "Casing").Write();
					new Choice("3", "Dot Replacement").Write();
					new Choice("D", "Done").WriteLine();
				EnterBodyFileChoice:
					Console.Write(" Enter Choice: ", Color.LimeGreen);
					Choice = Console.ReadLine().ToUpper();
					switch (Choice) {
					case "D":
						goto ListSourceFileConfig;
					case "1":
						Console.Write(" Body File Name: ");
						Configuration.SourceFileNames.BodyFileName.UnitFileName = Console.ReadLine();
						goto ListBodyFileChoice;
					case "2":
						new Choice("1", "Lowercase").Write();
						new Choice("2", "Uppercase").Write();
						new Choice("3", "Mixedcase").Write();
						new Choice("C", "Cancel").WriteLine();
					EnterBodyFileCasing:
						Console.Write(" Enter Choice: ", Color.LimeGreen);
						Choice = Console.ReadLine().ToUpper();
						switch (Choice) {
						case "C":
							goto ListBodyFileChoice;
						case "1":
							Configuration.SourceFileNames.BodyFileName.Casing = Casing.Lowercase;
							break;
						case "2":
							Configuration.SourceFileNames.BodyFileName.Casing = Casing.Uppercase;
							break;
						case "3":
							Configuration.SourceFileNames.BodyFileName.Casing = Casing.Mixedcase;
							break;
						default:
							goto EnterBodyFileCasing;
						}
						goto ListBodyFileChoice;
					case "3":
						Console.Write(" Dot Replacement: ");
						Configuration.SourceFileNames.BodyFileName.DotReplacement = Console.ReadLine();
						goto ListBodyFileChoice;
					default:
						goto EnterBodyFileChoice;
					}
				case "3":
					if (Configuration.SourceFileNames.SubunitFileName is null) Configuration.SourceFileNames.SubunitFileName = new SubunitFileName();
					ListSubunitFileChoice:
					new Choice("1", "Subunit File Name").Write();
					new Choice("2", "Casing").Write();
					new Choice("3", "Dot Replacement").Write();
					new Choice("D", "Done").WriteLine();
				EnterSubunitFileChoice:
					Console.Write(" Enter Choice: ", Color.LimeGreen);
					Choice = Console.ReadLine().ToUpper();
					switch (Choice) {
					case "D":
						goto ListSourceFileConfig;
					case "1":
						Console.Write(" Subunit File Name: ");
						Configuration.SourceFileNames.SubunitFileName.UnitFileName = Console.ReadLine();
						goto ListSubunitFileChoice;
					case "2":
						new Choice("1", "Lowercase").Write();
						new Choice("2", "Uppercase").Write();
						new Choice("3", "Mixedcase").Write();
						new Choice("C", "Cancel").WriteLine();
					EnterSubunitFileCasing:
						Console.Write(" Enter Choice: ", Color.LimeGreen);
						Choice = Console.ReadLine().ToUpper();
						switch (Choice) {
						case "C":
							goto ListSubunitFileChoice;
						case "1":
							Configuration.SourceFileNames.SubunitFileName.Casing = Casing.Lowercase;
							break;
						case "2":
							Configuration.SourceFileNames.SubunitFileName.Casing = Casing.Uppercase;
							break;
						case "3":
							Configuration.SourceFileNames.SubunitFileName.Casing = Casing.Mixedcase;
							break;
						default:
							goto EnterSubunitFileCasing;
						}
						goto ListSubunitFileChoice;
					case "3":
						Console.Write(" Dot Replacement: ");
						Configuration.SourceFileNames.SubunitFileName.DotReplacement = Console.ReadLine();
						goto ListSubunitFileChoice;
					default:
						goto EnterSubunitFileChoice;
					}
				default:
					goto EnterSourceFileNameChoice;
				}
			case "8": //Wide Character Encoding
				new Choice("1", "Hex").Write();
				new Choice("2", "Upper").Write();
				new Choice("3", "Shift_JIS").Write();
				new Choice("4", "EUC").Write();
				new Choice("5", "UTF-8").Write();
				new Choice("6", "Brackets").Write();
				new Choice("C", "Cancel").WriteLine();
			EnterWideCharacterEncodingChoice:
				Console.Write(" Enter Choice: ", Color.LimeGreen);
				Choice = Console.ReadLine().ToUpper();
				switch (Choice) {
				case "C":
					goto ListConfig;
				case "1":
					Configuration.WideCharacterEncoding = WideCharacterEncoding.Hex;
					break;
				case "2":
					Configuration.WideCharacterEncoding = WideCharacterEncoding.Upper;
					break;
				case "3":
					Configuration.WideCharacterEncoding = WideCharacterEncoding.Shift_JIS;
					break;
				case "4":
					Configuration.WideCharacterEncoding = WideCharacterEncoding.EUC;
					break;
				case "5":
					Configuration.WideCharacterEncoding = WideCharacterEncoding.UTF8;
					break;
				case "6":
					Configuration.WideCharacterEncoding = WideCharacterEncoding.Brackets;
					break;
				default:
					goto EnterWideCharacterEncodingChoice;
				}
				goto ListConfig;
			default:
				goto EnterChoice;
			}
		}

		internal static ConfigFlags ParseConfigFlags(List<String> Args) {
			ConfigFlags Result = ConfigFlags.Config;
			foreach (String Arg in Args) {
				switch (Arg.ToUpper()) {
				case "--HELP":
					return ConfigFlags.Help;
				default:
					break;
				}
			}
			return Result;
		}

	}
}
