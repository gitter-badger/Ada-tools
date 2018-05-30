using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public sealed class PackageUnitsCollection : IEnumerable<PackageUnit> {

		private List<PackageUnit> Collection;

		internal void Add(PackageUnit Unit) {
			this.Collection.Add(Unit);
		}

		internal void Add(params PackageUnit[] Units) {
			foreach (PackageUnit Unit in Units) {
				this.Add(Unit);
			}
		}

		public Boolean Contains(PackageUnit Unit) => this.Contains(Unit);

		public Int32 Count { get => this.Collection.Count; }

		public PackageUnit this[String Name] {
			get {
				foreach (PackageUnit U in this.Collection) {
					if (U.Name.ToUpper() == Name.ToUpper()) return U;
				}
				return null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => new PackageUnitsEnumerator(this.Collection);

		IEnumerator<PackageUnit> IEnumerable<PackageUnit>.GetEnumerator() => new PackageUnitsEnumerator(this.Collection);

		internal PackageUnitsCollection() {
			this.Collection = new List<PackageUnit>();
		}

	}
}
