using OptimizerEngine.DirCompressors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static OptimizerEngine.Helpers.Globals;
using System.ComponentModel;
using OptimizerEngine.FileSystem;

namespace OptimizerEngine {
    public class OptimizerEngine {


        #region Private Properties

        //private List<string> allDirs;
        //private List<string> selectedDirs;
        private ZpDirectory activeZpDir;
        private BackgroundWorker activeBackgroundWorker;

        #endregion


        #region Constructors

        //OLD VERSION USING BACKGROUNDWORKER
        public OptimizerEngine(ZpDirectory dir, BackgroundWorker bw) {
            activeZpDir = dir;
            activeBackgroundWorker = bw;
        }


        //NEW VERSION USING ASYNC METHODS
        public OptimizerEngine(ZpDirectory dir) {
            activeZpDir = dir;           
        }

        #endregion

        #region Public Properties
        #endregion

        #region Public Methods

        // Apply compression on only the selected directories      
        public void CompressActiveDir(ZpDirectory activeDir, DirCompressionTypes compressionType) {
            ApplyCompression(compressionType, activeDir);
         
        }


        //New version
        public void CompressActiveDir(ZpDirectory activeDir, DirCompressionTypes compressionType, IProgress<int> progFile, CancellationToken ct) {
            ApplyCompression(compressionType, activeDir, progFile, ct);

        }

        #endregion


        #region Private Methods

        // Apply compression

        private void ApplyCompression(DirCompressionTypes compressionType, ZpDirectory activeDir) {
            DirCompressor compressor;


            // Determine the type of directory compression
            switch (compressionType) {
                case DirCompressionTypes.OPTIMAL:
                    compressor = new OptimalDirCompressor(activeDir);
                    break;
                case DirCompressionTypes.MAXIMUM:
                    compressor = new MaximumDirCompressor(activeDir);
                    break;
                case DirCompressionTypes.UNCOMPRESS:
                    compressor = new UncompressDirCompressor(activeDir);
                    break;
                default:
                    throw new Exception("Invalid CompressionType");
            }

            // Execute the directory compression
            compressor.Execute(activeBackgroundWorker);
            
        }

        private void ApplyCompression(DirCompressionTypes compressionType, ZpDirectory activeDir, IProgress<int> progFile, CancellationToken ct) {
            DirCompressor compressor;


            // Determine the type of directory compression
            switch (compressionType) {
                case DirCompressionTypes.OPTIMAL:
                    compressor = new OptimalDirCompressor(activeDir);
                    break;
                case DirCompressionTypes.MAXIMUM:
                    compressor = new MaximumDirCompressor(activeDir);
                    break;
                case DirCompressionTypes.UNCOMPRESS:
                    compressor = new UncompressDirCompressor(activeDir);
                    break;
                default:
                    throw new Exception("Invalid CompressionType");
            }

            // Execute the directory compression         
            compressor.Execute(progFile, ct);

        }
        #endregion
        
    }
}
