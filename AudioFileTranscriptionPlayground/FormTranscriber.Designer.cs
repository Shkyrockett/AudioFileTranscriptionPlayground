// <copyright file="FormTranscriber.Designer.cs" company="Shkyrockett" >
//     Copyright © 2020 - 2022 Shkyrockett. All rights reserved.
// </copyright>
// <author id="shkyrockett">Shkyrockett</author>
// <license>
//     Licensed under the MIT License. See LICENSE file in the project root for full license information.
// </license>
// <summary></summary>
// <remarks>
// </remarks>

namespace AudioFileTranscriptionPlayground
{
    /// <summary>
    /// The form transcriber.
    /// </summary>
    partial class FormTranscriber
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.TextBox textBoxTranscript;
        private System.Windows.Forms.Button buttonExecute;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ProgressBar progressBar;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components is not null))
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
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.textBoxTranscript = new System.Windows.Forms.TextBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonTranscribe = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonStopTranscription = new System.Windows.Forms.Button();
            this.buttonSaveResultsTextBox = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxFile
            // 
            this.textBoxFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxFile.Location = new System.Drawing.Point(14, 16);
            this.textBoxFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.PlaceholderText = "Select Audio File";
            this.textBoxFile.Size = new System.Drawing.Size(884, 23);
            this.textBoxFile.TabIndex = 0;
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectFile.Location = new System.Drawing.Point(897, 16);
            this.buttonSelectFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(23, 23);
            this.buttonSelectFile.TabIndex = 1;
            this.buttonSelectFile.Text = "…";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.ButtonSelectFile_Click);
            // 
            // textBoxTranscript
            // 
            this.textBoxTranscript.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTranscript.Location = new System.Drawing.Point(14, 45);
            this.textBoxTranscript.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBoxTranscript.Multiline = true;
            this.textBoxTranscript.Name = "textBoxTranscript";
            this.textBoxTranscript.PlaceholderText = "Awaiting audio file selection...";
            this.textBoxTranscript.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxTranscript.Size = new System.Drawing.Size(906, 400);
            this.textBoxTranscript.TabIndex = 2;
            // 
            // buttonExecute
            // 
            this.buttonExecute.Location = new System.Drawing.Point(12, 451);
            this.buttonExecute.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(906, 22);
            this.buttonExecute.TabIndex = 3;
            this.buttonExecute.Text = "Process File";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.ButtonExecute_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 451);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(907, 22);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 4;
            this.progressBar.Visible = false;
            // 
            // buttonTranscribe
            // 
            this.buttonTranscribe.Location = new System.Drawing.Point(3, 3);
            this.buttonTranscribe.Name = "buttonTranscribe";
            this.buttonTranscribe.Size = new System.Drawing.Size(296, 22);
            this.buttonTranscribe.TabIndex = 5;
            this.buttonTranscribe.Text = "Interactive Transcribe";
            this.buttonTranscribe.UseVisualStyleBackColor = true;
            this.buttonTranscribe.Click += new System.EventHandler(this.ButtonTranscribe_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.buttonTranscribe, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonStopTranscription, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSaveResultsTextBox, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 479);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(909, 28);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // buttonStopTranscription
            // 
            this.buttonStopTranscription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonStopTranscription.Enabled = false;
            this.buttonStopTranscription.Location = new System.Drawing.Point(306, 3);
            this.buttonStopTranscription.Name = "buttonStopTranscription";
            this.buttonStopTranscription.Size = new System.Drawing.Size(297, 22);
            this.buttonStopTranscription.TabIndex = 6;
            this.buttonStopTranscription.Text = "Stop Transcription";
            this.buttonStopTranscription.UseVisualStyleBackColor = true;
            this.buttonStopTranscription.Click += new System.EventHandler(this.ButtonStopTranscription_Click);
            // 
            // buttonSaveResultsTextBox
            // 
            this.buttonSaveResultsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSaveResultsTextBox.Enabled = false;
            this.buttonSaveResultsTextBox.Location = new System.Drawing.Point(609, 3);
            this.buttonSaveResultsTextBox.Name = "buttonSaveResultsTextBox";
            this.buttonSaveResultsTextBox.Size = new System.Drawing.Size(297, 22);
            this.buttonSaveResultsTextBox.TabIndex = 7;
            this.buttonSaveResultsTextBox.Text = "Save Results...";
            this.buttonSaveResultsTextBox.UseVisualStyleBackColor = true;
            this.buttonSaveResultsTextBox.Click += new System.EventHandler(this.ButtonSaveResultsTextBox_Click);
            // 
            // FormTranscriber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.textBoxTranscript);
            this.Controls.Add(this.buttonSelectFile);
            this.Controls.Add(this.textBoxFile);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormTranscriber";
            this.Text = "Audio Transcription";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTranscriber_FormClosed);
            this.Load += new System.EventHandler(this.FormTranscriber_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Button buttonTranscribe;
        private TableLayoutPanel tableLayoutPanel1;
        private Button buttonStopTranscription;
        private Button buttonSaveResultsTextBox;
        private SaveFileDialog saveFileDialog1;
    }
}

