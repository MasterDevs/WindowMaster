using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class DecreaseOpacityAction : HotKeyAction {

		private double Percentage = 0.05;

		protected override void ActionMethod(object sender, EventArgs args) {
			Window.ForeGroundWindow.DecreaseOpacity(Percentage);
		}

		public DecreaseOpacityAction() {
			Name = "Decrease Opacity";
			Description = string.Format("Decreases the opacity of the foreground window by {0:0%}.", Percentage);
		}

		public DecreaseOpacityAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}

