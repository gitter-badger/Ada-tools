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
		/// Try to parse the packages this package dependends on
		/// </summary>
		/// <returns>A list of the dependent packages</returns>
		public List<String> TryParseDependencies() {
			List<String> Result = new List<String>();
			foreach (String Match in this.Matches(new Regex(@"\bwith\s+(\w|\.|_)+(\s*,\s*(\w|\.|_)+)*;", RegexOptions.IgnoreCase | RegexOptions.Multiline))) {
				foreach (String Name in Match.Substring(4).TrimEnd(';').Trim().Split(',')) {
					Result.Add(Name.Trim());
				}
			}
			return Result;
		}

		/// <summary>
		/// Try to parse the internal name of the package
		/// </summary>
		/// <returns>The internal name, if any was found</returns>
		public String TryParseName() {
			String Candidate = "";
			// Try getting the name through a variety of means
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bpackage\s+(\w|\.|_)+\s+is", RegexOptions.IgnoreCase | RegexOptions.Multiline));
			if (String.IsNullOrEmpty(Candidate)) Candidate = this.Match(new Regex(@"\bpackage\s+body\s+(\w|\.|_)+\s+is", RegexOptions.IgnoreCase | RegexOptions.Multiline));
			// If no name was found, it's not an Ada package
			if (String.IsNullOrEmpty(Candidate)) throw new NotAdaPackageException();
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

		public Source(params String[] Lines) {
			this.SourceCode = String.Join('\n', Lines);
		}
	}
}
