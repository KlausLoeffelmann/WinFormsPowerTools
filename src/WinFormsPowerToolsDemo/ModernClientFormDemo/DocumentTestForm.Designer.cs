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
            themedVerticalScrollbar1 = new WinFormsPowerTools.ThemedScrollBars.ThemedVerticalScrollbar();
            SuspendLayout();
            // 
            // themedVerticalScrollbar1
            // 
            themedVerticalScrollbar1.Dock = System.Windows.Forms.DockStyle.Right;
            themedVerticalScrollbar1.IsDarkMode = true;
            themedVerticalScrollbar1.Location = new System.Drawing.Point(930, 0);
            themedVerticalScrollbar1.Name = "themedVerticalScrollbar1";
            themedVerticalScrollbar1.Size = new System.Drawing.Size(20, 741);
            themedVerticalScrollbar1.TabIndex = 0;
            themedVerticalScrollbar1.Text = "themedVerticalScrollbar1";
            themedVerticalScrollbar1.Value = 0F;
            // 
            // DocumentTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(32, 32, 32);
            ClientSize = new System.Drawing.Size(950, 741);
            Controls.Add(themedVerticalScrollbar1);
            Name = "DocumentTestForm";
            Text = "DocumentFormTest";
            ResumeLayout(false);
        }

        #endregion

        private WinFormsPowerTools.ThemedScrollBars.ThemedVerticalScrollbar themedVerticalScrollbar1;
    }
}