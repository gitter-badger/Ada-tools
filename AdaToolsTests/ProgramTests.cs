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
			Program Program = new Program("Prog");
		}

		[TestMethod]
		public void Name() {
			Program Program = new Program("Prog");
			Assert.AreEqual("Prog", Program.Name);
		}

		[TestMethod]
		public void Dependencies() {
			Program Program = new Program("Prog");
			Assert.AreEqual("Ada.Text_IO", String.Join(',', Program.Dependencies));
		}

	}
}
