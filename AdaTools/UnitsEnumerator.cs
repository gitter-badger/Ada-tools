﻿using System;
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

		public UnitsEnumerator(PackageUnitsCollection PackageUnits, ProgramUnitsCollection ProgramUnits) {
			List<Unit> Units = new List<Unit>();
			foreach (Unit U in PackageUnits) {
				Units.Add(U);
			}
			foreach (Unit U in ProgramUnits) {
				Units.Add(U);
			}
			this.Units = Units.ToArray();
		}

	}
}