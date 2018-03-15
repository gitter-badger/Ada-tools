using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace AdaToolsTests
{
	[TestClass]
	public class ProgramTests {

		[TestMethod]
		public void Constructor() {
			ProgramUnit Program = new ProgramUnit("Prog");
		}

		[TestMethod]
		public void Name() {
			ProgramUnit Program = new ProgramUnit("Prog");
			Assert.AreEqual("Prog", Program.Name);
		}

		[TestMethod]
		public void Dependencies() {
			ProgramUnit Program = new ProgramUnit("Prog");
			Assert.AreEqual("Ada.Text_IO", String.Join(',', Program.Dependencies));
		}

	}
}
