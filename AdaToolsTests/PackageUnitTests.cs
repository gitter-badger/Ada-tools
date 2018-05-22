using System;
using System.Diagnostics;
using AdaTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdaToolsTests {
	[TestClass]
	public class PackageUnitTests {

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
		public void DependencyArguments() {
			Assert.AreEqual(" -lSpec -lBody ", new PackageUnit("Both").DependencyArguments);
		}

		[TestMethod]
		public void LinkerArguments() {
			Assert.AreEqual(" -lSpec -lBody ", new PackageUnit("Both").LinkerArguments);
		}

		[TestMethod]
		public void OutputArguments() {
			if (Environment.OSVersion.Platform <= (PlatformID)3) {
				Assert.AreEqual(" -o Both.dll ", new PackageUnit("Both").OutputArguments);
			} else if (Environment.OSVersion.Platform == PlatformID.Unix) {
				Assert.AreEqual(" -o Both.so ", new PackageUnit("Both").OutputArguments);
			}
		}

		[TestMethod]
		public void GetFiles() {
			Assert.AreEqual("Both.adb,Both.ads", String.Join(',', new PackageUnit("Both").GetFiles()));
		}

		[TestMethod]
		public void Dependencies() {
			Assert.AreEqual("", String.Join(",", new PackageUnit("Test").Dependencies));
		}
	}
}
