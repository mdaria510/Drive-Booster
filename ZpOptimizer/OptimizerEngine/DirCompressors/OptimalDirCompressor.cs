using OptimizerEngine.FileSystem;
using OptimizerEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using OptimizerEngine.DirCompressors;

namespace OptimizerEngine.DirCompressors {

    public class OptimalDirCompressor : DirCompressor {

        #region Private Properties

        #endregion

        #region Constructors


        public OptimalDirCompressor(ZpDirectory dir) : base(dir) {
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public events

        #endregion

        #region Public Methods

        // Where the magic happens

        public override void Execute(IProgress<int> progFile, CancellationToken ct) {

            // Get the size of the folder before compressing
            long folderSizeBefore = activeDir.Size;
            double folderSizeBeforeGB = (double)folderSizeBefore / 1024 / 1024 / 1024;

            var fileList = activeDir.GetAllFiles();

            double percentToIncrement = 100.0 / Convert.ToDouble(fileList.Count);
            double percentComplete = percentToIncrement;
            //bgw.ReportProgress(0);\
            progFile.Report(0);

            // Loop through all files in folders and subfolders
            foreach (ZpFile file in fileList) {
                int percentCompleteInt = Convert.ToInt32(percentComplete);
                progFile.Report(percentCompleteInt);
                //bgw.ReportProgress(percentCompleteInt);
                percentComplete += percentToIncrement;

                if (file.Attributes.HasFlag(System.IO.FileAttributes.Archive) == false) //Skip files without Archive flag, already compressed.                                    
                    continue;

                if (loggingStarted == false) //Create logfile only if function reaches this point, this prevents the file from being created for folders that arent being recompressed. 
                {
                    logger.CreateNewLogFile(activeDir.Name + " " + DateTime.Now.ToFileTime() + ".txt");
                    loggingStarted = true;
                }

                if (file.IsTooSmall) //Skip Small Files
                {
                    logger.WriteLine("Too Small," + file.Name + "," + file.Extension + "," + file.Size + "," + file.Size + "," + "1.0" + ",Skipped");
                    file.RemoveArchiveAttribute();
                }
                else if (file.IsNonCompressible) //Skip incompressible files
                {
                    logger.WriteLine("Incompressible," + file.Name + "," + file.Extension + "," + file.Size + "," + file.Size + "," + "1.0" + ",Skipped");
                    file.RemoveArchiveAttribute();
                }
                else if (file.IsPerfSensitive)
                    ApplyFileCompression(Globals.FileCompressionTypes.PERFSENSITIVE, file);
                else if (file.IsNonPerfSensitive)
                    ApplyFileCompression(Globals.FileCompressionTypes.NONPERFSENSITIVE, file);
                else
                    ApplyFileCompression(Globals.FileCompressionTypes.UNRECOGNIZED, file);

            }

            activeDir.UpdateSize();
            activeDir.UpdateSizeOnDisk();
            activeDir.UpdateRatio();

            progFile.Report(100);
            // bgw.ReportProgress(100);

            if (loggingStarted == true) {
                long folderSizeAfter = activeDir.SizeOnDisk;
                double folderSizeAfterGB = (double)folderSizeAfter / 1024 / 1024 / 1024;
                logger.WriteLine("");
                logger.WriteLine("Compressed " + activeDir.Name + " Size = " + Math.Round(folderSizeAfterGB, 3) + "GB. Ratio = " + Math.Round(folderSizeBeforeGB / folderSizeAfterGB, 3) + " to 1");
                logger.WriteLine("");
            }

        }



        public override void Execute(BackgroundWorker bgw) {

            // Get the size of the folder before compressing
            long folderSizeBefore = activeDir.Size;
            double folderSizeBeforeGB = (double)folderSizeBefore / 1024 / 1024 / 1024;

            var fileList = activeDir.GetAllFiles();

            double percentToIncrement = 100.0 / Convert.ToDouble(fileList.Count);
            double percentComplete = percentToIncrement;
            bgw.ReportProgress(0);

            // Loop through all files in folders and subfolders
            foreach (ZpFile file in fileList) {
                int percentCompleteInt = Convert.ToInt32(percentComplete);
                bgw.ReportProgress(percentCompleteInt);
                percentComplete += percentToIncrement;

                if (file.Attributes.HasFlag(System.IO.FileAttributes.Archive) == false) //Skip files without Archive flag, already compressed.                                    
                    continue;

                if (loggingStarted == false) //Create logfile only if function reaches this point, this prevents the file from being created for folders that arent being recompressed. 
                {
                    logger.CreateNewLogFile(activeDir.Name + " " + DateTime.Now.ToFileTime() + ".txt");
                    loggingStarted = true;
                }

                if (file.IsTooSmall) //Skip Small Files
                {
                    logger.WriteLine("Too Small," + file.Name + "," + file.Extension + "," + file.Size + "," + file.Size + "," + "1.0" + ",Skipped");
                    file.RemoveArchiveAttribute();
                }
                else if (file.IsNonCompressible) //Skip incompressible files
                {
                    logger.WriteLine("Incompressible," + file.Name + "," + file.Extension + "," + file.Size + "," + file.Size + "," + "1.0" + ",Skipped");
                    file.RemoveArchiveAttribute();
                }
                else if (file.IsPerfSensitive)
                    ApplyFileCompression(Globals.FileCompressionTypes.PERFSENSITIVE, file);
                else if (file.IsNonPerfSensitive)
                    ApplyFileCompression(Globals.FileCompressionTypes.NONPERFSENSITIVE, file);
                else
                    ApplyFileCompression(Globals.FileCompressionTypes.UNRECOGNIZED, file);

            }

            activeDir.UpdateSize();
            activeDir.UpdateSizeOnDisk();
            activeDir.UpdateRatio();


            bgw.ReportProgress(100);

            if (loggingStarted == true) {
                long folderSizeAfter = activeDir.SizeOnDisk;
                double folderSizeAfterGB = (double)folderSizeAfter / 1024 / 1024 / 1024;
                logger.WriteLine("");
                logger.WriteLine("Compressed " + activeDir.Name + " Size = " + Math.Round(folderSizeAfterGB, 3) + "GB. Ratio = " + Math.Round(folderSizeBeforeGB / folderSizeAfterGB, 3) + " to 1");
                logger.WriteLine("");
            }

        }


        public override void Execute() {

            // Get the size of the folder before compressing
            long folderSizeBefore = activeDir.Size;
            double folderSizeBeforeGB = (double)folderSizeBefore / 1024 / 1024 / 1024;

            var fileList = activeDir.GetAllFiles();

            // Loop through all files in folders and subfolders
            foreach (ZpFile file in fileList) {

                if (file.Attributes.HasFlag(System.IO.FileAttributes.Archive) == false) {
                    continue;
                }
                if (loggingStarted == false) {
                    logger.CreateNewLogFile(activeDir.Name + " " + DateTime.Now.ToFileTime() + ".txt");
                    loggingStarted = true;
                }

                if (file.IsTooSmall) {
                    logger.WriteLine("Too Small," + file.Name + "," + file.Extension + "," + file.Size + "," + file.Size + "," + "1.0" + ",Skipped");
                    file.RemoveArchiveAttribute();
                }
                else if (file.IsNonCompressible) {
                    logger.WriteLine("Incompressible," + file.Name + "," + file.Extension + "," + file.Size + "," + file.Size + "," + "1.0" + ",Skipped");
                    file.RemoveArchiveAttribute();
                }
                else if (file.IsPerfSensitive)
                    ApplyFileCompression(Globals.FileCompressionTypes.PERFSENSITIVE, file);
                else if (file.IsNonPerfSensitive)
                    ApplyFileCompression(Globals.FileCompressionTypes.NONPERFSENSITIVE, file);
                else
                    ApplyFileCompression(Globals.FileCompressionTypes.UNRECOGNIZED, file);

            }

            activeDir.UpdateSize();
            activeDir.UpdateSizeOnDisk();
            activeDir.UpdateRatio();

            if (loggingStarted == true) {
                long folderSizeAfter = activeDir.SizeOnDisk;
                double folderSizeAfterGB = (double)folderSizeAfter / 1024 / 1024 / 1024;
                logger.WriteLine("");
                logger.WriteLine("Compressed " + activeDir.Name + " Size = " + Math.Round(folderSizeAfterGB, 3) + "GB. Ratio = " + Math.Round(folderSizeBeforeGB / folderSizeAfterGB, 3) + " to 1");
                logger.WriteLine("");
            }

        }


        #endregion

        #region Private Methods
        #endregion
    }
}
