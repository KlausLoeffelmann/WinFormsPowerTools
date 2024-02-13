using WinForms.PowerTools.Components;
using WinForms.PowerTools.Controls;

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
            _mainMenuStrip = new System.Windows.Forms.MenuStrip();
            _openMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _searchMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _taskMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _bottomMenuStrip = new System.Windows.Forms.MenuStrip();
            _addMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _deleteMenuItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _editItem = new PowerTools.Controls.ToolStripSymbolMenuItem();
            _demoViewModelBindingSource = new System.Windows.Forms.BindingSource(components);
            _themingComponent = new PowerTools.Components.ThemingComponent();
            _bindingTypeConverterExtender = new PowerTools.Components.BindingTypeConverterExtender();
            _optLightMode = new System.Windows.Forms.RadioButton();
            _optDarkMode = new System.Windows.Forms.RadioButton();
            _optSystemMode = new System.Windows.Forms.RadioButton();
            _grpTheme = new System.Windows.Forms.GroupBox();
            demoViewModelBindingSource = new System.Windows.Forms.BindingSource(components);
            _mainMenuStrip.SuspendLayout();
            _bottomMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_demoViewModelBindingSource).BeginInit();
            _grpTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)demoViewModelBindingSource).BeginInit();
            SuspendLayout();
            // 
            // _mainMenuStrip
            // 
            _mainMenuStrip.BackColor = System.Drawing.SystemColors.ButtonShadow;
            _mainMenuStrip.Dock = System.Windows.Forms.DockStyle.Left;
            _mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            _mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _openMenuItem, _searchMenuItem, _taskMenuItem });
            _mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            _mainMenuStrip.Name = "_mainMenuStrip";
            _mainMenuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            _mainMenuStrip.Size = new System.Drawing.Size(101, 462);
            _mainMenuStrip.TabIndex = 0;
            _mainMenuStrip.Text = "menuStrip1";
            // 
            // _openMenuItem
            // 
            _openMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _openMenuItem.Name = "_openMenuItem";
            _openMenuItem.Size = new System.Drawing.Size(84, 88);
            _openMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.OpenFile;
            _openMenuItem.SymbolScaling = 100;
            _openMenuItem.SymbolSize = new System.Drawing.Size(64, 64);
            _openMenuItem.Text = "Open...";
            _openMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _searchMenuItem
            // 
            _searchMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _searchMenuItem.Name = "_searchMenuItem";
            _searchMenuItem.Size = new System.Drawing.Size(84, 88);
            _searchMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.Search;
            _searchMenuItem.SymbolScaling = 100;
            _searchMenuItem.SymbolSize = new System.Drawing.Size(64, 64);
            _searchMenuItem.Text = "Search...";
            _searchMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _taskMenuItem
            // 
            _taskMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _taskMenuItem.Name = "_taskMenuItem";
            _taskMenuItem.Size = new System.Drawing.Size(84, 77);
            _taskMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.TaskView;
            _taskMenuItem.SymbolScaling = 100;
            _taskMenuItem.SymbolSize = new System.Drawing.Size(53, 53);
            _taskMenuItem.Text = "Task view...";
            _taskMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _bottomMenuStrip
            // 
            _bottomMenuStrip.BackColor = System.Drawing.SystemColors.ButtonShadow;
            _bottomMenuStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            _bottomMenuStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            _bottomMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            _bottomMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _addMenuItem, _deleteMenuItem, _editItem });
            _bottomMenuStrip.Location = new System.Drawing.Point(101, 402);
            _bottomMenuStrip.Name = "_bottomMenuStrip";
            _bottomMenuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            _bottomMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            _bottomMenuStrip.Size = new System.Drawing.Size(640, 60);
            _bottomMenuStrip.TabIndex = 1;
            _bottomMenuStrip.Text = "_bottomMenuStrip";
            // 
            // _addMenuItem
            // 
            _addMenuItem.DataBindings.Add(new System.Windows.Forms.Binding("Command", demoViewModelBindingSource, "SwitchToDarkModeCommand", true));
            _addMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _addMenuItem.Name = "_addMenuItem";
            _addMenuItem.Size = new System.Drawing.Size(51, 56);
            _addMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.Add;
            _addMenuItem.SymbolScaling = 100;
            _addMenuItem.SymbolSize = new System.Drawing.Size(32, 32);
            _addMenuItem.Text = "Add";
            _addMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _deleteMenuItem
            // 
            _deleteMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _deleteMenuItem.Name = "_deleteMenuItem";
            _deleteMenuItem.Size = new System.Drawing.Size(67, 56);
            _deleteMenuItem.Symbol = PowerTools.Controls.SegoeFluentIcons.Delete;
            _deleteMenuItem.SymbolScaling = 100;
            _deleteMenuItem.SymbolSize = new System.Drawing.Size(32, 32);
            _deleteMenuItem.Text = "Delete";
            _deleteMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _editItem
            // 
            _editItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            _editItem.Name = "_editItem";
            _editItem.Size = new System.Drawing.Size(49, 56);
            _editItem.Symbol = PowerTools.Controls.SegoeFluentIcons.Edit;
            _editItem.SymbolScaling = 100;
            _editItem.SymbolSize = new System.Drawing.Size(32, 32);
            _editItem.Text = "Edit";
            _editItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // _demoViewModelBindingSource
            // 
            _demoViewModelBindingSource.DataSource = typeof(DemoViewModel);
            // 
            // _themingComponent
            // 
            _themingComponent.DataBindings.Add(new System.Windows.Forms.Binding("ThemingMode", _demoViewModelBindingSource, "ThemingMode", true));
            _themingComponent.ParentContainer = this;
            // 
            // _optLightMode
            // 
            _optLightMode.AutoSize = true;
            _optLightMode.DataBindings.Add(new System.Windows.Forms.Binding("Command", _demoViewModelBindingSource, "SwitchToLightModeCommand", true));
            _optLightMode.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            _optLightMode.Location = new System.Drawing.Point(115, 48);
            _optLightMode.Margin = new System.Windows.Forms.Padding(4);
            _optLightMode.Name = "_optLightMode";
            _optLightMode.Size = new System.Drawing.Size(124, 29);
            _optLightMode.TabIndex = 0;
            _optLightMode.TabStop = true;
            _optLightMode.Text = "Light Mode";
            _optLightMode.UseVisualStyleBackColor = false;
            // 
            // _optDarkMode
            // 
            _optDarkMode.AutoSize = true;
            _optDarkMode.DataBindings.Add(new System.Windows.Forms.Binding("Command", _demoViewModelBindingSource, "SwitchToDarkModeCommand", true));
            _optDarkMode.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            _optDarkMode.Location = new System.Drawing.Point(115, 85);
            _optDarkMode.Margin = new System.Windows.Forms.Padding(4);
            _optDarkMode.Name = "_optDarkMode";
            _optDarkMode.Size = new System.Drawing.Size(122, 29);
            _optDarkMode.TabIndex = 1;
            _optDarkMode.TabStop = true;
            _optDarkMode.Text = "Dark Mode";
            _optDarkMode.UseVisualStyleBackColor = false;
            // 
            // _optSystemMode
            // 
            _optSystemMode.AutoSize = true;
            _optSystemMode.DataBindings.Add(new System.Windows.Forms.Binding("Command", _demoViewModelBindingSource, "SwitchToSystemModeCommand", true));
            _optSystemMode.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            _optSystemMode.Location = new System.Drawing.Point(115, 122);
            _optSystemMode.Margin = new System.Windows.Forms.Padding(4);
            _optSystemMode.Name = "_optSystemMode";
            _optSystemMode.Size = new System.Drawing.Size(90, 29);
            _optSystemMode.TabIndex = 2;
            _optSystemMode.TabStop = true;
            _optSystemMode.Text = "System";
            _optSystemMode.UseVisualStyleBackColor = false;
            // 
            // _grpTheme
            // 
            _grpTheme.Controls.Add(_optSystemMode);
            _grpTheme.Controls.Add(_optDarkMode);
            _grpTheme.Controls.Add(_optLightMode);
            _grpTheme.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            _grpTheme.Location = new System.Drawing.Point(209, 92);
            _grpTheme.Margin = new System.Windows.Forms.Padding(4);
            _grpTheme.Name = "_grpTheme";
            _grpTheme.Padding = new System.Windows.Forms.Padding(4);
            _grpTheme.Size = new System.Drawing.Size(400, 180);
            _grpTheme.TabIndex = 3;
            _grpTheme.TabStop = false;
            _grpTheme.Text = "Theming";
            // 
            // demoViewModelBindingSource
            // 
            demoViewModelBindingSource.DataSource = typeof(DemoViewModel);
            // 
            // ControlsTestForm2
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(741, 462);
            Controls.Add(_grpTheme);
            Controls.Add(_bottomMenuStrip);
            Controls.Add(_mainMenuStrip);
            Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ForeColor = System.Drawing.Color.FromArgb(0, 0, 0);
            MainMenuStrip = _mainMenuStrip;
            Margin = new System.Windows.Forms.Padding(4);
            Name = "ControlsTestForm2";
            Text = "2nd Controls Test Form";
            Load += ControlsTestForm2_Load;
            _mainMenuStrip.ResumeLayout(false);
            _mainMenuStrip.PerformLayout();
            _bottomMenuStrip.ResumeLayout(false);
            _bottomMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_demoViewModelBindingSource).EndInit();
            _grpTheme.ResumeLayout(false);
            _grpTheme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)demoViewModelBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ThemingComponent _themingComponent;
        private ToolStripSymbolMenuItem toolStripFluentSymbolMenuItem1;
        private PowerTools.Components.ThemingComponent themingComponent2;
        private System.Windows.Forms.MenuStrip _mainMenuStrip;
        private System.Windows.Forms.MenuStrip _bottomMenuStrip;
        private PowerTools.Controls.ToolStripSymbolMenuItem _tsmClear;
        private PowerTools.Controls.ToolStripSymbolMenuItem _addMenuItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem toolStripSymbolMenuItem2;
        private PowerTools.Controls.ToolStripSymbolMenuItem _openMenuItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem _deleteMenuItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem _editItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem _searchMenuItem;
        private PowerTools.Controls.ToolStripSymbolMenuItem _taskMenuItem;
        private PowerTools.Components.BindingTypeConverterExtender _bindingTypeConverterExtender;
        private System.Windows.Forms.BindingSource _demoViewModelBindingSource;
        private System.Windows.Forms.GroupBox _grpTheme;
        private System.Windows.Forms.RadioButton _optSystemMode;
        private System.Windows.Forms.RadioButton _optDarkMode;
        private System.Windows.Forms.RadioButton _optLightMode;
        private System.Windows.Forms.BindingSource demoViewModelBindingSource;
    }
}
