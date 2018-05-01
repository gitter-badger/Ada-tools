using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class SourceTests {

		[TestMethod]
		public void Constructor() {
			Source Spec = new Source("Both.ads");
			Source Body = new Source("Both.adb");
			Source Prog = new Source("Proc.adb");
		}

		[TestMethod]
		public void Match() {
			Source Spec = new Source("Both.ads");
			Assert.AreEqual("with Ada.Text_IO, Spec;", Spec.Match(new Regex(@"with.*;", RegexOptions.IgnoreCase)));
			Assert.AreEqual("package Both", Spec.Match(new Regex(@"package\s+(\w|\.|_)+\b", RegexOptions.IgnoreCase)));
		}

		[TestMethod]
		public void ParseDependencies() {
			Assert.AreEqual("Ada.Text_IO,Spec", String.Join(',', new Source("Both.ads").ParseDependencies()));
			Assert.AreEqual("Intr", String.Join(',', new Source("Proc.adb").ParseDependencies()));
		}

		[TestMethod]
		public void ParseName() {
			Assert.AreEqual("Both", new Source("Both.ads").ParseName());
			Assert.AreEqual("Both", new Source("Both.adb").ParseName());
			Assert.AreEqual("Proc", new Source("Proc.adb").ParseName());

		}

		[TestMethod]
		public void ParsePurity() {
			Assert.IsFalse(new Source("Both.ads").ParsePurity());
			Assert.IsTrue(new Source("Pure.ads").ParsePurity());
		}

		[TestMethod]
		public void ParseSourceType() {
			Assert.AreEqual(SourceType.Package, new Source("Both.ads").ParseSourceType());
			Assert.AreEqual(SourceType.Package, new Source("Both.adb").ParseSourceType());
			Assert.AreEqual(SourceType.Program, new Source("Proc.adb").ParseSourceType());
		}

	}
}
