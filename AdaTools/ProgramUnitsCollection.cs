using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public sealed class ProgramUnitsCollection : UnitsCollection, ICollection<ProgramUnit> {

		private readonly new List<ProgramUnit> Collection;

		void ICollection<ProgramUnit>.Add(ProgramUnit Unit) => this.Add(Unit);

		internal void Add(ProgramUnit Unit) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			this.Collection.Add(Unit);
		}

		internal void Add(params ProgramUnit[] Units) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			foreach (ProgramUnit Unit in Units) {
				this.Add(Unit);
			}
		}

		void ICollection<ProgramUnit>.Clear() {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			this.Collection.Clear();
		}

		public Boolean Contains(ProgramUnit Unit) => this.Contains(Unit);

		void ICollection<ProgramUnit>.CopyTo(ProgramUnit[] Array, Int32 Index) => this.Collection.CopyTo(Array, Index);

		Boolean ICollection<ProgramUnit>.Remove(ProgramUnit Unit) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			return this.Collection.Remove(Unit);
		}

		public new ProgramUnit this[String Name] {
			get {
				foreach (ProgramUnit U in this.Collection) {
					if (U.Name.ToUpper() == Name.ToUpper()) return U;
				}
				return null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => new ProgramUnitsEnumerator(this.Collection);

		IEnumerator<ProgramUnit> IEnumerable<ProgramUnit>.GetEnumerator() => new ProgramUnitsEnumerator(this.Collection);

		private readonly Boolean Readonly = false;

		Boolean ICollection<ProgramUnit>.IsReadOnly { get => this.Readonly; }

		internal ProgramUnitsCollection() {
			this.Collection = new List<ProgramUnit>();
		}

	}
}
