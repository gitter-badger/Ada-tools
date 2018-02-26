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
		private List<Package> Packages { get; set; }


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
		public Project() {
			this.Name = Directory.GetCurrentDirectory();
		}

		/// <summary>
		/// Initialize a project in the specified location
		/// </summary>
		/// <param name="Location">Location of the project</param>
		public Project(String Location) {
			this.Name = Path.GetDirectoryName(Location);
		}
	}
}
