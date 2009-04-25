using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MoveWindowAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			if (Enabled)
				Window.ForeGroundWindow.MoveToNextScreen(PreserveSize);
		}

		private bool _PreserveSize = false;
		[Description("When set to true, the window will be relocated yet it's size (in pixels) will remain the same.")]
		public bool PreserveSize {
			get { return _PreserveSize; }
			set { _PreserveSize = value; }
		}

		public MoveWindowAction() {
			Name = "Move foreground window to next screen";
			Description = "This action will get the current foreground window and move it to the next screen. If there is only one screen, it will not move the window.";
		}

		public MoveWindowAction(KeyCombo kc) : this() {
			AddHotKey(kc);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);

			if (action is MoveWindowAction) {
				PreserveSize = ((MoveWindowAction)action).PreserveSize;
			}
		}

	}
}
