using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizerEngine.FileSystem;
using OptimizerEngine.Helpers;
using OptimizerEngine.DirCompressors;

namespace OptimizerEngine.FileCompressors {

    public class NonPerfSensitiveFileCompressor : FileCompressor {
        
        public NonPerfSensitiveFileCompressor(ZpFile file, Logger logger) : base(file, logger)
        {
        }

        public override void Execute()
        {
            currentDirLogger.Write("NonPerfSensitive," + fileToCompress.Name + "," + fileToCompress.Extension + ",");

            double compRatio = fileToCompress.Compress("LZX");

            if (compRatio < 1.2)
            { // Decompress if it doesn't compress well at all
                fileToCompress.Uncompress();

                currentDirLogger.WriteLine(sizeBefore + "," + fileToCompress.SizeOnDisk + "," + Math.Round(compRatio, 2) + ",Decompressed,Downgraded");
            }
            else if (compRatio < 1.4)
            { // Lower compression if it compresses poorly
                compRatio = fileToCompress.Compress("XPRESS16K");

                currentDirLogger.WriteLine(sizeBefore + "," + fileToCompress.SizeOnDisk + "," + Math.Round(compRatio, 2) + ",XPRESS16K,Downgraded");
            }
            else
            {
                // Stick with the original compression and write to the logger if neither applies
                currentDirLogger.WriteLine(sizeBefore + "," + fileToCompress.SizeOnDisk + "," + Math.Round(compRatio, 2) + ",LZX");
            }

            fileToCompress.RemoveArchiveAttribute();

        }
        
    }
}
