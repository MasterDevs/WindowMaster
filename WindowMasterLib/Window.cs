using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowMasterLib.Util;

namespace WindowMasterLib {
	public class Window {

		#region User32.dll Methods

		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern bool GetClientRect(IntPtr hWnd, ref RECT rect);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern bool ScreenToClient(IntPtr hwnd, ref POINT lpPoint);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern bool ScreenToClient(ref POINT lpPoint);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern bool ClientToScreen(IntPtr hwnd, ref POINT lpPoint);

		[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		internal static extern bool ClientToScreen(ref POINT lpPoint);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

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

		#endregion

		private IntPtr WindowHandle;
		private RECT ClientBounds;
		private RECT ScreenBounds;

		private static LoopList<Screen> Screens;

		static Window() {
			Screens = new LoopList<Screen>(Screen.AllScreens.Length);
			foreach (Screen s in Screen.AllScreens) {
				Screens.Add(s);
			}
		}

		public static Window ForeGroundWindow { get { return new Window(GetForegroundWindow()); } }

		public Window() {
			WindowHandle = IntPtr.Zero;
			ClientBounds = new RECT();
			ScreenBounds = new RECT();
		}

		public Window(IntPtr handle) {
			WindowHandle = handle;
			ClientBounds = new RECT();
			ScreenBounds = new RECT();
			GetClientRect(handle, ref ClientBounds);
			GetWindowRect(handle, ref ScreenBounds);
		}

		/// <summary>
		/// Sets WindowHandle to the ForeGround window and initializes
		/// ClientBounds & ScreenBounds for the window.
		/// </summary>
		public void GetCurrentForegroundWindow() {
			WindowHandle = GetForegroundWindow();
			GetClientRect(WindowHandle, ref ClientBounds);
			GetWindowRect(WindowHandle, ref ScreenBounds);
		}

		/// <summary>
		/// This method will move a window from it's current screen
		/// and place it into the toScr. The window is resized if it's too
		/// large and the window is slid so that the entire window will be contained
		/// within the bounds of toScr.
		/// </summary>
		public void GoToScreen(Screen toScr) {

			//-- Get the screen we're currently on
			Screen curScreen = Screens[GetScreenIndex(WindowHandle)];

			//-- Determine if the window is Currently Maximized
			WINDOWPLACEMENT wp = GetWindowPlacement();
			uint windowState = wp.showCmd;
			bool redraw = true;

			//-- Place window in normal mode if it's currently maximized
			if (windowState == showCmd.Maximized) {
				wp.showCmd = showCmd.Normal;
				//-- USE  --> wp.rcNormalPosition instead of GetClientRect when Maximized!
				SetWindowPlacement(wp);
				redraw = false;
			}

			//-- Initialize the Client & Screen bounds of our window
			GetClientRect(WindowHandle, ref ClientBounds);
			GetWindowRect(WindowHandle, ref ScreenBounds);

			//-- Initialize a point
			POINT p = ClientBounds.Location;

			//-- Get the screen coordinates for the window
			if (ClientToScreen(WindowHandle, ref p))
				ClientBounds.MoveToLocation(p);

			//-- Save Original CoOrdinates
			int oldLeft = ClientBounds.Left;
			int oldTop = ClientBounds.Top;

			//-- Slide the window to the new screen adjusting the size & location 
			// if the window is larger then the screen.
			RECT r =
				RECT.SlideInBounds(ClientBounds,
				new RECT(curScreen.WorkingArea),
				new RECT(toScr.WorkingArea), true);

			//-- If we started off maximized, let's finish that way
			if (windowState == showCmd.Maximized) {
				//-- Place window in Normal Mode on new screen
				wp.showCmd = showCmd.Normal;
				// ??? --> wp.rcNormalPosition = RECT.Translate(r, ScreenBounds, ClientBounds);
				wp.rcNormalPosition = r;
				//wp.ptMaxPosition = new POINT(toScr.WorkingArea.X, toScr.WorkingArea.Y);
				SetWindowPlacement(wp);
				//-- Now Maximize to finish move
				wp.showCmd = showCmd.Maximized;
				SetWindowPlacement(wp);
			} else {
				//-- Get the new Rectangle in Screen Bounds
				r.Translate(ClientBounds, ScreenBounds);
				MoveWindow(WindowHandle, r.Left, r.Top, r.Width, r.Height, redraw);
			}
		}

		/// <summary>
		/// Moves the window to the next screen. 
		/// </summary>
		public void MoveToNextScreen() {
			Screens.CurrentIndex = GetScreenIndex(WindowHandle);
			GoToScreen(Screens.Next);
		}

		public bool StretchHorizontally() {
			//-- Get Current Window Placement
			WINDOWPLACEMENT wp = GetWindowPlacement();

			if (wp.showCmd == showCmd.Normal) {
				//-- Check if we're stretched
				RECT curScreen = new RECT(Screen.AllScreens[GetScreenIndex(WindowHandle)].WorkingArea);
				if (curScreen.Width <= wp.rcNormalPosition.Width) { //-- If we're stretched, restore to 2/3
					wp.rcNormalPosition.Left = curScreen.Left + (curScreen.Width / 6);
					wp.rcNormalPosition.Right = wp.rcNormalPosition.Left + (curScreen.Width / 3 * 2);
					return SetWindowPlacement(wp);
				} else { //-- Stretch
					wp.rcNormalPosition.Left = curScreen.Left;
					wp.rcNormalPosition.Right = curScreen.Right;
					return SetWindowPlacement(wp);
				}
			}
			return false;
		}

		public bool StretchVertically() {
			//-- Get Current Window Placement
			WINDOWPLACEMENT wp = GetWindowPlacement();

			//-- First make sure the window is in Normal Mode
			if (wp.showCmd == showCmd.Normal) {
				RECT curScreen = new RECT(Screen.AllScreens[GetScreenIndex(WindowHandle)].WorkingArea);
				
				//-- Check if we're stretched
				if (curScreen.Height <= wp.rcNormalPosition.Height) { //-- If we're stretched, restore to 2/3
					wp.rcNormalPosition.Top = curScreen.Top+ (curScreen.Height/ 6);
					wp.rcNormalPosition.Bottom = wp.rcNormalPosition.Top + (curScreen.Height / 3 * 2);
					return SetWindowPlacement(wp);
				} else { //-- Stretch
					wp.rcNormalPosition.Top = curScreen.Top;
					wp.rcNormalPosition.Bottom = curScreen.Bottom;
					return SetWindowPlacement(wp);
				}
			}
			return false;
		}
		
		
		/// <summary>
		/// Minimizes the window.
		/// </summary>
		/// <returns>True if the window was minimized</returns>
		public bool Minimize() {
			return SetWindowPlacement(showCmd.Minimized);
		}
		/// <summary>
		/// Maximizes the window.
		/// </summary>
		/// <returns>True if the window was maximized</returns>
		public bool Maximize() {
			return SetWindowPlacement(showCmd.Maximized);
		}
		/// <summary>
		/// Move the window to it's normal location and size.
		/// </summary>
		/// <returns>True if the window was restored</returns>
		public bool Restore() {
			return SetWindowPlacement(showCmd.Normal);
		}

		public bool IncreaseOpacity(double percentage) {
			return SetOpacity(percentage, true);
		}

		public bool DecreaseOpacity(double percentage) {
			return SetOpacity(percentage, false);
		}

		public bool MakeInvisible() {
			return SetOpacity(0, true);
		}

		public bool MakeOpaque() {
			return SetOpacity(1, true);
		}

		/// <summary>
		/// This method will modify the bAlpha channel by adding / subtracting the
		/// percentage of 255 to it's current value. This will effectively
		/// increase or decrease the windows opacity. 
		/// </summary>
		private bool SetOpacity(double percentage, bool increase) {
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
		/// Gets the index in Screen.AllScreens that contains
		/// the largest portion of the window.
		/// </summary>
		/// <param name="handle">Handle to the window</param>
		/// <returns>The index in Scree.AllScreens</returns>
		public int GetScreenIndex(IntPtr handle) {
			return Screens.IndexOf(Screen.FromHandle(handle));
		}

		/// <summary>
		/// Gets the window info of the current window
		/// </summary>
		public WINDOWINFO GetWindowInfo() {
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
		public bool SetWindowPlacement(uint showCmd) {
			WINDOWPLACEMENT wp = GetWindowPlacement();
			wp.showCmd = showCmd;
			return SetWindowPlacement(wp);
		}

		/// <summary>
		/// Values of all of the set window position flags.
		/// </summary>
		public class SWP_FLAGS {
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
	}

	/// <summary>
	/// Defines the WINDOWINFO Structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct WINDOWINFO {
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
	[StructLayout(LayoutKind.Sequential)]
	public struct WINDOWPLACEMENT {
		public uint length;
		public uint flags;
		public uint showCmd;
		public POINT ptMinPosition;
		public POINT ptMaxPosition;
		public RECT rcNormalPosition;

		public static WINDOWPLACEMENT Default {
			get {
				WINDOWPLACEMENT w = new WINDOWPLACEMENT();
				w.length = (uint)Marshal.SizeOf(w);
				return w;
			}
		}
	}

	/// <summary>
	/// Values of showCmd
	/// </summary>
	public class showCmd {
		public static readonly uint Normal = 1;
		public static readonly uint Minimized = 2;
		public static readonly uint Maximized = 3;
	}

	/// <summary>
	/// Simple List to all constants.
	/// </summary>
	public class WindowStyles {
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
}
