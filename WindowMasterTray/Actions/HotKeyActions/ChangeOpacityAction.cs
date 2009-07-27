using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class ChangeOpacityAction  : HotKeyAction {

		private string DescriptionFormatString = "Changes the opacity of the foreground window by {0:0%}."; 
		private double _Percentage = 0.05;
		[Description("Percent of total opacity the window will be changed by. Valid Values: [-1.0, 1.0]")]
		public double Percentage {
			get {
				return _Percentage;
			}
			set {
				if (value >= -1 && value <= 1) {
					_Percentage = value;
					Description = string.Format(DescriptionFormatString, _Percentage);
				}
			}
		}

		protected override void ActionMethod(object sender, EventArgs args) {
			Window.ForeGroundWindow.ChangeOpacity(Percentage);
		}

		public ChangeOpacityAction () {
			Name = "Change Opacity";
			Description = string.Format(DescriptionFormatString, _Percentage);
		}

		public ChangeOpacityAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			ChangeOpacityAction coa = action as ChangeOpacityAction;
			if(coa != null) {
				Percentage = coa.Percentage;
			}
		}
	}
}

