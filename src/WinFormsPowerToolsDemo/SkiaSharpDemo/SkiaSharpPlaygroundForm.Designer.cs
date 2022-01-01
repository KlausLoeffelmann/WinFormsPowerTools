namespace WinFormsPowerToolsDemo
{
    partial class SkiaSharpPlaygroundForm
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
            this.skiaCanvas1 = new SkiaWinForms.SkiaCanvas();
            this.testButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // skiaCanvas1
            // 
            this.skiaCanvas1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skiaCanvas1.BackColor = System.Drawing.Color.Black;
            this.skiaCanvas1.Location = new System.Drawing.Point(14, 25);
            this.skiaCanvas1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.skiaCanvas1.Name = "skiaCanvas1";
            this.skiaCanvas1.Size = new System.Drawing.Size(865, 720);
            this.skiaCanvas1.TabIndex = 0;
            this.skiaCanvas1.PaintSurface += new System.EventHandler<SkiaWinForms.SkiaPaintEventArgs>(this.skiaCanvas1_PaintSurface);
            // 
            // testButton
            // 
            this.testButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.testButton.Location = new System.Drawing.Point(380, 778);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(139, 41);
            this.testButton.TabIndex = 1;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            // 
            // SkiaSharpPlaygroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 843);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.skiaCanvas1);
            this.Name = "SkiaSharpPlaygroundForm";
            this.Text = "SkiaSharpPlaygroundForm";
            this.ResumeLayout(false);

        }

        #endregion

        private SkiaWinForms.SkiaCanvas skiaCanvas1;
        private System.Windows.Forms.Button testButton;
    }
}