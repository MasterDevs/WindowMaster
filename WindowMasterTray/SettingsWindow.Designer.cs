namespace WindowMasterLib {
	partial class SettingsWindow {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
			this.lbActions = new System.Windows.Forms.CheckedListBox();
			this.bOK = new System.Windows.Forms.Button();
			this.lbHotKeys = new System.Windows.Forms.ListBox();
			this.bAddHotKey = new System.Windows.Forms.Button();
			this.bRemoveHotKey = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.bModifyAction = new System.Windows.Forms.Button();
			this.bRemoveAction = new System.Windows.Forms.Button();
			this.bAddAction = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tbActionDescription = new System.Windows.Forms.TextBox();
			this.bApply = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mi_StartWithWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mi_ImportConfiguration = new System.Windows.Forms.ToolStripMenuItem();
			this.mi_ExportConfiguration = new System.Windows.Forms.ToolStripMenuItem();
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbActions
			// 
			this.lbActions.FormattingEnabled = true;
			this.lbActions.Location = new System.Drawing.Point(7, 22);
			this.lbActions.Name = "lbActions";
			this.lbActions.Size = new System.Drawing.Size(269, 112);
			this.lbActions.TabIndex = 0;
			this.lbActions.ThreeDCheckBoxes = true;
			this.toolTip.SetToolTip(this.lbActions, "Actions");
			this.lbActions.SelectedIndexChanged += new System.EventHandler(this.lbActions_SelectedIndexChanged);
			this.lbActions.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbActions_ItemCheck);
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(19, 243);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(91, 27);
			this.bOK.TabIndex = 3;
			this.bOK.Text = "&OK";
			this.toolTip.SetToolTip(this.bOK, "Save Changes and Close");
			this.bOK.UseVisualStyleBackColor = true;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// lbHotKeys
			// 
			this.lbHotKeys.FormattingEnabled = true;
			this.lbHotKeys.ItemHeight = 15;
			this.lbHotKeys.Location = new System.Drawing.Point(7, 23);
			this.lbHotKeys.Name = "lbHotKeys";
			this.lbHotKeys.Size = new System.Drawing.Size(164, 109);
			this.lbHotKeys.TabIndex = 6;
			this.toolTip.SetToolTip(this.lbHotKeys, "HotKeys");
			this.lbHotKeys.DoubleClick += new System.EventHandler(this.lbHotKeys_DoubleClick);
			// 
			// bAddHotKey
			// 
			this.bAddHotKey.Enabled = false;
			this.bAddHotKey.Location = new System.Drawing.Point(178, 23);
			this.bAddHotKey.Name = "bAddHotKey";
			this.bAddHotKey.Size = new System.Drawing.Size(47, 27);
			this.bAddHotKey.TabIndex = 7;
			this.bAddHotKey.Text = "+";
			this.toolTip.SetToolTip(this.bAddHotKey, "Add HotKey for Selected Action");
			this.bAddHotKey.UseVisualStyleBackColor = true;
			this.bAddHotKey.Click += new System.EventHandler(this.bAddHotKey_Click);
			// 
			// bRemoveHotKey
			// 
			this.bRemoveHotKey.Enabled = false;
			this.bRemoveHotKey.Location = new System.Drawing.Point(178, 104);
			this.bRemoveHotKey.Name = "bRemoveHotKey";
			this.bRemoveHotKey.Size = new System.Drawing.Size(47, 27);
			this.bRemoveHotKey.TabIndex = 8;
			this.bRemoveHotKey.Text = "-";
			this.toolTip.SetToolTip(this.bRemoveHotKey, "Remove Selected HotKey for Selected Action");
			this.bRemoveHotKey.UseVisualStyleBackColor = true;
			this.bRemoveHotKey.Click += new System.EventHandler(this.bRemoveKey_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.bModifyAction);
			this.groupBox1.Controls.Add(this.bRemoveAction);
			this.groupBox1.Controls.Add(this.bAddAction);
			this.groupBox1.Controls.Add(this.lbActions);
			this.groupBox1.Location = new System.Drawing.Point(12, 27);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(337, 145);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Actions";
			// 
			// bModifyAction
			// 
			this.bModifyAction.Enabled = false;
			this.bModifyAction.Location = new System.Drawing.Point(285, 63);
			this.bModifyAction.Name = "bModifyAction";
			this.bModifyAction.Size = new System.Drawing.Size(45, 27);
			this.bModifyAction.TabIndex = 3;
			this.bModifyAction.Text = "%";
			this.toolTip.SetToolTip(this.bModifyAction, "Modify Selected Action");
			this.bModifyAction.UseVisualStyleBackColor = true;
			this.bModifyAction.Click += new System.EventHandler(this.bModify_Click);
			// 
			// bRemoveAction
			// 
			this.bRemoveAction.Enabled = false;
			this.bRemoveAction.Location = new System.Drawing.Point(283, 104);
			this.bRemoveAction.Name = "bRemoveAction";
			this.bRemoveAction.Size = new System.Drawing.Size(47, 27);
			this.bRemoveAction.TabIndex = 2;
			this.bRemoveAction.Text = "-";
			this.toolTip.SetToolTip(this.bRemoveAction, "Remove Selected Action");
			this.bRemoveAction.UseVisualStyleBackColor = true;
			this.bRemoveAction.Click += new System.EventHandler(this.bRemoveAction_Click);
			// 
			// bAddAction
			// 
			this.bAddAction.Location = new System.Drawing.Point(283, 23);
			this.bAddAction.Name = "bAddAction";
			this.bAddAction.Size = new System.Drawing.Size(47, 27);
			this.bAddAction.TabIndex = 1;
			this.bAddAction.Text = "+";
			this.toolTip.SetToolTip(this.bAddAction, "Add Action");
			this.bAddAction.UseVisualStyleBackColor = true;
			this.bAddAction.Click += new System.EventHandler(this.bAddAction_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lbHotKeys);
			this.groupBox2.Controls.Add(this.bAddHotKey);
			this.groupBox2.Controls.Add(this.bRemoveHotKey);
			this.groupBox2.Location = new System.Drawing.Point(356, 27);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(233, 145);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "HotKeys";
			// 
			// tbActionDescription
			// 
			this.tbActionDescription.Location = new System.Drawing.Point(19, 180);
			this.tbActionDescription.Multiline = true;
			this.tbActionDescription.Name = "tbActionDescription";
			this.tbActionDescription.ReadOnly = true;
			this.tbActionDescription.Size = new System.Drawing.Size(562, 57);
			this.tbActionDescription.TabIndex = 11;
			// 
			// bApply
			// 
			this.bApply.Enabled = false;
			this.bApply.Location = new System.Drawing.Point(254, 243);
			this.bApply.Name = "bApply";
			this.bApply.Size = new System.Drawing.Size(91, 27);
			this.bApply.TabIndex = 12;
			this.bApply.Text = "&Apply";
			this.toolTip.SetToolTip(this.bApply, "Save Changes");
			this.bApply.UseVisualStyleBackColor = true;
			this.bApply.Click += new System.EventHandler(this.bApply_Click);
			// 
			// bCancel
			// 
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(489, 243);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(91, 27);
			this.bCancel.TabIndex = 13;
			this.bCancel.Text = "&Cancel";
			this.toolTip.SetToolTip(this.bCancel, "Close");
			this.bCancel.UseVisualStyleBackColor = true;
			this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(601, 24);
			this.menuStrip1.TabIndex = 16;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// configurationToolStripMenuItem
			// 
			this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_StartWithWindows,
            this.toolStripSeparator1,
            this.mi_ImportConfiguration,
            this.mi_ExportConfiguration});
			this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
			this.configurationToolStripMenuItem.ShowShortcutKeys = false;
			this.configurationToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
			this.configurationToolStripMenuItem.Text = "Configuration";
			// 
			// mi_StartWithWindows
			// 
			this.mi_StartWithWindows.CheckOnClick = true;
			this.mi_StartWithWindows.Name = "mi_StartWithWindows";
			this.mi_StartWithWindows.ShowShortcutKeys = false;
			this.mi_StartWithWindows.Size = new System.Drawing.Size(162, 22);
			this.mi_StartWithWindows.Text = "Start With Windows";
			this.mi_StartWithWindows.Click += new System.EventHandler(this.MenuItem_StartWithWindows_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(159, 6);
			// 
			// mi_ImportConfiguration
			// 
			this.mi_ImportConfiguration.Name = "mi_ImportConfiguration";
			this.mi_ImportConfiguration.ShowShortcutKeys = false;
			this.mi_ImportConfiguration.Size = new System.Drawing.Size(162, 22);
			this.mi_ImportConfiguration.Text = "Import Actions";
			this.mi_ImportConfiguration.Click += new System.EventHandler(this.mi_ImportConfiguration_Click);
			// 
			// mi_ExportConfiguration
			// 
			this.mi_ExportConfiguration.Name = "mi_ExportConfiguration";
			this.mi_ExportConfiguration.ShowShortcutKeys = false;
			this.mi_ExportConfiguration.Size = new System.Drawing.Size(162, 22);
			this.mi_ExportConfiguration.Text = "Export Actions";
			this.mi_ExportConfiguration.Click += new System.EventHandler(this.mi_ExportConfiguration_Click);
			// 
			// saveDialog
			// 
			this.saveDialog.Filter = "WindowMaster Config File (*.xml) | *.xml";
			// 
			// openDialog
			// 
			this.openDialog.Filter = "WindowMaster Config File (*.xml) | *.xml";
			// 
			// SettingsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(601, 279);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.tbActionDescription);
			this.Controls.Add(this.bOK);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bApply);
			this.Controls.Add(this.menuStrip1);
			this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(601, 292);
			this.Name = "SettingsWindow";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WindowMaster Settings";
			this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.SettingsWindow_HelpButtonClicked);
			this.VisibleChanged += new System.EventHandler(this.SettingsWindow_VisibleChanged);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsWindow_FormClosing);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox lbActions;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.ListBox lbHotKeys;
		private System.Windows.Forms.Button bAddHotKey;
		private System.Windows.Forms.Button bRemoveHotKey;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tbActionDescription;
		private System.Windows.Forms.Button bApply;
		private System.Windows.Forms.Button bCancel;
		private System.Windows.Forms.Button bAddAction;
		private System.Windows.Forms.Button bRemoveAction;
		private System.Windows.Forms.Button bModifyAction;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mi_StartWithWindows;
		private System.Windows.Forms.ToolStripMenuItem mi_ImportConfiguration;
		private System.Windows.Forms.ToolStripMenuItem mi_ExportConfiguration;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
	}
}