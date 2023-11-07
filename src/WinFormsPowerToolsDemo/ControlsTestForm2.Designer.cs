namespace WinForms.PowerToolsDemo
{
    partial class ControlsTestForm2
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
            components = new System.ComponentModel.Container();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            _mainBindingSource = new System.Windows.Forms.BindingSource(components);
            _bottomMenueStrip = new System.Windows.Forms.MenuStrip();
            groupBox1 = new System.Windows.Forms.GroupBox();
            _toSystem = new System.Windows.Forms.RadioButton();
            radioButton2 = new System.Windows.Forms.RadioButton();
            _toLightMode = new System.Windows.Forms.RadioButton();
            _openMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _addMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _deleteMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _editItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _searchMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _taskMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_mainBindingSource).BeginInit();
            _bottomMenueStrip.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _openMenuItem, _searchMenuItem, _taskMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(101, 462);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // _bottomMenueStrip
            // 
            _bottomMenueStrip.BackColor = System.Drawing.SystemColors.ButtonShadow;
            _bottomMenueStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            _bottomMenueStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            _bottomMenueStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            _bottomMenueStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _addMenuItem, _deleteMenuItem, _editItem });
            _bottomMenueStrip.Location = new System.Drawing.Point(101, 402);
            _bottomMenueStrip.Name = "_bottomMenueStrip";
            _bottomMenueStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            _bottomMenueStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            _bottomMenueStrip.Size = new System.Drawing.Size(640, 60);
            _bottomMenueStrip.TabIndex = 1;
            _bottomMenueStrip.Text = "menuStrip2";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(_toSystem);
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(_toLightMode);
            groupBox1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            groupBox1.Location = new System.Drawing.Point(209, 92);
            groupBox1.Margin = new System.Windows.Forms.Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4);
            groupBox1.Size = new System.Drawing.Size(400, 180);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Theming";
            // 
            // _toSystem
            // 
            _toSystem.AutoSize = true;
            _toSystem.DataBindings.Add(new System.Windows.Forms.Binding("Command", _mainBindingSource, "SwitchToSystemModeCommand", true));
            _toSystem.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            _toSystem.Location = new System.Drawing.Point(115, 122);
            _toSystem.Margin = new System.Windows.Forms.Padding(4);
            _toSystem.Name = "_toSystem";
            _toSystem.Size = new System.Drawing.Size(90, 29);
            _toSystem.TabIndex = 2;
            _toSystem.TabStop = true;
            _toSystem.Text = "System";
            _toSystem.UseVisualStyleBackColor = false;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.DataBindings.Add(new System.Windows.Forms.Binding("Command", _mainBindingSource, "SwitchToDarkModeCommand", true));
            radioButton2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            radioButton2.Location = new System.Drawing.Point(115, 85);
            radioButton2.Margin = new System.Windows.Forms.Padding(4);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new System.Drawing.Size(122, 29);
            radioButton2.TabIndex = 1;
            radioButton2.TabStop = true;
            radioButton2.Text = "Dark Mode";
            radioButton2.UseVisualStyleBackColor = false;
            // 
            // _toLightMode
            // 
            _toLightMode.AutoSize = true;
            _toLightMode.DataBindings.Add(new System.Windows.Forms.Binding("Command", _mainBindingSource, "SwitchToLightModeCommand", true));
            _toLightMode.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            _toLightMode.Location = new System.Drawing.Point(115, 48);
            _toLightMode.Margin = new System.Windows.Forms.Padding(4);
            _toLightMode.Name = "_toLightMode";
            _toLightMode.Size = new System.Drawing.Size(124, 29);
            _toLightMode.TabIndex = 0;
            _toLightMode.TabStop = true;
            _toLightMode.Text = "Light Mode";
            _toLightMode.UseVisualStyleBackColor = false;
            // 
            // _openMenuItem
            // 
            _openMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _openMenuItem.Name = "_openMenuItem";
            _openMenuItem.ScalePercentage = 100;
            _openMenuItem.Size = new System.Drawing.Size(84, 88);
            _openMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.OpenFile;
            _openMenuItem.SymbolSize = new System.Drawing.Size(64, 64);
            _openMenuItem.Text = "Open...";
            _openMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _addMenuItem
            // 
            _addMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _addMenuItem.Name = "_addMenuItem";
            _addMenuItem.ScalePercentage = 100;
            _addMenuItem.Size = new System.Drawing.Size(51, 56);
            _addMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.Add;
            _addMenuItem.SymbolSize = new System.Drawing.Size(32, 32);
            _addMenuItem.Text = "Add";
            _addMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _deleteMenuItem
            // 
            _deleteMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _deleteMenuItem.Name = "_deleteMenuItem";
            _deleteMenuItem.ScalePercentage = 100;
            _deleteMenuItem.Size = new System.Drawing.Size(67, 56);
            _deleteMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.Delete;
            _deleteMenuItem.SymbolSize = new System.Drawing.Size(32, 32);
            _deleteMenuItem.Text = "Delete";
            _deleteMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _editItem
            // 
            _editItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _editItem.Name = "_editItem";
            _editItem.ScalePercentage = 100;
            _editItem.Size = new System.Drawing.Size(49, 56);
            _editItem.Symbol = PowerTools.Controls.SegoeFluentIcons.Edit;
            _editItem.SymbolSize = new System.Drawing.Size(32, 32);
            _editItem.Text = "Edit";
            _editItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _searchMenuItem
            // 
            _searchMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _searchMenuItem.Name = "_searchMenuItem";
            _searchMenuItem.ScalePercentage = 100;
            _searchMenuItem.Size = new System.Drawing.Size(141, 88);
            _searchMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.Search;
            _searchMenuItem.SymbolSize = new System.Drawing.Size(64, 64);
            _searchMenuItem.Text = "Search...";
            _searchMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _taskMenuItem
            // 
            _taskMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _taskMenuItem.Name = "_taskMenuItem";
            _taskMenuItem.ScalePercentage = 100;
            _taskMenuItem.Size = new System.Drawing.Size(141, 77);
            _taskMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.TaskView;
            _taskMenuItem.SymbolSize = new System.Drawing.Size(53, 53);
            _taskMenuItem.Text = "Task view...";
            _taskMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ControlsTestForm2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(741, 462);
            Controls.Add(groupBox1);
            Controls.Add(_bottomMenueStrip);
            Controls.Add(menuStrip1);
            Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "ControlsTestForm2";
            Text = "2nd Controls Test Form";
            Load += ControlsTestForm2_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_mainBindingSource).EndInit();
            _bottomMenueStrip.ResumeLayout(false);
            _bottomMenueStrip.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PowerTools.Components.ThemingComponent themingComponent1;
        private PowerTools.Controls.ToolStripSymbolMenuItem toolStripFluentSymbolMenuItem1;
        private PowerTools.Components.ThemingComponent themingComponent2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private PowerTools.Controls.ToolStripSymbolMenuItem _open;
        private System.Windows.Forms.MenuStrip _bottomMenueStrip;
        private PowerTools.Controls.ToolStripSymbolMenuItem _tsmClear;
        private PowerTools.Controls.ToolStripSymbolMenuItem _addMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton _toSystem;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton _toLightMode;
        private System.Windows.Forms.BindingSource _mainBindingSource;
        private PowerTools.Controls.ToolStripSymbolMenuItem toolStripSymbolMenuItem2;
        private PowerTools.Components.BindingTypeConverterExtender bindingConverterExtender1;
        private PowerTools.Controls.ToolStripSymbolMenuItem _openMenuItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem _deleteMenuItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem _editItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem _searchMenuItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem _taskMenuItem;
    }
}