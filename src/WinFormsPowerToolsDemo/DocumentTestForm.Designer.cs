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
            label1 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // documentControl1
            // 
            documentControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            documentControl1.Location = new System.Drawing.Point(32, 21);
            document1.Height = 600F;
            document1.Width = 800F;
            documentControl1.MainDocument = document1;
            documentControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            documentControl1.Name = "documentControl1";
            documentControl1.Size = new System.Drawing.Size(805, 331);
            documentControl1.TabIndex = 0;
            documentControl1.Text = "documentControl1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(326, 401);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(292, 448);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(152, 28);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // DocumentTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(860, 556);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(documentControl1);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "DocumentTestForm";
            Text = "DocumentFormTest";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Documents.DocumentControl documentControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}