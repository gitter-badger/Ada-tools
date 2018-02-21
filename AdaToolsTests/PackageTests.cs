using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdaToolsTests {
	[TestClass]
	public class PackageTests {

		[TestMethod]
		public void Constructor() {
			Package Package = new Package("Test");
		}

	}
}
