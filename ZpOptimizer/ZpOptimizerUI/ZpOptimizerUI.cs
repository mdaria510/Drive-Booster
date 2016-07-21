using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using OptimizerEngine.FileSystem;
using static OptimizerEngine.Helpers.Globals;
using ZpOptimizerUI.Helpers;
using System.Runtime.Serialization.Formatters.Binary;


namespace ZpOptimizerUI {
    public partial class ZpOptimizerUI : Form {
        private OptimizerEngine.OptimizerEngine engine;
        private DirCompressionTypes compressionType;
        //public List<string> selectedDirList;
        public List<ZpDirectory> masterObjectList;
        public List<ZpDirectory> selectedObjectList;

        public ObjectStorage objectStorage;

        #region constructors

        public ZpOptimizerUI() {
            InitializeComponent();
            Show();

            //Initialize disk reader/writer object
            objectStorage = new ObjectStorage();

            //Initialize main list of games
            //SplashScreen splashScreen = new SplashScreen("SplashScreenImage.bmp");

            masterObjectList = new List<ZpDirectory>();
            selectedObjectList = new List<ZpDirectory>();
            masterObjectList = InitializeMasterObjectList();

            //Initialize other components
            
            InitializeBackgroundWorker();         
            InitializeObjectListView();

        }

        #endregion



        #region Initializers

        //Retrieve and update master list of games
        private List<ZpDirectory> InitializeMasterObjectList() {

            var dirList = new List<string> { };
            var dirsToAdd = new List<string> { };
            var dirsToRemove = new List<ZpDirectory> { };

            //Retrieve the saved list from disk
           
            if (File.Exists("data.bin")) {
                masterObjectList = objectStorage.RetrieveMasterObjectList("data.bin");

                //get the full folder list to match against
                foreach (string rootFolder in Settings1.Default.locationList) {
                    foreach (string subFolder in Directory.EnumerateDirectories(rootFolder)) {                    
                        dirList.Add(subFolder);
                    }
                }

                //Remove folders that no longer exist or are not in one of the set locations
                foreach (ZpDirectory zpdir in masterObjectList) {
                    if (Directory.Exists(zpdir.Path) == false || dirList.Contains(zpdir.Path) == false) {
                        dirsToRemove.Add(zpdir);
                    }
                }

                //remove them from the master list
                foreach (ZpDirectory dir in dirsToRemove) {
                    masterObjectList.Remove(dir);
                }

                //remove paths from the list that already match objects
                foreach (ZpDirectory zpdir in masterObjectList) {
                    if (dirList.Contains(zpdir.Path) == true){
                        dirList.Remove(zpdir.Path);                        
                    }
                }

                //finally add the ones remaining because they are unique
                foreach (string dir in dirList) {
                    masterObjectList.Add(new ZpDirectory(dir));
                }
        
            }   
            else {
                //if the saved object file doesnt exist, just recreate it from scratch
                foreach (string rootFolder in Settings1.Default.locationList) {
                    foreach (string subFolder in Directory.EnumerateDirectories(rootFolder)) {                        
                        try {
                            masterObjectList.Add(new ZpDirectory(subFolder));
                        }
                        catch (Exception) {

                            throw;
                        }                  
                    }
                }
            }

            //Save the list to disk after all operations complete
            objectStorage.SaveMasterObjectList("data.bin", masterObjectList);

            return masterObjectList;
        }

        //Populate listview with data from directory list   
        private void InitializeObjectListView() {

            //TODO: Implement saving previous column state.
            objectListView1.SetObjects(masterObjectList);
          

        }

        //Set essential properties for background worker
        private void InitializeBackgroundWorker() {

            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true; //Allow for the process to be cancelled

        }

        #endregion



        #region EVENTS

       

        private void locationsToolStripMenuItem_Click(object sender, EventArgs e) {
            var optionsForm = new OptionsForm();
            
            optionsForm.ShowDialog();
           

        }

        private void objectListView1_ItemChecked(object sender, ItemCheckedEventArgs e) {

            

        }
    
        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        //Add new folders to the list

        private bool m_running = false;
        CancellationTokenSource m_cancelTokenSource = null;

        private async void applyAsyncButton_Click(object sender, EventArgs e) {

            //Create a list of the checked objects
            foreach (ZpDirectory ob in objectListView1.CheckedObjects) {
                selectedObjectList.Add(ob);
            }

            //Get the compression type
            if (radioButtonOptimized.Checked == true) {
                compressionType = DirCompressionTypes.OPTIMAL;
            }
            else if (radioButtonMaxComp.Checked == true) {
                compressionType = DirCompressionTypes.MAXIMUM;
            }
            else if (radioButtonUncompressed.Checked == true) {
                compressionType = DirCompressionTypes.UNCOMPRESS;
            }

            //LAUNCH MODAL PROGRESS DIALOG
            //var progressForm = new ProgressForm(selectedObjectList, compressionType);
            //progressForm.ShowDialog();

            if (!m_running) {
                m_running = true;
                labelResult.Text = "Working";
                //cancelAsyncButton.Text = "Cancel";
                Progress<int> progFolder = new Progress<int>(SetFolderProgress);
                Progress<int> progFile = new Progress<int>(SetFileProgress);
                m_cancelTokenSource = new CancellationTokenSource();
                try {
                    await CompressionTask(progFile, progFolder, m_cancelTokenSource.Token, selectedObjectList);
                    labelResult.Text = "Done";
                }
                catch (OperationCanceledException) {
                    labelResult.Text = "Canceled";
                }
                finally {
                    m_running = false;
                    m_cancelTokenSource = null;
                }
            }
            else {
                labelResult.Text = "Waiting to cancel...";
                m_cancelTokenSource.Cancel();
            }

            objectListView1.RefreshObjects(masterObjectList);

        }

