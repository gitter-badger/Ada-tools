using System;
using System.Collections.Generic;
using System.Text;

namespace AdaTools {
	public static class UnitPool {
		private static readonly UnitsCollection Units = new UnitsCollection();

		public static void Register(Unit Unit) => Units.Add(Unit);
	}
}
