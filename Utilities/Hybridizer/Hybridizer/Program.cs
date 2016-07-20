using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.VisualBasic.FileIO;



namespace ConsoleApplication6
{
    class Program
    {

        static void Main(string[] args)
        {

            foreach (string source in args)
            {
                string targetDirectory = "H:\\!Hybrid";
                string steamDirectory = "G:\\Steam\\steamapps\\common";
                          
                // Choose a destination for the copied files.
                int baseTargetIndex = source.LastIndexOf(steamDirectory) + steamDirectory.Length;
                string destination = targetDirectory + source.Substring(baseTargetIndex);
               
                //Console.WriteLine(destination);



                //Source = The link itself
                //Destination = the folder to be linked to


                FileAttributes attr = File.GetAttributes(source);

                if (attr.HasFlag(FileAttributes.ReparsePoint))
                {
                    
                    if (attr.HasFlag(FileAttributes.Directory))
                        DehybridizeFolder(source, destination);
                    else
                        DehybridizeFile(source, destination);
                }
                else if (attr.HasFlag(FileAttributes.Directory))             
                    HybridizeFolder(source, destination);             
                else
                    HybridizeFile(source, destination);
          

            }


            Console.WriteLine("Done");
            //CreateSymbolicLink(linkLoc, destination, 0x1);

           

        }

        public static void HybridizeFolder(string source, string destination)
        {
            FileSystem.CopyDirectory(source, destination, UIOption.AllDialogs);
            Console.WriteLine("Hybridizing folder: " + source);
            FileSystem.DeleteDirectory(source, DeleteDirectoryOption.DeleteAllContents);
            CreateSymbolicLink(source, destination, 0x1);
        }

        public static void HybridizeFile(string source, string destination)
        {
            FileSystem.CopyFile(source, destination, UIOption.AllDialogs);
            Console.WriteLine("Hybridizing file: " + source);
            FileSystem.DeleteFile(source);
            CreateSymbolicLink(source, destination, 0x0);
        }

        public static void DehybridizeFile(string source, string destination)
        {
            Console.WriteLine("Dehybridizing file: " + source);
            FileSystem.DeleteFile(source);
            FileSystem.CopyFile(destination, source, UIOption.AllDialogs);
            FileSystem.DeleteFile(destination);
        }

        public static void DehybridizeFolder(string source, string destination)
        {
            Console.WriteLine("Dehybridizing folder: " + source);
            FileSystem.DeleteDirectory(source, DeleteDirectoryOption.DeleteAllContents);
            FileSystem.CopyDirectory(destination, source, UIOption.AllDialogs);
            FileSystem.DeleteDirectory(destination, DeleteDirectoryOption.DeleteAllContents);
        }



        public static void LinkDirectory(string sourceDirName, string destDirName)
        {
            CreateSymbolicLink(sourceDirName, destDirName, 0x1);
        }

        [DllImport("kernel32.dll")]
        static extern bool CreateSymbolicLink(string lpSymlinkFileName,
        string lpTargetFileName, int dwFlags);




    }       

}
