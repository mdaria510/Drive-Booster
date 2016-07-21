using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizerEngine.FileSystem;
using OptimizerEngine.FileCompressors;
using static OptimizerEngine.Helpers.Globals;
using OptimizerEngine.Helpers;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;

namespace OptimizerEngine.DirCompressors {

    public abstract class DirCompressor {

        #region Private Properties

        protected ZpDirectory activeDir;  // Root folder where compression starts
        public Logger logger;  // Logs stuff
        protected bool loggingStarted;

        public delegate void ProgressUpdate(int value);
        public event ProgressUpdate OnProgressUpdate;

        #endregion



        #region Constructors


        public DirCompressor(ZpDirectory dir) {
            // Initialize directory
            activeDir = dir;

            // Initialize logger
            logger = new Logger();

            loggingStarted = false;
        }



        #endregion



        #region Public Properties

        #endregion



        #region Public Methods

        public abstract void Execute(BackgroundWorker bw);

        public abstract void Execute();

        public abstract void Execute(IProgress<int> progFile, CancellationToken ct);


        #endregion



        #region Private Methods

        protected void ApplyFileCompression(FileCompressionTypes fileCompressorType, ZpFile file) {
            FileCompressor fileCompressor;

            // Determine the type of directory compression
            switch (fileCompressorType) {
                case FileCompressionTypes.PERFSENSITIVE:
                    fileCompressor = new PerfSensitiveFileCompressor(file, logger);
                    break;
                case FileCompressionTypes.NONPERFSENSITIVE:
                    fileCompressor = new NonPerfSensitiveFileCompressor(file, logger);
                    break;
                case FileCompressionTypes.UNRECOGNIZED:
                    fileCompressor = new UnrecognizedFileCompressor(file, logger);
                    break;
                case FileCompressionTypes.MAXIMUM:
                    fileCompressor = new MaximumFileCompressor(file, logger);
                    break;
                default:
                    throw new Exception("Invalid CompressionType");
            }

            // Execute the directory compression
            fileCompressor.Execute();
        }

        #endregion
    }
}
