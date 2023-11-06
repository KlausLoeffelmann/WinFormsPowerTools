namespace WinForms.PowerToolsDemo
{
    partial class ControlsTestForm
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
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            toolStripFluentSymbolMenuItem2 = new PowerTools.Controls.ToolStripFluentSymbolMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripFluentSymbolMenuItem2 });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(800, 92);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripFluentSymbolMenuItem2
            // 
            toolStripFluentSymbolMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            toolStripFluentSymbolMenuItem2.Name = "toolStripFluentSymbolMenuItem2";
            toolStripFluentSymbolMenuItem2.ScalePercentage = 100;
            toolStripFluentSymbolMenuItem2.Size = new System.Drawing.Size(78, 88);
            toolStripFluentSymbolMenuItem2.Symbol = PowerTools.Controls.SegoeFluentIcons.Calculator;
            toolStripFluentSymbolMenuItem2.SymbolColor = System.Drawing.Color.FromArgb(0, 0, 192);
            toolStripFluentSymbolMenuItem2.SymbolSize = new System.Drawing.Size(64, 64);
            toolStripFluentSymbolMenuItem2.Text = "Test";
            toolStripFluentSymbolMenuItem2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ControlsTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "ControlsTestForm";
            Text = "DarkModeTestForm2";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PowerTools.Components.ThemingComponent themingComponent1;
        private PowerTools.Controls.ToolStripFluentSymbolMenuItem toolStripFluentSymbolMenuItem1;
        private PowerTools.Components.ThemingComponent themingComponent2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private PowerTools.Controls.ToolStripFluentSymbolMenuItem toolStripFluentSymbolMenuItem2;
    }
}