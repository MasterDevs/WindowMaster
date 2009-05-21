using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MinimizeWindowAction : HotKeyAction{
		protected override void ActionMethod(object sender, EventArgs args) {
			if (Enabled) {
				Window.ForeGroundWindow.Minimize(RemainActive);
			}
		}
		
		[Description("When set to true, the window will remain active. When set to false, the next highest window in the Z-order will be made active")]
		public bool RemainActive { get; set; }

		public MinimizeWindowAction() {
			Name = "Minimize Window";
			Description = "This action will minimize the foreground window.";
			RemainActive = true;
		}

		public MinimizeWindowAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}
