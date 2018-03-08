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

		[TestMethod]
		public void Name() {
			Package Package = new Package("Test");
			Assert.AreEqual("Test", Package.Name);
		}

		[TestMethod]
		public void HasSpec() {
			Package Package = new Package("Test");
			Assert.IsTrue(Package.HasSpec);
		}

		[TestMethod]
		public void HasBody() {
			Package Package = new Package("Test");
			Assert.IsTrue(Package.HasBody);
		}

		[TestMethod]
		public void Dependencies() {
			Package Package = new Package("Test");
			Assert.AreEqual("Ada.Text_IO,Ada.Characters.Latin_1,Ada.Characters.Handling", String.Join(",", Package.Dependencies));
		}
	}
}
