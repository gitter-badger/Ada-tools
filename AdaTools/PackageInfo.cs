using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdaTools {
	/// <summary>
	/// Represents the info about a package
	/// </summary>
	/// <remarks>
	/// This is used both inside of the package itself, as well as the install record
	/// </remarks>
	public class PackageInfo {

		/// <summary>
		/// Name of the package
		/// </summary>
		/// <remarks>
		/// If packaging Mathematics.Arrays, this name would also be Mathematics.Arrays
		/// </remarks>
		public readonly String Name;

		/// <summary>
		/// Variant of the package
		/// </summary>
		/// <remarks>
		/// If packaging Mathematics.Arrays specific SIMD code for the SSE Instruction Set, this would be SSE
		/// If packaging Console for Windows, this would be Windows
		/// </remarks>
		public readonly String Variant;

		/// <summary>
		/// Version of the package
		/// </summary>
		public readonly Version Version;

		/// <summary>
		/// Description of the package
		/// </summary>
		/// <remarks>
		/// This should be the same as the comment description or summary on the package itself
		/// </remarks>
		public readonly String Description;

		/// <summary>
		/// Dependencies of the package
		/// </summary>
		/// <remarks>
		/// This is imported from the package unit
		/// </remarks>
		public readonly List<String> Dependencies;

		public override Int32 GetHashCode() => this.Name.GetHashCode() ^ (this.Variant?.GetHashCode() ?? 0) ^ this.Version.GetHashCode();

		public PackageInfo(String Name, Version Version, String Description, List<String> Dependencies) {
			this.Name = Name;
			this.Version = Version;
			this.Description = Description;
			this.Dependencies = Dependencies;
		}

		public PackageInfo(String Name, String Variant, Version Version, String Description, List<String> Dependencies) : this(Name, Version, Description, Dependencies) {
			this.Variant = Variant;
		}

		public PackageInfo(Stream Stream) {
			using (StreamReader Reader = new StreamReader(Stream)) {
				this.Name = Reader.ReadLine();
				this.Variant = Reader.ReadLine();
				this.Version = new Version(Reader.ReadLine());
				this.Description = Reader.ReadLine();
				this.Dependencies = new List<String>(Reader.ReadLine().Split(','));
			}
		}

		public PackageInfo(String Path) : this(new FileStream(Path, FileMode.Open)) {
		}
	}
}
