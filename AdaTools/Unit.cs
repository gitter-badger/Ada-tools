using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdaTools {
	/// <summary>
	/// Represents any of the various Ada units that can exist
	/// </summary>
	public abstract class Unit : IComparable<Unit> {

		/// <summary>
		/// The name of this unit
		/// </summary>
		public readonly String Name;

		/// <summary>
		/// The full list of package names this unit depends on
		/// </summary>
		public readonly List<String> Dependencies = new List<String>();

		/// <summary>
		/// Return the library dependency argument section for this unit
		/// </summary>
		public virtual String DependencyArguments {
			get {
				String Result = " ";
				foreach (String Dependency in this.Dependencies) {
					// Ada standard libraries are included anyways, so don't even bother
					if (new Regex(@"^(ada|interfaces|system)\b", RegexOptions.IgnoreCase).IsMatch(Dependency)) continue;
					// Add the linker dependency argument
					Result += "-l" + Dependency + " ";
				}
				return Result;
			}
		}

		/// <summary>
		/// Return the linker argument section for this unit
		/// </summary>
		public virtual String LinkerArguments {
			get {
				return this.DependencyArguments;
			}
		}

		/// <summary>
		/// Return the output argument section for this unit
		/// </summary>
		public virtual String OutputArguments {
			get => " -o " + this.Name + ' ';
		}

		/// <summary>
		/// Whether the unit has a specification
		/// </summary>
		public abstract Boolean HasSpec { get; protected set; }

		/// <summary>
		/// Whether the unit has a body
		/// </summary>
		public abstract Boolean HasBody { get; protected set; }
		
		/// <summary>
		/// Whether the unit makes entirely remote calls
		/// </summary>
		public abstract Boolean IsAllCallsRemote { get; protected set; }

		/// <summary>
		/// Whether the unit is pure
		/// </summary>
		public abstract Boolean IsPure { get; protected set; }

		/// <summary>
		/// Whether the unit is a remote call interface
		/// </summary>
		public abstract Boolean IsRemoteCallInterface { get; protected set; }

		public TypesCollection Types { get; protected set; }

		/// <summary>
		/// Get all associated files of this unit
		/// </summary>
		/// <returns>An array of the file names</returns>
		public abstract String[] GetFiles();
		
		public Int32 CompareTo(Unit Unit) {
			if (Unit is null) return 0;
			if (!this.Dependencies.Contains(Unit.Name) && !Unit.Dependencies.Contains(this.Name)) {
				// Neither lists each other as a dependency, so order does not matter
				return 0;
			} else if (this.Dependencies.Contains(Unit.Name) && !Unit.Dependencies.Contains(this.Name)) {
				// This unit depends on the specified unit, so the specified unit must come first
				return 1;
			} else if (!this.Dependencies.Contains(Unit.Name) && Unit.Dependencies.Contains(this.Name)) {
				// This unit is a dependency of the specified unit, so this unit must come first
				return -1;
			} else {
				// Each unit depends on each other, which isn't allowable
				// GNAT would catch this anyways, but it saves wasting time by trying anyways
				throw new CircularDependencyException(this.Name + " depends on " + Unit.Name + " which depends on " + this.Name);
			}
		}

		/// <summary>
		/// Does this unit depend on a package with the specified <paramref name="Name"/>?
		/// </summary>
		/// <param name="Name">Name of the unit</param>
		/// <returns>True if a dependency, false otherwise</returns>
		public Boolean DependsOn(String Name) => this.Dependencies.Contains(Name);

		/// <summary>
		/// Does this unit depend on the specified <paramref name="Unit"/>?
		/// </summary>
		/// <param name="Unit">Unit to compare to this one</param>
		/// <returns>True if a dependency, false otherwise</returns>
		public Boolean DependsOn(Unit Unit) => this.Dependencies.Contains(Unit.Name);

		public override Int32 GetHashCode() => this.Name.GetHashCode();

		protected Unit(String Name) {
			this.Name = Name;
		}

	}
}
