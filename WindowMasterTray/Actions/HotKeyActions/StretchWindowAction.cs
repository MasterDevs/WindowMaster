using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class StretchWindowAction : HotKeyAction {

		[Serializable]
		public enum StretchDirection : int {
			Horizontally,
			Vertically
		}

		private StretchDirection _Direction;
		[Description("The direction in which the window will be stretched.")]
		public StretchDirection Direction {
		  get { return _Direction; }
		  set { _Direction = value; }
		}


		protected override void ActionMethod(object sender, EventArgs args) {
			if (Direction == StretchDirection.Vertically) {
				Window.ForeGroundWindow.StretchVertically();
			} else
				Window.ForeGroundWindow.StretchHorizontally();
		}

		public StretchWindowAction () {
			Name = "Stretch Window";
			Description = "If the foreground window is in normal position, it will stretch the window in the given direction. If pressed again, it will restore the window to its location before the stretch.";
		}

		public StretchWindowAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			if (action is StretchWindowAction)
				Direction = ((StretchWindowAction)action).Direction;
		}
	}
}

