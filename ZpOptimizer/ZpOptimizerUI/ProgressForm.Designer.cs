namespace ZpOptimizerUI {
    partial class ProgressForm {
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.filesProgressBar = new System.Windows.Forms.ProgressBar();
            this.foldersProgressBar = new System.Windows.Forms.ProgressBar();
            this.currentFileLabel = new System.Windows.Forms.Label();
            this.currentFolderLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(98, 226);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // filesProgressBar
            // 
            this.filesProgressBar.Location = new System.Drawing.Point(13, 70);
            this.filesProgressBar.Name = "filesProgressBar";
            this.filesProgressBar.Size = new System.Drawing.Size(259, 35);
            this.filesProgressBar.TabIndex = 1;
            // 
            // foldersProgressBar
            // 
            this.foldersProgressBar.Location = new System.Drawing.Point(13, 171);
            this.foldersProgressBar.Name = "foldersProgressBar";
            this.foldersProgressBar.Size = new System.Drawing.Size(259, 35);
            this.foldersProgressBar.TabIndex = 2;
            // 
            // currentFileLabel
            // 
            this.currentFileLabel.AutoSize = true;
            this.currentFileLabel.Location = new System.Drawing.Point(12, 54);
            this.currentFileLabel.Name = "currentFileLabel";
            this.currentFileLabel.Size = new System.Drawing.Size(82, 13);
            this.currentFileLabel.TabIndex = 3;
            this.currentFileLabel.Text = "currentFileLabel";
            // 
            // currentFolderLabel
            // 
            this.currentFolderLabel.AutoSize = true;
            this.currentFolderLabel.Location = new System.Drawing.Point(12, 155);
            this.currentFolderLabel.Name = "currentFolderLabel";
            this.currentFolderLabel.Size = new System.Drawing.Size(95, 13);
            this.currentFolderLabel.TabIndex = 4;
            this.currentFolderLabel.Text = "currentFolderLabel";
            // 
            // ProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.currentFolderLabel);
            this.Controls.Add(this.currentFileLabel);
            this.Controls.Add(this.foldersProgressBar);
            this.Controls.Add(this.filesProgressBar);
            this.Controls.Add(this.cancelButton);
            this.Name = "ProgressForm";
            this.Text = "Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ProgressBar filesProgressBar;
        private System.Windows.Forms.ProgressBar foldersProgressBar;
        private System.Windows.Forms.Label currentFileLabel;
        private System.Windows.Forms.Label currentFolderLabel;
    }
}