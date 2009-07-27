using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMasterLib;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Text;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class MinimizeToTray : HotKeyAction {

		#region P/Invokes
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CloseHandle(IntPtr hHandle);
		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DestroyIcon(IntPtr hIcon);

		public const uint SHGFI_ICON = 0x100;
		public const uint SHGFI_SMALLICON = 0x1;

		[StructLayout(LayoutKind.Sequential)]
		public struct SHFILEINFO {
			public IntPtr hIcon;
			public IntPtr iIcon;
			public uint dwAttributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string szDisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string szTypeName;
		};

		#endregion

		private class window {
			public Window win;
			public WindowState previousState;
			public window(Window w, WindowState prevState) {
				win = w;
				previousState = prevState;
			}
		}

		private List<NotifyIcon> MinimizedWindows;

		protected override void ActionMethod(object sender, EventArgs args) {
			Window win = Window.ForeGroundWindow;
			if (Enabled && !WindowHasBeenMinimized(win.WindowHandle) ) {
				//-- Try to grab the icon from the path to the executible of the window
				Icon icon = IconFromPath(win.ExecutiblePath);
				
				//-- Create the tray icon
				NotifyIcon tray = new NotifyIcon();
				tray.Icon = icon;
				tray.Visible = true;
				tray.Tag = new window(win, win.State);
				tray.Text = win.Title.Length > 64 ? win.Title.Substring(0, 63) : win.Title;
				tray.Click += new EventHandler(tray_Click);

				//-- Minimize the window
				win.Minimize(false);
				
				//-- Hide the window
				win.Hide();

				//-- Add the new Notify Icon to the list
				MinimizedWindows.Add(tray);
			}
		}

		public MinimizeToTray() {
			Name = "Minimize to System Tray";
			Description = "This action will minimize the foreground window to the system tray. It can be restored by clicking on the tray icon that's created.";
			MinimizedWindows = new List<NotifyIcon>();
			Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
		}

		void Application_ApplicationExit(object sender, EventArgs e) {
			for (int i = 0; i < MinimizedWindows.Count; i++) {
				tray_Click(MinimizedWindows[0], null);
			}
		}

		public MinimizeToTray(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		private void tray_Click(object sender, EventArgs e) {
			NotifyIcon tray = sender as NotifyIcon;
			window win = tray.Tag as window;
			if (Window.IsWindow(win.win.WindowHandle)) {
				win.win.SetWindowState(win.previousState);
				win.win.SetAsForegroundWindow();
			} else
				MessageBox.Show("Window does not exist");
			tray.Click -= new EventHandler(tray_Click);

			//-- Remove the window from the list
			MinimizedWindows.Remove(tray);
			//-- Dispose of the tray
			tray.Dispose();
		}

		#region Helper Methods
		/// <summary>
		/// This method will retrieve the default icon from an executable
		/// that is located at the parameter path
		/// </summary>
		private Icon IconFromPath(string path) {
			Icon icon = null;

			if (System.IO.File.Exists(path)) {
				SHFILEINFO info = new SHFILEINFO();
				SHGetFileInfo(path, 0, ref info, (uint)Marshal.SizeOf(info), SHGFI_ICON | SHGFI_SMALLICON);

				Icon temp = Icon.FromHandle(info.hIcon);
				icon = (System.Drawing.Icon)temp.Clone();
				DestroyIcon(temp.Handle);
			}

			return icon;
		}

		/// <summary>
		/// Returns true if the window has already been minimized to tray
		/// </summary>
		private bool WindowHasBeenMinimized(IntPtr handle) {
			foreach (NotifyIcon ni in MinimizedWindows) {
				window w = (window)ni.Tag;
				if (w.win.WindowHandle == handle)
					return true;
			}
			return false;
		}
		#endregion
	}
}
