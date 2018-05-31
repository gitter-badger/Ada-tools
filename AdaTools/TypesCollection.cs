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
	public class TypesCollection : IEnumerable<Type> {

		private List<Type> Collection;

		/// <summary>
		/// Add the <paramref name="Type"/> definition to the collection, joining the definitions if one already exists
		/// </summary>
		/// <param name="Type">Type definition to add</param>
		public void Add(Type Type) {
			if (Type is null) return;
			foreach (Type T in this.Collection) {
				if (T == Type) {
					T.Join(Type);
					return;
				}
			}
			this.Collection.Add(Type);
		}

		public void Add(params Type[] Types) {
			if (Types is null) return;
			foreach (Type T in Types) {
				this.Add(T);
			}
		}

		public void Add(TypesCollection Types) {
			foreach (Type Type in Types.Collection) {
				this.Add(Type);
			}
		}

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
					if (T.Name.ToUpper() == Name.ToUpper()) return T;
				}
				return null;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => new TypesEnumerator(this.Collection);

		IEnumerator<Type> IEnumerable<Type>.GetEnumerator() => new TypesEnumerator(this.Collection);

		public TypesCollection() {
			this.Collection = new List<Type>();
		}

		public TypesCollection(params Type[] Types) {
			this.Collection = new List<Type>(Types);
		}

		public TypesCollection(IEnumerable<Type> Types) {
			this.Collection = new List<Type>(Types);
		}

		public TypesCollection(List<Type> Types) {
			this.Collection = Types;
		}
	}
}