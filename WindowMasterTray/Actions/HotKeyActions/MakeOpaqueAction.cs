using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MakeOpaqueAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			Window.ForeGroundWindow.MakeOpaque();
		}

		public MakeOpaqueAction() {
			Name = "Make Opaque";
			Description = "Makes the foreground window completely visible.";
		}

		public MakeOpaqueAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}

