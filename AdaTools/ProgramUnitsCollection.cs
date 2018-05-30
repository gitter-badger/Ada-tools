using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public sealed class ProgramUnitsCollection : IEnumerable<ProgramUnit> {

		private List<ProgramUnit> Collection;

		internal void Add(ProgramUnit Unit) {
			this.Collection.Add(Unit);
		}

		internal void Add(params ProgramUnit[] Units) {
			foreach (ProgramUnit Unit in Units) {
				this.Add(Unit);
			}
		}

		public Boolean Contains(ProgramUnit Unit) => this.Contains(Unit);

		public Int32 Count { get => this.Collection.Count; }

		public ProgramUnit this[String Name] {
			get {
				foreach (ProgramUnit U in this.Collection) {
					if (U.Name.ToUpper() == Name.ToUpper()) return U;
				}
				return null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => new ProgramUnitsEnumerator(this.Collection);

		IEnumerator<ProgramUnit> IEnumerable<ProgramUnit>.GetEnumerator() => new ProgramUnitsEnumerator(this.Collection);

		internal ProgramUnitsCollection() {
			this.Collection = new List<ProgramUnit>();
		}

	}
}
