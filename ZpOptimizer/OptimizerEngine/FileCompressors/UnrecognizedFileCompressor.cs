using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizerEngine.FileSystem;
using OptimizerEngine.Helpers;
using OptimizerEngine.DirCompressors;

namespace OptimizerEngine.FileCompressors {

    public class UnrecognizedFileCompressor : FileCompressor {

        public UnrecognizedFileCompressor(ZpFile file, Logger logger) : base(file, logger)
        {
        }

        public override void Execute() {
            currentDirLogger.Write("NonPerfSensitive," + fileToCompress.Name + "," + fileToCompress.Extension + ",");

            double compRatio = fileToCompress.Compress("XPRESS16K");

            if (compRatio < 1.2)
            { // Decompress if it doesn't compress well at all
                fileToCompress.Uncompress();

                currentDirLogger.WriteLine(sizeBefore + "," + fileToCompress.SizeOnDisk + "," + Math.Round(compRatio, 2) + ",Decompressed,Downgraded");
            }
            else if (compRatio < 1.4)
            { // Lower compression if it compresses poorly
                compRatio = fileToCompress.Compress("XPRESS8K");

                currentDirLogger.WriteLine(sizeBefore + "," + fileToCompress.SizeOnDisk + "," + Math.Round(compRatio, 2) + ",XPRESS8K,Downgraded");
            }
            else if (compRatio > 3.0)
            {
                compRatio = fileToCompress.Compress("LZX");
                currentDirLogger.WriteLine(sizeBefore + "," + fileToCompress.SizeOnDisk + "," + Math.Round(compRatio, 2) + ",LZX,Upgraded");
            }
            else
            { // Stick with the original compression and write to the logger if neither applies
                currentDirLogger.WriteLine(sizeBefore + "," + fileToCompress.SizeOnDisk + "," + Math.Round(compRatio, 2) + ",XPRESS16K");
            }

            fileToCompress.RemoveArchiveAttribute();
        }
    }
}
