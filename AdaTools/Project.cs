using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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
		/// Get the dependencies of the project as a whole, instead of individual unit dependencies
		/// </summary>
		public List<String> Dependencies {
			get {
				List<String> deps = new List<String>();
				foreach (Package Package in this.Packages) {
					deps.AddRange(Package.Dependencies);
				}
				return deps.Distinct().ToList();
			}
		}

		/// <summary>
		/// All the units in this project
		/// </summary>
		public List<Unit> Units { get; private set; }

		/// <summary>
		/// All the packages in this project
		/// </summary>
		public List<Package> Packages {
			get {
				List<Package> Result = new List<Package>();
				foreach (Unit U in this.Units) {
					if (U is Package) Result.Add(U as Package);
				}
				return Result;
			}
		}

		/// <summary>
		/// All the programs in this project
		/// </summary>
		public List<Program> Programs {
			get {
				List<Program> Result = new List<Program>();
				foreach (Unit U in this.Units) {
					if (U is Program) Result.Add(U as Program);
				}
				return Result;
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
			this.Units = new List<Unit>();
			List<String> AdaSources = new List<String>();
			// Assign the project name to the current directory
			this.Name = new DirectoryInfo(Location).Name;
			// Iterate through the files, adding the names of Ada source files to a list
			foreach (String FileName in Directory.GetFiles(Location)) {
				String Extension = Path.GetExtension(FileName).ToLower();
				if (Extension == Package.SpecExtension || Extension == Package.BodyExtension || Extension == Program.Extension) {
					Source Source = new Source(FileName);
					if (AdaSources.Contains(Path.GetFileNameWithoutExtension(FileName))) continue; // Already exists, so don't add it again
					String SourceName = Source.TryParseName();
					AdaSources.Add(Path.GetFileNameWithoutExtension(FileName));
					SourceType SourceType = Source.TryParseSourceType();
					switch (SourceType) {
						case SourceType.Package:
							this.Units.Add(new Package(SourceName));
							break;
						case SourceType.Program:
							this.Units.Add(new Program(SourceName));
							break;
						default:
							break;
					}
				}
			}
		}
	}
}
