namespace TransporterClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonUpload = new System.Windows.Forms.Button();
            this.textBoxUploadFileName = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBoxTargetFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxReceiver = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBrowseFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.checkBoxConvert = new System.Windows.Forms.CheckBox();
            this.radioAudio = new System.Windows.Forms.RadioButton();
            this.radioVideo = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioOther = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // buttonUpload
            // 
            this.buttonUpload.Enabled = false;
            this.buttonUpload.Location = new System.Drawing.Point(320, 212);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(75, 23);
            this.buttonUpload.TabIndex = 0;
            this.buttonUpload.Text = "UPLOAD";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // textBoxUploadFileName
            // 
            this.textBoxUploadFileName.Location = new System.Drawing.Point(12, 112);
            this.textBoxUploadFileName.Name = "textBoxUploadFileName";
            this.textBoxUploadFileName.ReadOnly = true;
            this.textBoxUploadFileName.Size = new System.Drawing.Size(287, 20);
            this.textBoxUploadFileName.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 161);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(383, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // textBoxTargetFolder
            // 
            this.textBoxTargetFolder.Location = new System.Drawing.Point(16, 64);
            this.textBoxTargetFolder.Name = "textBoxTargetFolder";
            this.textBoxTargetFolder.ReadOnly = true;
            this.textBoxTargetFolder.Size = new System.Drawing.Size(287, 20);
            this.textBoxTargetFolder.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server Target Folder";
            // 
            // textBoxReceiver
            // 
            this.textBoxReceiver.Location = new System.Drawing.Point(18, 24);
            this.textBoxReceiver.Name = "textBoxReceiver";
            this.textBoxReceiver.ReadOnly = true;
            this.textBoxReceiver.Size = new System.Drawing.Size(284, 20);
            this.textBoxReceiver.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Server Address";
            // 
            // buttonBrowseFile
            // 
            this.buttonBrowseFile.Location = new System.Drawing.Point(320, 112);
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.buttonBrowseFile.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowseFile.TabIndex = 7;
            this.buttonBrowseFile.Text = "Browse...";
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Browse and select input audio/video file";
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelProgress.ForeColor = System.Drawing.Color.Green;
            this.labelProgress.Location = new System.Drawing.Point(9, 242);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(43, 13);
            this.labelProgress.TabIndex = 11;
            this.labelProgress.Text = "Ready";
            // 
            // checkBoxConvert
            // 
            this.checkBoxConvert.AutoSize = true;
            this.checkBoxConvert.Location = new System.Drawing.Point(12, 190);
            this.checkBoxConvert.Name = "checkBoxConvert";
            this.checkBoxConvert.Size = new System.Drawing.Size(269, 17);
            this.checkBoxConvert.TabIndex = 12;
            this.checkBoxConvert.Text = "Re-Encode Input audio/video file for best streaming";
            this.checkBoxConvert.UseVisualStyleBackColor = true;
            this.checkBoxConvert.Click += new System.EventHandler(this.checkBoxConvert_Click);
            this.checkBoxConvert.CheckedChanged += new System.EventHandler(this.checkBoxConvert_CheckedChanged);
            // 
            // radioAudio
            // 
            this.radioAudio.AutoSize = true;
            this.radioAudio.Location = new System.Drawing.Point(110, 135);
            this.radioAudio.Name = "radioAudio";
            this.radioAudio.Size = new System.Drawing.Size(52, 17);
            this.radioAudio.TabIndex = 13;
            this.radioAudio.Text = "Audio";
            this.radioAudio.UseVisualStyleBackColor = true;
            this.radioAudio.Click += new System.EventHandler(this.radioAudio_Click);
            // 
            // radioVideo
            // 
            this.radioVideo.AutoSize = true;
            this.radioVideo.Location = new System.Drawing.Point(168, 135);
            this.radioVideo.Name = "radioVideo";
            this.radioVideo.Size = new System.Drawing.Size(52, 17);
            this.radioVideo.TabIndex = 14;
            this.radioVideo.Text = "Video";
            this.radioVideo.UseVisualStyleBackColor = true;
            this.radioVideo.Click += new System.EventHandler(this.radioVideo_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Stream File Type:";
            // 
            // radioOther
            // 
            this.radioOther.AutoSize = true;
            this.radioOther.Location = new System.Drawing.Point(224, 136);
            this.radioOther.Name = "radioOther";
            this.radioOther.Size = new System.Drawing.Size(51, 17);
            this.radioOther.TabIndex = 16;
            this.radioOther.TabStop = true;
            this.radioOther.Text = "Other";
            this.radioOther.UseVisualStyleBackColor = true;
            this.radioOther.CheckedChanged += new System.EventHandler(this.radioOther_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 264);
            this.Controls.Add(this.radioOther);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioVideo);
            this.Controls.Add(this.radioAudio);
            this.Controls.Add(this.checkBoxConvert);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonBrowseFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxReceiver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxTargetFolder);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.textBoxUploadFileName);
            this.Controls.Add(this.buttonUpload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Transporter V3.2.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.TextBox textBoxUploadFileName;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBoxTargetFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxReceiver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonBrowseFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.CheckBox checkBoxConvert;
        private System.Windows.Forms.RadioButton radioAudio;
        private System.Windows.Forms.RadioButton radioVideo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioOther;
    }
}

