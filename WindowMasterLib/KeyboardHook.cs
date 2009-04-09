using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/*
 * Special thanks to http://www.liensberger.it/web/blog/?p=207
 * for the shell of this code.
 * ************** */


namespace WindowMasterLib {
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
		private class Window : NativeWindow, IDisposable {
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
					ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

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

		public HotKey() {
			// register the event of the inner native window.
			_window.KeyPressed += delegate(object sender, KeyPressedEventArgs args) {
				if (KeyPressed != null)
					KeyPressed(this, args);
			};
		}

		/// <summary>
		/// Registers a hot key in the system.
		/// </summary>
		/// <param name="modifier">The modifiers that are associated with the hot key.</param>
		/// <param name="key">The key itself that is associated with the hot key.</param>
		public void Register(ModifierKeys modifiers, Keys key) {
			// increment the counter.
			_currentId = _currentId + 1;

			// register the hot key.
			if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifiers, (uint)key))
				throw new InvalidOperationException("Couldn’t register the hot key.");
		}

		public void UnRegister(ModifierKeys modifers, Keys key) {
			int hotkeyID = 0;
			if (!RegisterHotKey(_window.Handle, hotkeyID, (uint)modifers, (uint)key))
			  UnregisterHotKey(_window.Handle, hotkeyID);

		}

		/// <summary>
		/// A hot key has been pressed.
		/// </summary>
		public event EventHandler<KeyPressedEventArgs> KeyPressed;

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

	/// <summary>
	/// Event Args for the event that is fired after the hot key has been pressed.
	/// </summary>
	public class KeyPressedEventArgs : EventArgs {
		private ModifierKeys _modifier;
		private Keys _key;

		internal KeyPressedEventArgs(ModifierKeys modifier, Keys key) {
			_modifier = modifier;
			_key = key;
		}

		public ModifierKeys Modifier {
			get { return _modifier; }
		}

		public Keys Key {
			get { return _key; }
		}
	}

	/// <summary>
	/// The enumeration of possible modifiers.
	/// </summary>
	[Flags]
	public enum ModifierKeys : uint {
		Alt = 1,
		Control = 2,
		Shift = 4,
		Win = 8
	}
}