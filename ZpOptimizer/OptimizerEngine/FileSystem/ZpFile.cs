using OptimizerEngine.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OptimizerEngine.FileSystem {

    [Serializable()]
    public class ZpFile {

        #region Private Properties

        private FileInfo fileInfo;
        public FileAttributes attributes;
        private long size;
        private long sizeOnDisk;


        #endregion

        #region Constructors

        public ZpFile(string fileName) {

            // Initialize necessary properties
            fileInfo = new FileInfo(fileName);
            attributes = File.GetAttributes(fileInfo.FullName);
            //sizeOnDisk = GetSizeOnDisk();
            size = fileInfo.Length;
        }

        //Alternate constructor using FileInfo
        public ZpFile(FileInfo fi) {

            // Initialize necessary properties
            fileInfo = fi;
            attributes = File.GetAttributes(fileInfo.FullName);
            sizeOnDisk = GetSizeOnDisk();
            size = fileInfo.Length;

        }

        #endregion

        #region Public Properties

        // Name of the file
        public string Name {
            get {
                return fileInfo.Name;
            }
        }

        // Full path of file
        public string FullName {
            get {
                return fileInfo.FullName;
            }
        }

        public string Extension {
            get {
                return fileInfo.Extension;
            }
        }

        public FileAttributes Attributes {
            get {
                return fileInfo.Attributes;
            }
        }

        // Get the Size (in bytes) of the file
        public long Size {
            get {
                return size;
            }
        }

        public long SizeOnDisk {
            get {
                return sizeOnDisk;
            }
        }

        // File is too small
        public bool IsTooSmall {
            get {
                return this.Size < 4096;
            }
        }

        // File is not compressible
        public bool IsNonCompressible {
            get {
                return Globals.NonCompressibleFiles.Contains(this.Extension.ToLower());
            }
        }

        // File is performance sensitive
        public bool IsPerfSensitive {
            get {
                return Globals.PerfSensitiveFiles.Contains(this.Extension.ToLower());
            }
        }

        // File is not performance sensitive
        public bool IsNonPerfSensitive {
            get {
                return Globals.NonPerfSensitiveFiles.Contains(this.Extension.ToLower());
            }
        }

        #endregion

        #region Public Methods


        // Get the Size on Disk (in bytes) of the file
        private long GetSizeOnDisk() {
            uint dummy, sectorsPerCluster, bytesPerSector;

            int result = GetDiskFreeSpaceW(fileInfo.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
            if (result == 0) throw new Win32Exception();
            uint clusterSize = sectorsPerCluster * bytesPerSector;
            uint hosize;
            uint losize = GetCompressedFileSizeW(this.FullName, out hosize);
            long size;
            size = (long)hosize << 32 | losize;
            return ((size + clusterSize - 1) / clusterSize) * clusterSize;
        }

        // Uncompress file
        public void Uncompress() {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C compact /U /F " + '"' + this.FullName + '"';
            p.Start();

            StreamReader reader = p.StandardOutput;
            string output = reader.ReadLine();
            p.WaitForExit();
        }

        // Compress file. Returns compression ratio
        public double Compress(string compressionType) {
            Process p = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C compact /C /F /EXE:" + compressionType + " " + '"' + this.FullName + '"';
            p.Start();

            StreamReader reader = p.StandardOutput;
            string output = reader.ReadLine();
            p.WaitForExit();

            UpdateSizeOnDisk();
            double ratio = (double)this.Size / (double)this.SizeOnDisk;
            return ratio;
        }

        public void RemoveArchiveAttribute() {
            attributes = RemoveAttribute(attributes, FileAttributes.Archive);
            File.SetAttributes(FullName, attributes);
        }

        public void AddArchiveAttribute() {
            File.SetAttributes(FullName, File.GetAttributes(FullName) | FileAttributes.Archive);
        }


        #endregion

        #region Private Methods

        private void UpdateSizeOnDisk() {
            this.sizeOnDisk = GetSizeOnDisk();
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove) {
            return attributes & ~attributesToRemove;
        }

        [DllImport("kernel32.dll")]
        private static extern uint GetCompressedFileSizeW([In, MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
           [Out, MarshalAs(UnmanagedType.U4)] out uint lpFileSizeHigh);

        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        private static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
           out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
           out uint lpTotalNumberOfClusters);

        #endregion
    }
}
