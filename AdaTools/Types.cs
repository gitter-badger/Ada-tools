﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents a collection of types
	/// </summary>
	/// <remarks>
	/// <para>This is used to hold all the types found within a unit, or within a project. Collecting them all enables easy updating of types when they are listed twice, for example, with private implementations of publicly visible types.</para>
	/// </remarks>
	public class Types : IEnumerable<Type> {

		private List<Type> Collection;

		/// <summary>
		/// Add the <paramref name="Type"/> definition to the collection, joining the definitions if one already exists
		/// </summary>
		/// <param name="Type">Type definition to add.</param>
		public void Add(Type Type) {
			Console.WriteLine("Types.Add()");
			if (Type is null) return;
			foreach (Type T in this.Collection) {
				if (T == Type) {
					T.Join(Type);
					return;
				}
			}
			this.Collection.Add(Type);
			Console.WriteLine("Count: " + this.Collection.Count);
		}

		public void Add(params Type[] Types) {
			Console.WriteLine(".Add(params Type[])");
			if (Types is null) return;
			foreach (Type T in Types) {
				this.Add(T);
			}
		}

		public void Add(IEnumerable<Type> Types) {
			Console.WriteLine(".Add(IEnumerable<Type>");
			if (Types is null) return;
			foreach (Type T in Types) {
				this.Add(T);
			}
		}

		public void Add(Types Types) => this.Add(Types?.Collection);

		public Boolean Contains(Type Type) => this.Collection.Contains(Type);

		public Int32 Count { get => this.Collection.Count; }

		/// <summary>
		/// Look up the type by <paramref name="Name"/>
		/// </summary>
		/// <param name="Name">Name of the type to look up</param>
		/// <returns>The type if found, null otherwise</returns>
		public Type this[String Name] {
			get {
				foreach (Type T in this.Collection) {
					if (T.Name == Name) return T;
				}
				return null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			Console.WriteLine("this.Collection.Count: " + this.Collection.Count);
			return new TypesEnumerator(this.Collection);
		}

		IEnumerator<Type> IEnumerable<Type>.GetEnumerator() {
			Console.WriteLine("this.Collection.Count: " + this.Collection.Count);
			return new TypesEnumerator(this.Collection);
		}

		public Types() {
			this.Collection = new List<Type>();
		}

		public Types(params Type[] Types) {
			this.Collection = new List<Type>(Types);
		}

		public Types(IEnumerable<Type> Types) {
			this.Collection = new List<Type>(Types);
		}

		public Types(List<Type> Types) {
			this.Collection = Types;
		}
	}
}