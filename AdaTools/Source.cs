﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdaTools {
	/// <summary>
	/// Represents Ada source code
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	public sealed class Source {

		/// <summary>
		/// Holds the actual source code
		/// </summary>
		private readonly String SourceCode = "";

		/// <summary>
		/// Attempt to find the specified <paramref name="Pattern"/> within the source code
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <returns>True if a match was found, false otherwise</returns>
		public Boolean IsMatch(Regex Pattern) => Pattern.IsMatch(this.SourceCode);

		/// <summary>
		/// Attempt to find the specified <paramref name="Pattern"/> within the source code
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <returns>If a match was found, returns the matched source code</returns>
		public String Match(Regex Pattern) => Pattern.Match(this.SourceCode).Value;

		/// <summary>
		/// Attempt to find all of the specified <paramref name="Pattern"/> within the source code
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <returns>Returns all of the matches in the source code, if any</returns>
		public String[] Matches(Regex Pattern) {
			List<String> Matches = new List<String>();
			foreach (Match Match in Pattern.Matches(this.SourceCode)) {
				Matches.Add(Match.Value);
			}
			return Matches.ToArray();
		}

		/// <summary>
		/// Try to parse what Ada Version this unit is
		/// </summary>
		/// <returns>The Ada Version if one was found, null otherwise</returns>
		public AdaVersion? ParseAdaVersion() {
			if (this.IsMatch(new Regex(@"\bpragma\s+Ada_83", RegexOptions.IgnoreCase | RegexOptions.Singleline))) return AdaVersion.Ada1983;
			if (this.IsMatch(new Regex(@"\bpragma\s+Ada_95", RegexOptions.IgnoreCase | RegexOptions.Singleline))) return AdaVersion.Ada1995;
			if (this.IsMatch(new Regex(@"\bpragma\s+Ada_(20)?05", RegexOptions.IgnoreCase | RegexOptions.Singleline))) return AdaVersion.Ada2005;
			if (this.IsMatch(new Regex(@"\bpragma\s+Ada_(20)?12", RegexOptions.IgnoreCase | RegexOptions.Singleline))) return AdaVersion.Ada2012;
			return null;
		}

		/// <summary>
		/// Try to parse whether this unit makes remote calls
		/// </summary>
		/// <returns></returns>
		public Boolean ParseAllCallsRemote() {
			if (this.IsMatch(new Regex(@"\bwith\s+.*all_calls_remote.*\s+is", RegexOptions.IgnoreCase | RegexOptions.Singleline))) {
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
			String Policy = this.Match(new Regex(@"\bpragma\s+Assertion_Policy\s*\((\\.|[^)])*\);", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (new Regex(@"\(\s*check\s*\)", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Policy)) {
				return new AssertionPolicy(PolicyIdentifier.Check);
			} else if (new Regex(@"\(\s*disable\s*\)", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Policy)) {
				return new AssertionPolicy(PolicyIdentifier.Disable);
			} else if (new Regex(@"\(\s*ignore\s*\)", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Policy)) {
				return new AssertionPolicy(PolicyIdentifier.Ignore);
			} else if (new Regex(@"\(\s*suppressible\s\)", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Policy)) {
				// This isn't correct, but is done to be recognized non-the-less
				return new AssertionPolicy(PolicyIdentifier.Suppressible);
			} else {
				// The policy isn't global, it's a list, so parse the list
				Dictionary<String, PolicyIdentifier> Policies = new Dictionary<String, PolicyIdentifier>();
				foreach (String P in new Regex(@"\(.*\)", RegexOptions.IgnoreCase | RegexOptions.Singleline)
					.Match(Policy).ToString()
					.TrimStart('(').TrimEnd(')').Trim()
					.Split(',')) {
					String AspectMark = new Regex(@"\b(\w|_)+\s*=>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(P).ToString().Replace("=>", "").Trim();
					String Identifier = new Regex(@"=>\s*(\w|_)+\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).Match(P).ToString().Replace("=>", "").Trim().ToUpper();
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
			String Config = this.Match(new Regex(@"\bpragma\s+Assume_No_Invalid_Values\(.*\);", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (Config == null) return null;
			if (new Regex(@"\bon\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
				return AssumeNoInvalidValues.On;
			} else if (new Regex(@"\boff\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
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
			foreach (String Match in this.Matches(new Regex(@"\b((function|procedure).*\s+)?with\s+(\w|\.|_)+(\s*,\s*(\w|\.|_)+)*;", RegexOptions.IgnoreCase | RegexOptions.Singleline))) {
				// If the match contains "function" or "procedure" the "with" section is an aspect, so skip this match and move onto the next one
				if (new Regex(@"\b(function|procedure)\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Match)) {
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
		/// Try to parse the elaboration checks configuration
		/// </summary>
		/// <returns></returns>
		public ElaborationChecks? ParseElaborationChecks() {
			String Config = this.Match(new Regex(@"\bpragma\s+Elaboration_Checks\(.*\);", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (Config == null) return null;
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
			String Config = this.Match(new Regex(@"\bpragma\s+Extensions_Allowed\(.*\);", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (Config == null) return null;
			if (new Regex(@"\bon\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
				return ExtensionsAllowed.On;
			} else if (new Regex(@"\boff\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
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
			String Config = this.Match(new Regex(@"\bpragma\s+Fast_Math;", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (Config == null) {
				return false;
			} else {
				return true;
			}
		}

		/// <summary>
		/// Try to parse the license configuration
		/// </summary>
		public License? ParseLicense() {
			String Config = this.Match(new Regex(@"\bpragma\s+License\(.*\);", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (Config == null) return null;
			if (new Regex(@"\bgpl\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
				return License.GPL;
			} else if (new Regex(@"\bmodified_gpl\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
				return License.Modified_GPL;
			} else if (new Regex(@"\brestricted\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
				return License.Restricted;
			} else if (new Regex(@"\bunrestricted\b", RegexOptions.IgnoreCase | RegexOptions.Singleline).IsMatch(Config)) {
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
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bpackage\s+body\s+(\w|\.|_)+\s+(is|with)\b", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bpackage\s+(\w|\.|_)+\s+(is|with)\b", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bfunction\s+(\w|_)+\s+return\b", RegexOptions.IgnoreCase | RegexOptions.Singleline));
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bprocedure\s+(\w|_)+\s+is\b", RegexOptions.IgnoreCase | RegexOptions.Singleline));
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
			String Candidate = this.Match(new Regex(@"\b(function|procedure)\s(\w|_)+\b", RegexOptions.IgnoreCase | RegexOptions.Singleline));
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
			if (this.IsMatch(new Regex(@"\bpragma\s+pure\s*\(\s*(\w|\.|_)+\s*\);", RegexOptions.IgnoreCase | RegexOptions.Singleline))) {
				return true;
			} else if (this.IsMatch(new Regex(@"\bwith\s+.*pure.*\s+is\b", RegexOptions.IgnoreCase | RegexOptions.Singleline))) {
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
			if (this.IsMatch(new Regex(@"\bwith\s+.*remote_call_interface.*\s+is\b", RegexOptions.IgnoreCase | RegexOptions.Singleline))) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Try to parse the type of Ada source
		/// </summary>
		/// <remarks>
		/// This method attempts to distinguish a package spec or body from a function program or procedure program. This is necessary because packages and programs have different representations within this tool, as well as different information that will be gathered about them.
		/// </remarks>
		/// <returns>The type of source</returns>
		public SourceType ParseSourceType() {
			String Candidate = this.Match(new Regex(@"\b(package|function|procedure)\s+(\w|\.|_)+\b", RegexOptions.IgnoreCase | RegexOptions.Singleline));
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

		public String this[Int32 Index] {
			get => this.SourceCode.Split('\n')[Index];
		}

		public static implicit operator String(Source Source) => Source.SourceCode;

		public static implicit operator String[] (Source Source) => Source.SourceCode.Split('\n');

		public Source(FileStream File) {
			using (StreamReader Reader = new StreamReader(File)) {
				String Line;
				List<String> Lines = new List<String>();
				while ((Line = Reader.ReadLine()) != null) {
					Lines.Add(Line);
				}
				this.SourceCode = String.Join('\n', Lines);
			}
		}

		public Source(String FileName) : this(new FileStream(FileName, FileMode.Open)) {
			// Everything necessary should happen through chaining
		}
	}
}
