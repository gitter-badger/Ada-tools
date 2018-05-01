using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
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
			Assert.AreEqual(7, BuildPlan.Length);
		}

		[TestMethod]
		public void Plan() {
			BuildPlan BuildPlan = new BuildPlan(new Project());
			Assert.AreEqual("Body", BuildPlan[0].Name);
			Assert.AreEqual("Spec", BuildPlan[1].Name);
			Assert.AreEqual("Both", BuildPlan[2].Name);
		}

	}
}
