using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MoveWindowAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			if (Enabled)
				Window.ForeGroundWindow.MoveToNextScreen();
		}

		public MoveWindowAction() {
			Name = "Move foreground window to next screen";
			Description = "This action will get the current foreground window and move it to the next screen. If there is only one screen, it will not move the window.";
		}

		public MoveWindowAction(KeyCombo kc) : this() {
			AddHotKey(kc);
		}

	}
}
