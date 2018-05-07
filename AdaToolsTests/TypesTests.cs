using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class TypesTests {

		[TestMethod]
		public void Constructor() {
			Types Types = new Types(new SignedType("Integer"));
			Assert.AreEqual(1, Types.Count);
		}

		[TestMethod]
		public void Add() {
			Types Types = new Types(new SignedType("Integer"));
			Assert.AreEqual(1, Types.Count);
			Types.Add(new ModularType("Modular", 2 ^ 32));
			Assert.AreEqual(2, Types.Count);
		}

		[TestMethod]
		public void Indexer() {
			SignedType Integer = new SignedType("Integer");
			ModularType Modular = new ModularType("Modular", 2 ^ 32);
			Types Types = new Types(Integer, Modular);
			Assert.AreEqual(Integer, Types["Integer"]);
			Assert.AreEqual(Modular, Types["Modular"]);
		}

	}
}
