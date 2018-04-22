using System;
using AdaTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdaToolsTests {
	[TestClass]
	public class PackageTests {

		[TestMethod]
		public void Constructor() {
			PackageUnit Package = new PackageUnit("Test");
		}

		[TestMethod]
		public void Name() {
			Assert.AreEqual("Test", new PackageUnit("Test").Name);
		}
		
		[TestMethod]
		public void HasSpec() {
			Assert.IsTrue(new PackageUnit("Test").HasSpec);
		}

		[TestMethod]
		public void HasBody() {
			Assert.IsTrue(new PackageUnit("Test").HasBody);
		}

		[TestMethod]
		public void IsAllCallsRemote() {
			Assert.IsFalse(new PackageUnit("Test").IsAllCallsRemote);
		}

		[TestMethod]
		public void IsPure() {
			Assert.IsFalse(new PackageUnit("Test").IsPure);
			Assert.IsTrue(new PackageUnit("Pure").IsPure);
		}

		[TestMethod]
		public void IsRemoteCallInterface() {
			Assert.IsFalse(new PackageUnit("Test").IsRemoteCallInterface);
		}

		[TestMethod]
		public void LinkerArguments() {
			Assert.AreEqual("", new PackageUnit("Test").LinkerArguments);
		}

		[TestMethod]
		public void GetFiles() {
			Assert.AreEqual("Test.ads,Test.adb", String.Join(',', new PackageUnit("Test").GetFiles()));
		}

		[TestMethod]
		public void Dependencies() {
			Assert.AreEqual("Ada.Text_IO,Ada.Characters.Latin_1,Ada.Characters.Handling", String.Join(",", new PackageUnit("Test").Dependencies));
		}
	}
}
