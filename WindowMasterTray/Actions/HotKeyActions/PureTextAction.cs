using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class PureTextAction : HotKeyAction {


		public PureTextAction() {
			Name = "Pure Text Clipboard";
			Description = "If the content of your clipboard is rich formatted text, this action will convert it to plain text.";
		}

		protected override void ActionMethod(object sender, EventArgs args) {
			//-- Grab the text as Plain Text (Windows Unicode Format)
			string text = Clipboard.GetText(TextDataFormat.UnicodeText);

			//-- If we didn't get any text, simply return, nothing to do.
			if (string.IsNullOrEmpty(text)) return;

			//-- If we did get text, set the clipboard and specify now that's it's simply
			//plain text.
			Clipboard.SetText(text, TextDataFormat.UnicodeText);
		}
	}
}
