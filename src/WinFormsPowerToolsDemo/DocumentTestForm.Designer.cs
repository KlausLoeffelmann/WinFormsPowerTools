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
            System.Windows.Forms.Documents.Document document1 = new System.Windows.Forms.Documents.Document();
            this.documentControl1 = new System.Windows.Forms.Documents.DocumentControl();
            this.SuspendLayout();
            // 
            // documentControl1
            // 
            this.documentControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            document1.Height = 600F;
            document1.Width = 800F;
            this.documentControl1.Document = document1;
            this.documentControl1.Location = new System.Drawing.Point(12, 12);
            this.documentControl1.Name = "documentControl1";
            this.documentControl1.Size = new System.Drawing.Size(806, 619);
            this.documentControl1.TabIndex = 0;
            this.documentControl1.Text = "documentControl1";
            this.documentControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.documentControl1_Paint);
            // 
            // DocumentTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 643);
            this.Controls.Add(this.documentControl1);
            this.Name = "DocumentTestForm";
            this.Text = "DocumentFormTest";
            this.Load += new System.EventHandler(this.DocumentTestForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Documents.DocumentControl documentControl1;
    }
}