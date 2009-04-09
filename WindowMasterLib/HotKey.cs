using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/*
 * Special thanks to http://www.liensberger.it/web/blog/?p=207
 * for the shell of this code.
 *************************************************************/


namespace WindowMasterLib {
	[Serializable]
	public sealed class HotKey : IDisposable {
		// Registers a hot key with Windows.
		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
		// Unregisters the hot key with Windows.
		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		/// <summary>
		/// Represents the window that is used internally to get the messages.
		/// </summary>
		public class Window : NativeWindow, IDisposable {
			private static int WM_HOTKEY = 0x0312;

			public Window() {
				// create the handle for the window.
				this.CreateHandle(new CreateParams());
			}

			/// <summary>
			/// Overridden to get the notifications.
			/// </summary>
			protected override void WndProc(ref Message m) {
				base.WndProc(ref m);

				// check if we got a hot key pressed.
				if (m.Msg == WM_HOTKEY) {
					// get the keys.
					Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
					Modifiers modifier = (Modifiers)((int)m.LParam & 0xFFFF);

					// invoke the event to notify the parent.
					if (KeyPressed != null)
						KeyPressed(this, new KeyPressedEventArgs(modifier, key));
				}
			}

			public event EventHandler<KeyPressedEventArgs> KeyPressed;

			#region IDisposable Members

			public void Dispose() {
				this.DestroyHandle();
			}

			#endregion
		}

		private Window _window = new Window();
		private int _currentId;
		private Dictionary<KeyCombo, int> Combos;
		private static Dictionary<KeyCombo, EventHandler<KeyPressedEventArgs>> StaticKeyCombos;
		private static Dictionary<KeyCombo, int> StaticComboIDs;
		private static Window StaticWindow;
		private static int StaticID;

		/// <summary>
		/// A hot key has been pressed.
		/// </summary>
		public event EventHandler<KeyPressedEventArgs> KeyPressed;


		public HotKey() {
			// register the event of the inner native window.
			_window.KeyPressed += delegate(object sender, KeyPressedEventArgs args) {
				if (KeyPressed != null)
					KeyPressed(this, args);
			};

			Combos = new Dictionary<KeyCombo, int>();
		}

		/// <summary>
		/// Initializes all static variables
		/// </summary>
		static HotKey() {
			//-- Initialize Static Variables
			StaticKeyCombos = new Dictionary<KeyCombo, EventHandler<KeyPressedEventArgs>>();
			StaticComboIDs = new Dictionary<KeyCombo, int>();
			StaticWindow = new Window();
			//-- register the event of the inner static native window.
			StaticWindow.KeyPressed += delegate(object sender, KeyPressedEventArgs args) {
				if (StaticKeyCombos.ContainsKey(args.HotKey) &&
						StaticKeyCombos[args.HotKey] != null) {
					StaticKeyCombos[args.HotKey](StaticWindow, args);
				}

			};
		}

		/// <summary>
		/// Registers a hotkey with the static window instance.
		/// </summary>
		/// <param name="kc">KeyCobmination that makes up the hotkey</param>
		/// <param name="handler">Delegate to call once HotKey is triggered</param>
		/// <returns>True if hotkey was succesfully registered</returns>
		public static bool RegisterHotKey(KeyCombo hotkey, EventHandler<KeyPressedEventArgs> handler) {
			StaticID += 1;

			if (RegisterHotKey(StaticWindow.Handle, StaticID, (uint)hotkey.Modifiers, (uint)hotkey.Key)) {
				StaticKeyCombos.Add(hotkey, handler);
				StaticComboIDs.Add(hotkey, StaticID);
				return true;
			}
			StaticID -= 1; //-- Reset StaticID because we failed to add HotKey
			return false;
		}

		public static bool RegisterHotKey(Modifiers modfiers, Keys key, EventHandler<KeyPressedEventArgs> handler) {
			KeyCombo kc = new KeyCombo(modfiers, key);
			return RegisterHotKey(kc, handler);
		}

		public static void UnRegisterHotKey(KeyCombo kc) {
			if (StaticKeyCombos.ContainsKey(kc)) {
				StaticKeyCombos.Remove(kc);
				UnregisterHotKey(StaticWindow.Handle, StaticComboIDs[kc]);
				StaticComboIDs.Remove(kc);
			}
		}

		public static void UnRegisterHotKey(Modifiers modifer, Keys key) {
			KeyCombo kc = new KeyCombo(modifer, key);
			UnRegisterHotKey(kc);
		}

