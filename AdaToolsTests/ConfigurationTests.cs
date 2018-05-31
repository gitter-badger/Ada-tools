using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class ConfigurationTests {

		[TestMethod]
		public void Constructor() {
			ConfigurationUnit ConfigurationUnit = new ConfigurationUnit();
		}

		[TestMethod]
		public void CheckAdaVersion() {
			Assert.AreEqual(AdaVersion.Ada2012, new ConfigurationUnit().AdaVersion);
		}

		[TestMethod]
		public void CheckAssertionPolicy() {
			Assert.AreEqual(PolicyIdentifier.Check, new ConfigurationUnit().AssertionPolicy.GlobalPolicy);
		}

	}
}
