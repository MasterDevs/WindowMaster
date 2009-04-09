using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WindowMasterLib;

namespace WindowMaster {
	public partial class Form1 : Form {

		private HotKey hook = new HotKey();

		public Form1() {
			InitializeComponent();
			//hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
			//hook.Register(WindowMasterLib.ModifierKeys.Shift, Keys.Left);
			//hook.Register(WindowMasterLib.ModifierKeys.Shift, Keys.Right);
			
			HotKey.RegisterHotKey(Modifiers.Shift, Keys.Right, hook_KeyPressed);
			HotKey.RegisterHotKey(Modifiers.Shift, Keys.Left, hook_KeyPressed);
		}


		void hook_KeyPressed(object sender, KeyPressedEventArgs e) {
			//Window w = new Window();
			//w.MoveToNextScreen();
			MessageBox.Show("Worked");
		}

	}
}
