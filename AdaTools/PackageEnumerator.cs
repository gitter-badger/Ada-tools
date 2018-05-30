using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public class PackageUnitsEnumerator : IEnumerator, IEnumerator<PackageUnit> {

		private readonly PackageUnit[] Units;

		private Int32 Index = -1;

		Object IEnumerator.Current { get => this.Units[this.Index]; }

		PackageUnit IEnumerator<PackageUnit>.Current { get => this.Units[this.Index]; }

		public void Dispose() { }

		public Boolean MoveNext() {
			this.Index++;
			if (this.Index < this.Units.Length) {
				return true;
			} else {
				return false;
			}
		}

		public void Reset() => this.Index = -1;

		public PackageUnitsEnumerator(List<PackageUnit> Units) {
			this.Units = Units.ToArray();
		}

	}
}
