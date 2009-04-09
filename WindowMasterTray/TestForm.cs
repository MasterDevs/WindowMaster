using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowMaster {
	public partial class TestForm : Form {
		public TestForm() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			MessageBox.Show("Minimize");
		}

		private void button2_Click(object sender, EventArgs e) {
			MessageBox.Show("Restore");
		}

		private void button3_Click(object sender, EventArgs e) {
			MessageBox.Show("Maximize");
		}
	}
}
