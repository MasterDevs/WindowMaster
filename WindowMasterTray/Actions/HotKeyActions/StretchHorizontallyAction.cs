using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class StretchHorizontallyAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			Window.ForeGroundWindow.StretchHorizontally();
		}

		public StretchHorizontallyAction() {
			Name = "Stretch Window Horizontally";
			Description = "If the foreground window is in normal position, it will stretch it's width to fill the screen. If pressed again, it will restore the width to 2 /3 of the screen.";
		}

		public StretchHorizontallyAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}

