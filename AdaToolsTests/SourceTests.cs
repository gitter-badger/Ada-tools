using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
			Assert.AreEqual("with Ada.Text_IO, Spec;", Spec.Match(@"with\s+(\\.|[^;])*;"));
			Assert.AreEqual("package Both", Spec.Match(@"package\s+(\w|\.|_)+\b"));
		}

		[TestMethod]
		public void ParseDependencies() {
			Assert.AreEqual("Ada.Text_IO,Spec", String.Join(',', new Source("Both.ads").ParseDependencies()));
			Assert.AreEqual("Intr", String.Join(',', new Source("Proc.adb").ParseDependencies()));
		}

		[TestMethod]
		public void ParseDescription() {
			Assert.AreEqual("Just a simple test package", new Source("Both.ads").ParseDescription());
			Assert.AreEqual("A pure spec test package", new Source("Pure.ads").ParseDescription());
		}

		[TestMethod]
		public void ParseName() {
			Assert.AreEqual("Both", new Source("Both.ads").ParseName());
			Assert.AreEqual("Both", new Source("Both.adb").ParseName());
			Assert.AreEqual("Proc", new Source("Proc.adb").ParseName());

		}

		[TestMethod]
		public void ParseAdaVersion() {
			Assert.AreEqual(AdaVersion.Ada2012, new Source("gnat.adc").ParseAdaVersion());
		}

		[TestMethod]
		public void ParseAllCallsRemote() {
			Assert.AreEqual(false, new Source("Both.ads").ParseAllCallsRemote());
			Assert.AreEqual(true, new Source("Intr.ads").ParseAllCallsRemote());
		}

		[TestMethod]
		public void ParseAssertionPolicy() {
			Assert.AreEqual(PolicyIdentifier.Check, new Source("gnat.adc").ParseAssertionPolicy());
		}

		[TestMethod]
		public void ParseAssumeNoInvalidValues() {
			Assert.AreEqual(AssumeNoInvalidValues.Off, new Source("gnat.adc").ParseAssumeNoInvalidValues());
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
			Assert.AreEqual(SourceType.Separate, new Source("Get_Number.adb").ParseSourceType());
		}

		[TestMethod]
		public void ParseTypes() {
			TypesCollection Types = new Source("Both.ads").ParseTypes();
			Assert.AreEqual(new SignedType("TestInt", 0, 1), Types["TestInt"]);
			Assert.AreEqual(new ModularType("TestMod", 8), Types["TestMod"]);
			Assert.AreEqual(new FloatType("TestFloat", 8), Types["TestFloat"]);
			Assert.AreEqual(new FloatType("TestFloatRange", 8, 0.0, 12.0), Types["TestFloatRange"]);
			Assert.AreEqual(new OrdinaryType("TestFixed", 0.01, 0.0, 8.0), Types["TestFixed"]);
			Assert.AreEqual(new DecimalType("TestDecimal", 0.1m, 8), Types["TestDecimal"]);
			Assert.AreEqual(new DecimalType("TestDecimalRange", 0.1m, 8, 0.0m, 10.0m), Types["TestDecimalRange"]);
		}

		[TestMethod]
		public void ParseVersion() {
			Assert.AreEqual(new AdaTools.Version(1, 0), new Source("Both.ads").ParseVersion());
		}

	}
}
