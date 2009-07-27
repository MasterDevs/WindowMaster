using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;
using System.ComponentModel;
using System.Windows.Forms;
using WindowMasterLib.Util;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MoveWindowAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			if (Enabled && Screen.AllScreens.Length > 1) {
				//-- Create a LoopList of all screens
				LoopList<Screen> screens = new LoopList<Screen>(Screen.AllScreens.Length);
				foreach (Screen scr in Screen.AllScreens) {
					screens.Add(scr);
				}
				//-- Get the current foreground window
				Window w = Window.ForeGroundWindow;
				//-- Set the current index of the window
				screens.CurrentIndex = screens.IndexOf(Screen.FromHandle(w.WindowHandle));
				//-- Move the window
				w.MoveToScreen(screens.Next, PreserveSize);
			}
		}

		private bool _PreserveSize = false;
		[Description("When set to true, the window will be relocated but it's size (in pixels) will remain the same.")]
		public bool PreserveSize {
			get { return _PreserveSize; }
			set { _PreserveSize = value; }
		}

		public MoveWindowAction() {
			Name = "Move Window";
			Description = "This action will move the current foreground window to the next screen. If there is only one screen, it will not move the window.";
		}

		public MoveWindowAction(KeyCombo kc) : this() {
			AddHotKey(kc);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			MoveWindowAction mwa = action as MoveWindowAction;
			if (mwa != null) {
				PreserveSize = mwa.PreserveSize;
			}
		}

	}
}
