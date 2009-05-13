using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowMasterLib;

namespace WindowMasterLib {
	public partial class HotKeyForm : Form {
		
		public HotKeyForm() {
			InitializeComponent();

			#region Populate Items Drop Downl List
			//ddlKey.Items.Add(Keys.A);
			//ddlKey.Items.Add(Keys.B);
			//ddlKey.Items.Add(Keys.C);
			//ddlKey.Items.Add(Keys.D);
			//ddlKey.Items.Add(Keys.E);
			//ddlKey.Items.Add(Keys.F);
			//ddlKey.Items.Add(Keys.G);
			//ddlKey.Items.Add(Keys.H);
			//ddlKey.Items.Add(Keys.I);
			//ddlKey.Items.Add(Keys.J);
			//ddlKey.Items.Add(Keys.K);
			//ddlKey.Items.Add(Keys.L);
			//ddlKey.Items.Add(Keys.M);
			//ddlKey.Items.Add(Keys.N);
			//ddlKey.Items.Add(Keys.O);
			//ddlKey.Items.Add(Keys.P);
			//ddlKey.Items.Add(Keys.Q);
			//ddlKey.Items.Add(Keys.R);
			//ddlKey.Items.Add(Keys.S);
			//ddlKey.Items.Add(Keys.T);
			//ddlKey.Items.Add(Keys.U);
			//ddlKey.Items.Add(Keys.V);
			//ddlKey.Items.Add(Keys.W);
			//ddlKey.Items.Add(Keys.X);
			//ddlKey.Items.Add(Keys.Y);
			//ddlKey.Items.Add(Keys.Z);
			//ddlKey.Items.Add(Keys.NumPad0);
			//ddlKey.Items.Add(Keys.NumPad1);
			//ddlKey.Items.Add(Keys.NumPad2);
			//ddlKey.Items.Add(Keys.NumPad3);
			//ddlKey.Items.Add(Keys.NumPad4);
			//ddlKey.Items.Add(Keys.NumPad5);
			//ddlKey.Items.Add(Keys.NumPad6);
			//ddlKey.Items.Add(Keys.NumPad7);
			//ddlKey.Items.Add(Keys.NumPad8);
			//ddlKey.Items.Add(Keys.NumPad9);
			//ddlKey.Items.Add(Keys.NumLock);
			//ddlKey.Items.Add(Keys.Divide);
			//ddlKey.Items.Add(Keys.Multiply);
			//ddlKey.Items.Add(Keys.Subtract);
			//ddlKey.Items.Add(Keys.Add);
			//ddlKey.Items.Add(Keys.Left);
			//ddlKey.Items.Add(Keys.Right);
			//ddlKey.Items.Add(Keys.Up);
			//ddlKey.Items.Add(Keys.Down);
			//ddlKey.Items.Add(Keys.Space);
			//ddlKey.Items.Add(Keys.CapsLock);
			//ddlKey.Items.Add(Keys.Tab);
			//ddlKey.Items.Add(Keys.F1);
			//ddlKey.Items.Add(Keys.F10);
			//ddlKey.Items.Add(Keys.F11);
			//ddlKey.Items.Add(Keys.F12);
			//ddlKey.Items.Add(Keys.F2);
			//ddlKey.Items.Add(Keys.F3);
			//ddlKey.Items.Add(Keys.F4);
			//ddlKey.Items.Add(Keys.F5);
			//ddlKey.Items.Add(Keys.F6);
			//ddlKey.Items.Add(Keys.F7);
			//ddlKey.Items.Add(Keys.F8);
			//ddlKey.Items.Add(Keys.F9);
			#endregion

			foreach (Keys key in Enum.GetValues(typeof(Keys))) {
				ddlKey.Items.Add(key);
			}
		}

		public HotKeyForm(KeyCombo kc) : this() {
			cbAlt.Checked = (kc.Modifiers & Modifiers.Alt) == Modifiers.Alt;
			cbCtrl.Checked = (kc.Modifiers & Modifiers.Control) == Modifiers.Control;
			cbShift.Checked = (kc.Modifiers & Modifiers.Shift) == Modifiers.Shift;
			cbWin.Checked = (kc.Modifiers & Modifiers.Win) == Modifiers.Win;
			ddlKey.SelectedItem = kc.Key;
		}

		public KeyCombo HotKey {
			get {
				Modifiers mf = 0;

				if (cbAlt.Checked)
					mf |= Modifiers.Alt;
				if (cbCtrl.Checked)
					mf |= Modifiers.Control;
				if (cbShift.Checked)
					mf |= Modifiers.Shift;
				if (cbWin.Checked)
					mf |= Modifiers.Win;

				return new KeyCombo(mf, (Keys)ddlKey.SelectedItem);
			}
		}

		private void Verify_HotKey_Selected(object sender, EventArgs e) {
			//-- The ok button is only enabled if we have at least 1 modifier
			// and a key is selected
			bOK.Enabled =
				(cbAlt.Checked || cbCtrl.Checked || cbShift.Checked || cbWin.Checked) &&
				(ddlKey.SelectedItem != null);
		}
	}
}
