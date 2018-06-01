using System;
using System.Collections.Generic;
using System.Text;
using AdaTools;

namespace Cmdline {
	public static class Types {

		public static void Each() {
			foreach (AdaTools.Type T in new Project().Types) {
				Console.Write(T.Name + "  ");
			}
		}

		public static void Find(String TypeName, out AdaTools.Type Type, out Unit Unit) {
			foreach (Unit U in new Project().Units) {
				foreach (AdaTools.Type T in U.Types) {
					if (T.Name.ToUpper() == TypeName.ToUpper()) {
						Type = T;
						Unit = U;
						return;
					}
				}
			}
			foreach (Unit U in Library.Units) {
				foreach (AdaTools.Type T in U.Types) {
					if (T.Name.ToUpper() == TypeName.ToUpper()) {
						Type = T;
						Unit = U;
						return;
					}
				}
			}
			Type = null;
			Unit = null;
		}

		public static void FullHelp() {

		}

		public static void Help() {

		}

		public static void Info(String TypeName) {
			AdaTools.Type Type;
			Unit Unit;
			Find(TypeName, out Type, out Unit);
			if (Type is null) {
				Console.WriteLine("Couldn't find the type");
				return;
			} else {
				switch (Unit) {
					case PackageUnit PackageUnit:
						Console.WriteLine("In Package: " + PackageUnit.Name);
						break;
					case ProgramUnit ProgramUnit:
						Console.WriteLine("In Program: " + ProgramUnit.Name);
						break;
					default:
						Console.WriteLine("In Unit: " + Unit.Name);
						break;
				}
				switch (Type) {
					case AccessType AccessType:
						Console.WriteLine("Access to: " + AccessType.Accesses.Name);
						break;
					case ArrayType ArrayType:
						Console.WriteLine("Array of " + ArrayType.Of.Name);
						//TODO: Write out indices
						break;
					case DecimalType DecimalType:
						Console.WriteLine("Decimal Fixed Point");
						Console.WriteLine("Delta: " + DecimalType.Delta);
						Console.WriteLine("Digits: " + DecimalType.Digits);
						if (!(DecimalType.Range is null)) {
							Console.WriteLine("Range: " + DecimalType.Range);
						}
						break;
					case EnumerationType EnumerationType:
						Console.WriteLine("Enumeration");
						Console.WriteLine("Values: " + String.Join("  ", EnumerationType.Values));
						break;
					case FloatType FloatType:
						Console.WriteLine("Floating Point");
						Console.WriteLine("Digits: " + FloatType.Digits);
						if (!(FloatType.Range is null)) {
							Console.WriteLine("Range: " + FloatType.Range);
						}
						break;
					case ModularType ModularType:
						Console.WriteLine("Modular Integer");
						Console.WriteLine("Modulus: " + ModularType.Modulus);
						break;
					case OrdinaryType OrdinaryType:
						Console.WriteLine("Ordinary Fixed Point");
						Console.WriteLine("Delta: " + OrdinaryType.Delta);
						Console.WriteLine("Range: " + OrdinaryType.Range);
						break;
					case SignedType SignedType:
						Console.WriteLine("Signed Integer");
						Console.WriteLine("Range: " + SignedType.Range);
						break;
					default:
						throw new Exception();
				}
			}
		}

		public static void Table() {
			Console.WriteLine(String.Format("{0,10} {1,4}", "   Kind   ", "Name"));
			Console.WriteLine(String.Format("{0,10} {1,4}", "----------", "----"));
			foreach (AdaTools.Type Type in new Project().Types) {
				String Kind;
				switch (Type) {
					case AccessType AccessType:
						Kind = "  Access  ";
						break;
					case DecimalType DecimalType:
						Kind = "  Decimal ";
						break;
					case FloatType FloatType:
						Kind = "  Float   ";
						break;
					case ModularType ModularType:
						Kind = "  Modular ";
						break;
					case OrdinaryType OrdinaryType:
						Kind = "  Ordinal ";
						break;
					case SignedType SignedType:
						Kind = "  Signed  ";
						break;
					default:
						Kind = "";
						break;
				}
				Console.WriteLine(String.Format("{0,10} {1,4}", Kind, Type.Name));
			}
		}

	}
}
