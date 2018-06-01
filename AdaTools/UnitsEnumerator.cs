using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public class UnitsEnumerator : IEnumerator, IEnumerator<Unit> {

		private readonly Unit[] Units;

		private Int32 Index = -1;

		Object IEnumerator.Current { get => this.Units[this.Index]; }

		Unit IEnumerator<Unit>.Current { get => this.Units[this.Index]; }

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

		public UnitsEnumerator(List<Unit> Units) {
			this.Units = Units.ToArray();
		}

	}
}
