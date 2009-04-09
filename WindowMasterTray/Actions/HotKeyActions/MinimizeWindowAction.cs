using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MinimizeWindowAction : HotKeyAction{
		protected override void ActionMethod(object sender, EventArgs args) {
			if (Enabled) {
				Window.ForeGroundWindow.Minimize();
			}
		}

		public MinimizeWindowAction() {
			Name = "Minimize Window";
			Description = "This action will minimize the foreground window.";
		}

		public MinimizeWindowAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}
