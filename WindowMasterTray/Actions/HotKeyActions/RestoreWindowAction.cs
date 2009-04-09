using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;

namespace WindowMasterLib.Actions.HotKeyActions {
	public class RestoreWindowAction : HotKeyAction {
		protected override void ActionMethod(object sender, EventArgs args) {
			if (Enabled) {
				Window.ForeGroundWindow.Restore();
			}
		}

		public RestoreWindowAction() {
			Name = "Restore Window";
			Description = "This will restore the active window to it's normal size and location.";
		}

		public RestoreWindowAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}
