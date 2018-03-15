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
		public void TryParseDependencies() {
			Assert.AreEqual("Ada.Text_IO,Ada.Characters.Latin_1,Ada.Characters.Handling", String.Join(',', new Source("Test.ads").TryParseDependencies()));
			Assert.AreEqual("", String.Join(',', new Source("Test.adb").TryParseDependencies()));
			Assert.AreEqual("Ada.Text_IO", String.Join(',', new Source("Prog.adb").TryParseDependencies()));
		}

		[TestMethod]
		public void TryParseName() {
			Assert.AreEqual("Test", new Source("Test.ads").TryParseName());
			Assert.AreEqual("Test", new Source("Test.adb").TryParseName());
			Assert.AreEqual("Prog", new Source("Prog.adb").TryParseName());

		}

		[TestMethod]
		public void TryParsePurity() {
			Assert.IsFalse(new Source("Test.ads").TryParsePurity());
			Assert.IsTrue(new Source("Pure.ads").TryParsePurity());
		}

		[TestMethod]
		public void TryParseSourceType() {
			Assert.AreEqual(SourceType.Package, new Source("Test.ads").TryParseSourceType());
			Assert.AreEqual(SourceType.Package, new Source("Test.adb").TryParseSourceType());
			Assert.AreEqual(SourceType.Program, new Source("Prog.adb").TryParseSourceType());
		}

	}
}
