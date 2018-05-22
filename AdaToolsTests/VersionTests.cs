using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class VersionTests {

		[TestMethod]
		public void Constructor() {
			AdaTools.Version SimpleVersion = new AdaTools.Version(1, 0);
			Assert.AreEqual(1, SimpleVersion.Major);
			Assert.AreEqual(0, SimpleVersion.Minor);
			Assert.AreEqual(0, SimpleVersion.Patch);
			AdaTools.Version PatchVersion = new AdaTools.Version(1, 0, 1);
			Assert.AreEqual(1, PatchVersion.Major);
			Assert.AreEqual(0, PatchVersion.Minor);
			Assert.AreEqual(1, PatchVersion.Patch);
			AdaTools.Version StringVersion = new AdaTools.Version("1.0.1");
			Assert.AreEqual(1, StringVersion.Major);
			Assert.AreEqual(0, StringVersion.Minor);
			Assert.AreEqual(1, StringVersion.Patch);
		}

		[TestMethod]
		public void Equality() {
			Assert.AreEqual(new AdaTools.Version(1, 0), new AdaTools.Version(1, 0));
			Assert.AreEqual(new Version(1, 0, 0), new Version("1.0"));
			Assert.AreNotEqual(new Version(1, 0, 1), new Version(1, 0, 0));
		}

		[TestMethod]
		public void LesserThan() {
			Assert.IsTrue(new Version(1, 0) < new Version(2, 0));
			Assert.IsFalse(new Version(2, 0) < new Version(1, 0));
			Assert.IsFalse(new Version(1, 0, 1) < new Version(1, 0));
		}

		[TestMethod]
		public void GreaterThan() {
			Assert.IsTrue(new Version(2, 0) > new Version(1, 0));
			Assert.IsFalse(new Version(1, 0) > new Version(2, 0));
			Assert.IsTrue(new Version(1, 0, 1) > new Version(1, 0));
		}
	}
}