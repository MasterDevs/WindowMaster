using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class IncreaseOpacityAction : HotKeyAction {

        private double Percentage = 0.05;

		protected override void ActionMethod(object sender, EventArgs args) {
            Window.ForeGroundWindow.IncreaseOpacity(Percentage);
		}

		public IncreaseOpacityAction() {
			Name = "Increase Opacity";
			Description = string.Format("Increases the opacity of the foreground window by {0:0%}.", Percentage);
		}

        public IncreaseOpacityAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}

