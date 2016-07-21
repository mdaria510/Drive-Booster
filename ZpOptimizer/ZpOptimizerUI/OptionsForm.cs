using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ZpOptimizerUI;

namespace ZpOptimizerUI {
    public partial class OptionsForm : Form {
        public OptionsForm() {
            InitializeComponent();

            listBoxFolderList.Items.Clear();

            //Retrive saved folders and populate listbox
            foreach (string str in Settings1.Default.locationList) {
                listBoxFolderList.Items.Add(str);
            }

        }


        private void buttonAddFolder_Click(object sender, EventArgs e) {
            FolderBrowserDialog folderBrowserDialog1;
            folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDialog1.Description = "Select Steam Folder";
            folderBrowserDialog1.ShowNewFolderButton = false;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            
            if (result == DialogResult.OK) {
                //Add to the list
                listBoxFolderList.Items.Add(folderBrowserDialog1.SelectedPath.ToString());
            }
        }

        private void buttonRemoveFolder_Click(object sender, EventArgs e) {
            listBoxFolderList.Items.Remove(listBoxFolderList.SelectedItem);
        }


        private void buttonOK_Click(object sender, EventArgs e) {

            //Clear the old file
            Settings1.Default.locationList.Clear();

            //Recreate it with the new list
            foreach (string item in listBoxFolderList.Items) {
                Settings1.Default.locationList.Add(item);
            }

            //Save it and clean up
            Settings1.Default.Save();
            listBoxFolderList.Items.Clear();
            this.Close();

            //TODO: Make objectlist and form refresh itself after closing
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            //Just closes everything without saving
        }


        //UNUSED
        private void labelFolderListCaption_Click(object sender, EventArgs e) {
        }

        private void OptionsForm_FormClosed(object sender, FormClosedEventArgs e) {           
        }
    }
}
