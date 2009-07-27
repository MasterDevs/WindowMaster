using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class RememberWindowLocationAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
				Window w = Window.ForeGroundWindow;
				if (WindowMemory.Lookup(WindowMemoryID, w.ExecutiblePath)) {
					Window.WINDOWPLACEMENT wp = WindowMemory.Remember(WindowMemoryID, w.ExecutiblePath);
					w.SetWindowPlacement(wp);
				}
		}

		[Browsable(true), Description("Links a Remember Window Location Action with a Memorize Window Location Action")]
		public int WindowMemoryID { get; set; }

		public RememberWindowLocationAction () {
			Name = "Remember Window Action";
			Description = "Will restore the size & position of the foreground window if we memorized it before. To save the position, create a Memorize Window Location Action and give it the same WindowMemory ID.";
		}

		public RememberWindowLocationAction (KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			RememberWindowLocationAction rwla = action as RememberWindowLocationAction;

			if(rwla != null) {
				WindowMemoryID = rwla.WindowMemoryID;
			}
		}
	}
}

