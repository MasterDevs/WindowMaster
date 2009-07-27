using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class DockWindowAction : HotKeyAction {

		private string DescriptionFormatString = "Docks a window to {0} part of the current screen and resizes the window to {1:0%} of the working area of the screen.";
		private DockStyle _Dock = DockStyle.Left;
		[Description("The location of the window")]
		public DockStyle Dock {
			get { return _Dock; }
			set { _Dock = value; InitDescription(); }
		}

		private double _Percentage = .3;
		[Browsable(true), Description("Percentage of screen window will take up after being docked. Valid Values: [0.0, 1.0]")]
		public double Percentage {
			get { return _Percentage; }
			set {
				if (value >= 0 && value <= 1) {
					_Percentage = value;
					InitDescription();
				}
			}
		}

		private void InitDescription() {
			Description = string.Format(DescriptionFormatString, Dock, Percentage);
		}

		protected override void ActionMethod(object sender, EventArgs args) {
			Window w = Window.ForeGroundWindow;
			if (w.IsDocked)
				w.UnDock();
			else
				w.Dock(Dock, Percentage);
		}

		public DockWindowAction() {
			Name = "Dock Window";
			InitDescription();
		}

		public DockWindowAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			DockWindowAction dwa = action as DockWindowAction;
			if(dwa != null) {
				Dock = dwa.Dock;
				Percentage = dwa.Percentage;
			}
		}
	}
}

