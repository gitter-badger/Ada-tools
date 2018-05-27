using System;
using System.Collections.Generic;
using System.IO;
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
		/// Create the package in the filesystem
		/// </summary>
		/// <param name="IncludeBody">Whether to include the body file or not. This is required for generic units and for native builds. Not including it makes the package closed source.</param>
		public void Create(Boolean IncludeBody = true) {
			// Figure out how to name the archive.
			String ArchiveName;
			if (this.Variant is null || this.Variant == "") {
				ArchiveName = this.Name + ".apkg";
			} else {
				ArchiveName = this.Name + "." + this.Variant + ".apkg";
			}
			// Create the actual archive and put everything necessary in it
			using (FileStream File = new FileStream(ArchiveName, FileMode.Create)) {
				using (ZipArchive Archive = new ZipArchive(File, ZipArchiveMode.Update)) {
					try {
						Archive.CreateEntryFromFile(this.Name + ".ali", this.Name + ".ali");
						Archive.CreateEntryFromFile(this.Name + ".ads", this.Name + ".ads");
						if (IncludeBody) {
							Archive.CreateEntryFromFile(this.Name + ".adb", this.Name + ".adb");
						}
						if (Environment.OSVersion.Platform <= (PlatformID)3) {
							Archive.CreateEntryFromFile(this.Name + ".dll", this.Name + ".dll");
						} else if (Environment.OSVersion.Platform == PlatformID.Unix) {
							Archive.CreateEntryFromFile(this.Name + ".so", this.Name + ".so");
						}
					} catch (FileNotFoundException) {
						// A file to be packaged was not found, so we can assume the package has not been built
						throw new PackageNotBuildException();
					}
					// Create the package info file and write everything to it
					ZipArchiveEntry InfoEntry = Archive.CreateEntry("info");
					using (StreamWriter InfoFile = new StreamWriter(InfoEntry.Open())) {
						this.WriteInfo(InfoFile);
					}
				}
			}
		}

		/// <summary>
		/// Write the info of this package out to the console
		/// </summary>
		public void WriteInfo() {
			Console.WriteLine("Name: " + this.Name);
			Console.WriteLine("Variant: " + this.Variant);
			Console.WriteLine("Version: " + this.Version);
			Console.WriteLine("Description: " + this.Description);
			Console.WriteLine("Dependencies: " + String.Join(", ", this.Dependencies));
		}

		/// <summary>
		/// Write the info of this package out to the specified <paramref name="Output"/> Stream
		/// </summary>
		/// <param name="Output">Output Stream to write to</param>
		public void WriteInfo(StreamWriter Output) {
			Output.WriteLine(this.Name);
			Output.WriteLine(this.Variant);
			Output.WriteLine(this.Version);
			Output.WriteLine(this.Description);
			Output.WriteLine(String.Join(',', this.Dependencies));
		}

		/// <summary>
		/// Create an install package from the specified package unit
		/// </summary>
		/// <param name="PackageUnit">Unit to package</param>
		public Package(PackageUnit PackageUnit, String Variant = "") {
			this.Name = PackageUnit.Name;
			this.Variant = Variant;
			this.Version = new Source(PackageUnit.GetSpec()).ParseVersion();
			this.Description = new Source(PackageUnit.GetSpec()).ParseDescription();
			this.Dependencies = PackageUnit.Dependencies;
		}

		/// <summary>
		/// Parse an install package
		/// </summary>
		/// <param name="FileName">Filename of the install package</param>
		public Package(String FileName) {
			try {
				using (FileStream File = new FileStream(FileName, FileMode.Open)) {
					using (ZipArchive Archive = new ZipArchive(File, ZipArchiveMode.Read)) {
						using (StreamReader Stream = new StreamReader(Archive.GetEntry("info").Open())) {
							this.Name = Stream.ReadLine();
							this.Variant = Stream.ReadLine();
							this.Version = new Version(Stream.ReadLine());
							this.Description = Stream.ReadLine();
							this.Dependencies = new List<String>(Stream.ReadLine().Split(','));
						}
					}
				}
			} catch {
				// If any of the above failed, we can pretty reliably assume it wasn't actually an install package we were reading.
				//? Shouldn't the case of reading the package info be given a different exception if it's malformatted?
				throw new NotInstallPackageException();
			}
		}

	}
}