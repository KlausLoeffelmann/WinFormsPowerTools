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
            this.d2dPanel1 = new System.Windows.Forms.Direct2D.Direct2DPanel();
            this._btnWritePixels = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // d2dPanel1
            // 
            this.d2dPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.d2dPanel1.Location = new System.Drawing.Point(35, 32);
            this.d2dPanel1.Name = "d2dPanel1";
            this.d2dPanel1.Size = new System.Drawing.Size(728, 350);
            this.d2dPanel1.TabIndex = 0;
            this.d2dPanel1.Text = "d2dPanel1";
            this.d2dPanel1.PaintIGraphics += new System.EventHandler<System.Windows.Forms.Direct2D.GraphicsPaintEventArgs>(this.d2dPanel1_PaintIGraphics);
            // 
            // _btnWritePixels
            // 
            this._btnWritePixels.Location = new System.Drawing.Point(342, 393);
            this._btnWritePixels.Name = "_btnWritePixels";
            this._btnWritePixels.Size = new System.Drawing.Size(171, 45);
            this._btnWritePixels.TabIndex = 1;
            this._btnWritePixels.Text = "Write Pixels";
            this._btnWritePixels.UseVisualStyleBackColor = true;
            this._btnWritePixels.Click += new System.EventHandler(this._btnWritePixels_Click);
            // 
            // D2DTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._btnWritePixels);
            this.Controls.Add(this.d2dPanel1);
            this.Name = "D2DTestForm";
            this.Text = "D2DTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Direct2D.Direct2DPanel d2dPanel1;
        private System.Windows.Forms.Button _btnWritePixels;
    }
}