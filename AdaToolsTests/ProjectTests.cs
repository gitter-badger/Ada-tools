using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
			Assert.AreEqual(6, Packages.Count);
		}

		[TestMethod]
		public void Subroutines() {
			Project Project = new Project();
			List<SubroutineUnit> Subroutines = Project.Subroutines;
			Assert.AreEqual(1, Subroutines.Count);
		}

		[TestMethod]
		public void Programs() {
			Project Project = new Project();
			List<ProgramUnit> Programs = Project.Programs;
			Assert.AreEqual(1, Programs.Count);
		}

	}
}
