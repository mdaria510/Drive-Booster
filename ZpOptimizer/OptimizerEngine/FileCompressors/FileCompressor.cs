using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimizerEngine.FileSystem;
using OptimizerEngine.Helpers;
using static OptimizerEngine.Helpers.Globals;

namespace OptimizerEngine.FileCompressors {

    public abstract class FileCompressor {

        #region Private Properties
        protected ZpFile fileToCompress; //The File being compressed
        protected Logger currentDirLogger;
        protected long sizeBefore;  //Establish size before compressing

        #endregion

        #region Constructors
        public FileCompressor(ZpFile file, Logger logger)
        {
            fileToCompress = file;
            currentDirLogger = logger;
            sizeBefore = fileToCompress.Size;
        }
        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        public abstract void Execute();


        #endregion

        #region Private Methods
        #endregion
    }

}
