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
			Assert.AreEqual("Prog", new ProgramUnit("Prog").Name);
		}

		[TestMethod]
		public void Type() {
			Assert.AreEqual(ProgramType.Procedure, new ProgramUnit("Prog").Type);
		}

		[TestMethod]
		public void GetFiles() {
			Assert.AreEqual("Prog.adb", String.Join(',', new ProgramUnit("Prog").GetFiles()));
		}

		[TestMethod]
		public void LinkerArguments() {
			Assert.AreEqual("-lTest", new ProgramUnit("Prog").LinkerArguments);
		}

		[TestMethod]
		public void Dependencies() {
			ProgramUnit Program = new ProgramUnit("Prog");
			Assert.AreEqual("Ada.Text_IO,Test", String.Join(',', Program.Dependencies));
		}

	}
}
