using System;
using System.Collections.Generic;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MinimizeRestoreOtherWindowsAction : HotKeyAction {

		#region Helper Methods

		private Dictionary<Window, WindowState> PreviousStates;
		private Window MainWindow;

		/// <summary>
		/// This method will restore all windows in the PreviousStates collection
		/// </summary>
		private void RestoreOtherWindows() {
			//-- Go threw all windows, restoring their previous state and
			// removing them from the list of PreviousPositions
			foreach (Window w in PreviousStates.Keys) {
				w.SetWindowState(PreviousStates[w]);
			}
			PreviousStates.Clear();
			//-- Set MainWindow to ForegroundWindow
			MainWindow.SetAsForegroundWindow();
		}

		/// <summary>
		/// This method will minimize all visible windows other then the main window
		/// by calling EnumWindows
		/// </summary>
		private void MinimizeOtherWindows() {
			MainWindow = Window.ForeGroundWindow;
			foreach (Window w in Window.AllVisibleWindows) {
				if (w.WindowHandle != MainWindow.WindowHandle) {
					PreviousStates.Add(w, w.State);
					w.Minimize();
				}
			}
			//-- After we minimize, we need to se the MainWindow as the foreground window
			// and activate it. It will be visible, but it will not be activated.
			MainWindow.SetAsForegroundWindow();
		} 
		#endregion

		protected override void ActionMethod(object sender, EventArgs args) {
			if (PreviousStates.Count == 0)
				MinimizeOtherWindows();
			else
				RestoreOtherWindows();
		}
		

		public MinimizeRestoreOtherWindowsAction() {
			Name = "Minimize / Restore All Other Windows";
			Description = "Minimizes all active visible windows other then the foreground window. When pressed again, will restore all of those windows to their previous positions.";
			PreviousStates = new Dictionary<Window, WindowState>();
		}

		public MinimizeRestoreOtherWindowsAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}
	}
}

