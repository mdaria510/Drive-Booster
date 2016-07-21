using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizerEngine.Helpers {
    public static class Globals {

        // Used to define compression type
        public enum DirCompressionTypes {
            OPTIMAL = 0,
            MAXIMUM = 1,
            UNCOMPRESS = 2
        }

        public enum FileCompressionTypes
        {
            PERFSENSITIVE = 0,
            NONPERFSENSITIVE = 1,
            UNRECOGNIZED = 2,
            MAXIMUM = 3
        }


        // Used to determine compression behavior of files
        // Holds file extensions used for evaluating compression behavior
        public static string[] HighCompressibleFiles = Resources.HighCompressible.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        public static string[] NonCompressibleFiles = Resources.NonCompressible.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        public static string[] PerfSensitiveFiles = Resources.PerfSensitive.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        public static string[] NonPerfSensitiveFiles = Resources.NonPerfSensitive.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
    }
}
