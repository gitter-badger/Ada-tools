using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdaTools;

namespace AdaToolsTests {
	[TestClass]
	public class TypesTests {

		[TestMethod]
		public void Constructor() {
			TypesCollection Types = new TypesCollection(new SignedType("Integer"));
			Assert.AreEqual(1, Types.Count);
		}

		[TestMethod]
		public void Add() {
			TypesCollection Types = new TypesCollection(new SignedType("Integer"));
			Assert.AreEqual(1, Types.Count);
			Types.Add(new FloatType("Float", 8));
			Assert.AreEqual(2, Types.Count);
			Types.Add(new FloatType("Float", 8, 0.0, 8.0)); // This should join, not add
			Assert.AreEqual(2, Types.Count);
		}

		[TestMethod]
		public void AddGroup() {
			TypesCollection Types = new TypesCollection();
			Types.Add(new SignedType("Int1"), new SignedType("Int2"));
			Assert.AreEqual(2, Types.Count);
		}

		[TestMethod]
		public void Combine() {
			TypesCollection Types1 = new TypesCollection(new SignedType("Int1"), new SignedType("Int2"));
			TypesCollection Types2 = new TypesCollection(new ModularType("Mod1"), new ModularType("Mod2"));
			TypesCollection CombinedTypes = new TypesCollection();
			Assert.AreEqual(0, CombinedTypes.Count);
			CombinedTypes.Add(Types1);
			Assert.AreEqual(2, CombinedTypes.Count);
			CombinedTypes.Add(Types2);
			Assert.AreEqual(4, CombinedTypes.Count);
			CombinedTypes.Add();
			Assert.AreEqual(4, CombinedTypes.Count);
			CombinedTypes.Add(new TypesCollection());
			Assert.AreEqual(4, CombinedTypes.Count);
		}

		[TestMethod]
		public void Indexer() {
			SignedType Integer = new SignedType("Integer");
			ModularType Modular = new ModularType("Modular", 2 ^ 32);
			TypesCollection Types = new TypesCollection(Integer, Modular);
			Assert.AreEqual(Integer, Types["Integer"]);
			Assert.AreEqual(Modular, Types["Modular"]);
			Types.Add(new FloatType("Float", 8));
			Assert.AreEqual(new FloatType("Float", 8), Types["Float"]);
			Types.Add(new FloatType("Float", 8, 0.0, 8.0)); // This should join, not add
			Assert.AreNotEqual(new FloatType("Float", 8), Types["Float"]);
			Assert.AreEqual(new FloatType("Float", 8, 0.0, 8.0), Types["Float"]);
		}

	}
}
