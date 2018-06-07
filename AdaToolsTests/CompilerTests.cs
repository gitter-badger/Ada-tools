using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class CompilerTests {

		[TestMethod]
		public void Krunch() {
			Assert.AreEqual("a-assert.adb", Compiler.Krunch("Ada.Assertions.adb"));
			Assert.AreEqual("contlist.ads", Compiler.Krunch("Containers.Lists.ads"));
		}

	}
}
