using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class BuildPlanTests {

		[TestMethod]
		public void Constructor() {
			BuildPlan BuildPlan = new BuildPlan(new Project());
		}

		[TestMethod]
		public void Length() {
			BuildPlan BuildPlan = new BuildPlan(new Project());
			Assert.AreEqual(8, BuildPlan.Length);
		}

		[TestMethod]
		public void Plan() {
			BuildPlan BuildPlan = new BuildPlan(new Project());
			//TODO: Removed indexer because it didn't make semantic sense, now how to verify build order?
		}

	}
}
