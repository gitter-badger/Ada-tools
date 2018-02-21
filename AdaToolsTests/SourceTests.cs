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
		}

		[TestMethod]
		public void Match() {
			Source Spec = new Source("Test.ads");
			Assert.AreEqual("with Ada.Text_IO, Ada.Characters.Latin_1;", Spec.Match(new Regex(@"with.*;", RegexOptions.IgnoreCase)));
			Assert.AreEqual("package Test is", Spec.Match(new Regex(@"package\s+(\w|\.|_)+\s+is", RegexOptions.IgnoreCase)));
		}

	}
}
