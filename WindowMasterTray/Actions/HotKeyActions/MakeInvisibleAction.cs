using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MakeInvisibleAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			Window.ForeGroundWindow.MakeInvisible();
		}

		public MakeInvisibleAction() {
			Name = "Make Invisible";
			Description = "Makes the foreground window invisible.";
		}

		public MakeInvisibleAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}

