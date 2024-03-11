namespace WinFormsPowerToolsDemo
{
    partial class ThreadingGraphicsTestForm
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
            customPanel1 = new CustomPanel();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripSymbolDropDownButton1 = new WinForms.PowerTools.Controls.ToolStripSymbolDropDownButton();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // customPanel1
            // 
            customPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            customPanel1.Location = new System.Drawing.Point(12, 12);
            customPanel1.Name = "customPanel1";
            customPanel1.Size = new System.Drawing.Size(1301, 710);
            customPanel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripSymbolDropDownButton1 });
            statusStrip1.Location = new System.Drawing.Point(0, 718);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            statusStrip1.Size = new System.Drawing.Size(1325, 67);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripSymbolDropDownButton1
            // 
            toolStripSymbolDropDownButton1.AutoSize = true;
            toolStripSymbolDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            toolStripSymbolDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripSymbolDropDownButton1.Name = "toolStripSymbolDropDownButton1";
            toolStripSymbolDropDownButton1.Size = new System.Drawing.Size(178, 65);
            toolStripSymbolDropDownButton1.Symbol = WinForms.PowerTools.Controls.SegoeFluentIcons.InkingTool;
            toolStripSymbolDropDownButton1.SymbolColor = System.Drawing.SystemColors.ButtonFace;
            toolStripSymbolDropDownButton1.SymbolScaling = 100;
            toolStripSymbolDropDownButton1.SymbolSize = new System.Drawing.Size(36, 36);
            toolStripSymbolDropDownButton1.Text = "Async Spiral-Demo";
            toolStripSymbolDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            toolStripSymbolDropDownButton1.Click += toolStripSymbolDropDownButton1_Click;
            // 
            // ThreadingGraphicsTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1325, 785);
            Controls.Add(statusStrip1);
            Controls.Add(customPanel1);
            Name = "ThreadingGraphicsTestForm";
            Text = "ThreadingGraphicsTestForm";
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomPanel customPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private WinForms.PowerTools.Controls.ToolStripSymbolDropDownButton toolStripSymbolDropDownButton1;
    }
}