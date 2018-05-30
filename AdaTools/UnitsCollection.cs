using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a collection of units
	/// </summary>
	/// <remarks>
	/// <para>This is used to hold all the units found within a project. Collecting them all here provides additional semantics possible upon the collection over just a simple list</para>
	/// </remarks>
	public class UnitsCollection : IEnumerable<Unit> {

		public readonly PackageUnitsCollection Packages;

		public readonly ProgramUnitsCollection Programs;

		/// <summary>
		/// Add the <paramref name="Unit"/> to the collection
		/// </summary>
		/// <param name="Unit">Unit to add</param>
		public void Add(Unit Unit) {
			if (Unit is null) return;
			switch (Unit) {
				case PackageUnit PackageUnit:
					this.Packages.Add(PackageUnit);
					break;
				case ProgramUnit ProgramUnit:
					this.Programs.Add(ProgramUnit);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Add the <paramref name="Units"/> to the collection
		/// </summary>
		/// <param name="Units">Array of Units to add</param>
		public void Add(params Unit[] Units) {
			foreach (Unit Unit in Units) {
				this.Add(Unit);
			}
		}

		public Boolean Contains(Unit Unit) {
			switch (Unit) {
				case PackageUnit PackageUnit:
					if (this.Packages.Contains(PackageUnit)) return true;
					break;
				case ProgramUnit ProgramUnit:
					if (this.Programs.Contains(ProgramUnit)) return true;
					break;
				default:
					break;
			}
			return false;
		}

		public Int32 Count { get => this.Packages.Count + this.Programs.Count; }

		/// <summary>
		/// Look up the unit by <paramref name="Name"/>
		/// </summary>
		/// <param name="Name">Name of the unit to look up</param>
		/// <returns>The unit if found, null otherwise</returns>
		public Unit this[String Name] {
			get {
				return (this.Packages[Name] as Unit) ?? (this.Programs[Name] as Unit) ?? null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => new UnitsEnumerator(this.Packages, this.Programs);

		IEnumerator<Unit> IEnumerable<Unit>.GetEnumerator() => new UnitsEnumerator(this.Packages, this.Programs);

		public UnitsCollection() {
			this.Packages = new PackageUnitsCollection();
			this.Programs = new ProgramUnitsCollection();
		}

		public UnitsCollection(params Unit[] Units) : this() {
			this.Add(Units);
		}

	}
}
