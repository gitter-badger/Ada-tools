using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class IndexTests {

		[TestMethod]
		public void Constructor() {
			SignedType SignedInt = new SignedType("SignedInt");
			SignedIndex SignedIndex = new SignedIndex(SignedInt, 1, 10);

			ModularType ModularInt = new ModularType("ModularInt");
			ModularIndex ModularIndex = new ModularIndex(ModularInt, 1, 10);
		}

	}
}
