using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using WindowMasterLib.Util;

namespace WindowMasterLib.Actions.HotKeyActions {
	public class SetWindowPlacement : HotKeyAction {

		protected override void ActionMethod(object sender, EventArgs args) {
			Window w = Window.ForeGroundWindow;

			//-- Grab the WindowPlacement object
			Window.WINDOWPLACEMENT placement = w.GetWindowPlacement();

			//-- Set the rcNormalPosition rectangle to the specified coordinates
			placement.rcNormalPosition = new RECT(Left, Top, Right, Bottom);

			//-- Set the position of the window to Normal
			placement.showCmd = (uint)WindowState.Normal;

			//-- Update the WindowPlacement
			w.SetWindowPlacement(placement);
		}

		#region Custom Properties
		[Browsable(true), Description("Location of the bottom of the window.")]
		public int Bottom { get; set; }
		[Browsable(true), Description("Location of the left of the window.")]
		public int Left { get; set; }
		[Browsable(true), Description("Location of the right of the window.")]
		public int Right { get; set; }
		[Browsable(true), Description("Location of the top of the window.")]
		public int Top { get; set; }
		#endregion

		public SetWindowPlacement() {
			Name = "Set Window Placement";
			Description = "Takes the foreground window and set's it's position and size to the specified parameters.";
		}

		public SetWindowPlacement(KeyCombo combo)
			: this() {
			AddHotKey(combo);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			SetWindowPlacement swspa = action as SetWindowPlacement;
			if (swspa != null) {
				Top = swspa.Top;
				Left = swspa.Left;
				Bottom = swspa.Bottom;
				Right = swspa.Right;
			}
		}

	}
}
