using System;
using System.Diagnostics;

namespace WinFormsPowerToolsDemo
{
    partial class DocumentTestForm
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
                // Wie should always dispose!
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
            System.Windows.Forms.Documents.Document document1 = new System.Windows.Forms.Documents.Document();
            documentControl1 = new System.Windows.Forms.Documents.DocumentControl();
            SuspendLayout();
            // 
            // documentControl1
            // 
            documentControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            documentControl1.Location = new System.Drawing.Point(36, 28);
            document1.Height = 600F;
            document1.Width = 800F;
            documentControl1.MainDocument = document1;
            documentControl1.Name = "documentControl1";
            documentControl1.Size = new System.Drawing.Size(920, 669);
            documentControl1.TabIndex = 0;
            documentControl1.Text = "documentControl1";
            // 
            // DocumentTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(983, 741);
            Controls.Add(documentControl1);
            Name = "DocumentTestForm";
            Text = "DocumentFormTest";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Documents.DocumentControl documentControl1;
    }
}