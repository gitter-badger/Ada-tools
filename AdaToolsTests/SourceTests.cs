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
			Source Spec = new Source("Test.ads");
			Source Body = new Source("Test.adb");
			Source Prog = new Source("Prog.adb");
		}

		[TestMethod]
		public void Match() {
			Source Spec = new Source("Test.ads");
			Assert.AreEqual("with Ada.Text_IO, Ada.Characters.Latin_1;", Spec.Match(new Regex(@"with.*;", RegexOptions.IgnoreCase)));
			Assert.AreEqual("package Test is", Spec.Match(new Regex(@"package\s+(\w|\.|_)+\s+is", RegexOptions.IgnoreCase)));
		}

		[TestMethod]
		public void ParseDependencies() {
			Assert.AreEqual("Ada.Text_IO,Ada.Characters.Latin_1,Ada.Characters.Handling", String.Join(',', new Source("Test.ads").ParseDependencies()));
			Assert.AreEqual("", String.Join(',', new Source("Test.adb").ParseDependencies()));
			Assert.AreEqual("Ada.Text_IO,Test", String.Join(',', new Source("Prog.adb").ParseDependencies()));
		}

		[TestMethod]
		public void ParseName() {
			Assert.AreEqual("Test", new Source("Test.ads").ParseName());
			Assert.AreEqual("Test", new Source("Test.adb").ParseName());
			Assert.AreEqual("Prog", new Source("Prog.adb").ParseName());

		}

		[TestMethod]
		public void ParsePurity() {
			Assert.IsFalse(new Source("Test.ads").ParsePurity());
			Assert.IsTrue(new Source("Pure.ads").ParsePurity());
		}

		[TestMethod]
		public void ParseSourceType() {
			Assert.AreEqual(SourceType.Package, new Source("Test.ads").ParseSourceType());
			Assert.AreEqual(SourceType.Package, new Source("Test.adb").ParseSourceType());
			Assert.AreEqual(SourceType.Program, new Source("Prog.adb").ParseSourceType());
		}

	}
}
