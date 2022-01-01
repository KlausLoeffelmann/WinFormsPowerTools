namespace WinFormsPowerToolsDemo
{
    partial class SkiaSharpDemoForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.gdiplusTabPage = new System.Windows.Forms.TabPage();
            this.gdiPlusRenderTargetPanel = new WinFormsPowerToolsDemo.SkiaSharpDemoForm.GdiPlusRenderTargetPanel();
            this.skiaTabPage = new System.Windows.Forms.TabPage();
            this.skiaCanvasRenderTarget = new SkiaWinForms.SkiaCanvas();
            this.skiaGLTabPage = new System.Windows.Forms.TabPage();
            this.skiaCanvasGLRenderTarget = new SkiaWinForms.SkiaGLCanvas();
            this.startStopButton = new System.Windows.Forms.Button();
            this.openSkiaPlaygroundButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.gdiplusTabPage.SuspendLayout();
            this.skiaTabPage.SuspendLayout();
            this.skiaGLTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.gdiplusTabPage);
            this.tabControl1.Controls.Add(this.skiaTabPage);
            this.tabControl1.Controls.Add(this.skiaGLTabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(787, 543);
            this.tabControl1.TabIndex = 3;
            // 
            // gdiplusTabPage
            // 
            this.gdiplusTabPage.Controls.Add(this.gdiPlusRenderTargetPanel);
            this.gdiplusTabPage.Location = new System.Drawing.Point(4, 29);
            this.gdiplusTabPage.Name = "gdiplusTabPage";
            this.gdiplusTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.gdiplusTabPage.Size = new System.Drawing.Size(779, 510);
            this.gdiplusTabPage.TabIndex = 0;
            this.gdiplusTabPage.Text = "GDI+";
            this.gdiplusTabPage.UseVisualStyleBackColor = true;
            // 
            // gdiPlusRenderTargetPanel
            // 
            this.gdiPlusRenderTargetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gdiPlusRenderTargetPanel.BackColor = System.Drawing.Color.Black;
            this.gdiPlusRenderTargetPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gdiPlusRenderTargetPanel.Location = new System.Drawing.Point(6, 6);
            this.gdiPlusRenderTargetPanel.Name = "gdiPlusRenderTargetPanel";
            this.gdiPlusRenderTargetPanel.Size = new System.Drawing.Size(767, 498);
            this.gdiPlusRenderTargetPanel.TabIndex = 0;
            this.gdiPlusRenderTargetPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GdiPlus_Paint);
            this.gdiPlusRenderTargetPanel.Resize += new System.EventHandler(this.Panel_Resize);
            // 
            // skiaTabPage
            // 
            this.skiaTabPage.Controls.Add(this.skiaCanvasRenderTarget);
            this.skiaTabPage.Location = new System.Drawing.Point(4, 29);
            this.skiaTabPage.Name = "skiaTabPage";
            this.skiaTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.skiaTabPage.Size = new System.Drawing.Size(779, 510);
            this.skiaTabPage.TabIndex = 1;
            this.skiaTabPage.Text = "SkiaCanvas";
            this.skiaTabPage.UseVisualStyleBackColor = true;
            // 
            // skiaCanvasRenderTarget
            // 
            this.skiaCanvasRenderTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skiaCanvasRenderTarget.BackColor = System.Drawing.Color.Black;
            this.skiaCanvasRenderTarget.Location = new System.Drawing.Point(6, 6);
            this.skiaCanvasRenderTarget.Name = "skiaCanvasRenderTarget";
            this.skiaCanvasRenderTarget.Size = new System.Drawing.Size(767, 498);
            this.skiaCanvasRenderTarget.TabIndex = 3;
            this.skiaCanvasRenderTarget.Text = "skTestControl1";
            this.skiaCanvasRenderTarget.PaintSurface += new System.EventHandler<SkiaWinForms.SkiaPaintEventArgs>(this.SkiaAndSkiaGL_PaintSurface);
            this.skiaCanvasRenderTarget.Resize += new System.EventHandler(this.Panel_Resize);
            // 
            // skiaGLTabPage
            // 
            this.skiaGLTabPage.Controls.Add(this.skiaCanvasGLRenderTarget);
            this.skiaGLTabPage.Location = new System.Drawing.Point(4, 29);
            this.skiaGLTabPage.Name = "skiaGLTabPage";
            this.skiaGLTabPage.Size = new System.Drawing.Size(779, 510);
            this.skiaGLTabPage.TabIndex = 2;
            this.skiaGLTabPage.Text = "SkiaGLCanvas (Experimental)";
            this.skiaGLTabPage.UseVisualStyleBackColor = true;
            // 
            // skiaCanvasGLRenderTarget
            // 
            this.skiaCanvasGLRenderTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skiaCanvasGLRenderTarget.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            this.skiaCanvasGLRenderTarget.APIVersion = new System.Version(3, 3, 0, 0);
            this.skiaCanvasGLRenderTarget.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            this.skiaCanvasGLRenderTarget.IsEventDriven = true;
            this.skiaCanvasGLRenderTarget.Location = new System.Drawing.Point(15, 12);
            this.skiaCanvasGLRenderTarget.Name = "skiaCanvasGLRenderTarget";
            this.skiaCanvasGLRenderTarget.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            this.skiaCanvasGLRenderTarget.Size = new System.Drawing.Size(748, 477);
            this.skiaCanvasGLRenderTarget.TabIndex = 0;
            this.skiaCanvasGLRenderTarget.Text = "skiaglCanvas1";
            this.skiaCanvasGLRenderTarget.PaintSurface += new System.EventHandler<SkiaWinForms.SkiaPaintEventArgs>(this.SkiaAndSkiaGL_PaintSurface);
            // 
            // startStopButton
            // 
            this.startStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startStopButton.Location = new System.Drawing.Point(805, 47);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(158, 44);
            this.startStopButton.TabIndex = 4;
            this.startStopButton.Text = "Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // openSkiaPlaygroundButton
            // 
            this.openSkiaPlaygroundButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openSkiaPlaygroundButton.Location = new System.Drawing.Point(805, 118);
            this.openSkiaPlaygroundButton.Name = "openSkiaPlaygroundButton";
            this.openSkiaPlaygroundButton.Size = new System.Drawing.Size(158, 59);
            this.openSkiaPlaygroundButton.TabIndex = 5;
            this.openSkiaPlaygroundButton.Text = "Open Skia Playground...";
            this.openSkiaPlaygroundButton.UseVisualStyleBackColor = true;
            this.openSkiaPlaygroundButton.Click += new System.EventHandler(this.openSkiaPlaygroundButton_Click);
            // 
            // SkiaSharpDemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 562);
            this.Controls.Add(this.openSkiaPlaygroundButton);
            this.Controls.Add(this.startStopButton);
            this.Controls.Add(this.tabControl1);
            this.Name = "SkiaSharpDemoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SkiaSharp Demo";
            this.tabControl1.ResumeLayout(false);
            this.gdiplusTabPage.ResumeLayout(false);
            this.skiaTabPage.ResumeLayout(false);
            this.skiaGLTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage gdiplusTabPage;
        private GdiPlusRenderTargetPanel gdiPlusRenderTargetPanel;
        private System.Windows.Forms.TabPage skiaTabPage;
        private SkiaWinForms.SkiaCanvas skiaCanvasRenderTarget;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.Button openSkiaPlaygroundButton;
        private System.Windows.Forms.TabPage skiaGLTabPage;
        private SkiaWinForms.SkiaGLCanvas skiaCanvasGLRenderTarget;
    }
}
