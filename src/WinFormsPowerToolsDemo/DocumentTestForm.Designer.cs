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
            this.button1 = new System.Windows.Forms.Button();
            this.skTestControl1 = new SkWinFormsDocumentControl.SkTestControl();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(160, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 46);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // skTestControl1
            // 
            this.skTestControl1.Location = new System.Drawing.Point(12, 87);
            this.skTestControl1.Name = "skTestControl1";
            this.skTestControl1.Size = new System.Drawing.Size(796, 535);
            this.skTestControl1.TabIndex = 2;
            this.skTestControl1.Text = "skTestControl1";
            this.skTestControl1.PaintSurface += new System.EventHandler<SkWinFormsDocumentControl.SKPaintGLSurfaceEventArgs>(this.skTestControl1_PaintSurface);
            // 
            // DocumentTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 643);
            this.Controls.Add(this.skTestControl1);
            this.Controls.Add(this.button1);
            this.Name = "DocumentTestForm";
            this.Text = "DocumentFormTest";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private SkWinFormsDocumentControl.SkTestControl skTestControl1;
    }
}