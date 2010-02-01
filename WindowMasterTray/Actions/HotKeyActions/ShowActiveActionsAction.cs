using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class ShowActiveActionsAction : HotKeyAction {

		private int _timeout;

		[Description("Time (in milliseconds) you wish to display the popup for. Must be between 500-30000.")]
		public int Timeout {
			get { return _timeout; }
			set {
				if (value > 499 && value < 30001)
					_timeout = value;
			}
		}

		protected override void ActionMethod(object sender, EventArgs args) {
			Program.DisplayActiveHotKeys(Timeout);
		}

		public ShowActiveActionsAction () {
			Name = "Show Active Actions";
			Description = "Displays a Balloon Icon showing the user the currently Active Actions";
		}

		public ShowActiveActionsAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			ShowActiveActionsAction saaa = action as ShowActiveActionsAction;
			if (saaa != null)
				Timeout = saaa.Timeout;
		}
	}
}

