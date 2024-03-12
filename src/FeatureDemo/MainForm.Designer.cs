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
            _flashingTitelDemo = new ToolStripMenuItem();
            _spiralAsyncDemo = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            _asyncListViewBindingDemo = new ToolStripMenuItem();
            toolStripSymbolMenuItem2 = new WinForms.PowerTools.Controls.ToolStripSymbolMenuItem();
            statusStrip1 = new StatusStrip();
            _lblRunningNowText = new ToolStripStatusLabel();
            _lblDemoName = new ToolStripStatusLabel();
            _lblSpring = new ToolStripStatusLabel();
            _clockLabel = new ToolStripStatusLabel();
            clockPanel1 = new Controls.ClockPanel();
            _debugPanel = new Controls.DebugPanel();
            _periodicTimerComponent = new Controls.PeriodicTimerComponent();
            _flashingTitelTimer = new Controls.PeriodicTimerComponent();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Font = new Font("Segoe UI", 10.8F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { _asyncEventsDemo, _flashingTitelDemo, _spiralAsyncDemo, _asyncListViewBindingDemo, toolStripSymbolMenuItem2 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 2, 0, 2);
            menuStrip1.Size = new Size(1251, 43);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // _asyncEventsDemo
            // 
            _asyncEventsDemo.AutoSize = true;
            _asyncEventsDemo.ImageScaling = ToolStripItemImageScaling.None;
            _asyncEventsDemo.Margin = new Padding(5);
            _asyncEventsDemo.Name = "_asyncEventsDemo";
            _asyncEventsDemo.Size = new Size(90, 29);
            _asyncEventsDemo.SymbolColor = SystemColors.ControlText;
            _asyncEventsDemo.SymbolScaling = 100;
            _asyncEventsDemo.SymbolSize = new Size(32, 32);
            _asyncEventsDemo.Text = "Demo 1";
            _asyncEventsDemo.TextImageRelation = TextImageRelation.ImageAboveText;
            _asyncEventsDemo.Click += Demo1_AsyncEventsDemo_Click;
            // 
            // _flashingTitelDemo
            // 
            _flashingTitelDemo.Margin = new Padding(5);
            _flashingTitelDemo.Name = "_flashingTitelDemo";
            _flashingTitelDemo.Size = new Size(90, 29);
            _flashingTitelDemo.Text = "Demo 2";
            _flashingTitelDemo.Click += Demo2_FlashingTitleDemo_Click;
            // 
            // _spiralAsyncDemo
            // 
            _spiralAsyncDemo.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2 });
            _spiralAsyncDemo.Margin = new Padding(5);
            _spiralAsyncDemo.Name = "_spiralAsyncDemo";
            _spiralAsyncDemo.Size = new Size(90, 29);
            _spiralAsyncDemo.Text = "Demo 3";
            _spiralAsyncDemo.Click += SpiralAsyncDemo_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(221, 6);
            // 
            // _asyncListViewBindingDemo
            // 
            _asyncListViewBindingDemo.Margin = new Padding(5);
            _asyncListViewBindingDemo.Name = "_asyncListViewBindingDemo";
            _asyncListViewBindingDemo.Size = new Size(90, 29);
            _asyncListViewBindingDemo.Text = "Demo 4";
            // 
            // toolStripSymbolMenuItem2
            // 
            toolStripSymbolMenuItem2.AutoSize = true;
            toolStripSymbolMenuItem2.ImageScaling = ToolStripItemImageScaling.None;
            toolStripSymbolMenuItem2.Margin = new Padding(5);
            toolStripSymbolMenuItem2.Name = "toolStripSymbolMenuItem2";
            toolStripSymbolMenuItem2.Size = new Size(102, 29);
            toolStripSymbolMenuItem2.SymbolColor = SystemColors.ControlText;
            toolStripSymbolMenuItem2.SymbolScaling = 100;
            toolStripSymbolMenuItem2.SymbolSize = new Size(32, 32);
            toolStripSymbolMenuItem2.Text = "Options...";
            toolStripSymbolMenuItem2.TextImageRelation = TextImageRelation.ImageAboveText;
            // 
            // statusStrip1
            // 
            statusStrip1.AutoSize = false;
            statusStrip1.Font = new Font("Segoe UI", 10.8F);
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { _lblRunningNowText, _lblDemoName, _lblSpring, _clockLabel, clockPanel1 });
            statusStrip1.Location = new Point(0, 705);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 18, 0);
            statusStrip1.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            statusStrip1.Size = new Size(1251, 100);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // _lblRunningNowText
            // 
            _lblRunningNowText.Name = "_lblRunningNowText";
            _lblRunningNowText.Size = new Size(121, 94);
            _lblRunningNowText.Text = "Running now:";
            // 
            // _lblDemoName
            // 
            _lblDemoName.Name = "_lblDemoName";
            _lblDemoName.Size = new Size(118, 94);
            _lblDemoName.Text = "{DemoName}";
            // 
            // _lblSpring
            // 
            _lblSpring.Name = "_lblSpring";
            _lblSpring.Size = new Size(726, 94);
            _lblSpring.Spring = true;
            // 
            // _clockLabel
            // 
            _clockLabel.Name = "_clockLabel";
            _clockLabel.Size = new Size(160, 94);
            _clockLabel.Text = "#ClockPlaceHolder";
            // 
            // clockPanel1
            // 
            clockPanel1.BackColor = SystemColors.ControlDarkDark;
            clockPanel1.ClockFaceColor = SystemColors.Window;
            clockPanel1.Name = "clockPanel1";
            clockPanel1.Size = new Size(107, 98);
            clockPanel1.Text = "clockPanel1";
            clockPanel1.TimeAndDate = new DateTime(2024, 3, 12, 1, 33, 34, 0);
            // 
            // _debugPanel
            // 
            _debugPanel.Dock = DockStyle.Bottom;
            _debugPanel.Location = new Point(0, 563);
            _debugPanel.Name = "_debugPanel";
            _debugPanel.Size = new Size(1251, 142);
            _debugPanel.TabIndex = 2;
            // 
            // _periodicTimerComponent
            // 
            _periodicTimerComponent.EngageAsync += _periodicTimerComponent_EngageAsync;
            // 
            // _flashingTitelTimer
            // 
            _flashingTitelTimer.IntervalMs = 50;
            _flashingTitelTimer.EngageAsync += FlashingTitleTimer_EngageAsync;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1251, 805);
            Controls.Add(_debugPanel);
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
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem _flashingTitelDemo;
        private ToolStripMenuItem _asyncListViewBindingDemo;
        private StatusStrip statusStrip1;
        private WinForms.PowerTools.Controls.ToolStripSymbolMenuItem _asyncEventsDemo;
        private WinForms.PowerTools.Controls.ToolStripSymbolMenuItem toolStripSymbolMenuItem2;
        private ToolStripStatusLabel _lblRunningNowText;
        private ToolStripStatusLabel _lblDemoName;
        private ToolStripStatusLabel _lblSpring;
        private ToolStripStatusLabel _clockLabel;
        private Controls.DebugPanel _debugPanel;
        private Controls.PeriodicTimerComponent _periodicTimerComponent;
        private Controls.ClockPanel clockPanel1;
        private Controls.PeriodicTimerComponent _flashingTitelTimer;
    }
}
