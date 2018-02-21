﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AdaTools {
	/// <summary>
	/// Represents an Ada package
	/// </summary>
	/// <remarks>
	/// Holds traits about the package for easy analysis
	/// </remarks>
	public class Package {

		/// <summary>
		/// The name of the package
		/// </summary>
		public readonly String Name;

		/// <summary>
		/// The full list of packages this package depends on
		/// </summary>
		public readonly List<Package> Dependencies;

		public Package(String Name) {
			this.Name = Name;

			// We need to tollerate missing specs or bodies, as long as at least one is found.
			Source SpecSource;
			String SpecName = null;
			try {
				SpecSource = new Source(Name + SpecExtension);
				SpecName = TryParseName(SpecSource);
			} catch {
				SpecSource = null;
			}
			Source BodySource;
			String BodyName = null;
			try {
				BodySource = new Source(Name + BodyExtension);
				BodyName = TryParseName(BodySource);
			} catch {
				BodySource = null;
			}

			// Validate all the names match up
			if (SpecName != null && this.Name.ToLower() != SpecName.ToLower()) throw new PackageNameDoesNotMatchException();
			if (BodyName != null && this.Name.ToLower() != BodyName.ToLower()) throw new PackageNameDoesNotMatchException();
			if ((SpecName != null && BodyName != null) && SpecName.ToLower() != BodyName.ToLower()) throw new PackageNameDoesNotMatchException();
		}

		/// <summary>
		/// The file extension to use for package specifications
		/// </summary>
		public static String SpecExtension = ".ads";

		/// <summary>
		/// The file extension to use for package bodies
		/// </summary>
		public static String BodyExtension = ".adb";

		/// <summary>
		/// Try to parse the internal name of the package
		/// </summary>
		/// <param name="Source">Source code to use</param>
		/// <returns>The internal name, if any was found</returns>
		public static String TryParseName(Source Source) {
			String Candidate = null;
			// Try getting the name through a variety of means
			if (Candidate == null) Candidate = Source.Match(new Regex(@"\bpackage\s+(\w|\.|_)+\s+is", RegexOptions.IgnoreCase | RegexOptions.Multiline));
			if (Candidate == null) Candidate = Source.Match(new Regex(@"\bpackage\s+body\s(\w|\.|_)+\s+is", RegexOptions.IgnoreCase | RegexOptions.Multiline));
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
	}
}