		public static void UnRegisterAllHotKeys() {
			foreach (KeyCombo kc in StaticComboIDs.Keys) {
				UnRegisterHotKey(kc);
			}
			//-- Re-Initialize Static Variables
			StaticID = 0;
			StaticKeyCombos.Clear();
			StaticComboIDs.Clear();
		}

		/// <summary>
		/// Registers a hot key in the system.
		/// </summary>
		/// <param name="modifier">The modifiers that are associated with the hot key.</param>
		/// <param name="key">The key itself that is associated with the hot key.</param>
		public bool Register(Modifiers modifiers, Keys key) {

			//-- Check if we've already added the KeyCombo
			if (Combos.ContainsKey(new KeyCombo(modifiers, key))) {
				return false;
			}

			// increment the counter.
			_currentId = _currentId + 1;

			// register the hot key.
			if (RegisterHotKey(_window.Handle, _currentId, (uint)modifiers, (uint)key)) {
				Combos.Add(new KeyCombo(modifiers, key), _currentId);
				return true;
			} else
				return false;
		}

		public void UnRegister(Modifiers modifers, Keys key) {
			KeyCombo combo = new KeyCombo(modifers, key);
			int hotkeyID = -1;

			if (Combos.ContainsKey(combo)) {
				hotkeyID = Combos[combo];
			}

			if (hotkeyID != -1) {
				UnregisterHotKey(_window.Handle, hotkeyID);
			}
		}

		#region IDisposable Members

		public void Dispose() {
			// unregister all the registered hot keys.
			for (int i = _currentId; i > 0; i--) {
				UnregisterHotKey(_window.Handle, i);
			}

			// dispose the inner native window.
			_window.Dispose();
		}

		#endregion
	}

	[Serializable]
	public struct KeyCombo {
		public Modifiers Modifiers;
		public Keys Key;

		public KeyCombo(Modifiers modifers, Keys keys) {
			this.Modifiers = modifers;
			this.Key = keys;
		}

		public override string ToString() {
			string combo = string.Empty;
			if ((Modifiers & Modifiers.Win) == Modifiers.Win)
				combo += "Win+";
			if ((Modifiers & Modifiers.Control) == Modifiers.Control)
				combo += "Control+";
			if ((Modifiers & Modifiers.Alt) == Modifiers.Alt)
				combo += "Alt+";
			if ((Modifiers & Modifiers.Shift) == Modifiers.Shift)
				combo += "Shift+";
			return combo + Key.ToString();

		}

		#region Equality
		public override bool Equals(Object obj) {
			return obj is KeyCombo && this == (KeyCombo)obj;
		}
		public bool IsEqual(KeyCombo kc) {
			return kc.Key == this.Key && kc.Modifiers == this.Modifiers;
		}
		public override int GetHashCode() {
			return Modifiers.GetHashCode() ^ Key.GetHashCode();
		}
		public static bool operator ==(KeyCombo x, KeyCombo y) {
			return x.Key == y.Key && x.Modifiers == y.Modifiers;
		}
		public static bool operator !=(KeyCombo x, KeyCombo y) {
			return !(x == y);
		}
		#endregion
	}

	/// <summary>
	/// Event Args for the event that is fired after the hot key has been pressed.
	/// </summary>
	public class KeyPressedEventArgs : EventArgs {

		private KeyCombo _HotKey;

		internal KeyPressedEventArgs(Modifiers modifiers, Keys key) {
			_HotKey = new KeyCombo(modifiers, key);
		}

		public KeyCombo HotKey { get { return _HotKey; } }
	}

	/// <summary>
	/// The enumeration of possible modifiers.
	/// </summary>
	[Flags]
	[Serializable]
	public enum Modifiers : uint {
		Alt = 1,
		Control = 2,
		Shift = 4,
		Win = 8
	}

	public static class ModifierKeysExtension {
		public static string ToString(this Modifiers mk, int mode) {
			string x = string.Empty;
			if (mode == 0) {
				if (((Modifiers.Win & mk) == Modifiers.Win)) {
					x += "Win+";
				}
				if ((Modifiers.Alt & mk) == Modifiers.Alt) {
					x += "Alt+";
				}
				if ((Modifiers.Control & mk) == Modifiers.Control) {
					x += "Control+";
				}
				if (((Modifiers.Shift & mk) == Modifiers.Shift)) {
					x += "Shift+";
				}
			} else
				x = "Test";

			return x;
		}
	}
}
