using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public sealed class SubroutineUnitsCollection : UnitsCollection, ICollection<SubroutineUnit> {

		private readonly new List<SubroutineUnit> Collection;

		void ICollection<SubroutineUnit>.Add(SubroutineUnit Unit) => this.Add(Unit);

		internal void Add(SubroutineUnit Unit) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			this.Collection.Add(Unit);
		}

		internal void Add(params SubroutineUnit[] Units) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			foreach (SubroutineUnit Unit in Units) {
				this.Add(Unit);
			}
		}

		void ICollection<SubroutineUnit>.Clear() {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			this.Collection.Clear();
		}

		public Boolean Contains(SubroutineUnit Unit) => this.Contains(Unit);

		void ICollection<SubroutineUnit>.CopyTo(SubroutineUnit[] Array, Int32 Index) => this.Collection.CopyTo(Array, Index);

		Boolean ICollection<SubroutineUnit>.Remove(SubroutineUnit Unit) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			return this.Collection.Remove(Unit);
		}

		public new SubroutineUnit this[String Name] {
			get {
				foreach (SubroutineUnit U in this.Collection) {
					if (U.Name.ToUpper() == Name.ToUpper()) return U;
				}
				return null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => new SubroutineUnitsEnumerator(this.Collection);

		IEnumerator<SubroutineUnit> IEnumerable<SubroutineUnit>.GetEnumerator() => new SubroutineUnitsEnumerator(this.Collection);

		private readonly Boolean Readonly = false;

		Boolean ICollection<SubroutineUnit>.IsReadOnly { get => this.Readonly; }

		internal SubroutineUnitsCollection() {
			this.Collection = new List<SubroutineUnit>();
		}

	}
}
