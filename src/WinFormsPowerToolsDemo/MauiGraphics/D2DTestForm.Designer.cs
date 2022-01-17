namespace WinFormsPowerToolsDemo.MauiGraphics
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
            this._d2dGraphicsView = new Microsoft.Maui.Graphics.D2D.WinForms.D2DGraphicsView();
            this._startStopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _d2dGraphicsView
            // 
            this._d2dGraphicsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._d2dGraphicsView.Drawable = null;
            this._d2dGraphicsView.Location = new System.Drawing.Point(12, 12);
            this._d2dGraphicsView.Name = "_d2dGraphicsView";
            this._d2dGraphicsView.Size = new System.Drawing.Size(2187, 1395);
            this._d2dGraphicsView.TabIndex = 0;
            this._d2dGraphicsView.Text = "d2dGraphicsView1";
            this._d2dGraphicsView.Resize += new System.EventHandler(this.D2dGraphicsView_Resize);
            // 
            // _startStopButton
            // 
            this._startStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._startStopButton.Location = new System.Drawing.Point(2029, 1422);
            this._startStopButton.Name = "_startStopButton";
            this._startStopButton.Size = new System.Drawing.Size(170, 44);
            this._startStopButton.TabIndex = 1;
            this._startStopButton.Text = "Start";
            this._startStopButton.UseVisualStyleBackColor = true;
            this._startStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // D2DTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2211, 1478);
            this.Controls.Add(this._startStopButton);
            this.Controls.Add(this._d2dGraphicsView);
            this.Name = "D2DTestForm";
            this.Text = "D2DTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Maui.Graphics.D2D.WinForms.D2DPanel d2dPanel1;
        private Microsoft.Maui.Graphics.D2D.WinForms.D2DGraphicsView _d2dGraphicsView;
        private System.Windows.Forms.Button _startStopButton;
    }
}