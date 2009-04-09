using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MaximizeWindowAction : HotKeyAction{
		
		protected override void ActionMethod(object sender, EventArgs args) {
			if (Enabled) {
				Window.ForeGroundWindow.Maximize();
			}
		}
		
		public MaximizeWindowAction() {
			Name = "Maximize Window";
			Description = "This action will maximize the foreground window.";
		}
		public MaximizeWindowAction(KeyCombo hotKey) : this(){
			AddHotKey(hotKey);
		}
	}
}
