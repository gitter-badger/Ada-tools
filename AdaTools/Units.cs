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
	public class Units : IEnumerable<Unit> {

		private List<Unit> Collection;

		/// <summary>
		/// Add the <paramref name="Unit"/> to the collection
		/// </summary>
		/// <param name="Unit">Unit to add</param>
		public void Add(Unit Unit) {
			if (Unit is null) return;
			this.Collection.Add(Unit);
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

		public Boolean Contains(Unit Unit) => this.Collection.Contains(Unit);

		public Int32 Count { get => this.Collection.Count; }

		/// <summary>
		/// Look up the unit by <paramref name="Name"/>
		/// </summary>
		/// <param name="Name">Name of the unit to look up</param>
		/// <returns>The unit if found, null otherwise</returns>
		public Unit this[String Name] {
			get {
				foreach (Unit U in this.Collection) {
					if (U.Name.ToUpper() == Name.ToUpper()) return U;
				}
				return null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => new UnitsEnumerator(this.Collection);

		IEnumerator<Unit> IEnumerable<Unit>.GetEnumerator() => new UnitsEnumerator(this.Collection);

		public Units() {
			this.Collection = new List<Unit>();
		}

		public Units(params Unit[] Units) {
			this.Collection = new List<Unit>(Units);
		}

		public Units(IEnumerable<Unit> Units) {
			this.Collection = new List<Unit>(Units);
		}

		public Units(List<Unit> Units) {
			this.Collection = Units;
		}

	}
}
