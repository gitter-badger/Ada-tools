using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents an Ada project
	/// </summary>
	public sealed class Project {

		/// <summary>
		/// The name of the project
		/// </summary>
		/// <remarks>
		/// In most cases, this is the current working directory
		/// </remarks>
		public readonly String Name;

		/// <summary>
		/// The packages in this project
		/// </summary>
		public List<Package> Packages { get; private set; }

		/// <summary>
		/// Try to lookup the package by its name
		/// </summary>
		/// <param name="Name">The name to lookup</param>
		/// <returns>The package with the specified <paramref name="Name"/> if found, otherwise null</returns>
		public Package this[String Name] {
			get {
				foreach (Package Package in this.Packages) {
					if (Package.Name.ToLower() == Name.ToLower()) return Package;
				}
				return null;
			}
		}

		/// <summary>
		/// Initialize a project in the current directory
		/// </summary>
		public Project() : this(Directory.GetCurrentDirectory()) {
			// Everything necessary should happen through chaining
		}

		/// <summary>
		/// Initialize a project in the specified location
		/// </summary>
		/// <param name="Location">Location of the project</param>
		public Project(String Location) {
			this.Packages = new List<Package>();
			List<String> AdaSources = new List<String>();
			// Assign the project name to the current directory
			this.Name = new DirectoryInfo(Location).Name;
			// Iterate through the files, adding the names of Ada source files to a list
			foreach (String FileName in Directory.GetFiles(Location)) {
				String Extension = Path.GetExtension(FileName).ToLower();
				if (Extension == Package.SpecExtension || Extension == Package.BodyExtension) {
					if (AdaSources.Contains(Path.GetFileNameWithoutExtension(FileName))) continue; // Already exists, so don't add it again
					AdaSources.Add(Path.GetFileNameWithoutExtension(FileName));
				}
			}
			// Iterate through the list of Ada source files, creating package objects
			foreach (String Source in AdaSources) {
				String PackageName = Path.GetFileNameWithoutExtension(Source);
				this.Packages.Add(new Package(PackageName));
			}
		}
	}
}
