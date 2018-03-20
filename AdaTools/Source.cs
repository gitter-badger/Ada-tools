using System;
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
		/// Try to parse whether this unit makes remote calls
		/// </summary>
		/// <returns></returns>
		public Boolean TryParseAllCallsRemote() {
			if (this.IsMatch(new Regex(@"\bwith\s+.*all_calls_remote.*\s+is", RegexOptions.IgnoreCase | RegexOptions.Multiline))) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Try to parse the packages this unit dependends on
		/// </summary>
		/// <returns>A list of the dependent packages</returns>
		public List<String> TryParseDependencies() {
			List<String> Result = new List<String>();
			// This seemingly obtuse approach is done to filter out aspects, which share the same syntax. A full blown semantic parser would be able to recognize when a dependency is appropriate versus an aspect, but a simple regex parser can't. So we do this instead.
			foreach (String Match in this.Matches(new Regex(@"\b((function|procedure).*\s+)?with\s+(\w|\.|_)+(\s*,\s*(\w|\.|_)+)*;", RegexOptions.IgnoreCase | RegexOptions.Multiline))) {
				// If the match contains "function" or "procedure" the "with" section is an aspect, so skip this match and move onto the next one
				if (new Regex(@"\b(function|procedure)\b", RegexOptions.IgnoreCase | RegexOptions.Multiline).IsMatch(Match)) {
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
		/// Try to parse the internal name of the unit
		/// </summary>
		/// <returns>The internal name, if any was found</returns>
		public String TryParseName() {
			String Candidate = "";
			// Try getting the name through a variety of means
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bpackage\s+body\s+(\w|\.|_)+\s+(is|with)\b", RegexOptions.IgnoreCase | RegexOptions.Multiline));
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bpackage\s+(\w|\.|_)+\s+(is|with)\b", RegexOptions.IgnoreCase | RegexOptions.Multiline));
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bfunction\s+(\w|_)+\s+return\b", RegexOptions.IgnoreCase | RegexOptions.Multiline));
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bprocedure\s+(\w|_)+\s+is\b", RegexOptions.IgnoreCase | RegexOptions.Multiline));
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
		public ProgramType TryParseProgramType() {
			String Candidate = this.Match(new Regex(@"\b(function|procedure)\s(\w|_)+\b", RegexOptions.IgnoreCase | RegexOptions.Multiline));
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
		public Boolean TryParsePurity() {
			if (this.IsMatch(new Regex(@"\bpragma\s+pure\s*\(\s*(\w|\.|_)+\s*\);", RegexOptions.IgnoreCase | RegexOptions.Multiline))) {
				return true;
			} else if (this.IsMatch(new Regex(@"\bwith\s+.*pure.*\s+is\b", RegexOptions.IgnoreCase | RegexOptions.Multiline))) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Try to parse whether the unit is a remote call interface
		/// </summary>
		/// <returns>True if RCI, false otherwise</returns>
		public Boolean TryParseRemoteCallInterface() {
			if (this.IsMatch(new Regex(@"\bwith\s+.*remote_call_interface.*\s+is\b", RegexOptions.IgnoreCase | RegexOptions.Multiline))) {
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
		public SourceType TryParseSourceType() {
			String Candidate = this.Match(new Regex(@"\b(package|function|procedure)\s+(\w|\.|_)+\b", RegexOptions.IgnoreCase | RegexOptions.Multiline));
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

		public static implicit operator String[](Source Source) => Source.SourceCode.Split('\n');

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
