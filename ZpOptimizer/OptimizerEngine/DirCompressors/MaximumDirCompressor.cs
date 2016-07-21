using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizerEngine.FileSystem;
using OptimizerEngine.Helpers;
using System.ComponentModel;
using System.Threading;

namespace OptimizerEngine.DirCompressors {

    public class MaximumDirCompressor : DirCompressor {

        public MaximumDirCompressor(ZpDirectory dir) : base(dir) {
        }

        public override void Execute() {
        }

        public override void Execute(BackgroundWorker bgw) {
           
            var fileList = activeDir.GetAllFiles();

            double percentToIncrement = 100.0 / Convert.ToDouble(fileList.Count);
            double percentComplete = percentToIncrement;
            bgw.ReportProgress(0);

            // Loop through all files in folders and subfolders
            foreach (ZpFile file in fileList) {
                int percentCompleteInt = Convert.ToInt32(percentComplete);
                bgw.ReportProgress(percentCompleteInt);
                percentComplete += percentToIncrement;

                if (!file.IsTooSmall || !file.IsNonCompressible) //Skip Small and Incompressible Files                   
                    ApplyFileCompression(Globals.FileCompressionTypes.MAXIMUM, file);

                //file.AddArchiveAttribute(); 
                file.RemoveArchiveAttribute();
            }

            bgw.ReportProgress(100);
        }

        public override void Execute(IProgress<int> progFile, CancellationToken ct) {
            throw new NotImplementedException();
        }
    }
}
