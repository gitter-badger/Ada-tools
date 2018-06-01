using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public sealed class PackageUnitsCollection : UnitsCollection, ICollection<PackageUnit> {

		private readonly new List<PackageUnit> Collection;

		void ICollection<PackageUnit>.Add(PackageUnit Unit) => this.Add(Unit);

		internal void Add(PackageUnit Unit) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			this.Collection.Add(Unit);
		}

		internal void Add(params PackageUnit[] Units) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			foreach (PackageUnit Unit in Units) {
				this.Add(Unit);
			}
		}

		void ICollection<PackageUnit>.Clear() {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			this.Collection.Clear();
		}

		public Boolean Contains(PackageUnit Unit) => this.Contains(Unit);

		void ICollection<PackageUnit>.CopyTo(PackageUnit[] Array, Int32 Index) => this.Collection.CopyTo(Array, Index);

		Boolean ICollection<PackageUnit>.Remove(PackageUnit Unit) {
			if (this.Readonly) throw new NotSupportedException("Collection is readonly");
			return this.Collection.Remove(Unit);
		}

		public new PackageUnit this[String Name] {
			get {
				foreach (PackageUnit U in this.Collection) {
					if (U.Name.ToUpper() == Name.ToUpper()) return U;
				}
				return null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => new PackageUnitsEnumerator(this.Collection);

		IEnumerator<PackageUnit> IEnumerable<PackageUnit>.GetEnumerator() => new PackageUnitsEnumerator(this.Collection);

		private readonly Boolean Readonly = false;

		Boolean ICollection<PackageUnit>.IsReadOnly { get => this.Readonly; }

		internal PackageUnitsCollection() {
			this.Collection = new List<PackageUnit>();
		}

	}
}
