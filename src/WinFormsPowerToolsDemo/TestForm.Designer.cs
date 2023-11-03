namespace WinFormsPowerToolsDemo
{
    partial class TestForm
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
            themedVerticalScrollbar1 = new WinFormsPowerTools.ThemedScrollBars.ThemedVerticalScrollbar();
            SuspendLayout();
            // 
            // themedVerticalScrollbar1
            // 
            themedVerticalScrollbar1.Dock = System.Windows.Forms.DockStyle.Right;
            themedVerticalScrollbar1.Location = new System.Drawing.Point(775, 0);
            themedVerticalScrollbar1.Name = "themedVerticalScrollbar1";
            themedVerticalScrollbar1.Size = new System.Drawing.Size(25, 450);
            themedVerticalScrollbar1.TabIndex = 0;
            themedVerticalScrollbar1.Text = "themedVerticalScrollbar1";
            themedVerticalScrollbar1.Value = 0F;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(themedVerticalScrollbar1);
            Name = "TestForm";
            Text = "TestForm";
            ResumeLayout(false);
        }

        #endregion

        private WinFormsPowerTools.ThemedScrollBars.ThemedVerticalScrollbar themedVerticalScrollbar1;
    }
}