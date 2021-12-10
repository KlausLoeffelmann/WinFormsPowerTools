using System.Windows.Forms;
using System.Windows.Forms.TemplateBinding;

namespace WinFormsPowerTools.UnitTests.TemplateBinding
{
    partial class TemplateBindingTestForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UiControllerBindingItem uiControllerBindingItem1 = new UiControllerBindingItem();
            UiControllerBindingItem uiControllerBindingItem2 = new UiControllerBindingItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateBindingTestForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.uiControllerManagerComponent1 = new UiControllerManagerComponent();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(421, 27);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(117, 99);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(421, 27);
            this.textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(117, 153);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(421, 27);
            this.textBox3.TabIndex = 2;
            // 
            // uiControllerManagerComponent1
            // 
            uiControllerBindingItem1.BindableComponentPropertyPath = "1";
            uiControllerBindingItem1.Name = "Name1";
            uiControllerBindingItem1.UiControllerPropertyPath = "2";
            uiControllerBindingItem2.BindableComponentPropertyPath = "3";
            uiControllerBindingItem2.Name = "Name2";
            uiControllerBindingItem2.UiControllerPropertyPath = "4";
            this.uiControllerManagerComponent1.BindingAssignments.Add(uiControllerBindingItem1);
            this.uiControllerManagerComponent1.BindingAssignments.Add(uiControllerBindingItem2);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(239, 249);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(363, 146);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private UiControllerManagerComponent uiControllerManagerComponent1;
        private Button button1;
    }
}