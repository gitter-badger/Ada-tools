using System;
using AdaTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdaToolsTests {
	[TestClass]
	public class PackageTests {

		[TestMethod]
		public void Constructor() {
			PackageUnit Package = new PackageUnit("Both");
		}

		[TestMethod]
		public void Name() {
			Assert.AreEqual("Both", new PackageUnit("Both").Name);
		}
		
		[TestMethod]
		public void HasSpec() {
			Assert.IsTrue(new PackageUnit("Both").HasSpec);
		}

		[TestMethod]
		public void HasBody() {
			Assert.IsTrue(new PackageUnit("Both").HasBody);
		}

		[TestMethod]
		public void IsAllCallsRemote() {
			Assert.IsFalse(new PackageUnit("Both").IsAllCallsRemote);
		}

		[TestMethod]
		public void IsPure() {
			Assert.IsFalse(new PackageUnit("Both").IsPure);
			Assert.IsTrue(new PackageUnit("Pure").IsPure);
		}

		[TestMethod]
		public void IsRemoteCallInterface() {
			Assert.IsFalse(new PackageUnit("Both").IsRemoteCallInterface);
		}

		[TestMethod]
		public void LinkerArguments() {
			Assert.AreEqual("-lSpec -lBody -soname Both -o libBoth.so", new PackageUnit("Both").LinkerArguments);
		}

		[TestMethod]
		public void GetFiles() {
			Assert.AreEqual("Both.ads,Both.adb", String.Join(',', new PackageUnit("Both").GetFiles()));
		}

		[TestMethod]
		public void Dependencies() {
			Assert.AreEqual("", String.Join(",", new PackageUnit("Test").Dependencies));
		}
	}
}
