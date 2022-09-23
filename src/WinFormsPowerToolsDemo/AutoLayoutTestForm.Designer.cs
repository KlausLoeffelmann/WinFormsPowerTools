using System.Windows.Forms.DataEntryForms.Controls;

namespace ExtenderPropertiesTest
{
    partial class AutoLayoutTestForm
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
            this.SuspendLayout();
            // 
            // AutoLayoutTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 377);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AutoLayoutTestForm";
            this.Text = "AutoLayoutTestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private WinFormsPowerToolsDemo.AutoLayoutTestUserControl autoLayoutTestUserControl1;
    }
}