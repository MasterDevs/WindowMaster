using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class RestoreDown : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			uint state = Window.ForeGroundWindow.GetWindowPlacement().showCmd;

			if (state == showCmd.Maximized) {
				Window.ForeGroundWindow.SetWindowPlacement(showCmd.Normal);
			} else if (state == showCmd.Normal) {
				Window.ForeGroundWindow.SetWindowPlacement(showCmd.Minimized);
			}
		}

		public RestoreDown() {
			Name = "Restore Window Down";
			Description = "If the window is maximized, this action will set the window state to normal placing it in it's normal location and size. If it's in the normal state, the window will be minimized.";
		}

		public RestoreDown(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}
