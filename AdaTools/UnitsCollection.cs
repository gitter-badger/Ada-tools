using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a collection of abstract units
	/// </summary>
	public class UnitsCollection : IEnumerable<Unit>, ICollection<Unit> {

		public readonly List<Unit> Collection;

		/// <summary>
		/// Add the <paramref name="Unit"/> to the collection
		/// </summary>
		/// <param name="Unit">Unit to add</param>
		public void Add(Unit Unit) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			this.Collection.Add(Unit);
		}

		/// <summary>
		/// Add the <paramref name="Units"/> to the collection
		/// </summary>
		/// <param name="Units">Array of Units to add</param>
		public void Add(params Unit[] Units) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			foreach (Unit Unit in Units) {
				this.Add(Unit);
			}
		}

		void ICollection<Unit>.Clear() {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			this.Collection.Clear();
		}

		public Boolean Contains(Unit Unit) => this.Collection.Contains(Unit);

		void ICollection<Unit>.CopyTo(Unit[] Array, Int32 Index) => this.Collection.CopyTo(Array, Index);

		public Int32 Count { get => this.Collection.Count; }

		Boolean ICollection<Unit>.Remove(Unit Unit) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			return this.Collection.Remove(Unit);
		}

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

		private readonly Boolean Readonly = false;

		Boolean ICollection<Unit>.IsReadOnly { get => this.Readonly; }

		public UnitsCollection() {
			this.Collection = new List<Unit>();
		}

		public UnitsCollection(params Unit[] Units) : this() {
			this.Add(Units);
		}

	}
}
