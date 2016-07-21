using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizerEngine.FileSystem;
using OptimizerEngine.Helpers;
using OptimizerEngine.DirCompressors;

namespace OptimizerEngine.FileCompressors {

    public class MaximumFileCompressor : FileCompressor {

        public MaximumFileCompressor(ZpFile file, Logger logger) : base(file, logger)
        {
        }

        public override void Execute()
        {          
            fileToCompress.Compress("LZX");          
            fileToCompress.RemoveArchiveAttribute();
        }
    }
}
