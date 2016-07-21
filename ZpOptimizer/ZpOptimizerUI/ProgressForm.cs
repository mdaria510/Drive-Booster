using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OptimizerEngine.FileSystem;
using static OptimizerEngine.Helpers.Globals;
using OptimizerEngine.DirCompressors;
using OptimizerEngine;
using System.Threading;


namespace ZpOptimizerUI {
    public partial class ProgressForm : Form {

        private bool m_running = false;
        CancellationTokenSource m_cancelTokenSource = null;

        public ProgressForm(List<ZpDirectory> zpDirList, DirCompressionTypes compressionType) {
       
            InitializeComponent();

           


        }

      


    private void button1_Click(object sender, EventArgs e) {
            //CANCEL OPERATION
        }

        //CHECK ON STATUS AND
        //UPDATE PROGRESS BARS AND LABELS


    }
}