        private void SetFolderProgress(int value) {
            // Add 1 so that progress is "completed"
            int adjustedValue = value + 1;

            // Make sure value is in range
            adjustedValue = Math.Max(adjustedValue, folderProgressBar.Minimum);
            adjustedValue = Math.Min(adjustedValue, folderProgressBar.Maximum);

            folderProgressBar.Value = adjustedValue;
        }

        private void SetFileProgress(int value) {
            // Add 1 so that progress is "completed"
            int adjustedValue = value + 1;

            // Make sure value is in range
            adjustedValue = Math.Max(adjustedValue, fileProgressBar.Minimum);
            adjustedValue = Math.Min(adjustedValue, fileProgressBar.Maximum);

            fileProgressBar.Value = adjustedValue;
        }



        private Task CompressionTask(IProgress<int> progFile, IProgress<int> progFolder, CancellationToken ct, List<ZpDirectory>zpDirList) {

            return Task.Run(() => {

                int iteratorChunk = Convert.ToInt32(100 / zpDirList.Count);

                foreach (ZpDirectory activeDir in zpDirList) {

                    ct.ThrowIfCancellationRequested();

                    var engine = new OptimizerEngine.OptimizerEngine(activeDir);
                    //engine.CompressActiveDir(activeDir, compressionType);
                    engine.CompressActiveDir(activeDir, compressionType, progFile, ct);


                    progFolder.Report(iteratorChunk);
                    iteratorChunk += iteratorChunk;

                }

            }, ct);

        }


        //TODO: Replace this with it's own dialog
        //Apply compression on selected items
        private void buttonApplySelected_Click(object sender, EventArgs e) {

            

            //Create a list of the checked objects
            //List<ZpDirectory> selectedObjectList = new List<ZpDirectory> { };

            selectedObjectList.Clear();

            foreach (ZpDirectory ob in objectListView1.CheckedObjects) {
                selectedObjectList.Add(ob);
            }


            //Start compression job in background
            if (backgroundWorker1.IsBusy == false)
                backgroundWorker1.RunWorkerAsync("Selected");
            else
                MessageBox.Show("Chill out brah, let me finish.");  //HACK: This is a really shitty way to do this.                
        }

        //Cancel compression, only works per game right now
        private void buttonCancel_Click(object sender, EventArgs e) {
            //Check if background worker is doing anything and send a cancellation if it is
            if (backgroundWorker1.IsBusy) {
                backgroundWorker1.CancelAsync();
            }
        }

        //Save master game list when closing program
        private void ZpOptimizerUI_FormClosing(object sender, FormClosingEventArgs e) {

            objectStorage.SaveMasterObjectList("data.bin", masterObjectList);
        }

        //Unused
        private void radioButtonOptimized_CheckedChanged(object sender, EventArgs e) { }
        private void radioButtonUncompressed_CheckedChanged(object sender, EventArgs e) { }
        private void radioButtonMaxComp_CheckedChanged(object sender, EventArgs e) { }
        void UpdateGUI(object userData) { }

        #endregion
     
        #region BackgroundWorker

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {

            if (radioButtonOptimized.Checked == true) {
                compressionType = DirCompressionTypes.OPTIMAL;
            }
            else if (radioButtonMaxComp.Checked == true) {
                compressionType = DirCompressionTypes.MAXIMUM;
            }
            else if (radioButtonUncompressed.Checked == true) {
                compressionType = DirCompressionTypes.UNCOMPRESS;
            }

            //double percentToIncrement = 100.0 / Convert.ToDouble(selectedDirList.Count);
            double percentToIncrement = 100.0 / Convert.ToDouble(selectedObjectList.Count);
            double percentComplete = percentToIncrement;
            folderProgressBar.BeginInvoke((MethodInvoker)delegate { folderProgressBar.Value = 0; });


            //foreach (string dir in selectedDirList)
            foreach (ZpDirectory activeDir in selectedObjectList) {

                //Check if there is a request to cancel the process
                if (backgroundWorker1.CancellationPending) {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;
                }

                engine = new OptimizerEngine.OptimizerEngine(activeDir, backgroundWorker1);
                //engine.CompressSelected(compressionType); OLD
                engine.CompressActiveDir(activeDir, compressionType);

                int percentCompleteInt = Convert.ToInt32(percentComplete);

                folderProgressBar.BeginInvoke((MethodInvoker)delegate { folderProgressBar.Value = percentCompleteInt; });

                percentComplete += percentToIncrement;
            }


            //backgroundWorker1.ReportProgress(1);

            //If the process exits the loop, ensure that progress is set to 100%
            //folderProgressBar.Value = 100;
            //backgroundWorker1.ReportProgress(100);

            objectListView1.RefreshObjects(masterObjectList);

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            fileProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                labelResult.Text = "Process was cancelled";
            }
            else if (e.Error != null) {
                labelResult.Text = "There was an error running the process. The thread aborted";
            }
            else {
                labelResult.Text = "Process was completed";
            }
        }

        #endregion

        private void objectListView1_SelectedIndexChanged_1(object sender, EventArgs e) {

        }

        




    }
}
