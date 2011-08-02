namespace WindowMasterLib {
	partial class StartApplicationOutput {
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabStdOut = new System.Windows.Forms.TabPage();
			this.tbStdOutput = new System.Windows.Forms.TextBox();
			this.btnCopyStdOutput = new System.Windows.Forms.Button();
			this.tabStdErr = new System.Windows.Forms.TabPage();
			this.tbStdError = new System.Windows.Forms.TextBox();
			this.btnStdErrorCopy = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabStdOut.SuspendLayout();
			this.tabStdErr.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabStdOut);
			this.tabControl1.Controls.Add(this.tabStdErr);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(501, 290);
			this.tabControl1.TabIndex = 0;
			// 
			// tabStdOut
			// 
			this.tabStdOut.Controls.Add(this.tbStdOutput);
			this.tabStdOut.Controls.Add(this.btnCopyStdOutput);
			this.tabStdOut.Location = new System.Drawing.Point(4, 22);
			this.tabStdOut.Name = "tabStdOut";
			this.tabStdOut.Padding = new System.Windows.Forms.Padding(3);
			this.tabStdOut.Size = new System.Drawing.Size(493, 264);
			this.tabStdOut.TabIndex = 0;
			this.tabStdOut.Text = "Standard Output";
			this.tabStdOut.UseVisualStyleBackColor = true;
			// 
			// tbStdOutput
			// 
			this.tbStdOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbStdOutput.Location = new System.Drawing.Point(3, 3);
			this.tbStdOutput.Multiline = true;
			this.tbStdOutput.Name = "tbStdOutput";
			this.tbStdOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbStdOutput.Size = new System.Drawing.Size(487, 235);
			this.tbStdOutput.TabIndex = 1;
			// 
			// btnCopyStdOutput
			// 
			this.btnCopyStdOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnCopyStdOutput.Location = new System.Drawing.Point(3, 238);
			this.btnCopyStdOutput.Name = "btnCopyStdOutput";
			this.btnCopyStdOutput.Size = new System.Drawing.Size(487, 23);
			this.btnCopyStdOutput.TabIndex = 0;
			this.btnCopyStdOutput.Text = "Copy to Clipboard";
			this.btnCopyStdOutput.UseVisualStyleBackColor = true;
			this.btnCopyStdOutput.Click += new System.EventHandler(this.btnCopyStdOutput_Click);
			// 
			// tabStdErr
			// 
			this.tabStdErr.Controls.Add(this.tbStdError);
			this.tabStdErr.Controls.Add(this.btnStdErrorCopy);
			this.tabStdErr.Location = new System.Drawing.Point(4, 22);
			this.tabStdErr.Name = "tabStdErr";
			this.tabStdErr.Padding = new System.Windows.Forms.Padding(3);
			this.tabStdErr.Size = new System.Drawing.Size(493, 264);
			this.tabStdErr.TabIndex = 1;
			this.tabStdErr.Text = "Stadard Error";
			this.tabStdErr.UseVisualStyleBackColor = true;
			// 
			// tbStdError
			// 
			this.tbStdError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbStdError.Location = new System.Drawing.Point(3, 3);
			this.tbStdError.Multiline = true;
			this.tbStdError.Name = "tbStdError";
			this.tbStdError.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbStdError.Size = new System.Drawing.Size(487, 235);
			this.tbStdError.TabIndex = 1;
			// 
			// btnStdErrorCopy
			// 
			this.btnStdErrorCopy.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnStdErrorCopy.Location = new System.Drawing.Point(3, 238);
			this.btnStdErrorCopy.Name = "btnStdErrorCopy";
			this.btnStdErrorCopy.Size = new System.Drawing.Size(487, 23);
			this.btnStdErrorCopy.TabIndex = 0;
			this.btnStdErrorCopy.Text = "Copy to Clipboard";
			this.btnStdErrorCopy.UseVisualStyleBackColor = true;
			this.btnStdErrorCopy.Click += new System.EventHandler(this.btnStdErrorCopy_Click);
			// 
			// StartApplicationOutput
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.ClientSize = new System.Drawing.Size(501, 290);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(800, 800);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(200, 200);
			this.Name = "StartApplicationOutput";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StartApplicationOutput_KeyUp);
			this.tabControl1.ResumeLayout(false);
			this.tabStdOut.ResumeLayout(false);
			this.tabStdOut.PerformLayout();
			this.tabStdErr.ResumeLayout(false);
			this.tabStdErr.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabStdOut;
		private System.Windows.Forms.TextBox tbStdOutput;
		private System.Windows.Forms.Button btnCopyStdOutput;
		private System.Windows.Forms.TabPage tabStdErr;
		private System.Windows.Forms.TextBox tbStdError;
		private System.Windows.Forms.Button btnStdErrorCopy;
	}
}