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
	public class Package : IDisposable {

		public readonly PackageInfo Info;

		private ZipArchive archive;

		/// <summary>
		/// The archive of the package
		/// </summary>
		public ZipArchive Archive {
			get {
				if (this.archive is null) {
					this.archive = new ZipArchive(new FileStream(this.Info.Name + ".apkg", FileMode.Open));
				}
				return this.archive;
			}
		}

		/// <summary>
		/// Create the package in the filesystem
		/// </summary>
		/// <param name="IncludeBody">Whether to include the body file or not. This is required for generic units and for native builds. Not including it makes the package closed source.</param>
		public void Create(Boolean IncludeBody = true) {
			// Figure out how to name the archive.
			String ArchiveName;
			if (this.Info.Variant is null || this.Info.Variant == "") {
				ArchiveName = this.Info.Name + ".apkg";
			} else {
				ArchiveName = this.Info.Name + "." + this.Info.Variant + ".apkg";
			}
			// Create the actual archive and put everything necessary in it
			using (FileStream File = new FileStream(ArchiveName, FileMode.Create)) {
				using (ZipArchive Archive = new ZipArchive(File, ZipArchiveMode.Update)) {
					try {
						Archive.CreateEntryFromFile(this.Info.Name + ".ali", this.Info.Name + ".ali");
						Archive.CreateEntryFromFile(this.Info.Name + ".ads", this.Info.Name + ".ads");
						if (IncludeBody) {
							if (System.IO.File.Exists(this.Info.Name + ".adb")) {
								Archive.CreateEntryFromFile(this.Info.Name + ".adb", this.Info.Name + ".adb");
							}
						}
						switch (Environment.OSVersion.Platform) {
						case (PlatformID)1:
						case (PlatformID)2:
						case (PlatformID)3:
							Archive.CreateEntryFromFile(this.Info.Name + ".dll", this.Info.Name + ".dll");
							break;
						case PlatformID.Unix:
						default:
							Archive.CreateEntryFromFile(this.Info.Name + ".so", this.Info.Name + ".so");
							break;
						}
					} catch (FileNotFoundException) {
						goto Cleanup; // This goto is so that we can move outside of the `using` block, where File has been properly closed and disposed of, so that we can delete it.
					}
					// Create the package info file and write everything to it
					ZipArchiveEntry InfoEntry = Archive.CreateEntry("info");
					this.WriteInfo(InfoEntry.Open());
				}
			}
			return; // Proper execution stops here
			Cleanup:
			// Delete the archive, since we can't properly create it
			System.IO.File.Delete(ArchiveName);
			// A file to be packaged was not found, so we can assume the package has not been built
			throw new PackageNotBuildException();
		}

		/// <summary>
		/// Write the info of this package out to the console
		/// </summary>
		public void WriteInfo() {
			Console.WriteLine("Name: " + this.Info.Name);
			Console.WriteLine("Variant: " + this.Info.Variant);
			Console.WriteLine("Version: " + this.Info.Version);
			Console.WriteLine("Description: " + this.Info.Description);
			Console.WriteLine("Dependencies: " + String.Join(", ", this.Info.Dependencies));
		}

		/// <summary>
		/// Write the info of this package out to the specified <paramref name="Output"/> Stream
		/// </summary>
		/// <param name="Output">Output Stream to write to</param>
		public void WriteInfo(Stream Output) {
			using (StreamWriter Writer = new StreamWriter(Output)) {
				Writer.WriteLine(this.Info.Name);
				Writer.WriteLine(this.Info.Variant);
				Writer.WriteLine(this.Info.Version);
				Writer.WriteLine(this.Info.Description);
				Writer.WriteLine(String.Join(',', this.Info.Dependencies));
			}
		}

		/// <summary>
		/// Write the validation of this package to the console
		/// </summary>
		public void WriteValidation() {
			Boolean NoIssues = true;
			if (this.Archive.GetEntry(this.Info.Name + ".ads") is null) {
				Console.WriteLine("Missing Spec");
				NoIssues = false;
			}
			if (this.Archive.GetEntry(this.Info.Name + ".adb") is null) {
				// A body is not required, so just report this and move on
				Console.WriteLine("No Body");
			}
			if (this.Archive.GetEntry(this.Info.Name + ".dll") is null && this.Archive.GetEntry(this.Info.Name + ".so") is null) {
				Console.WriteLine("Missing Libraries");
				NoIssues = false;
			} else if (this.Archive.GetEntry(this.Info.Name + ".dll") is null) {
				Console.WriteLine("Missing Library (Windows)");
			} else if (this.Archive.GetEntry(this.Info.Name + ".so") is null) {
				Console.WriteLine("Missing Library (UNIX)");
			}
			if (this.Archive.GetEntry(this.Info.Name + ".ali") is null) {
				Console.WriteLine("Missing ALI");
				NoIssues = false;
			}
			if (NoIssues) {
				Console.WriteLine("Package Valid");
			}
		}

		public override Int32 GetHashCode() => this.Info.Name.GetHashCode() ^ this.Info.Variant.GetHashCode() ^ this.Info.Version.GetHashCode();

		void IDisposable.Dispose() => this.archive.Dispose();

		/// <summary>
		/// Create an install package from the specified package unit
		/// </summary>
		/// <param name="PackageUnit">Unit to package</param>
		public Package(PackageUnit PackageUnit, String Variant = "") {
			Source Source = new Source(PackageUnit.GetSpec());
			this.Info = new PackageInfo(PackageUnit.Name, Variant, Source.ParseVersion(), Source.ParseDescription(), PackageUnit.Dependencies);
		}

		/// <summary>
		/// Parse an install package
		/// </summary>
		/// <param name="FileName">Filename of the install package</param>
		public Package(String FileName) {
			try {
				using (FileStream File = new FileStream(FileName, FileMode.Open)) {
					using (ZipArchive Archive = new ZipArchive(File, ZipArchiveMode.Read)) {
						this.Info = new PackageInfo(Archive.GetEntry("info").Open());
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