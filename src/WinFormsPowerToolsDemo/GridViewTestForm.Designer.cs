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
            _gridView = new WinForms.PowerTools.Controls.GridView();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            test1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // _gridView
            // 
            _gridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            _gridView.Location = new System.Drawing.Point(12, 43);
            _gridView.MinimumAdjacentItemsCount = 1;
            _gridView.Name = "_gridView";
            _gridView.Orientation = System.Windows.Forms.Orientation.Horizontal;
            _gridView.Size = new System.Drawing.Size(1082, 551);
            _gridView.TabIndex = 0;
            _gridView.Text = "gridView1";
            _gridView.ViewTemplate = null;
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { test1ToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1106, 31);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // test1ToolStripMenuItem
            // 
            test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
            test1ToolStripMenuItem.Size = new System.Drawing.Size(67, 27);
            test1ToolStripMenuItem.Text = "Test &1";
            test1ToolStripMenuItem.Click += test1ToolStripMenuItem_Click;
            // 
            // GridViewTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1106, 606);
            Controls.Add(_gridView);
            Controls.Add(menuStrip1);
            Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip1;
            Name = "GridViewTestForm";
            Text = "DocumentFormTest";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WinForms.PowerTools.Controls.GridView _gridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem test1ToolStripMenuItem;
    }
}