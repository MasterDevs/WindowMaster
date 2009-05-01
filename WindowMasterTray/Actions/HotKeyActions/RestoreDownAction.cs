using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class RestoreDownAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			Window w = Window.ForeGroundWindow;
			if (w.State == WindowState.Maximized)
				w.Restore();
			else if (w.State == WindowState.Normal)
				w.Minimize();
		}

		public RestoreDownAction() {
			Name = "Restore Window Down";
			Description = "If the window is maximized, this action will set the window state to normal placing it in it's normal location and size. If it's in the normal state, the window will be minimized.";
		}

		public RestoreDownAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}
