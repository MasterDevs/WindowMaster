using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MemorizeWindowLocationAction  : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			WindowMemory.Memorize(WindowMemoryID, Window.ForeGroundWindow);
		}

		[Browsable(true), Description("Links a Memorize Window Location Action with a Remember Window Location")]
		public int WindowMemoryID { get; set; }

		public MemorizeWindowLocationAction () {
			Name = "Memorize Window Action";
			Description = "Will store the current size & position of the foreground window based on the application it corresponds to. To remember position, create a Remembor Window Action and give it the same WindowMemory ID.";
		}

		public MemorizeWindowLocationAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			MemorizeWindowLocationAction mwla = action as MemorizeWindowLocationAction;
			if (mwla != null) {
				WindowMemoryID = mwla.WindowMemoryID;
			}
		}
	}
}

