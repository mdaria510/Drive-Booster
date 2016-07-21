using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizerEngine.Helpers {
    public class Logger {

        #region Private Properties

        private const bool CONSOLE_ENABLED = true;  // Change this to false if you want to turn off console logging
        private const bool LOGFILE_ENABLED = true;  // Change this to false if you want to turn off file logging

        private string logsDir;
        private StreamWriter logFile;

        #endregion

        #region Constructors

        public Logger() {

            if (LOGFILE_ENABLED) {
                // Initialize logs folder
                logsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\ZpOptimizer\Logs\";
                System.IO.Directory.CreateDirectory(logsDir);
                
            }
        }

        #endregion

        #region Public Properties

        #endregion

        #region Public Methods

        // Create new log file and initialize StreamWriter
        public void CreateNewLogFile(string fileName) {
            if (LOGFILE_ENABLED) {
                logFile = new StreamWriter(logsDir + fileName);
                logFile.AutoFlush = true;
            }
        }

        // Writes a single line to the console and log file
        public void WriteLine(string text) {
            if (CONSOLE_ENABLED) {
                Console.WriteLine(text);
            }

            if (LOGFILE_ENABLED) {
                logFile.WriteLine(text);
            }
        }

        public void Write(string text)
        {
            if (CONSOLE_ENABLED)
            {
                Console.Write(text);
            }

            if (LOGFILE_ENABLED)
            {
                logFile.Write(text);
            }
        }


        // Close StreamWriter
        public void Close() {
            logFile.Close();
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
