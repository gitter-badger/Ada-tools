using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public class TypesEnumerator : IEnumerator, IEnumerator<Type> {

		private readonly Type[] Types;

		private Int32 Index = -1;

		Object IEnumerator.Current { get => this.Types[this.Index]; }

		Type IEnumerator<Type>.Current { get => this.Types[this.Index]; }

		public void Dispose() { }

		public Boolean MoveNext() {
			this.Index++;
			if (this.Index < this.Types.Length) {
				return true;
			} else {
				return false;
			}
		}

		public void Reset() => this.Index = -1;

		public TypesEnumerator(List<Type> Types) {
			this.Types = Types.ToArray();
		}

	}
}
