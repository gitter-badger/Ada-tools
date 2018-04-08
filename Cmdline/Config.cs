using System;
using System.Collections.Generic;
using AdaTools;

namespace Cmdline {
	internal static class Config {
		
		internal static void FullHelp() {
			Console.WriteLine("\t" + "config,configure,configuration — Interactively modify the configuration pragmas");
		}

		internal static void Help() {
			Console.WriteLine("\t" + "config,configure,configuration — Manage the configuration pragmas");
		}

		private static void WriteChoices(params Choice[] Choices) {
			foreach (Choice Choice in Choices) {
				Console.Write(Choice);
			}
			Console.WriteLine();
		}

		private static void WriteChoices(params PragmaChoice[] Choices) {
			foreach (PragmaChoice Choice in Choices) {
				Console.WriteLine(" [{0,1}] {1,25} := {2} ", Choice.Code, Choice.Pragma, Choice.Value);
			}
		}

		/// <summary>
		/// Enter the Config REPL
		/// </summary>
		internal static void Interactive() {
			ConfigurationUnit Configuration = new ConfigurationUnit();
			String Choice;
			ListConfig:
			Config.WriteChoices(
				new PragmaChoice("1", "Ada Version", Configuration.AdaVersion),
				new PragmaChoice("2", "Assertion Policy", Configuration.AssertionPolicy),
				new PragmaChoice("3", "Assume No Invalid Values", Configuration.AssumeNoInvalidValues),
				new PragmaChoice("4", "Elaboration Checks", Configuration.ElaborationChecks),
				new PragmaChoice("5", "Extensions Allowed", Configuration.ExtensionsAllowed),
				new PragmaChoice("6", "Fast Math", Configuration.FastMath));
			// No, License was not forgotten. Rather, it is modified through another command.
			Config.WriteChoices(
				new Choice("Q", "Quit"),
				new Choice("S", "Save"));
			EnterChoice:
			Console.Write(" Enter Choice: ");
			Choice = Console.ReadLine().ToUpper();
			switch (Choice.ToUpper()) {
				case "Q":
					return;
				case "S":
					Configuration.Save();
					Console.WriteLine(" Configuration Saved ");
					goto EnterChoice;
				case "1": // Ada Version
					Config.WriteChoices(
						new Choice("1", "Ada1983"),
						new Choice("2", "Ada1995"),
						new Choice("3", "Ada2005"),
						new Choice("4", "Ada2012"),
						new Choice("C", "Cancel"));
					EnterAdaChoice:
					Console.Write(" Enter Choice: ");
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
					Config.WriteChoices(
						new Choice("1", "Check Globally"),
						new Choice("2", "Disable Globally"),
						new Choice("3", "Ignore Globally"),
						new Choice("4", "Specific Policies"),
						new Choice("C", "Cancel"));
					EnterAssertionPolicyChoice:
					Console.Write(" Enter Choice: ");
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
							Dictionary<String, PolicyIdentifier> Policies = new Dictionary<String, PolicyIdentifier>(Configuration.AssertionPolicy.Policies);
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
							Config.WriteChoices(
								new Choice("V", "View"),
								new Choice("D", "Done"));
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
									Config.WriteChoices(
										new Choice("1", "Check"),
										new Choice("2", "Disable"),
										new Choice("3", "Ignore"),
										new Choice("4", "Suppressible"),
										new Choice("R", "Remove"),
										new Choice("C", "Cancel"));
									Console.Write(" Enter Choice: ");
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
					Config.WriteChoices(
						new Choice("1", "On"),
						new Choice("2", "Off"),
						new Choice("C", "Cancel"));
					EnterAssumeNoInvalidValuesChoice:
					Console.Write(" Enter Choice: ");
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
					Config.WriteChoices(
						new Choice("1", "Dynamic"),
						new Choice("2", "Static"),
						new Choice("C", "Cancel"));
					EnterElaborationChecksChoice:
					Console.Write(" Enter Choice: ");
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
					Config.WriteChoices(
						new Choice("1", "On"),
						new Choice("2", "Off"),
						new Choice("C", "Cancel"));
					EnterExtensionsAllowedChoice:
					Console.Write(" Enter Choice: ");
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
					Config.WriteChoices(
						new Choice("1", "Proper"),
						new Choice("2", "Fast"),
						new Choice("C", "Cancel"));
					EnterFastMathChoice:
					Console.Write(" Enter Choice: ");
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
				default:
					goto EnterChoice;
			}
		}

	}
}
