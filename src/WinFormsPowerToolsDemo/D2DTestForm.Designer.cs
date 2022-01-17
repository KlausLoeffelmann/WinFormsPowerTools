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
            this.d2dGraphicsView1 = new Microsoft.Maui.Graphics.D2D.WinForms.D2DGraphicsView();
            this.SuspendLayout();
            // 
            // d2dGraphicsView1
            // 
            this.d2dGraphicsView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.d2dGraphicsView1.Drawable = null;
            this.d2dGraphicsView1.Location = new System.Drawing.Point(12, 12);
            this.d2dGraphicsView1.Name = "d2dGraphicsView1";
            this.d2dGraphicsView1.Size = new System.Drawing.Size(881, 629);
            this.d2dGraphicsView1.TabIndex = 0;
            this.d2dGraphicsView1.Text = "d2dGraphicsView1";
            // 
            // D2DTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 653);
            this.Controls.Add(this.d2dGraphicsView1);
            this.Name = "D2DTestForm";
            this.Text = "D2DTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Maui.Graphics.D2D.WinForms.D2DPanel d2dPanel1;
        private Microsoft.Maui.Graphics.D2D.WinForms.D2DGraphicsView d2dGraphicsView1;
    }
}