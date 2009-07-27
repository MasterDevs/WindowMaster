using System;
using WindowMasterLib.Util;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class RecoverOrphanWindowsAction : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			foreach (Window w in Window.AllVisibleWindows) {
				if (WindowIsOutOfBounds(w)) {
					PlaceWindowInBounds(w);
				}
			}
		}

		private bool WindowIsOutOfBounds(Window w) {
			//-- If the window is maximized, it is within the bounds
			if (w.State == WindowState.Maximized || w.State == WindowState.Minimized || w.State == WindowState.Hide)
				return false;

			//-- Make sure it's not the taskbar or program manager
			if (w.Title == string.Empty || w.Title == "Program Manager")
				return false;

			foreach (Screen s in Screen.AllScreens) {
				RECT r = new RECT(s.WorkingArea);
				if (w.CurrentPosition.IsInsideOf(r))
					return false;
			}
			return true;
		}

		private void PlaceWindowInBounds(Window w) {
			w.MoveToScreen(w.CurrentScreen, true, true);
		}

		public RecoverOrphanWindowsAction() {
			Name = "Recover Orphan Windows Action";
			Description = "Looks for any windows that are off of the bounds of the display. If they are, they are placed within the visible area of the screen.";
		}

		public RecoverOrphanWindowsAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}

