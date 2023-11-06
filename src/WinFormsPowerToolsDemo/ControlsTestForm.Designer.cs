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
            textBox1 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            menuStrip2 = new System.Windows.Forms.MenuStrip();
            toolStripFluentSymbolMenuItem2 = new PowerTools.Controls.ToolStripFluentSymbolMenuItem();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            menuStrip1.ImageScalingSize = new System.Drawing.Size(128, 128);
            menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            menuStrip1.Location = new System.Drawing.Point(0, 28);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(30, 422);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(277, 248);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(260, 27);
            textBox1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(399, 331);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(144, 66);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // menuStrip2
            // 
            menuStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripFluentSymbolMenuItem2 });
            menuStrip2.Location = new System.Drawing.Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new System.Drawing.Size(800, 28);
            menuStrip2.TabIndex = 3;
            menuStrip2.Text = "menuStrip2";
            // 
            // toolStripFluentSymbolMenuItem2
            // 
            toolStripFluentSymbolMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            toolStripFluentSymbolMenuItem2.Name = "toolStripFluentSymbolMenuItem2";
            toolStripFluentSymbolMenuItem2.Size = new System.Drawing.Size(49, 24);
            toolStripFluentSymbolMenuItem2.Symbol = null;
            toolStripFluentSymbolMenuItem2.SymbolColor = System.Drawing.Color.Blue;
            toolStripFluentSymbolMenuItem2.SymbolOffset = new System.Drawing.Size(0, 0);
            toolStripFluentSymbolMenuItem2.SymbolSize = new System.Drawing.Size(32, 32);
            toolStripFluentSymbolMenuItem2.Text = "Test";
            toolStripFluentSymbolMenuItem2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ControlsTestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(menuStrip1);
            Controls.Add(menuStrip2);
            MainMenuStrip = menuStrip1;
            Name = "ControlsTestForm";
            Text = "DarkModeTestForm2";
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PowerTools.Components.ThemingComponent themingComponent1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private PowerTools.Controls.ToolStripFluentSymbolMenuItem toolStripFluentSymbolMenuItem1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private PowerTools.Components.ThemingComponent themingComponent2;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private PowerTools.Controls.ToolStripFluentSymbolMenuItem toolStripFluentSymbolMenuItem2;
    }
}