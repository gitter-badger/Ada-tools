using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdaTools {
	/// <summary>
	/// Represents Ada source code
	/// </summary>
	public class Source {
		private readonly String SourceCode = "";

		/// <summary>
		/// Attempt to find the specified <paramref name="Pattern"/> within the source code
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <returns>If a match was found, returns the matched source code</returns>
		public String Match(Regex Pattern) {
			return Pattern.Match(SourceCode).Value;
		}

		/// <summary>
		/// Attempt to find all of the specified <paramref name="Pattern"/> within the source code
		/// </summary>
		/// <param name="Pattern">The pattern to try to find</param>
		/// <returns>Returns all of the matches in the source code, if any</returns>
		public String[] Matches(Regex Pattern) {
			List<String> Matches = new List<String>();
			foreach (Match Match in Pattern.Matches(SourceCode)) {
				Matches.Add(Match.Value);
			}
			return Matches.ToArray();
		}

		public String this[Int32 Index] {
			get => SourceCode.Split('\n')[Index];
		}

		public static implicit operator String(Source Source) => Source.SourceCode;

		public Source(FileStream File) {
			using (StreamReader Reader = new StreamReader(File)) {
				String Line;
				List<String> Lines = new List<String>();
				while ((Line = Reader.ReadLine()) != null) {
					Lines.Add(Line);
				}
				SourceCode = String.Join('\n', Lines);
			}
		}

		public Source(String FileName) : this(new FileStream(FileName, FileMode.Open)) {
			// Everything necessary should happen through chaining
		}
	}
}
