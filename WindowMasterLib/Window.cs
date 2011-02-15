using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowMasterLib.Util;
using System.Text;

namespace WindowMasterLib {
	public class Window {

		#region User32.dll Methods
		#region Together, these methods will grab the file path of the executible based on a window handle
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle,
			 uint dwProcessId);
		private static uint VMRead = 0x00000010;
		private static uint QueryInformation = 0x00000400;
		[DllImport("Psapi.dll", SetLastError = true)]
		[PreserveSig]
		public static extern uint GetModuleFileNameEx([In]IntPtr hProcess, [In] IntPtr hModule, [Out] StringBuilder lpFilename,
				[In][MarshalAs(UnmanagedType.U4)]int nSize);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CloseHandle(IntPtr hHandle);
		#endregion

		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		[DllImport("user32.dll")]
		private static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		private static extern bool SetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

		[DllImport("user32.dll")]
		private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool GetLayeredWindowAttributes(IntPtr hwnd, out uint crKey, out byte bAlpha, out uint dwFlags);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

		[DllImport("user32.dll")]
		static extern bool EnumWindows(EnumWindowsDelagate lpfn, IntPtr lParam);
		[DllImport("user32.dll")]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		private delegate bool EnumWindowsDelagate(IntPtr hWnd, int lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		static extern int GetWindowTextLength(IntPtr hWnd);

		/// <summary>
		/// Deterimines if a handle corresponds to a window
		/// </summary>
		/// <param name="hWnd">Handle that should correspond to a window</param>
		[DllImport("user32.dll")]
		public static extern bool IsWindow(IntPtr hWnd);

		#endregion

		public IntPtr WindowHandle;
		private RECT ScreenBounds;
		/// <summary>
		/// Retrieves the current screen that contains the largest
		/// portion of the window.
		/// </summary>
		public Screen CurrentScreen {
			get {
				return Screen.FromHandle(WindowHandle);
			}
		}

		private static Dictionary<IntPtr, RECT> VerticalStretches;
		private static Dictionary<IntPtr, RECT> HorizontalStretches;
		private static Dictionary<IntPtr, DockInfo> Docks;
		private static List<Window> VisibleWindows;

		static Window() {
			VerticalStretches = new Dictionary<IntPtr, RECT>();
			HorizontalStretches = new Dictionary<IntPtr, RECT>();
			Docks = new Dictionary<IntPtr, DockInfo>();
			VisibleWindows = new List<Window>();
		}
		/// <summary>
		/// The active foreground window
		/// </summary>
		public static Window ForeGroundWindow { get { return new Window(GetForegroundWindow()); } }
		/// <summary>
		/// List of all visible windows
		/// </summary>
		public static List<Window> AllVisibleWindows {
			get {
				VisibleWindows.Clear();
				EnumWindows(new EnumWindowsDelagate(EnumWindowsCallBack), IntPtr.Zero);
				return VisibleWindows;
			}
		}
		/// <summary>
		/// This is the callback method for the EnumWindows command
		/// </summary>
		/// <param name="hWnd">The window in the enumeration</param>
		/// <param name="lParam">Parameters set from initial call. In this case it's IntPtr.Zero</param>
		private static bool EnumWindowsCallBack(IntPtr hWnd, int lParam) {
			//-- Make sure the window is visible before adding it to the collection
			if (IsWindowVisible(hWnd)) {
				VisibleWindows.Add(new Window(hWnd));
			}
			return true; //-- Continue Enumeration
		}
		/// <summary>
		/// This method will retrieve the path of the process based on the 
		/// handle to it's window.
		/// </summary>
		public static string PathFromWindowHandle(IntPtr hwnd) {
			uint dwProcessId;
			GetWindowThreadProcessId(hwnd, out dwProcessId);
			IntPtr hProcess = OpenProcess(VMRead | QueryInformation, false, dwProcessId);
			StringBuilder path = new StringBuilder(1024);
			GetModuleFileNameEx(hProcess, IntPtr.Zero, path, 1024);
			CloseHandle(hProcess);
			return path.ToString();
		}

		public string Title {
			get {
				int length = GetWindowTextLength(WindowHandle);
				StringBuilder sb = new StringBuilder(length + 1);
				GetWindowText(WindowHandle, sb, sb.Capacity);
				return sb.ToString();
			}
		}

		public string ExecutiblePath {
			get { return PathFromWindowHandle(WindowHandle); }
		}

		public Window() {
			WindowHandle = IntPtr.Zero;
			ScreenBounds = new RECT();
		}

		public Window(IntPtr handle) {
			if (IsWindow(handle)) {
				WindowHandle = handle;
				ScreenBounds = new RECT();
				GetWindowRect(handle, ref ScreenBounds);
			} else {
				throw new ArgumentException("Handle does not corrspond to a window");
			}
		}

		/// <summary>
		/// This method will move a window from it's current screen
		/// and place it into the toScr. The window is repositioned such
		/// that the ratio of the space around it is relative to the
		/// new screen.
		/// <param name="preserveSize">When set to true, the window will be
		/// relocated to the next screen then it's size will be reset to
		/// it's original size based from the Top Left relocated corner</param>
		/// </summary>
		public void MoveToScreen(Screen toScr, bool preserveSize, bool keepInBounds) {

			//-- Get the current state of the window
			WINDOWPLACEMENT wp = GetWindowPlacement();
			uint windowStateBeforeMove = wp.showCmd;

			//-- Place window in normal mode if it's currently maximized
			if (windowStateBeforeMove == (uint)WindowState.Maximized) {
				wp.showCmd = (uint)WindowState.Normal;
				SetWindowPlacement(wp);
			}

			//-- Initialize the Screen bounds of our window
			GetWindowRect(WindowHandle, ref ScreenBounds);

			//-- Grab the Minimum & Maximum Size of the Window

			//-- If the Maximum Size is Larger then the toScreen size
			//or if the Minimum size is la

			//-- Relocate the window to the other screen
			if (keepInBounds)
				ScreenBounds.RelocateInBounds(
					new RECT(CurrentScreen.WorkingArea), new RECT(toScr.WorkingArea), preserveSize);
			else
				ScreenBounds.Relocate(
					new RECT(CurrentScreen.WorkingArea), new RECT(toScr.WorkingArea), preserveSize);

			//-- Move & ReDraw the window
			MoveWindow(WindowHandle,
				ScreenBounds.Left, ScreenBounds.Top, ScreenBounds.Width, ScreenBounds.Height, true);

			//-- If we started off maximized, let's finish that way
			if (windowStateBeforeMove == (uint)WindowState.Maximized) {

				//GetClientRect(WindowHandle, ref ClientBounds);
				GetWindowRect(WindowHandle, ref ScreenBounds);

				//-- Set the windows normal position to exist on the new screen
				// that way if the window is restored, it will be restored on the new screen
				// not the old screen
				wp.rcNormalPosition = ScreenBounds;
				//-- Set the showCmd to Maximized as that was the state of the window before the move.
				wp.showCmd = (uint)WindowState.Maximized;
				//-- Set the new window placement.
				SetWindowPlacement(wp);
			}
		}

		/// <summary>
		/// This method will save the windows' current location, then 
		/// stretch the window horizontally. When called again for the
		/// same window, it will restore the window to its' previous size
		/// and location before the stretch.
		/// </summary>
		public void StretchHorizontally() {
			//-- Get Current Window Placement
			WINDOWPLACEMENT wp = GetWindowPlacement();
			GetWindowRect(WindowHandle, ref ScreenBounds);

			//-- First make sure the window is in Normal Mode
			if (wp.showCmd == (uint)WindowState.Normal) {
				//-- Check if we're stretched
				RECT curScreen = new RECT(CurrentScreen.WorkingArea);
				if (ScreenBounds.Width == curScreen.Width) { //-- If we're stretched, restore to 2/3
					//-- Check if we stored the position before the stretch
					if (HorizontalStretches.ContainsKey(WindowHandle)) {
						ScreenBounds = HorizontalStretches[WindowHandle];
						//-- Remove the key
						HorizontalStretches.Remove(WindowHandle);
					} else { //-- Set to default width of 2/3 screen width and center the window
						ScreenBounds.Left = curScreen.Left + (curScreen.Width / 6);
						ScreenBounds.Right = ScreenBounds.Left + (curScreen.Width / 3 * 2);
					}
				} else { //-- Stretch
					//-- Store Old Position
					HorizontalStretches[WindowHandle] = ScreenBounds;

					ScreenBounds.Left = curScreen.Left;
					ScreenBounds.Right = curScreen.Right;
				}
				//-- RePosition the window
				MoveWindow(WindowHandle,
					ScreenBounds.Left, ScreenBounds.Top, ScreenBounds.Width, ScreenBounds.Height, true);
			}
		}

		/// <summary>
		/// This method will save the windows' current location, then
		/// stretch the window vertically. When called again for the same
		/// window, it will restore the window to its' previous size
		/// and location before the stretch
		/// </summary>
		public void StretchVertically() {
			//-- Get Current Window Placement
			WINDOWPLACEMENT wp = GetWindowPlacement();
			GetWindowRect(WindowHandle, ref ScreenBounds);

			//-- First make sure the window is in Normal Mode
			if (wp.showCmd == (uint)WindowState.Normal) {
				RECT curScreen = new RECT(CurrentScreen.WorkingArea);

				//-- Check if we're stretched
				if (wp.rcNormalPosition.Height == curScreen.Height) { //-- If we're stretched, restore to 2/3
					//-- check if we stored the position before the stretch
					if (VerticalStretches.ContainsKey(WindowHandle)) {
						ScreenBounds = VerticalStretches[WindowHandle];
						VerticalStretches.Remove(WindowHandle);
					} else { //-- Set to default height of 2/3 screen and center window
						ScreenBounds.Top = curScreen.Top + (curScreen.Height / 6);
						ScreenBounds.Bottom = ScreenBounds.Top + (curScreen.Height / 3 * 2);
					}
				} else { //-- Stretch
					//-- Store Old Position
					VerticalStretches[WindowHandle] = ScreenBounds;

					ScreenBounds.Top = curScreen.Top;
					ScreenBounds.Bottom = curScreen.Bottom;
				}
				//-- RePosition the window
				MoveWindow(WindowHandle,
					ScreenBounds.Left, ScreenBounds.Top, ScreenBounds.Width, ScreenBounds.Height, true);
			}
		}

		/// <summary>
		/// Docks the window on the current screen. 
		/// </summary>
		/// <param name="ds">Position to dock window. Fill will center a window.</param>
		/// <param name="percentage">Percentage of screen window will take up after dock</param>
		public void Dock(DockStyle ds, double percentage) {
			Dock(CurrentScreen, ds, percentage);
		}

		/// <summary>
		/// Docks a window to a particular screen.
		/// </summary>
		/// <param name="scr">Screen to dock</param>
		/// <param name="ds">Position to dock window. Fill will center a window.</param>
		/// <param name="percentage">Percentage of screen window will take up after dock</param>
		public void Dock(Screen scr, DockStyle ds, double percentage) {
			WINDOWPLACEMENT wp = GetWindowPlacement();
			RECT r = wp.rcNormalPosition;
			RECT wa = new RECT(scr.WorkingArea);

			//-- Save location before dock
			Docks.Add(WindowHandle, new DockInfo(wp, ds));

			r = RECT.GetDockedRECT(wa, ds, percentage);
			wp.showCmd = (uint)WindowState.Normal;
			SetWindowPlacement(wp); // -- Put the window into Normal State
			MoveWindow(WindowHandle, r.Left, r.Top, r.Width, r.Height, true);
		}

		/// <summary>
		/// If a window has been docked, this will restore the window to its' position
		/// before the dock.
		/// </summary>
		public void UnDock() {
			if (Docks.ContainsKey(WindowHandle)) {
				SetWindowState(WindowState.Hide);
				WINDOWPLACEMENT wp = Docks[WindowHandle].WindowPlacement;
				SetWindowPlacement(wp);
				Docks.Remove(WindowHandle);
			}
		}

		/// <summary>
		/// Returns true if a window is currently docked.
		/// </summary>
		public bool IsDocked { get { return Docks.ContainsKey(WindowHandle); } }

		public DockStyle CurrentDockPosition {
			get {
				if (Docks.ContainsKey(WindowHandle))
					return Docks[WindowHandle].DockStyle;
				else
					return DockStyle.None;
			}
		}

		public RECT CurrentPosition {
			get {
				RECT r = new RECT();
				GetWindowRect(WindowHandle, ref r);
				return r;
			}
		}

		/// <summary>
		/// Hides the window and activates another window.
		/// <para>The window will no longer be visible in the taskbar if
		/// it was visible before the operation</para>
		/// </summary>
		/// <returns>True if hide was succesfull</returns>
		public bool Hide() {
			WINDOWPLACEMENT wp = GetWindowPlacement();
			wp.showCmd = (uint)ShowCMD.SW_HIDE;
			return SetWindowPlacement(wp);
		}

		/// <summary>
		/// Minimizes the window.
		/// </summary>
		/// <returns>True if the window was minimized</returns>
		public bool Minimize(bool remainActive) {
			if (remainActive)
				return SetWindowState(WindowState.Minimized);

			WINDOWPLACEMENT wp = GetWindowPlacement();
			wp.showCmd = (uint)ShowCMD.SW_MINIMIZE;
			return SetWindowPlacement(wp);
		}
		/// <summary>
		/// Maximizes the window.
		/// </summary>
		/// <returns>True if the window was maximized</returns>
		public bool Maximize() {
			return SetWindowState(WindowState.Maximized);
		}
		/// <summary>
		/// Move the window to it's normal location and size.
		/// </summary>
		/// <returns>True if the window was restored</returns>
		public bool Restore() {
			return SetWindowState(WindowState.Normal);
		}

		/// <summary>
		/// This method will make the window completely invisible.
		/// </summary>
		/// <returns>True if change was successfull</returns>
		public bool MakeInvisible() {
			return ChangeOpacity(0);
		}

		/// <summary>
		/// This method will make the window completely opaque.
		/// </summary>
		/// <returns>True if change was successfull</returns>
		public bool MakeOpaque() {
			return ChangeOpacity(1);
		}

		/// <summary>
		/// Sets the current window as the active foreground window
		/// </summary>
		/// <returns>True if the window was brought to the foreground</returns>
		public bool SetAsForegroundWindow() {
			return SetForegroundWindow(WindowHandle);
		}

		/// <summary>
		/// This method will modify the bAlpha channel by adding / subtracting the
		/// percentage of 255 to it's current value. This will effectively
		/// increase or decrease the windows opacity. 
		/// </summary>
		public bool ChangeOpacity(double percentage) {
			uint crKey;
			byte bAlpha;
			uint dwFlags;
			int GWL_EXSTYLE = WindowStyles.GWL_EXSTYLE;
			uint WS_EX_LAYERED = WindowStyles.WS_EX_LAYERED;
			uint LWA_ALPHA = 0x2;

			int Win = GetWindowLong(WindowHandle, GWL_EXSTYLE);

			//-- Get Current Values
			GetLayeredWindowAttributes(WindowHandle, out crKey, out bAlpha, out dwFlags);

			//-- Extend the window style to a Layered Window if it's not already layered
			if ((Win & WS_EX_LAYERED) != WS_EX_LAYERED) {
				SetWindowLong(WindowHandle,
					WindowStyles.GWL_EXSTYLE, (int)WS_EX_LAYERED);
				bAlpha = 255;
			}

			//-- Generate value to modify by
			byte val;
			bool increase = percentage > 0;
			percentage = Math.Abs(percentage);
			if (percentage < 1 && percentage > 0)
				val = (byte)(percentage * 255);
			else if (percentage == 1) //-- Set the window to Opaqe
				return SetLayeredWindowAttributes(WindowHandle, 0, 255, LWA_ALPHA);
			else if (percentage == 0) //-- Set the window to Invisible
				return SetLayeredWindowAttributes(WindowHandle, 0, 0, LWA_ALPHA);
			else //-- Default value
				val = 10;

			//-- Modify bAlpha, making sure it's within the bounds
			if (increase) {
				if ((bAlpha + val) > 255)
					bAlpha = 255;
				else
					bAlpha += val;
			} else {
				if ((bAlpha - val) < 0)
					bAlpha = 0;
				else
					bAlpha -= val;
			}

			//-- Set Layred Attributes
			return SetLayeredWindowAttributes(WindowHandle, 0, bAlpha, LWA_ALPHA);
		}

		/// <summary>
		/// Gets the window info of the current window
		/// </summary>
		private WINDOWINFO GetWindowInfo() {
			WINDOWINFO w = WINDOWINFO.Default;
			GetWindowInfo(WindowHandle, ref w);
			return w;
		}

		/// <summary>
		/// Gets the window placement of the current window
		/// </summary>
		public WINDOWPLACEMENT GetWindowPlacement() {
			WINDOWPLACEMENT w = WINDOWPLACEMENT.Default;
			GetWindowPlacement(WindowHandle, ref w);
			return w;
		}

		/// <summary>
		/// Returns the current state of the window
		/// </summary>
		/// <returns></returns>
		public WindowState State {
			get {
				switch (GetWindowPlacement().showCmd) {
					case (uint)WindowState.Maximized:
						return WindowState.Maximized;
					case (uint)WindowState.Minimized:
						return WindowState.Minimized;
					case (uint)WindowState.Normal:
						return WindowState.Normal;
					default:
						return WindowState.Normal;
				}
			}
		}

		public void SetWindowLocation(int left, int top, int right, int bottom) {
			int height = bottom - top;
			int width = right - left;
			MoveWindow(WindowHandle, left, top, width, height, true);
		}

		/// <summary>
		/// Sets the placement of the current window
		/// </summary>
		/// <param name="wp">Info about the new placement of the window</param>
		/// <returns>True if the windows placement has been set</returns>
		public bool SetWindowPlacement(WINDOWPLACEMENT wp) {
			return SetWindowPlacement(WindowHandle, ref wp);
		}

		/// <summary>
		/// Sets the showCmd of the current window. This is used
		/// to Minimize / Maximize / Restore the window.
		/// </summary>
		/// <param name="showCmd">Desired state of the window</param>
		/// <returns>True if the state has been set</returns>
		public bool SetWindowState(WindowState showCmd) {
			WINDOWPLACEMENT wp = GetWindowPlacement();
			wp.showCmd = (uint)showCmd;
			return SetWindowPlacement(wp);
		}

		#region Helper Structs

		private struct DockInfo {
			public WINDOWPLACEMENT WindowPlacement;
			public DockStyle DockStyle;
			public DockInfo(WINDOWPLACEMENT wp, DockStyle ds) {
				WindowPlacement = wp;
				DockStyle = ds;
			}
		}

		/// <summary>
		/// Values of all of the set window position flags.
		/// </summary>
		private struct SWP_FLAGS {
			/// <summary>
			/// If the calling thread and the thread that owns the window are attached to different input queues, 
			/// the system posts the request to the thread that owns the window. 
			/// This prevents the calling thread from blocking its execution while other threads process the request. 
			/// </summary>
			public const int SWP_ASYNCWINDOWPOS = 0x4000;
			/// <summary>
			/// Prevents generation of the WM_SYNCPAINT message. 
			/// </summary>
			public const int SWP_DEFERERASE = 0x2000;
			/// <summary>
			/// Draws a frame (defined in the window's class description) around the window.
			/// </summary>
			public const int SWP_DRAWFRAME = 0x0020;
			/// <summary>
			/// Applies new frame styles set using the SetWindowLong function. 
			/// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. 
			/// If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
			/// </summary>
			public const int SWP_FRAMECHANGED = 0x0020;
			/// <summary>
			/// Hides the window.
			/// </summary>
			public const int SWP_HIDEWINDOW = 0x0080;
			/// <summary>
			/// Does not activate the window. If this flag is not set, the window is activated and moved to the 
			/// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
			/// </summary>
			public const int SWP_NOACTIVATE = 0x0010;
			/// <summary>
			/// Discards the entire contents of the client area. 
			/// If this flag is not specified, the valid contents of the client area are saved and copied back into the 
			/// client area after the window is sized or repositioned.
			/// </summary>
			public const int SWP_NOCOPYBITS = 0x0100;
			/// <summary>
			/// Retains the current position (ignores X and Y parameters).
			/// </summary>
			public const int SWP_NOMOVE = 0x0002;
			/// <summary>
			/// Does not change the owner window's position in the Z order.
			/// </summary>
			public const int SWP_NOOWNERZORDER = 0x0200;
			/// <summary>
			/// Does not redraw changes. If this flag is set, no repainting of any kind occurs. 
			/// This applies to the client area, the nonclient area (including the title bar and scroll bars), 
			/// and any part of the parent window uncovered as a result of the window being moved. 
			/// When this flag is set, the application must explicitly invalidate or redraw any parts of the window and 
			/// parent window that need redrawing.
			/// </summary>
			public const int SWP_NOREDRAW = 0x0008;
			/// <summary>
			/// Same as the SWP_NOOWNERZORDER flag.
			/// </summary>
			public const int SWP_NOREPOSITION = 0x0200;
			/// <summary>
			/// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
			/// </summary>
			public const int SWP_NOSENDCHANGING = 0x0400;
			/// <summary>
			/// Retains the current size (ignores the cx and cy parameters).
			/// </summary>
			public const int SWP_NOSIZE = 0x0001;
			/// <summary>
			/// Retains the current Z order (ignores the hWndInsertAfter parameter).
			/// </summary>
			public const int SWP_NOZORDER = 0x0004;
			/// <summary>
			/// Displays the window.
			/// </summary>
			public const int SWP_SHOWWINDOW = 0x0040;


			/// <summary>
			/// Places the window at the top of the Z order.
			/// </summary>
			public const int HWND_TOP = 0;
			/// <summary>
			/// Places the window at the bottom of the Z order. 
			/// If the hWnd parameter identifies a topmost window, the window loses its 
			/// topmost status and is placed at the bottom of all other windows.
			/// </summary>
			public const int HWND_BOTTOM = 1;
			/// <summary>
			/// Places the window above all non-topmost windows. 
			/// The window maintains its topmost position even when it is deactivated.
			/// </summary>
			public const int HWND_TOPMOST = -1;
			/// <summary>
			/// Places the window above all non-topmost windows (that is, behind all topmost windows). 
			/// This flag has no effect if the window is already a non-topmost window.
			/// </summary>
			public const int HWND_NOTOPMOST = -2;
		}

		/// <summary>
		/// Simple List to all constants.
		/// </summary>
		private struct WindowStyles {
			public const int GWL_ID = (-12);
			public const int GWL_STYLE = (-16);
			public const int GWL_EXSTYLE = (-20);

			// Window Styles
			public const UInt32 WS_OVERLAPPED = 0;
			public const UInt32 WS_POPUP = 0x80000000;
			public const UInt32 WS_CHILD = 0x40000000;
			public const UInt32 WS_MINIMIZE = 0x20000000;
			public const UInt32 WS_VISIBLE = 0x10000000;
			public const UInt32 WS_DISABLED = 0x8000000;
			public const UInt32 WS_CLIPSIBLINGS = 0x4000000;
			public const UInt32 WS_CLIPCHILDREN = 0x2000000;
			public const UInt32 WS_MAXIMIZE = 0x1000000;
			public const UInt32 WS_CAPTION = 0xC00000;      // WS_BORDER or WS_DLGFRAME  
			public const UInt32 WS_BORDER = 0x800000;
			public const UInt32 WS_DLGFRAME = 0x400000;
			public const UInt32 WS_VSCROLL = 0x200000;
			public const UInt32 WS_HSCROLL = 0x100000;
			public const UInt32 WS_SYSMENU = 0x80000;
			public const UInt32 WS_THICKFRAME = 0x40000;
			public const UInt32 WS_GROUP = 0x20000;
			public const UInt32 WS_TABSTOP = 0x10000;
			public const UInt32 WS_MINIMIZEBOX = 0x20000;
			public const UInt32 WS_MAXIMIZEBOX = 0x10000;
			public const UInt32 WS_TILED = WS_OVERLAPPED;
			public const UInt32 WS_ICONIC = WS_MINIMIZE;
			public const UInt32 WS_SIZEBOX = WS_THICKFRAME;

			// Extended Window Styles
			public const UInt32 WS_EX_DLGMODALFRAME = 0x0001;
			public const UInt32 WS_EX_NOPARENTNOTIFY = 0x0004;
			public const UInt32 WS_EX_TOPMOST = 0x0008;
			public const UInt32 WS_EX_ACCEPTFILES = 0x0010;
			public const UInt32 WS_EX_TRANSPARENT = 0x0020;
			public const UInt32 WS_EX_MDICHILD = 0x0040;
			public const UInt32 WS_EX_TOOLWINDOW = 0x0080;
			public const UInt32 WS_EX_WINDOWEDGE = 0x0100;
			public const UInt32 WS_EX_CLIENTEDGE = 0x0200;
			public const UInt32 WS_EX_CONTEXTHELP = 0x0400;
			public const UInt32 WS_EX_RIGHT = 0x1000;
			public const UInt32 WS_EX_LEFT = 0x0000;
			public const UInt32 WS_EX_RTLREADING = 0x2000;
			public const UInt32 WS_EX_LTRREADING = 0x0000;
			public const UInt32 WS_EX_LEFTSCROLLBAR = 0x4000;
			public const UInt32 WS_EX_RIGHTSCROLLBAR = 0x0000;
			public const UInt32 WS_EX_CONTROLPARENT = 0x10000;
			public const UInt32 WS_EX_STATICEDGE = 0x20000;
			public const UInt32 WS_EX_APPWINDOW = 0x40000;
			public const UInt32 WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
			public const UInt32 WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
			public const UInt32 WS_EX_LAYERED = 0x00080000;
			public const UInt32 WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
			public const UInt32 WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
			public const UInt32 WS_EX_COMPOSITED = 0x02000000;
			public const UInt32 WS_EX_NOACTIVATE = 0x08000000;
		}

		/// <summary>
		/// Defines the WINDOWINFO Structure
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		private struct WINDOWINFO {
			public uint cbSize;
			public RECT rcWindow;
			public RECT rcClient;
			public uint dwStyle;
			public uint dwExStyle;
			public uint dwWindowStatus;
			public uint cxWindowBorders;
			public uint cyWindowBorders;
			public ushort atomWindowType;
			public ushort wCreatorVersion;

			public static WINDOWINFO Default {
				get {
					WINDOWINFO w = new WINDOWINFO();
					w.cbSize = (uint)Marshal.SizeOf(w);
					return w;
				}
			}
		}

		/// <summary>
		/// Defines the WINDOWPLACMENT Structure
		/// </summary>
		[Serializable()]
		[StructLayout(LayoutKind.Sequential)]
		public struct WINDOWPLACEMENT {
			/// <summary>
			/// The length of the structure, in bytes.
			/// </summary>
			public uint length;
			/// <summary>
			/// The flags that control the position of the minimized window and the 
			/// method by which the window is restored. This member can be one or 
			/// more of the following values.<para>
			/// WPF_ASYNCWINDOWPLACEMENT [0x0004] - If the calling thread and the 
			/// thread that owns the window are attached to different input queues, 
			/// the system posts the request to the thread that owns the window. 
			/// This prevents the calling thread from blocking its execution while 
			/// other threads process the request.</para><para>
			/// WPF_RESTORETOMAXIMIZED [0x0002] - The restored window will be maximized, 
			/// regardless of whether it was maximized before it was minimized. 
			/// This setting is only valid the next time the window is restored. 
			/// It does not change the default restoration behavior. This flag is 
			/// only valid when the SW_SHOWMINIMIZED value is specified for the 
			/// showCmd member.</para><para>
			/// WPF_SETMINPOSITION [0x0001] - The coordinates of the minimized window 
			/// may be specified. This flag must be specified if the coordinates are 
			/// set in the ptMinPosition member.</para>
			/// </summary>
			public uint flags;
			/// <summary>
			/// The current show state of the window. This member can be one of the 
			/// following values.<para>
			/// SW_HIDE [0] - Hides the window and activates another window.</para><para>
			/// SW_MAXIMIZE [3] - Maximizes the specified window.</para><para>
			/// SW_MINIMIZE [6] - Minimizes the specified window and activates the 
			/// next top-level window in the z-order.</para><para>
			/// SW_RESTORE [9] - Activates and displays the window. If the window is 
			/// minimized or maximized, the system restores it to its original size 
			/// and position. An application should specify this flag when restoring 
			/// a minimized window.</para><para>
			/// SW_SHOW [5] - Activates the window and displays it in its current size and position.</para><para>
			/// SW_SHOWMINIMIZED [2] - Activates the window and displays it as a 
			/// minimized window.</para><para>
			/// SW_SHOWMINNOACTIVE [7] - Displays the window as a minimized window. 
			/// This value is similar to SW_SHOWMINIMIZED, except the window is not activated.</para><para>
			/// SW_SHOWNA [8] - Displays the window in its current size and position. 
			/// This value is similar to SW_SHOW, except the window is not activated.</para><para>
			/// SW_SHOWNOACTIVATE [4] - Displays a window in its most recent size and
			/// position. This value is similar to SW_SHOWNORMAL, except the window is not activated.</para>
			/// SW_SHOWNORMAL [1] - Activates and displays a window. If the window is
			/// minimized or maximized, the system restores it to its original size 
			/// and position. An application should specify this flag when displaying
			/// the window for the first time.
			/// </summary>
			public uint showCmd;
			/// <summary>
			/// The coordinates of the window's upper-left corner when the window is minimized.
			/// </summary>
			public POINT ptMinPosition;
			/// <summary>
			/// The coordinates of the window's upper-left corner when the window is maximized.
			/// </summary>
			public POINT ptMaxPosition;
			/// <summary>
			/// The window's coordinates when the window is in the restored position.
			/// </summary>
			public RECT rcNormalPosition;

			public static WINDOWPLACEMENT Default {
				get {
					WINDOWPLACEMENT w = new WINDOWPLACEMENT();
					w.length = (uint)Marshal.SizeOf(w);
					return w;
				}
			}
		}

		private enum ShowCMD : uint {
			/// <summary>
			/// Hides the window and activates another window.
			/// </summary>
			SW_HIDE = 0,
			/// <summary>
			/// Activates and displays a window. 
			/// If the window is minimized or maximized, the system restores 
			/// it to the original size and position. 
			/// An application specifies this flag when displaying the window for the first time.
			/// </summary>
			SW_NORMAL = 1,
			/// <summary>
			/// Activates the window, and displays it as a minimized window.
			/// </summary>
			SW_SHOWMINIMIZED = 2,
			/// <summary>
			/// Activates the window, and displays it as a maximized window.
			/// </summary>
			SW_SHOWMAXIMIZED = 3,
			/// <summary>
			/// Displays a window in its most recent size and position. 
			/// This value is similar to SW_SHOWNORMAL, except that the window is not activated.
			/// </summary>
			SW_SHOWNOACTIVATE = 4,
			/// <summary>
			/// Activates the window, and displays it at the current size and position.
			/// </summary>
			SW_SHOW = 5,
			/// <summary>
			/// Minimizes the specified window, and activates the next top-level window in the Z order.
			/// </summary>
			SW_MINIMIZE = 6,
			/// <summary>
			/// Displays the window as a minimized window. 
			/// This value is similar to SW_SHOWMINIMZED, except that the window is not activated.
			/// </summary>
			SW_SHOWMINNOACTIVE = 7,
			/// <summary>
			/// Displays the window at the current size and position. 
			/// This value is similar to SW_SHOW, except that the window is not activated.
			/// </summary>
			SW_SHOWNA = 8,
			/// <summary>
			/// Activates and displays the window. If the window is minimized or maximized, 
			/// the system restores it to the original size and position. 
			/// An application specifies this flag when restoring a minimized window.
			/// </summary>
			SW_RESTORE = 9,
			/// <summary>
			/// Sets the show state based on the SW_ value that is specified in the STARTUPINFO structure 
			/// passed to the CreateProcess function by the program that starts the application.
			/// </summary>
			SW_SHOWDEFAULT = 10,
			/// <summary>
			/// Windows Server 2003, Windows 2000, and Windows XP:  
			/// Minimizes a window, even when the thread that owns the window is hung. 
			/// Only use this flag when minimizing windows from a different thread.
			/// </summary>
			SW_FORCEMINIMIZE = 11
		}
		#endregion
	}

	/// <summary>
	/// Values of showCmd field inside of WindowPlacement.
	/// Lets you know the current 'State' of the window
	/// </summary>
	public enum WindowState : uint {
		/// <summary>
		/// Hides the window.
		/// <para>Note: The window will not be shown in the taskbar.</para><para>
		/// 'SW_HIDE'</para>
		/// </summary>
		Hide = 0,
		/// <summary>
		/// Shows the window in its regular (non-maximized) position.<para>
		///'SW_SHOWNORMAL'</para>
		/// </summary>
		Normal = 1,
		/// <summary>
		/// Window is in the minimized state, only visible in taskbar.<para>
		/// 'SW_SHOWMINIMIZED'</para>
		/// </summary>
		Minimized = 2,
		/// <summary>
		/// Window is maximized, taking up the entire working area of the screen.<para>
		/// 'SW_MAXIMIZE' / 'SW_SHOWMAXIMIZED'</para>
		/// </summary>
		Maximized = 3
	}
}
