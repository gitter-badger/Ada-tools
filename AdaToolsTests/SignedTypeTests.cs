using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class SignedTypeTests {

		[TestMethod]
		public void Constructor() {
			SignedType Signed = new SignedType("Integer", new Range<Int32>(-2 ^ 31, 2 ^ 31 - 1));
		}

		[TestMethod]
		public void Join() {
			SignedType Signed = new SignedType("Integer");
			Signed.Join(new SignedType("Integer", new Range<Int32>(-2 ^ 31, 2 ^ 31 - 1)));
			Assert.AreEqual(new Range<Int64>(-2 ^ 31, 2 ^ 31 - 1), Signed.Range);
		}

	}
}
