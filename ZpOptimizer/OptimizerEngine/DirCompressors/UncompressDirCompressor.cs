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
    public class UncompressDirCompressor : DirCompressor {

        /* OLD
        public UncompressDirCompressor(string dir) : base(dir) {
        }
        */

        public UncompressDirCompressor(ZpDirectory dir) : base(dir) {
        }


        public override void Execute() {
        }

        public override void Execute(BackgroundWorker bgw) {

            var fileList = activeDir.GetAllFiles();
            double percentToIncrement = 100.0 / Convert.ToDouble(fileList.Count);
            double percentComplete = percentToIncrement;
            bgw.ReportProgress(0);

            // Loop through all files in folders and subfolders
            foreach (ZpFile file in fileList)
            {
                int percentCompleteInt = Convert.ToInt32(percentComplete);
                bgw.ReportProgress(percentCompleteInt);
                percentComplete += percentToIncrement;

                if (!file.IsTooSmall || !file.IsNonCompressible)                                                   
                    file.Uncompress();

                file.AddArchiveAttribute();
            }

            activeDir.UpdateSize();
            activeDir.UpdateSizeOnDisk();
            activeDir.UpdateRatio();

        }

        public override void Execute(IProgress<int> progFile, CancellationToken ct) {
            throw new NotImplementedException();
        }
    }
}
