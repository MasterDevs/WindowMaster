using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class RestoreUpAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			Window w = Window.ForeGroundWindow;
			if(w.WindowState == WindowState.Minimized)
				w.Restore();
			else if(w.WindowState == WindowState.Normal)
				w.Maximize();
		}

		public RestoreUpAction() {
			Name = "Restore Window Up";
			Description = "If the window is minimized, this action will set the window state to normal placing it in it's normal location and size. If it's in the normal state, the window will be maximized.";
		}

		public RestoreUpAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}
