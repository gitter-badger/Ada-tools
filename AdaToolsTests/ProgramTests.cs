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
			ProgramUnit Program = new ProgramUnit("Proc");
		}

		[TestMethod]
		public void Name() {
			Assert.AreEqual("Proc", new ProgramUnit("Proc").Name);
		}

		[TestMethod]
		public void Type() {
			Assert.AreEqual(ProgramType.Procedure, new ProgramUnit("Proc").Type);
		}

		[TestMethod]
		public void GetFiles() {
			Assert.AreEqual("Proc.adb", String.Join(',', new ProgramUnit("Proc").GetFiles()));
		}

		[TestMethod]
		public void LinkerArguments() {
			Assert.AreEqual(" -lIntr ", new ProgramUnit("Proc").LinkerArguments);
		}

		[TestMethod]
		public void Dependencies() {
			ProgramUnit Program = new ProgramUnit("Proc");
			Assert.AreEqual("Intr", String.Join(',', Program.Dependencies));
		}

		[TestMethod]
		public void DependsOn() {
			Assert.IsTrue(new ProgramUnit("Proc").DependsOn(new PackageUnit("Intr")));
			Assert.IsFalse(new ProgramUnit("Proc").DependsOn("Func"));
		}

	}
}
