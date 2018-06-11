using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdaTools {
	/// <summary>
	/// Represents Ada source code
	/// </summary>
	/// <remarks>
	/// The source code is broken up into sections which can be referenced with <see cref="SourceSection"/>
	/// </remarks>
	public sealed class Source {

		private readonly String Imports = "";

		private readonly String Generic = "";

		private readonly String Public = "";

		private readonly String Private = "";

		private readonly FileStream FileStream;

		/// <summary>
		/// Regex options to apply to every IsMatch, Match, and Matches method which accepts only a string
		/// </summary>
		public const RegexOptions DefaultRegexOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;

		/// <summary>
		/// Attempt to find the specified <paramref name="Pattern"/> within the entire source code
		/// </summary>
		/// <param name="Pattern"></param>
		/// <returns></returns>
		public Boolean IsMatch(String Pattern, RegexOptions RegexOptions = DefaultRegexOptions) {
			Regex Regex = new Regex(Pattern, RegexOptions);
			if (Regex.IsMatch(this.Imports)) return true;
			if (Regex.IsMatch(this.Generic)) return true;
			if (Regex.IsMatch(this.Public)) return true;
			if (Regex.IsMatch(this.Private)) return true;
			return false;
		}

		/// <summary>
		/// Attempt to find the specified <paramref name="Pattern"/> within the specified <paramref name="Sections"/>
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <param name="Sections">The sections to search</param>
		/// <returns>True if a match was found, false otherwise</returns>
		public Boolean IsMatch(String Pattern, SourceSection Sections, RegexOptions RegexOptions = DefaultRegexOptions) {
			Regex Regex = new Regex(Pattern, RegexOptions);
			switch (Sections) {
			case SourceSection.Generic:
				return Regex.IsMatch(this.Generic);
			case SourceSection.Imports:
				return Regex.IsMatch(this.Imports);
			case SourceSection.Private:
				return Regex.IsMatch(this.Private);
			case SourceSection.Public:
				return Regex.IsMatch(this.Public);
			case SourceSection.Generic | SourceSection.Imports:
				if (Regex.IsMatch(this.Generic)) return true;
				if (Regex.IsMatch(this.Imports)) return true;
				return false;
			case SourceSection.Generic | SourceSection.Private:
				if (Regex.IsMatch(this.Generic)) return true;
				if (Regex.IsMatch(this.Private)) return true;
				return false;
			case SourceSection.Generic | SourceSection.Public:
				if (Regex.IsMatch(this.Generic)) return true;
				if (Regex.IsMatch(this.Public)) return true;
				return false;
			case SourceSection.Imports | SourceSection.Private:
				if (Regex.IsMatch(this.Imports)) return true;
				if (Regex.IsMatch(this.Private)) return true;
				return false;
			case SourceSection.Imports | SourceSection.Public:
				if (Regex.IsMatch(this.Imports)) return true;
				if (Regex.IsMatch(this.Public)) return true;
				return false;
			case SourceSection.Private | SourceSection.Public:
				if (Regex.IsMatch(this.Public)) return true;
				if (Regex.IsMatch(this.Private)) return true;
				return false;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Private:
				if (Regex.IsMatch(this.Generic)) return true;
				if (Regex.IsMatch(this.Imports)) return true;
				if (Regex.IsMatch(this.Private)) return true;
				return false;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Public:
				if (Regex.IsMatch(this.Generic)) return true;
				if (Regex.IsMatch(this.Imports)) return true;
				if (Regex.IsMatch(this.Public)) return true;
				return false;
			case SourceSection.Generic | SourceSection.Private | SourceSection.Public:
				if (Regex.IsMatch(this.Generic)) return true;
				if (Regex.IsMatch(this.Public)) return true;
				if (Regex.IsMatch(this.Private)) return true;
				return false;
			case SourceSection.Imports | SourceSection.Private | SourceSection.Public:
				if (Regex.IsMatch(this.Imports)) return true;
				if (Regex.IsMatch(this.Public)) return true;
				if (Regex.IsMatch(this.Private)) return true;
				return false;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Private | SourceSection.Public:
				if (Regex.IsMatch(this.Generic)) return true;
				if (Regex.IsMatch(this.Imports)) return true;
				if (Regex.IsMatch(this.Public)) return true;
				if (Regex.IsMatch(this.Private)) return true;
				return false;
			default:
				throw new InvalidSourceSectionException();
			}
		}

		/// <summary>
		/// Attempt to find the specified <paramref name="Pattern"/> within the entire source code
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <returns>If a match was found, returns the matched source code</returns>
		public String Match(String Pattern, RegexOptions RegexOptions = DefaultRegexOptions) {
			Regex Regex = new Regex(Pattern, RegexOptions);
			Match Match;
			Match = Regex.Match(this.Generic);
			if (Match.Success) return Match.Value;
			Match = Regex.Match(this.Imports);
			if (Match.Success) return Match.Value;
			Match = Regex.Match(this.Public);
			if (Match.Success) return Match.Value;
			Match = Regex.Match(this.Private);
			if (Match.Success) return Match.Value;
			return "";
		}

		/// <summary>
		/// Attempt to find the specified <paramref name="Pattern"/> within the specified <paramref name="Sections"/>
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <param name="Sections">The sections to search</param>
		/// <returns>If a match was found, returns the matched source code</returns>
		public String Match(String Pattern, SourceSection Sections, RegexOptions RegexOptions = DefaultRegexOptions) {
			Regex Regex = new Regex(Pattern, RegexOptions);
			Match Match;
			switch (Sections) {
			case SourceSection.Generic:
				return Regex.Match(this.Generic).Value;
			case SourceSection.Imports:
				return Regex.Match(this.Imports).Value;
			case SourceSection.Private:
				return Regex.Match(this.Private).Value;
			case SourceSection.Public:
				return Regex.Match(this.Public).Value;
			case SourceSection.Generic | SourceSection.Imports:
				Match = Regex.Match(this.Generic);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Imports).Value;
			case SourceSection.Generic | SourceSection.Private:
				Match = Regex.Match(this.Generic);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Private).Value;
			case SourceSection.Generic | SourceSection.Public:
				Match = Regex.Match(this.Generic);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Public).Value;
			case SourceSection.Imports | SourceSection.Private:
				Match = Regex.Match(this.Imports);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Private).Value;
			case SourceSection.Imports | SourceSection.Public:
				Match = Regex.Match(this.Imports);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Public).Value;
			case SourceSection.Private | SourceSection.Public:
				Match = Regex.Match(this.Public);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Private).Value;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Private:
				Match = Regex.Match(this.Generic);
				if (Match.Success) return Match.Value;
				Match = Regex.Match(this.Imports);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Private).Value;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Public:
				Match = Regex.Match(this.Generic);
				if (Match.Success) return Match.Value;
				Match = Regex.Match(this.Imports);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Public).Value;
			case SourceSection.Generic | SourceSection.Private | SourceSection.Public:
				Match = Regex.Match(this.Generic);
				if (Match.Success) return Match.Value;
				Match = Regex.Match(this.Public);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Private).Value;
			case SourceSection.Imports | SourceSection.Private | SourceSection.Public:
				Match = Regex.Match(this.Imports);
				if (Match.Success) return Match.Value;
				Match = Regex.Match(this.Public);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Private).Value;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Private | SourceSection.Public:
				Match = Regex.Match(this.Generic);
				if (Match.Success) return Match.Value;
				Match = Regex.Match(this.Imports);
				if (Match.Success) return Match.Value;
				Match = Regex.Match(this.Public);
				if (Match.Success) return Match.Value;
				return Regex.Match(this.Private).Value;
			default:
				throw new InvalidSourceSectionException();
			}
		}

		/// <summary>
		/// Attempt to find all of the specified <paramref name="Pattern"/> within the entire source code
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <returns>Returns all of the matches in the source code, if any</returns>
		public String[] Matches(String Pattern, RegexOptions RegexOptions = DefaultRegexOptions) {
			Regex Regex = new Regex(Pattern, RegexOptions);
			List<String> Matches = new List<String>();
			foreach (Match Match in Regex.Matches(this.Generic)) {
				Matches.Add(Match.Value);
			}
			foreach (Match Match in Regex.Matches(this.Imports)) {
				Matches.Add(Match.Value);
			}
			foreach (Match Match in Regex.Matches(this.Public)) {
				Matches.Add(Match.Value);
			}
			foreach (Match Match in Regex.Matches(this.Private)) {
				Matches.Add(Match.Value);
			}
			return Matches.ToArray();
		}

		/// <summary>
		/// Attempt to find all of the specified <paramref name="Pattern"/> within the specified <paramref name="Sections"/>
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <param name="Sections">The sections to search</param>
		/// <returns>Returns all of the matches in the specified sections, if any</returns>
		public String[] Matches(String Pattern, SourceSection Sections, RegexOptions RegexOptions = DefaultRegexOptions) {
			Regex Regex = new Regex(Pattern, RegexOptions);
			List<String> Matches = new List<String>();
			switch (Sections) {
			case SourceSection.Generic:
				foreach (Match Match in Regex.Matches(this.Generic)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Imports:
				foreach (Match Match in Regex.Matches(this.Imports)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Private:
				foreach (Match Match in Regex.Matches(this.Private)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Public:
				foreach (Match Match in Regex.Matches(this.Public)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Generic | SourceSection.Imports:
				foreach (Match Match in Regex.Matches(this.Generic)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Imports)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Generic | SourceSection.Private:
				foreach (Match Match in Regex.Matches(this.Generic)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Private)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Generic | SourceSection.Public:
				foreach (Match Match in Regex.Matches(this.Generic)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Public)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Imports | SourceSection.Private:
				foreach (Match Match in Regex.Matches(this.Imports)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Private)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Imports | SourceSection.Public:
				foreach (Match Match in Regex.Matches(this.Imports)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Public)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Private | SourceSection.Public:
				foreach (Match Match in Regex.Matches(this.Public)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Private)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Private:
				foreach (Match Match in Regex.Matches(this.Generic)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Imports)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Private)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Public:
				foreach (Match Match in Regex.Matches(this.Generic)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Imports)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Public)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Generic | SourceSection.Private | SourceSection.Public:
				foreach (Match Match in Regex.Matches(this.Generic)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Public)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Private)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Imports | SourceSection.Private | SourceSection.Public:
				foreach (Match Match in Regex.Matches(this.Imports)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Public)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Private)) {
					Matches.Add(Match.Value);
				}
				break;
			case SourceSection.Generic | SourceSection.Imports | SourceSection.Private | SourceSection.Public:
				foreach (Match Match in Regex.Matches(this.Generic)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Imports)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Public)) {
					Matches.Add(Match.Value);
				}
				foreach (Match Match in Regex.Matches(this.Private)) {
					Matches.Add(Match.Value);
				}
				break;
			default:
				throw new InvalidSourceSectionException();
			}
			return Matches.ToArray();
		}

		/// <summary>
		/// Try to parse what Ada Version this unit is
		/// </summary>
		/// <returns>The Ada Version if one was found, null otherwise</returns>
		public AdaVersion? ParseAdaVersion() {
			if (this.IsMatch(@"\bpragma\s+Ada_83")) return AdaVersion.Ada1983;
			if (this.IsMatch(@"\bpragma\s+Ada_95")) return AdaVersion.Ada1995;
			if (this.IsMatch(@"\bpragma\s+Ada_(20)?05")) return AdaVersion.Ada2005;
			if (this.IsMatch(@"\bpragma\s+Ada_(20)?12")) return AdaVersion.Ada2012;
			return null;
		}

		/// <summary>
		/// Try to parse whether this unit makes remote calls
		/// </summary>
		/// <returns></returns>
		public Boolean ParseAllCallsRemote() {
			if (this.IsMatch(@"\bwith\s+.*all_calls_remote.*\s+is")) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Try to parse the assertion policy configuration
		/// </summary>
		/// <returns></returns>
		public AssertionPolicy ParseAssertionPolicy() {
			String Policy = this.Match(@"\bpragma\s+Assertion_Policy\s*\((\\.|[^)])*\);");
			if (new Regex(@"\(\s*check\s*\)", DefaultRegexOptions).IsMatch(Policy)) {
				return new AssertionPolicy(PolicyIdentifier.Check);
			} else if (new Regex(@"\(\s*disable\s*\)", DefaultRegexOptions).IsMatch(Policy)) {
				return new AssertionPolicy(PolicyIdentifier.Disable);
			} else if (new Regex(@"\(\s*ignore\s*\)", DefaultRegexOptions).IsMatch(Policy)) {
				return new AssertionPolicy(PolicyIdentifier.Ignore);
			} else if (new Regex(@"\(\s*suppressible\s\)", DefaultRegexOptions).IsMatch(Policy)) {
				// This isn't correct, but is done to be recognized non-the-less
				return new AssertionPolicy(PolicyIdentifier.Suppressible);
			} else {
				// The policy isn't global, it's a list, so parse the list
				Dictionary<String, PolicyIdentifier> Policies = new Dictionary<String, PolicyIdentifier>();
				foreach (String P in new Regex(@"\(.*\)", DefaultRegexOptions)
					.Match(Policy).ToString()
					.TrimStart('(').TrimEnd(')').Trim()
					.Split(',')) {
					String AspectMark = new Regex(@"\b(\w|_)+\s*=>", DefaultRegexOptions).Match(P).ToString().Replace("=>", "").Trim();
					String Identifier = new Regex(@"=>\s*(\w|_)+\b", DefaultRegexOptions).Match(P).ToString().Replace("=>", "").Trim().ToUpper();
					PolicyIdentifier PolID;
					switch (Identifier) {
					case "CHECK":
						PolID = PolicyIdentifier.Check;
						break;
					case "DISABLE":
						PolID = PolicyIdentifier.Disable;
						break;
					case "IGNORE":
						PolID = PolicyIdentifier.Ignore;
						break;
					case "SUPPRESSIBLE":
						PolID = PolicyIdentifier.Suppressible;
						break;
					default:
						continue;
					}
					Policies.Add(AspectMark, PolID);
				}
				return new AssertionPolicy(Policies);
			}
		}

		/// <summary>
		/// Try to parse the Assume_No_Invalid_Values configuration
		/// </summary>
		/// <returns></returns>
		public AssumeNoInvalidValues? ParseAssumeNoInvalidValues() {
			String Config = this.Match(@"\bpragma\s+Assume_No_Invalid_Values\(.*\);");
			if (Config is null) return null;
			if (new Regex(@"\bon\b", DefaultRegexOptions).IsMatch(Config)) {
				return AssumeNoInvalidValues.On;
			} else if (new Regex(@"\boff\b", DefaultRegexOptions).IsMatch(Config)) {
				return AssumeNoInvalidValues.Off;
			} else {
				return null;
			}

		}

		/// <summary>
		/// Try to parse the packages this unit dependends on
		/// </summary>
		/// <returns>A list of the dependent packages</returns>
		public List<String> ParseDependencies() {
			List<String> Result = new List<String>();
			// This seemingly obtuse approach is done to filter out aspects, which share the same syntax. A full blown semantic parser would be able to recognize when a dependency is appropriate versus an aspect, but a simple regex parser can't. So we do this instead.
			foreach (String Match in this.Matches(@"\b((function|procedure).*\s+)?with\s+(\w|\.|_)+(\s*,\s*(\w|\.|_)+)*;", SourceSection.Imports)) {
				// If the match contains "function" or "procedure" the "with" section is an aspect, so skip this match and move onto the next one
				if (new Regex(@"\b(function|procedure)\b", DefaultRegexOptions).IsMatch(Match)) {
					continue;
				} else {
					// We're actually looking at a dependency "with", so do the good stuff
					foreach (String Name in Match.Substring(4).TrimEnd(';').Trim().Split(',')) {
						Result.Add(Name.Trim());
					}
				}
			}
			return Result;
		}

		/// <summary>
		/// Try to parse the description comment
		/// </summary>
		/// <returns>The description comment if found, otherwise the summary comment if found, otherwise an empty string</returns>
		public String ParseDescription() {
			String Candidate;
			// This is correctly in multiline mode, as Ada comments only exist over single lines
			Candidate = this.Match(@"--@description.*$", RegexOptions.IgnoreCase | RegexOptions.Multiline)
			?? "--@description  "; // This last part ensures the parse does not fail, but rather, simply finds an empty description.
			Console.WriteLine("Candidate: " + Candidate);
			String[] Split = Candidate.Split();
			return String.Join(' ', Split.Skip(1).Take(Split.Length - 1));
		}

		/// <summary>
		/// Try to parse the elaboration checks configuration
		/// </summary>
		/// <returns></returns>
		public ElaborationChecks? ParseElaborationChecks() {
			String Config = this.Match(@"\bpragma\s+Elaboration_Checks\(.*\);");
			if (Config is null) return null;
			if (new Regex(@"\bdynamic\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
				return ElaborationChecks.Dynamic;
			} else if (new Regex(@"\bstatic\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
				return ElaborationChecks.Static;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Try to parse the extensions allowed configuration
		/// </summary>
		/// <returns></returns>
		public ExtensionsAllowed? ParseExtensionsAllowed() {
			String Config = this.Match(@"\bpragma\s+Extensions_Allowed\(.*\);");
			if (Config is null) return null;
			if (new Regex(@"\bon\b", DefaultRegexOptions).IsMatch(Config)) {
				return ExtensionsAllowed.On;
			} else if (new Regex(@"\boff\b", DefaultRegexOptions).IsMatch(Config)) {
				return ExtensionsAllowed.Off;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Try to parse the fast math configuration
		/// </summary>
		/// <returns></returns>
		public Boolean ParseFastMath() {
			String Config = this.Match(@"\bpragma\s+Fast_Math;");
			if (Config is null) {
				return false;
			} else {
				return true;
			}
		}

		/// <summary>
		/// Try to parse the license configuration
		/// </summary>
		public License? ParseLicense() {
			String Config = this.Match(@"\bpragma\s+License\(.*\);");
			if (Config is null) return null;
			if (new Regex(@"\bgpl\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
				return License.GPL;
			} else if (new Regex(@"\bmodified_gpl\b", DefaultRegexOptions).IsMatch(Config)) {
				return License.Modified_GPL;
			} else if (new Regex(@"\brestricted\b", DefaultRegexOptions).IsMatch(Config)) {
				return License.Restricted;
			} else if (new Regex(@"\bunrestricted\b", DefaultRegexOptions).IsMatch(Config)) {
				return License.Unrestricted;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Try to parse the internal name of the unit
		/// </summary>
		/// <returns>The internal name, if any was found</returns>
		public String ParseName() {
			String Candidate = "";
			// Try getting the name through a variety of means
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(@"(?<!with\s*)package\s+body\s+(\w|\.|_)+\s+(is|with)\b");
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(@"(?<!with\s*)package\s+(\w|\.|_)+\s+(is|with)\b");
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(@"(?<!with\s*)function\s+(\w|_)+\s+return\b");
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(@"(?<!with\s*)procedure\s+(\w|_)+\s+is\b");
			// If no name was found, it's not an Ada source file
			if (String.IsNullOrEmpty(Candidate)) throw new NotAdaSourceException();
			String[] Split = Candidate.Split();
			if (Split.Length == 4) {
				return Split[2];
			} else if (Split.Length == 3) {
				return Split[1];
			} else {
				// This should never happen because a match wouldn't happen, but still raise an exception
				throw new Exception("A critical error has occured");
			}
		}

		/// <summary>
		/// Try to parse the type of Ada Program
		/// </summary>
		/// <returns>The type of program</returns>
		public ProgramType ParseProgramType() {
			String Candidate = this.Match(@"\b(function|procedure)\s(\w|_)+\b");
			if (String.IsNullOrEmpty(Candidate)) throw new NotAdaProgramException();
			if (new Regex(@"\bfunction\b", RegexOptions.IgnoreCase).IsMatch(Candidate)) {
				return ProgramType.Function;
			} else if (new Regex(@"\bprocedure\b", RegexOptions.IgnoreCase).IsMatch(Candidate)) {
				return ProgramType.Procedure;
			} else {
				// This should never happen because a match wouldn't happen, but still raise an exception
				throw new Exception("A critical error has occured");
			}
		}

		/// <summary>
		/// Try to parse the purity of the unit
		/// </summary>
		/// <returns>True if pure, false otherwise</returns>
		public Boolean ParsePurity() {
			if (this.IsMatch(@"\bpragma\s+pure\s*\(\s*(\w|\.|_)+\s*\);")) {
				return true;
			} else if (this.IsMatch(@"\bwith\s+.*pure.*\s+is\b")) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Try to parse whether the unit is a remote call interface
		/// </summary>
		/// <returns>True if RCI, false otherwise</returns>
		public Boolean ParseRemoteCallInterface() {
			if (this.IsMatch(@"\bwith\s+.*remote_call_interface.*\s+is\b")) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Try to parse the source file name configurations
		/// </summary>
		/// <returns></returns>
		public SourceFileNames ParseSourceFileNames() {
			SourceFileNames SourceFileNames = new SourceFileNames();
			String[] Configs;
			Configs = this.Matches(@"\bpragma\s+Source_File_Name\s*\((\\.|[^\)])*\);");
			String FileName;
			Casing Casing;
			String DotReplacement;
			foreach (String Config in Configs) {
				switch (new Regex(@"\bcasing => \w+\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(Config).ToString().Split()[2].Trim('"')) {
				case "uppercase":
					Casing = Casing.Uppercase;
					break;
				case "lowercase":
					Casing = Casing.Lowercase;
					break;
				case "mixedcase":
				default: // This shuts up the compiler, and is a reasonably default
					Casing = Casing.Mixedcase;
					break;
				}
				if (new Regex(@"\bdot_replacement\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
					// I think this regex has to be a normal escaped string because of the quotes inside of it. Unfortunantly this makes it really awkward to read.
					DotReplacement = new Regex("\\bdot_replacement => \"(\\\\.|[^\"])*\"", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(Config).ToString().Split()[2].Trim('"');
				} else {
					DotReplacement = ".";
				}
				if (new Regex(@"\bspec_file_name\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
					FileName = new Regex("\\bspec_file_name => \"(\\\\.|[^\"])*\"", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(Config).ToString().Split()[2].Trim('"');
					SourceFileNames.SpecFileName = new SpecFileName(FileName, Casing, DotReplacement);
				} else if (new Regex(@"\bbody_file_name\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
					FileName = new Regex("\\bbody_file_name => \"(\\\\.|[^\"])*\"", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(Config).ToString().Split()[2].Trim('"');
					SourceFileNames.BodyFileName = new BodyFileName(FileName, Casing, DotReplacement);
				} else if (new Regex(@"\bsubunit_file_name\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
					FileName = new Regex("\\bsource_file_name => \"(\\\\.|[^\"])*\"", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(Config).ToString().Split()[2].Trim('"');
					SourceFileNames.SubunitFileName = new SubunitFileName(FileName, Casing, DotReplacement);
				}
			}
			return SourceFileNames;
		}

		/// <summary>
		/// Try to parse the type of Ada source
		/// </summary>
		/// <remarks>
		/// This method attempts to distinguish a package spec or body from a function program or procedure program. This is necessary because packages and programs have different representations within this tool, as well as different information that will be gathered about them.
		/// </remarks>
		/// <returns>The type of source</returns>
		public SourceType ParseSourceType() {
			String Candidate = this.Match(@"\b(package|function|procedure)\s+(\w|\.|_)+\b");
			// If no matche was found, it's not an Ada source file
			if (String.IsNullOrEmpty(Candidate)) throw new NotAdaSourceException();
			if (new Regex(@"\bpackage\b", RegexOptions.IgnoreCase).IsMatch(Candidate)) {
				return SourceType.Package;
			} else if (new Regex(@"\b(function|procedure)\b").IsMatch(Candidate)) {
				return SourceType.Program;
			} else {
				// This should never happen because a match wouldn't happen, but still raise an exception
				throw new Exception("A critical error has occured");
			}
		}

		/// <summary>
		/// Try to parse the types within the source
		/// </summary>
		/// <returns>The types found</returns>
		public TypesCollection ParseTypes() {
			TypesCollection Types = new TypesCollection();
			String[] Candidates = this.Matches(@"\btype\s+(\w|_)+\s+is(\\.|[^;])*;", SourceSection.Public | SourceSection.Private);
			Stack<String> Split;
			String TypeName;
			String TypeRange;
			foreach (String Candidate in Candidates) {
				if (new Regex(@"\btype\s+(\w|_)+\s+is\s+range\s+\d+\s*\.\.\s*\d+\s*;", DefaultRegexOptions).IsMatch(Candidate)) {
					Split = new Stack<String>(Candidate.TrimEnd(';').Split().Reverse());
					Split.Pop(); // "type"
					TypeName = Split.Pop();
					Split.Pop(); // "is"
					Split.Pop(); // "range"
					TypeRange = String.Join(null, Split); // Join the remaining candidate back together, because it's going to be split differently
					Int64 Lower = Int64.Parse(TypeRange.Split("..")[0]);
					Int64 Upper = Int64.Parse(TypeRange.Split("..")[1]);
					Types.Add(new SignedType(TypeName, new Range<Int64>(Lower, Upper)));
				} else if (new Regex(@"\btype\s+(\w|_)+\s+is\s+mod\s+\d+\s*;", DefaultRegexOptions).IsMatch(Candidate)) {
					Split = new Stack<String>(Candidate.TrimEnd(';').Split().Reverse());
					Split.Pop(); // "type"
					TypeName = Split.Pop();
					Split.Pop(); // "is"
					Split.Pop(); // "mod"
					UInt32 Mod = UInt32.Parse(Split.Pop());
					Types.Add(new ModularType(TypeName, Mod));
				} else if (new Regex(@"\btype\s+(\w|_)+\s+is\s+digits\s+\d+(\s+range\s+\d+\.\d+\s*\.\.\s*\d+\.\d+\s*)?;", DefaultRegexOptions).IsMatch(Candidate)) {
					Split = new Stack<String>(Candidate.TrimEnd(';').Split().Reverse());
					Split.Pop(); // "type"
					TypeName = Split.Pop();
					Split.Pop(); // "is"
					Split.Pop(); // "digits"
					UInt16 Digits = UInt16.Parse(Split.Pop());
					// If there is more than 2 items on the stack, there is definately a range clause, so parse that too
					if (Split.Count >= 2) {
						Split.Pop(); // "range"
						TypeRange = String.Join(null, Split); // Join the remaining candidate back together, because it's going to be split differently
						Double Lower = Double.Parse(TypeRange.Split("..")[0]);
						Double Upper = Double.Parse(TypeRange.Split("..")[1]);
						Types.Add(new FloatType(TypeName, Digits, new Range<Double>(Lower, Upper)));
					} else {
						Types.Add(new FloatType(TypeName, Digits));
					}
				} else if (new Regex(@"\btype\s+(\w|_)+\s+is\s+delta\s+\d+\.\d+(\s+range\s+\d+\.\d+\s*\.\.\s*\d+\.\d+\s*)?;", DefaultRegexOptions).IsMatch(Candidate)) {
					Split = new Stack<String>(Candidate.TrimEnd(';').Split().Reverse());
					Split.Pop(); // "type"
					TypeName = Split.Pop();
					Split.Pop(); // "is"
					Split.Pop(); // "delta"
					Double Delta = Double.Parse(Split.Pop());
					// If there is more than 2 items on the stack, there is definately a range clause, so parse that too
					if (Split.Count >= 2) {
						Split.Pop(); // "range"
						TypeRange = String.Join(null, Split); // Join the remaining candidate back together, because it's going to be split differently
						Double Lower = Double.Parse(TypeRange.Split("..")[0]);
						Double Upper = Double.Parse(TypeRange.Split("..")[1]);
						Types.Add(new OrdinaryType(TypeName, Delta, Lower, Upper));
					} else {
						Types.Add(new OrdinaryType(TypeName, Delta));
					}
				} else if (new Regex(@"\btype\s+(\w|_)+\s+is\s+delta\s+\d+\.\d+\s+digits\s+\d+(\s+range\s+\d+\.\d+\s*\.\.\s*\d+\.\d+\s*)?;", DefaultRegexOptions).IsMatch(Candidate)) {
					Split = new Stack<String>(Candidate.TrimEnd(';').Split().Reverse());
					Split.Pop(); // "type"
					TypeName = Split.Pop();
					Split.Pop(); // "is"
					Split.Pop(); // "delta"
					Decimal Delta = Decimal.Parse(Split.Pop());
					Split.Pop(); // "digits"
					UInt16 Digits = UInt16.Parse(Split.Pop());
					// If there is more than 2 items on the stack, there is definately a range clause, so parse that too
					if (Split.Count >= 2) {
						Split.Pop(); // "range"
						TypeRange = String.Join(null, Split); // Join the remaining candidate back together, because it's going to be split differently
						Decimal Lower = Decimal.Parse(TypeRange.Split("..")[0]);
						Decimal Upper = Decimal.Parse(TypeRange.Split("..")[1]);
						Types.Add(new DecimalType(TypeName, Delta, Digits, Lower, Upper));
					} else {
						Types.Add(new DecimalType(TypeName, Delta, Digits));
					}
				} else {
					continue;
				}
			}
			return Types;
		}

		/// <summary>
		/// Try to parse the unit version
		/// </summary>
		/// <returns>Returns the unit version if found, or 1.0.0 otherwise</returns>
		public Version ParseVersion() {
			// Try to match the psuedo gnatdoc convention for version comments. Gnatdoc doesn't actually have this, but it's the same style
			// This is correctly in multiline mode, as Ada comments only exist over single lines
			String Candidate = this.Match(@"--@version.*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			if (Candidate is null || Candidate == "") return new Version(1, 0, 0);

			String[] Split = Candidate.Split();

			return new Version(Split[1]);
		}

		/// <summary>
		/// Try to parse the wide character encoding configuration
		/// </summary>
		/// <returns>Returns the encoding type if found, or null if not specified</returns>
		public WideCharacterEncoding? ParseWideCharacterEncoding() {
			String Config = this.Match(@"\bpragma\s+Wide_Character_Encoding\s*\((\\.|[^)])*\)");

			if (new Regex(@"\bbrackets\b", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.Brackets;
			if (new Regex(@"'b'", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.Brackets;

			if (new Regex(@"\beuc\b", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.EUC;
			if (new Regex(@"'e'", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.EUC;

			if (new Regex(@"\bhex\b", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.Hex;
			if (new Regex(@"'h'", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.Hex;

			if (new Regex(@"\bshift_jis\b", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.Shift_JIS;
			if (new Regex(@"'s'", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.Shift_JIS;

			if (new Regex(@"\bupper\b", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.Upper;
			if (new Regex(@"'u'", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.Upper;

			if (new Regex(@"\butf8\b", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.UTF8;
			if (new Regex(@"'8'", DefaultRegexOptions).IsMatch(Config)) return WideCharacterEncoding.UTF8;

			return null;
		}

		public override Boolean Equals(Object obj) => (obj is Source) ? this.FileStream == (obj as Source).FileStream : false;

		public override Int32 GetHashCode() => this.FileStream.GetHashCode();

		public static Boolean operator ==(Source Left, Source Right) => Left.FileStream == Right.FileStream;

		public static Boolean operator ==(Source Left, FileStream Right) => Left.FileStream == Right;

		public static Boolean operator ==(FileStream Left, Source Right) => Left == Right.FileStream;

		public static Boolean operator !=(Source Left, Source Right) => Left.FileStream != Right.FileStream;

		public static Boolean operator !=(Source Left, FileStream Right) => Left.FileStream != Right;

		public static Boolean operator !=(FileStream Left, Source Right) => Left != Right.FileStream;

		public Source(FileStream FileStream) {
			Console.WriteLine("new Source(" + FileStream.Name + ")");
			using (StreamReader Reader = new StreamReader(FileStream)) {
				String Line;
				List<String> Lines = new List<String>();

				// This uses a simple state machine approach. Imports are always at the begining of the source file, then a possible Generic preamble, then the Public part, followed by a possible Private part
				// The general approach is to loop over each line. The following variables control where the line is placed, as this source object is split into sections. Furthermore, each state has checks for moving into the next state.
				// It is not an error that the loop is organized as a series of if statements, rather than if-elseif. This is because when we switch state, the line needs to be put into the next section. If an if-elseif approach was taken, the lines where state was switched would be consumed and not in the source at all.

				Boolean ReadingImports = true;
				Boolean ReadingGeneric = false;
				Boolean ReadingPublic = false;
				Boolean ReadingPrivate = false;


				// Read line by line
				while ((Line = Reader.ReadLine()) != null) {
					if (ReadingImports) {
						if (new Regex(@"(^|;)\s*(generic)\b").IsMatch(Line)) {
							this.Imports = String.Join('\n', Lines);
							Lines.Clear();
							ReadingImports = false;
							ReadingGeneric = true;
						} else if (new Regex(@"(^|;)\s*(function|package|procedure)\b").IsMatch(Line)) {
							this.Imports = String.Join('\n', Lines);
							Lines.Clear();
							ReadingImports = false;
							ReadingPublic = true;
						}
						Lines.Add(Line);
					}
					if (ReadingGeneric) {
						if (new Regex(@"(^|;)\s*(function|package|procedure)\b").IsMatch(Line)) {
							this.Generic = String.Join('\n', Lines);
							Lines.Clear();
							ReadingGeneric = false;
							ReadingPublic = true;
						}
						Lines.Add(Line);
					}
					if (ReadingPublic) {
						if (new Regex(@"(^|;)\s*(private)\b").IsMatch(Line)) {
							this.Public = String.Join('\n', Lines);
							Lines.Clear();
							ReadingPublic = false;
							ReadingPrivate = true;
						}
						Lines.Add(Line);
					}
					if (ReadingPrivate) {
						Lines.Add(Line);
					}
				}

				// An end of file was reached, but there's still lines in the List<String>. This decides where to put them.
				if (ReadingImports) {
					// The reason for ReadingImports placing the lines in the public part is that the source file being read doesn't seem to be a spec or body, and is probably something like a gnat.adc file, which is entirely "public".
					this.Public = String.Join('\n', Lines);
				} else if (ReadingPublic) {
					// A private part was never found, so the entire unit is publicly visable.
					this.Public = String.Join('\n', Lines);
				} else if (ReadingPrivate) {
					this.Private = String.Join('\n', Lines);
				}
			}
			this.FileStream = FileStream;

			// These are for debugging purposes. Uncomment if necessary. Sometimes writing to the console is the only reasonable way to debug. Fight me.
			//Console.WriteLine("Imports:\n" + this.Imports);
			//Console.WriteLine("Generic:\n" + this.Generic);
			//Console.WriteLine("Public:\n" + this.Public);
			//Console.WriteLine("Private:\n" + this.Private);
		}

		public Source(String FileName) : this(new FileStream(FileName, FileMode.Open)) {
			// Everything necessary should happen through chaining
		}
	}
}
