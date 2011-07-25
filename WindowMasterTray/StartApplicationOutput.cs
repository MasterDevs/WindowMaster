using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowMasterLib {
	public partial class StartApplicationOutput : Form {
		public StartApplicationOutput() {
			InitializeComponent();
		}

		public StartApplicationOutput(string procName, string stdOut, string stdErr)
			: this() {

			this.Text = procName;

			bool result1 = !ShowHideTab(tbStdError, tabStdErr, stdErr);
			bool result2 = !ShowHideTab(tbStdOutput, tabStdOut, stdOut);
			if (result1 && result2) {
				MessageBox.Show("No output from the Application");
				this.DialogResult = DialogResult.OK;
			}
		}

		private bool ShowHideTab(TextBox tb, TabPage tab, string text) {
			if (string.IsNullOrEmpty(text)) {
				tabControl1.TabPages.Remove(tab);
				return false;
			}

			tab.Show();
			tb.Text = text;
			return true;
		}


		private void btnStdErrorCopy_Click(object sender, EventArgs e) {
			if (!string.IsNullOrEmpty(tbStdError.Text))
				Clipboard.SetText(tbStdError.Text);
		}

		private void btnCopyStdOutput_Click(object sender, EventArgs e) {
			if (!string.IsNullOrEmpty(tbStdOutput.Text))
				Clipboard.SetText(tbStdOutput.Text);
		}

		private void StartApplicationOutput_KeyUp(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Escape)
				this.DialogResult = DialogResult.Cancel;
		}
	}
}
