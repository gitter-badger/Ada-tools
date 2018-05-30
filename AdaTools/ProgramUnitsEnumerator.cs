using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public class ProgramUnitsEnumerator : IEnumerator, IEnumerator<ProgramUnit> {

		private readonly ProgramUnit[] Units;

		private Int32 Index = -1;

		Object IEnumerator.Current { get => this.Units[this.Index]; }

		ProgramUnit IEnumerator<ProgramUnit>.Current { get => this.Units[this.Index]; }

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

		public ProgramUnitsEnumerator(List<ProgramUnit> Units) {
			this.Units = Units.ToArray();
		}

	}
}
