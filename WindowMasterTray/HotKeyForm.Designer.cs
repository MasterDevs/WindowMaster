namespace WindowMasterLib {
	partial class HotKeyForm {
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbShift = new System.Windows.Forms.CheckBox();
			this.cbAlt = new System.Windows.Forms.CheckBox();
			this.cbCtrl = new System.Windows.Forms.CheckBox();
			this.cbWin = new System.Windows.Forms.CheckBox();
			this.ddlKey = new System.Windows.Forms.ComboBox();
			this.gbKey = new System.Windows.Forms.GroupBox();
			this.bOK = new System.Windows.Forms.Button();
			this.bCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.gbKey.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbShift);
			this.groupBox1.Controls.Add(this.cbAlt);
			this.groupBox1.Controls.Add(this.cbCtrl);
			this.groupBox1.Controls.Add(this.cbWin);
			this.groupBox1.Location = new System.Drawing.Point(13, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(253, 55);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Modifiers";
			// 
			// cbShift
			// 
			this.cbShift.AutoSize = true;
			this.cbShift.Location = new System.Drawing.Point(199, 21);
			this.cbShift.Name = "cbShift";
			this.cbShift.Size = new System.Drawing.Size(47, 17);
			this.cbShift.TabIndex = 3;
			this.cbShift.Text = "&Shift";
			this.cbShift.UseVisualStyleBackColor = true;
			this.cbShift.CheckedChanged += new System.EventHandler(this.Verify_HotKey_Selected);
			// 
			// cbAlt
			// 
			this.cbAlt.AutoSize = true;
			this.cbAlt.Location = new System.Drawing.Point(138, 21);
			this.cbAlt.Name = "cbAlt";
			this.cbAlt.Size = new System.Drawing.Size(38, 17);
			this.cbAlt.TabIndex = 2;
			this.cbAlt.Text = "&Alt";
			this.cbAlt.UseVisualStyleBackColor = true;
			this.cbAlt.CheckedChanged += new System.EventHandler(this.Verify_HotKey_Selected);
			// 
			// cbCtrl
			// 
			this.cbCtrl.AutoSize = true;
			this.cbCtrl.Location = new System.Drawing.Point(74, 21);
			this.cbCtrl.Name = "cbCtrl";
			this.cbCtrl.Size = new System.Drawing.Size(41, 17);
			this.cbCtrl.TabIndex = 1;
			this.cbCtrl.Text = "&Ctrl";
			this.cbCtrl.UseVisualStyleBackColor = true;
			this.cbCtrl.CheckedChanged += new System.EventHandler(this.Verify_HotKey_Selected);
			// 
			// cbWin
			// 
			this.cbWin.AutoSize = true;
			this.cbWin.Location = new System.Drawing.Point(6, 21);
			this.cbWin.Name = "cbWin";
			this.cbWin.Size = new System.Drawing.Size(45, 17);
			this.cbWin.TabIndex = 0;
			this.cbWin.Text = "&Win";
			this.cbWin.UseVisualStyleBackColor = true;
			this.cbWin.CheckedChanged += new System.EventHandler(this.Verify_HotKey_Selected);
			// 
			// ddlKey
			// 
			this.ddlKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlKey.FormattingEnabled = true;
			this.ddlKey.Location = new System.Drawing.Point(7, 20);
			this.ddlKey.Name = "ddlKey";
			this.ddlKey.Size = new System.Drawing.Size(128, 21);
			this.ddlKey.TabIndex = 0;
			this.ddlKey.SelectedIndexChanged += new System.EventHandler(this.Verify_HotKey_Selected);
			// 
			// gbKey
			// 
			this.gbKey.Controls.Add(this.ddlKey);
			this.gbKey.Location = new System.Drawing.Point(69, 73);
			this.gbKey.Name = "gbKey";
			this.gbKey.Size = new System.Drawing.Size(141, 53);
			this.gbKey.TabIndex = 4;
			this.gbKey.TabStop = false;
			this.gbKey.Text = "Key";
			// 
			// bOK
			// 
			this.bOK.Enabled = false;
			this.bOK.Location = new System.Drawing.Point(13, 132);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(75, 23);
			this.bOK.TabIndex = 5;
			this.bOK.Text = "&OK";
			this.bOK.UseVisualStyleBackColor = true;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bCancel
			// 
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.Location = new System.Drawing.Point(183, 132);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(75, 23);
			this.bCancel.TabIndex = 6;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = true;
			// 
			// HotKeyForm
			// 
			this.AcceptButton = this.bOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(279, 168);
			this.ControlBox = false;
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bOK);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.gbKey);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new System.Drawing.Size(285, 192);
			this.MinimumSize = new System.Drawing.Size(285, 192);
			this.Name = "HotKeyForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Choose a HotKey:";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.gbKey.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox cbShift;
		private System.Windows.Forms.CheckBox cbAlt;
		private System.Windows.Forms.CheckBox cbCtrl;
		private System.Windows.Forms.CheckBox cbWin;
		private System.Windows.Forms.ComboBox ddlKey;
		private System.Windows.Forms.GroupBox gbKey;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.Button bCancel;
	}
}