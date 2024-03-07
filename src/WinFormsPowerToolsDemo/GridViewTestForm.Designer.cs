using System;
using System.Diagnostics;

namespace WinFormsPowerToolsDemo
{
    partial class GridViewTestForm
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
            gridView1 = new WinForms.PowerTools.Controls.GridView();
            SuspendLayout();
            // 
            // gridView1
            // 
            gridView1.AdjacentItemsCount = 1;
            gridView1.Location = new System.Drawing.Point(61, 67);
            gridView1.MainDocument = null;
            gridView1.Name = "gridView1";
            gridView1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            gridView1.Size = new System.Drawing.Size(587, 468);
            gridView1.TabIndex = 0;
            gridView1.Text = "gridView1";
            gridView1.ViewTemplate = null;
            // 
            // GridViewTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(983, 741);
            Controls.Add(gridView1);
            Name = "GridViewTestForm";
            Text = "DocumentFormTest";
            ResumeLayout(false);
        }

        #endregion

        private WinForms.PowerTools.Controls.GridView gridView1;
    }
}