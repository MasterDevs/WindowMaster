using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MediaKeyAction : HotKeyAction {


		[DllImport("user32.dll", SetLastError = true)]
		static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

		[DllImport("user32.dll")]
		static extern IntPtr GetMessageExtraInfo();
		
		#region Structs
		private struct MOUSEINPUT {
			public int dx;
			public int dy;
			public uint mouseData;
			public uint dwFlags;
			public uint time;
			public IntPtr dwExtraInfo;
		}

		private struct KEYBDINPUT {
			public ushort wVk;
			public ushort wScan;
			public uint dwFlags;
			public uint time;
			public IntPtr dwExtraInfo;
		}

		private struct HARDWAREINPUT {
			public uint uMsg;
			public ushort wParamL;
			public ushort wParamH;
		}
		
		[StructLayout(LayoutKind.Explicit, Size=28)]
		private struct MOUSEKEYBDHARDWAREINPUT {
			[FieldOffset(0)]
			public MOUSEINPUT mi;

			[FieldOffset(0)]
			public KEYBDINPUT ki;

			[FieldOffset(0)]
			public HARDWAREINPUT hi;
		}

		private struct INPUT {
			public int type;
			public MOUSEKEYBDHARDWAREINPUT mkhi;
		}
				
		#endregion

		#region Constants
		private const int INPUT_MOUSE = 0;
		private const int INPUT_KEYBOARD = 1;
		private const int INPUT_HARDWARE = 2;
		private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
		private const uint KEYEVENTF_KEYUP = 0x0002;
		private const uint KEYEVENTF_UNICODE = 0x0004;
		private const uint KEYEVENTF_SCANCODE = 0x0008;
		private const uint XBUTTON1 = 0x0001;
		private const uint XBUTTON2 = 0x0002;
		private const uint MOUSEEVENTF_MOVE = 0x0001;
		private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
		private const uint MOUSEEVENTF_LEFTUP = 0x0004;
		private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
		private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
		private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
		private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
		private const uint MOUSEEVENTF_XDOWN = 0x0080;
		private const uint MOUSEEVENTF_XUP = 0x0100;
		private const uint MOUSEEVENTF_WHEEL = 0x0800;
		private const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
		private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
		#endregion


		protected override void ActionMethod(object sender, EventArgs args) {
			//uint intReturn;
			INPUT structInput;
			structInput = new INPUT();
			structInput.type = INPUT_KEYBOARD;

			structInput.mkhi.ki.wScan = 0;
			structInput.mkhi.ki.time = 0;
			structInput.mkhi.ki.dwFlags = 0;
			structInput.mkhi.ki.wVk = 0xB3;

			SendInput(1, new INPUT[] { structInput }, (int)Marshal.SizeOf(typeof(INPUT)));
		}

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
			//MediaKeyAction mka = action as MediaKeyAction;
			//if (mka != null) {
			//}
		}
	}
}

