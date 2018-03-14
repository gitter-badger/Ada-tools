using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class ProjectTests {

		[TestMethod]
		public void Constructor() {
			Project Project = new Project();
		}

		[TestMethod]
		public void Packages() {
			Project Project = new Project();
			List<Package> Packages = Project.Packages;
			Assert.AreEqual("Test", Packages[0].Name);
		}

		[TestMethod]
		public void Programs() {
			Project Project = new Project();
			List<Program> Programs = Project.Programs;
			Assert.AreEqual("Prog", Programs[0].Name);
		}

	}
}
