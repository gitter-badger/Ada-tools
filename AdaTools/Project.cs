using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
		/// The projects configuration unit if one was found, null if no configuration exists
		/// </summary>
		public ConfigurationUnit Configuration { get; private set; }

		/// <summary>
		/// Get the dependencies of the project as a whole, instead of individual unit dependencies
		/// </summary>
		public List<String> Dependencies {
			get {
				List<String> deps = new List<String>();
				foreach (PackageUnit Package in this.Packages) {
					deps.AddRange(Package.Dependencies);
				}
				return deps.Distinct().ToList();
			}
		}

		/// <summary>
		/// All the units in this project
		/// </summary>
		public Units Units { get; private set; }

		/// <summary>
		/// All the packages in this project
		/// </summary>
		public List<PackageUnit> Packages {
			get {
				List<PackageUnit> Result = new List<PackageUnit>();
				foreach (Unit U in this.Units) {
					if (U is PackageUnit) Result.Add(U as PackageUnit);
				}
				return Result;
			}
		}

		/// <summary>
		/// All the programs in this project
		/// </summary>
		public List<ProgramUnit> Programs {
			get {
				List<ProgramUnit> Result = new List<ProgramUnit>();
				foreach (Unit U in this.Units) {
					if (U is ProgramUnit) Result.Add(U as ProgramUnit);
				}
				return Result;
			}
		}

		/// <summary>
		/// All the types in this project
		/// </summary>
		public Types Types {
			get {
				Types Types = new Types();
				foreach (Unit U in this.Units) {
					Types.Add(U.Types);
				}
				return Types;
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
			this.Units = new Units();
			List<String> AdaSources = new List<String>();
			// Assign the project name to the current directory
			this.Name = new DirectoryInfo(Location).Name;
			// Iterate through the files, adding the names of Ada source files to a list
			foreach (String FileName in Directory.GetFiles(Location)) {
				String Extension = Path.GetExtension(FileName).ToLower();
				if (Extension == PackageUnit.SpecExtension || Extension == PackageUnit.BodyExtension || Extension == ProgramUnit.Extension) {
					Source Source = new Source(FileName);
					if (AdaSources.Contains(Path.GetFileNameWithoutExtension(FileName))) continue; // Already exists, so don't add it again
					String SourceName = Source.ParseName();
					AdaSources.Add(Path.GetFileNameWithoutExtension(FileName));
					SourceType SourceType = Source.ParseSourceType();
					switch (SourceType) {
						case SourceType.Package:
							this.Units.Add(new PackageUnit(SourceName));
							break;
						case SourceType.Program:
							this.Units.Add(new ProgramUnit(SourceName));
							break;
						default:
							break;
					}
				} else if (Extension == ConfigurationUnit.Extension) {
					this.Configuration = new ConfigurationUnit();
				}
			}
		}
	}
}
