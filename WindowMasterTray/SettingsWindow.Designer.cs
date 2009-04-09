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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
			this.lbActions = new System.Windows.Forms.CheckedListBox();
			this.bOK = new System.Windows.Forms.Button();
			this.lbHotKeys = new System.Windows.Forms.ListBox();
			this.bAddHotKey = new System.Windows.Forms.Button();
			this.bDeleteHotKey = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tbActionDescription = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbActions
			// 
			this.lbActions.FormattingEnabled = true;
			this.lbActions.Location = new System.Drawing.Point(6, 19);
			this.lbActions.Name = "lbActions";
			this.lbActions.Size = new System.Drawing.Size(237, 94);
			this.lbActions.TabIndex = 0;
			this.lbActions.SelectedIndexChanged += new System.EventHandler(this.lbActions_SelectedIndexChanged);
			this.lbActions.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lbActions_ItemCheck);
			// 
			// bOK
			// 
			this.bOK.Location = new System.Drawing.Point(112, 191);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(247, 23);
			this.bOK.TabIndex = 3;
			this.bOK.Text = "&Save";
			this.bOK.UseVisualStyleBackColor = true;
			this.bOK.Click += new System.EventHandler(this.bSave_Click);
			// 
			// lbHotKeys
			// 
			this.lbHotKeys.FormattingEnabled = true;
			this.lbHotKeys.Location = new System.Drawing.Point(6, 20);
			this.lbHotKeys.Name = "lbHotKeys";
			this.lbHotKeys.Size = new System.Drawing.Size(141, 95);
			this.lbHotKeys.TabIndex = 6;
			this.lbHotKeys.DoubleClick += new System.EventHandler(this.lbHotKeys_DoubleClick);
			// 
			// bAddHotKey
			// 
			this.bAddHotKey.Enabled = false;
			this.bAddHotKey.Location = new System.Drawing.Point(153, 20);
			this.bAddHotKey.Name = "bAddHotKey";
			this.bAddHotKey.Size = new System.Drawing.Size(40, 23);
			this.bAddHotKey.TabIndex = 7;
			this.bAddHotKey.Text = "+";
			this.bAddHotKey.UseVisualStyleBackColor = true;
			this.bAddHotKey.Click += new System.EventHandler(this.bAddHotKey_Click);
			// 
			// bDeleteHotKey
			// 
			this.bDeleteHotKey.Enabled = false;
			this.bDeleteHotKey.Location = new System.Drawing.Point(153, 91);
			this.bDeleteHotKey.Name = "bDeleteHotKey";
			this.bDeleteHotKey.Size = new System.Drawing.Size(40, 23);
			this.bDeleteHotKey.TabIndex = 8;
			this.bDeleteHotKey.Text = "-";
			this.bDeleteHotKey.UseVisualStyleBackColor = true;
			this.bDeleteHotKey.Click += new System.EventHandler(this.bDeleteHotKey_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lbActions);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(249, 126);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Actions";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lbHotKeys);
			this.groupBox2.Controls.Add(this.bAddHotKey);
			this.groupBox2.Controls.Add(this.bDeleteHotKey);
			this.groupBox2.Location = new System.Drawing.Point(267, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 126);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "HotKeys";
			// 
			// tbActionDescription
			// 
			this.tbActionDescription.Location = new System.Drawing.Point(18, 145);
			this.tbActionDescription.Multiline = true;
			this.tbActionDescription.Name = "tbActionDescription";
			this.tbActionDescription.ReadOnly = true;
			this.tbActionDescription.Size = new System.Drawing.Size(442, 40);
			this.tbActionDescription.TabIndex = 11;
			// 
			// SettingsWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(479, 226);
			this.Controls.Add(this.tbActionDescription);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.bOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(485, 258);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(485, 258);
			this.Name = "SettingsWindow";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WindowMaster Settings";
			this.VisibleChanged += new System.EventHandler(this.SettingsWindow_VisibleChanged);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsWindow_FormClosing);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SettingsWindow_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox lbActions;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.ListBox lbHotKeys;
		private System.Windows.Forms.Button bAddHotKey;
		private System.Windows.Forms.Button bDeleteHotKey;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox tbActionDescription;
	}
}