using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Holds all package units not part of a project
	/// </summary>
	/// <remarks>
	/// <para>This is used for holding references to all units that are part of installed packages, and as such, would not belong to the project</para>
	/// <para>This does not actually hold everything installed. Rather, it holds all installed units that have been registered. This is mostly for efficiency reasons, as registering every single installed unit would require parsing every single installed unit and creating an absolutely huge hashset.</para>
	/// </remarks>
	public static class Library {

		/// <summary>
		/// Holds the actual units registered with the library
		/// </summary>
		/// <remarks>
		/// This automatically includes a definition for Standard, which doesn't actually exist as code
		/// </remarks>
		public static readonly HashSet<PackageUnit> Units = new HashSet<PackageUnit> {
			PackageUnit.Standard()
		};

		/// <summary>
		/// Whether the library has the <paramref name="Unit"/> registered
		/// </summary>
		/// <param name="Unit">Unit to check</param>
		/// <returns>True if registered, false otherwise</returns>
		public static Boolean Contains(PackageUnit Unit) => Units.Contains(Unit);

		/// <summary>
		/// Whether the library has a unit with <paramref name="Name"/> registered
		/// </summary>
		/// <param name="Name">Name of the unit to check</param>
		/// <returns>True if registered, false otherwise</returns>
		public static Boolean Contains(String Name) {
			foreach (Unit Unit in Units) {
				if (Name.ToUpper() == Unit.Name.ToUpper()) return true;
			}
			return false;
		}


		/// <summary>
		/// Look up the <paramref name="Unit"/>
		/// </summary>
		/// <param name="Unit">Unit to look up</param>
		/// <returns>Returns the unit, null if not registered</returns>
		public static PackageUnit Lookup(PackageUnit Unit) {
			foreach (PackageUnit U in Units) {
				if (U == Unit) return U;
			}
			return null;
		}

		/// <summary>
		/// Look up the package with <paramref name="Name"/>
		/// </summary>
		/// <param name="Name">Name of the unit to look up</param>
		/// <returns>Returns the unit, null if not registered</returns>
		public static PackageUnit Lookup(String Name) {
			foreach (PackageUnit Unit in Units) {
				if (Name.ToUpper() == Unit.Name.ToUpper()) return Unit;
			}
			return null;
		}


		/// <summary>
		/// Register the unit with the library
		/// </summary>
		/// <param name="Unit">Unit to register</param>
		public static void Register(PackageUnit Unit) {
			if (Unit is null) return;
			if (!Contains(Unit)) {
				Units.Add(Unit);
			}
		}

	}
}
