namespace ZpOptimizerUI {
    partial class OptionsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listBoxFolderList = new System.Windows.Forms.ListBox();
            this.buttonAddFolder = new System.Windows.Forms.Button();
            this.buttonRemoveFolder = new System.Windows.Forms.Button();
            this.labelFolderListCaption1 = new System.Windows.Forms.Label();
            this.labelFolderListCaption2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxFolderList
            // 
            this.listBoxFolderList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxFolderList.FormattingEnabled = true;
            this.listBoxFolderList.Location = new System.Drawing.Point(12, 9);
            this.listBoxFolderList.Name = "listBoxFolderList";
            this.listBoxFolderList.Size = new System.Drawing.Size(354, 158);
            this.listBoxFolderList.TabIndex = 0;
            // 
            // buttonAddFolder
            // 
            this.buttonAddFolder.Location = new System.Drawing.Point(372, 9);
            this.buttonAddFolder.Name = "buttonAddFolder";
            this.buttonAddFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFolder.TabIndex = 1;
            this.buttonAddFolder.Text = "Add";
            this.buttonAddFolder.UseVisualStyleBackColor = true;
            this.buttonAddFolder.Click += new System.EventHandler(this.buttonAddFolder_Click);
            // 
            // buttonRemoveFolder
            // 
            this.buttonRemoveFolder.Location = new System.Drawing.Point(372, 38);
            this.buttonRemoveFolder.Name = "buttonRemoveFolder";
            this.buttonRemoveFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveFolder.TabIndex = 2;
            this.buttonRemoveFolder.Text = "Remove";
            this.buttonRemoveFolder.UseVisualStyleBackColor = true;
            this.buttonRemoveFolder.Click += new System.EventHandler(this.buttonRemoveFolder_Click);
            // 
            // labelFolderListCaption1
            // 
            this.labelFolderListCaption1.AutoSize = true;
            this.labelFolderListCaption1.Location = new System.Drawing.Point(12, 176);
            this.labelFolderListCaption1.Name = "labelFolderListCaption1";
            this.labelFolderListCaption1.Size = new System.Drawing.Size(167, 13);
            this.labelFolderListCaption1.TabIndex = 4;
            this.labelFolderListCaption1.Text = "Select folders to search for games";
            this.labelFolderListCaption1.Click += new System.EventHandler(this.labelFolderListCaption_Click);
            // 
            // labelFolderListCaption2
            // 
            this.labelFolderListCaption2.AutoSize = true;
            this.labelFolderListCaption2.Location = new System.Drawing.Point(12, 191);
            this.labelFolderListCaption2.Name = "labelFolderListCaption2";
            this.labelFolderListCaption2.Size = new System.Drawing.Size(243, 13);
            this.labelFolderListCaption2.TabIndex = 5;
            this.labelFolderListCaption2.Text = "(Ex: C:\\Program Files (x86)\\Steamapps\\common\\)";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(291, 181);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(372, 181);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(459, 216);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelFolderListCaption2);
            this.Controls.Add(this.labelFolderListCaption1);
            this.Controls.Add(this.buttonRemoveFolder);
            this.Controls.Add(this.buttonAddFolder);
            this.Controls.Add(this.listBoxFolderList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OptionsForm";
            this.Text = "Locations";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OptionsForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxFolderList;
        private System.Windows.Forms.Button buttonAddFolder;
        private System.Windows.Forms.Button buttonRemoveFolder;
        private System.Windows.Forms.Label labelFolderListCaption1;
        private System.Windows.Forms.Label labelFolderListCaption2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}