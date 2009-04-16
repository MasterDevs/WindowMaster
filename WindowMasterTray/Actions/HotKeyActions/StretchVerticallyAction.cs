using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class StretchVerticallyAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			Window.ForeGroundWindow.StretchVertically();
		}

		public StretchVerticallyAction() {
			Name = "Stretch Window Vertically";
			Description = "If the foreground window is in normal position, it will stretch it's height to fill the screen. If pressed again, it will restore the height to 2 /3 of the screen.";
		}

		public StretchVerticallyAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}

