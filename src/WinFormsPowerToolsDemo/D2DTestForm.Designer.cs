namespace WinFormsPowerToolsDemo
{
    partial class D2DTestForm
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
            this.d2dPanel1 = new WinformsPowerTools.Direct2D.D2DPanel();
            this.SuspendLayout();
            // 
            // d2dPanel1
            // 
            this.d2dPanel1.Location = new System.Drawing.Point(26, 28);
            this.d2dPanel1.Name = "d2dPanel1";
            this.d2dPanel1.Size = new System.Drawing.Size(843, 584);
            this.d2dPanel1.TabIndex = 0;
            this.d2dPanel1.Text = "d2dPanel1";
            // 
            // D2DTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 653);
            this.Controls.Add(this.d2dPanel1);
            this.Name = "D2DTestForm";
            this.Text = "D2DTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private WinformsPowerTools.Direct2D.D2DPanel d2dPanel1;
    }
}