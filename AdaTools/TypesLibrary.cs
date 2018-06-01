using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Holds all known types
	/// </summary>
	public static class TypesLibrary {

		/// <summary>
		/// Holds the actual types registered with the library
		/// </summary>
		//? This should probably be a Tree, but which kind?
		private static readonly List<Type> Types = new List<Type>();

		public static Boolean Contains(Type Type) => Types.Contains(Type);

		public static Boolean Contains(String Name) {
			foreach (Type Type in Types) {
				if (Name.ToUpper() == Type.Name.ToUpper()) return true;
			}
			return false;
		}

		public static Type Lookup(String Name) {
			foreach (Type Type in Types) {
				if (Name.ToUpper() == Type.Name.ToUpper()) return Type;
			}
			return null;
		}

		public static void Register(Type Type) {
			if (!Contains(Type)) {
				Types.Add(Type);
			}
		}

	}
}
