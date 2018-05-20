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
			List<PackageUnit> Packages = Project.Packages;
		}

		[TestMethod]
		public void Programs() {
			Project Project = new Project();
			List<ProgramUnit> Programs = Project.Programs;
		}

	}
}
