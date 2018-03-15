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
			PackageUnit Package = new PackageUnit("Test");
		}

		[TestMethod]
		public void Name() {
			PackageUnit Package = new PackageUnit("Test");
			Assert.AreEqual("Test", Package.Name);
		}

		[TestMethod]
		public void HasSpec() {
			PackageUnit Package = new PackageUnit("Test");
			Assert.IsTrue(Package.HasSpec);
		}

		[TestMethod]
		public void HasBody() {
			PackageUnit Package = new PackageUnit("Test");
			Assert.IsTrue(Package.HasBody);
		}

		[TestMethod]
		public void IsPure() {
			Assert.IsFalse(new PackageUnit("Test").IsPure);
			Assert.IsTrue(new PackageUnit("Pure").IsPure);
		}

		[TestMethod]
		public void Dependencies() {
			PackageUnit Package = new PackageUnit("Test");
			Assert.AreEqual("Ada.Text_IO,Ada.Characters.Latin_1,Ada.Characters.Handling", String.Join(",", Package.Dependencies));
		}
	}
}
