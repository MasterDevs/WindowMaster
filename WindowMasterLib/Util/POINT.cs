using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WindowMasterLib.Util {

	[StructLayout(LayoutKind.Sequential)]
	[DebuggerDisplay("X = {X} Y = {Y}")]
	public struct POINT {
		public int X;
		public int Y;

		public POINT(int x, int y) {
			this.X = x;
			this.Y = y;
		}

		public static implicit operator System.Drawing.Point(POINT p) {
			return new System.Drawing.Point(p.X, p.Y);
		}

		public static implicit operator POINT(System.Drawing.Point p) {
			return new POINT(p.X, p.Y);
		}
	}

}
