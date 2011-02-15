using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowMasterLib.Actions.HotKeyActions {
	public class ShowWindowPlacement : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			Window w = Window.ForeGroundWindow;
			Window.WINDOWPLACEMENT wp = w.GetWindowPlacement();

			DialogResult dr =
				MessageBox.Show(
					string.Format("{4}:\nLeft: {0}\nTop: {1}\nRight: {2}\nBottom: {3}\n\nCreate 'Set Window Placement' action?",
						wp.rcNormalPosition.Left,
						wp.rcNormalPosition.Top,
						wp.rcNormalPosition.Right,
						wp.rcNormalPosition.Bottom,
						w.Title),
					"Window Placement",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Information,
					MessageBoxDefaultButton.Button2);

			if (dr == DialogResult.Yes) {
				SetWindowPlacement swp = new SetWindowPlacement();
				swp.Name = string.Format("SWP [{0}]", w.Title);
				swp.Top = wp.rcNormalPosition.Top;
				swp.Left = wp.rcNormalPosition.Left;
				swp.Right = wp.rcNormalPosition.Right;
				swp.Bottom = wp.rcNormalPosition.Bottom;
				ActionManager.Actions.Add(swp);
				ActionManager.SaveActions();
			}
		}

		public ShowWindowPlacement() {
			Name = "Show Window Placement";
			Description = "Displays a message box with the current location of the foreground window and provides the ability to quickly generate a 'Set Window Placement' action.";
		}

		public ShowWindowPlacement(KeyCombo hotkey)
			: this() {
			AddHotKey(hotkey);
		}
	}
}
