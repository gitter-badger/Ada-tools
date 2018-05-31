using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AdaTools;


namespace AdaToolsTests {
	[TestClass]
	public class RangeTests {

		[TestMethod]
		public void Constructor() {
			Range<Int32> GoodIntRange = new Range<Int32>(1, 5);
			try {
				Range<Int32> BadIntRange = new Range<Int32>(5, 1);
				Assert.Fail();
			} catch (InvalidRangeException) {
			}
			Range<Double> GoodDoubleRange = new Range<Double>(1.0, 5.2);
			try {
				Range<Double> BadDoubleRange = new Range<Double>(5.2, 1.0);
				Assert.Fail();
			} catch (InvalidRangeException) {
			}
		}

		[TestMethod]
		public void Contains() {
			Assert.IsTrue(new Range<Int32>(1, 5).Contains(3));
			Assert.IsTrue(new Range<Int32>(1, 5).Contains(5));
			Assert.IsFalse(new Range<Int32>(1, 5).Contains(0));
			Assert.IsFalse(new Range<Int32>(1, 5).Contains(6));
		}

		[TestMethod]
		public void ContainsRange() {
			Assert.IsTrue(new Range<Int32>(1, 10).Contains(new Range<Int32>(1, 5)));
			Assert.IsFalse(new Range<Int32>(1, 10).Contains(new Range<Int32>(0, 5)));
		}

		[TestMethod]
		public void Equals() {
			Assert.IsTrue(new Range<Int32>(1, 5) == new Range<Int32>(1, 5));
			Assert.IsFalse(new Range<Int32>(1, 5) == new Range<Int32>(2, 10));
			Assert.IsTrue(new Range<Int64>(1, 5).Equals(new Range<Int64>(1, 5)));
		}

	}
}
