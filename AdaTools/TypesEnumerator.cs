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
			Console.WriteLine("new TypesEnumerator");
			Console.WriteLine("Types.Count: " + Types.Count);
			this.Types = Types.ToArray();
			Console.WriteLine("this.Types.Count: " + this.Types.Length);
			foreach (Type Type in Types) {
				Console.WriteLine(Type.Name);
			}
		}

	}
}
