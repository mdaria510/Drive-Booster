using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using OptimizerEngine.FileSystem;


namespace ZpOptimizerUI.Helpers {
    public class ObjectStorage {

        #region Private Properties

        //private string dataFile;

        #endregion



        #region Constructors

        public ObjectStorage() {
        }
        #endregion  


        public List<ZpDirectory> RetrieveMasterObjectList(string dataFile) {
            try {
                using (Stream stream = File.Open(dataFile, FileMode.Open)) {
                    BinaryFormatter bin = new BinaryFormatter();

                    List<ZpDirectory> masterDirList = (List<ZpDirectory>)bin.Deserialize(stream);
                    return masterDirList;
                }
            }
            catch (IOException) {
                return null;
            }
        }



        public void SaveMasterObjectList(string dataFile, List<ZpDirectory> masterDirList) {
            try {
                using (Stream stream = File.Open(dataFile, FileMode.Create)) {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, masterDirList);
                }
            }
            catch (IOException) {
            }

        }



    }
}