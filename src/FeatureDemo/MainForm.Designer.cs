using FeatureDemo.Components;
using FeatureDemo.Controls.ToolStrip;

namespace FeatureDemo
{
    partial class MainForm
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
            menuStrip1 = new MenuStrip();
            _asyncEventsDemo = new WinForms.PowerTools.Controls.ToolStripSymbolMenuItem();
            _spiralAsyncDemo = new ToolStripMenuItem();
            _flashingTitleDemo = new ToolStripMenuItem();
            _asyncListViewBindingDemo = new ToolStripMenuItem();
            toolStripSymbolMenuItem2 = new WinForms.PowerTools.Controls.ToolStripSymbolMenuItem();
            debugPaneToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            _lblRunningNowText = new ToolStripStatusLabel();
            _lblDemoName = new ToolStripStatusLabel();
            _debugStatusPanel = new ToolsStripDebugPanel();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            _clockLabel = new ToolStripStatusLabel();
            _periodicTimerComponent = new PeriodicTimerComponent();
            _flashingTitleTimer = new PeriodicTimerComponent();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 10.8F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { _asyncEventsDemo, _spiralAsyncDemo, _flashingTitleDemo, _asyncListViewBindingDemo, toolStripSymbolMenuItem2 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.Size = new Size(1121, 38);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // _asyncEventsDemo
            // 
            _asyncEventsDemo.AutoSize = true;
            _asyncEventsDemo.ImageScaling = ToolStripItemImageScaling.None;
            _asyncEventsDemo.Margin = new Padding(5);
            _asyncEventsDemo.Name = "_asyncEventsDemo";
            _asyncEventsDemo.Size = new Size(63, 24);
            _asyncEventsDemo.SymbolColor = SystemColors.ControlText;
            _asyncEventsDemo.SymbolScaling = 100;
            _asyncEventsDemo.SymbolSize = new Size(32, 32);
            _asyncEventsDemo.Text = "Events";
            _asyncEventsDemo.TextImageRelation = TextImageRelation.ImageAboveText;
            _asyncEventsDemo.Click += Demo1_AsyncEventsDemo_Click;
            // 
            // _spiralAsyncDemo
            // 
            _spiralAsyncDemo.Margin = new Padding(5);
            _spiralAsyncDemo.Name = "_spiralAsyncDemo";
            _spiralAsyncDemo.Size = new Size(59, 24);
            _spiralAsyncDemo.Text = "Spiral";
            _spiralAsyncDemo.Click += SpiralAsyncDemo_Click;
            // 
            // _flashingTitleDemo
            // 
            _flashingTitleDemo.Margin = new Padding(5);
            _flashingTitleDemo.Name = "_flashingTitleDemo";
            _flashingTitleDemo.Size = new Size(50, 24);
            _flashingTitleDemo.Text = "Title";
            _flashingTitleDemo.Click += Demo2_FlashingTitleDemo_Click;
            // 
            // _asyncListViewBindingDemo
            // 
            _asyncListViewBindingDemo.Margin = new Padding(5);
            _asyncListViewBindingDemo.Name = "_asyncListViewBindingDemo";
            _asyncListViewBindingDemo.Size = new Size(72, 24);
            _asyncListViewBindingDemo.Text = "Binding";
            // 
            // toolStripSymbolMenuItem2
            // 
            toolStripSymbolMenuItem2.AutoSize = true;
            toolStripSymbolMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { debugPaneToolStripMenuItem });
            toolStripSymbolMenuItem2.ImageScaling = ToolStripItemImageScaling.None;
            toolStripSymbolMenuItem2.Margin = new Padding(5);
            toolStripSymbolMenuItem2.Name = "toolStripSymbolMenuItem2";
            toolStripSymbolMenuItem2.Size = new Size(56, 24);
            toolStripSymbolMenuItem2.SymbolColor = SystemColors.ControlText;
            toolStripSymbolMenuItem2.SymbolScaling = 100;
            toolStripSymbolMenuItem2.SymbolSize = new Size(32, 32);
            toolStripSymbolMenuItem2.Text = "Tools";
            toolStripSymbolMenuItem2.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // debugPaneToolStripMenuItem
            // 
            debugPaneToolStripMenuItem.Name = "debugPaneToolStripMenuItem";
            debugPaneToolStripMenuItem.Size = new Size(158, 24);
            debugPaneToolStripMenuItem.Text = "Debug Pane";
            // 
            // statusStrip1
            // 
            statusStrip1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { _lblRunningNowText, _lblDemoName, _debugStatusPanel, toolStripStatusLabel1, _clockLabel });
            statusStrip1.Location = new Point(0, 720);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 18, 0);
            statusStrip1.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            statusStrip1.Size = new Size(1121, 152);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // _lblRunningNowText
            // 
            _lblRunningNowText.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblRunningNowText.Name = "_lblRunningNowText";
            _lblRunningNowText.Size = new Size(116, 147);
            _lblRunningNowText.Text = "Running now:";
            // 
            // _lblDemoName
            // 
            _lblDemoName.Name = "_lblDemoName";
            _lblDemoName.Padding = new Padding(10, 0, 10, 0);
            _lblDemoName.Size = new Size(167, 147);
            _lblDemoName.Text = "No Demo in proces.";
            // 
            // _debugStatusPanel
            // 
            _debugStatusPanel.Name = "_debugStatusPanel";
            _debugStatusPanel.Size = new Size(500, 150);
            _debugStatusPanel.Text = "toolsStripDebugPanel1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(159, 147);
            toolStripStatusLabel1.Spring = true;
            // 
            // _clockLabel
            // 
            _clockLabel.Name = "_clockLabel";
            _clockLabel.Padding = new Padding(10, 0, 10, 0);
            _clockLabel.Size = new Size(160, 147);
            _clockLabel.Text = "#ClockPlaceHolder";
            _clockLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _periodicTimerComponent
            // 
            _periodicTimerComponent.EngageAsync += _periodicTimerComponent_EngageAsync;
            // 
            // _flashingTitleTimer
            // 
            _flashingTitleTimer.IntervalMs = 50;
            _flashingTitleTimer.EngageAsync += FlashingTitleTimer_EngageAsync;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1121, 872);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            Name = "MainForm";
            Text = "A WinForms Summit Feature Demo";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem _spiralAsyncDemo;
        private ToolStripMenuItem _flashingTitleDemo;
        private ToolStripMenuItem _asyncListViewBindingDemo;
        private StatusStrip statusStrip1;
        private WinForms.PowerTools.Controls.ToolStripSymbolMenuItem _asyncEventsDemo;
        private WinForms.PowerTools.Controls.ToolStripSymbolMenuItem toolStripSymbolMenuItem2;
        private ToolStripStatusLabel _lblRunningNowText;
        private ToolStripStatusLabel _lblDemoName;
        private ToolStripStatusLabel _clockLabel;
        private PeriodicTimerComponent _periodicTimerComponent;
        private PeriodicTimerComponent _flashingTitleTimer;
        private ToolStripMenuItem debugPaneToolStripMenuItem;
        private ToolsStripDebugPanel _debugStatusPanel;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}
