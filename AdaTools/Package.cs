using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;

namespace AdaTools {
	/// <summary>
	/// Represents a ready to install packaged Ada unit
	/// </summary>
	/// <remarks>
	/// <para>This is only intended to package up Ada packages. Programs should be installed by the system package manager. The intention is that the two tools should work together.</para>
	/// </remarks>
	public class Package {

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

		/// <summary>
		/// Create an install package from the specified package unit
		/// </summary>
		/// <param name="PackageUnit">Unit to package</param>
		public Package(PackageUnit PackageUnit, String Variant, String Description) {
			this.Name = PackageUnit.Name;
			this.Variant = Variant;
			this.Version = new Source(PackageUnit.GetSpec()).ParseVersion() ?? new Version(0, 0);
			this.Description = Description;
			this.Dependencies = PackageUnit.Dependencies;
		}

	}
}