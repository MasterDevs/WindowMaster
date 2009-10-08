using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MediaKeyAction : HotKeyAction {

		public enum MediaKey {
			PlayPause = 0,
			Next = 1,
			Previous = 2,
			Stop = 3,
			VolumeUp,
			VolumeDown,
			Mute
		}

		protected override void ActionMethod(object sender, EventArgs args) {
			switch (Key) {
				case MediaKey.PlayPause:
					Input.Send(Input.Keyboard.MediaPlayPause);
					break;
				case MediaKey.Next:
					Input.Send(Input.Keyboard.MediaNextTrack);
					break;
				case MediaKey.Previous:
					Input.Send(Input.Keyboard.MediaPreviousTrack);
					break;
				case MediaKey.Stop:
					Input.Send(Input.Keyboard.MediaStop);
					break;
				case MediaKey.VolumeUp:
					Input.Send(Input.Keyboard.VolumeUp);
					break; ;
				case MediaKey.VolumeDown:
					Input.Send(Input.Keyboard.VolumeDown);
					break;
				case MediaKey.Mute:
					Input.Send(Input.Keyboard.VolumeMute);
					break;
				default:
					break;
			}
		}

		[Browsable(true), Description("The Media Key you'd like to press")]
		public MediaKey Key { get; set; }

		public MediaKeyAction () {
			Name = "Media Key Action";
			Description = "Simulates a media key being pressed. Usefull if you don't have a media keyboard.";
		}

		public MediaKeyAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			MediaKeyAction mka = action as MediaKeyAction;
			if (mka != null) {
				Key = mka.Key;
			}
		}
	}
}

