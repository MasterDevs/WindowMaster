using System;
using System.Runtime.InteropServices;

namespace WindowMasterLib.Util {
	
	public class Rectangle{

		[Serializable, StructLayout(LayoutKind.Sequential)]
		public struct RECT {
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;

			public RECT(int left_, int top_, int right_, int bottom_) {
				Left = left_;
				Top = top_;
				Right = right_;
				Bottom = bottom_;
			}

			public RECT(System.Drawing.Rectangle r) {
				Left = r.Left;
				Top = r.Top;
				Right = r.Right;
				Bottom = r.Bottom;
			}

			public int Height { get { return Bottom - Top; } }
			public int Width { get { return Right - Left; } }
			public POINT Location { get { return new POINT(Left, Top); } }

			public override int GetHashCode() {
				return Left ^ ((Top << 13) | (Top >> 0x13))
					^ ((Width << 0x1a) | (Width >> 6))
					^ ((Height << 7) | (Height >> 0x19));
			}
		}

		[StructLayout(LayoutKind.Sequential)]
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


		private RECT rect;
		public Rectangle() { rect = new RECT(); }
		public Rectangle(System.Drawing.Rectangle r) {
			rect = new RECT();
			rect.Bottom = r.Bottom;
			rect.Top = r.Top;
			rect.Left = r.Left;
			rect.Right = r.Right; 
		}

		/// <summary>
		/// This method will determine if the parameter is
		/// inside of this rectangle
		/// </summary>
		public bool IsInside(RECT r) {

			bool left = r.Left >= rect.Left;
			bool right = r.Right <= rect.Right;
			bool top = r.Top >= rect.Top;
			bool bottom = r.Bottom <= rect.Bottom;

			//-- Simple Case -- r is full inside this instance
			if (left && right && top && bottom)
				return true;

			if (left && right && top) {
				return (r.Bottom - (r.Height / 2)) <= rect.Bottom;
			} else if (left && right && bottom) {
				return (r.Top + (r.Height / 2)) >= rect.Top;
			} else if (bottom && top && left) {
				return (r.Right - (r.Width / 2)) <= rect.Right;
			} else
				return (r.Left + (r.Width / 2)) >= rect.Left;

		}

		/// <summary>
		/// Returns a new RECT that will have the same dimensions
		/// as the parameter r but have it's top left corner at
		/// parameter lcoation
		/// <remarks>This doesn't actually alter the parameter r. It
		/// simply creates a new rectangle that would represent what r would
		/// look like if it was moved to the new location</remarks>
		/// </summary>
		/// <param name="r">The rectangle you'd like to move</param>
		/// <param name="location">The new top left location for the rectangle</param>
		public static RECT MoveToLocation(RECT r, POINT location) {
			return new RECT(
				location.X, location.Y, 
				location.X + r.Width,
				location.Y + r.Height);
		}

		public static RECT Translate(RECT r, RECT from, RECT to) {
			int width = (r.Width * to.Width) / from.Width;
			int height = (r.Height * to.Height) / from.Height;
			int left = r.Left + (to.Left - from.Left);
			int top = r.Top + (to.Top - from.Top);
			return new RECT(left, top, left + width, top + height);
		}

		public static RECT Slide(RECT r, RECT from, RECT to) {
			int left = r.Left + (to.Left - from.Left);
			int top = r.Top + (to.Top - from.Top);
			return new RECT(left, top, left + r.Width, top + r.Height);
		}

		public static RECT SlideInBounds(RECT r, RECT from, RECT to) {
			int left = r.Left + (to.Left - from.Left);
			int top = r.Top + (to.Top - from.Top);
			RECT nr = new RECT(left, top, left + r.Width, top + r.Height);
			
			//-- Make sure new rectangle is not larger then the box it's going to
			if (r.Width > to.Width)
				nr.Right = ((r.Width * to.Width) / from.Width) + nr.Left;
			if (r.Height > to.Height)
				nr.Bottom = ((r.Height * to.Height) / from.Height) + nr.Top;

			//-- Make sure the new rectnangle is inside the box it's going to
			//-- nr is hanging over right or the left
			if ((nr.Right > to.Right) || (nr.Left < to.Left)) {
				nr.Left = to.Left + ((to.Width - nr.Width) / 2);
				nr.Right = nr.Left + nr.Width;
			}
			//-- nr is haning over the bottom or the top
			if ((nr.Top < to.Top) || (nr.Bottom > to.Bottom)) {
				nr.Top = to.Top + ((to.Height - nr.Height) / 2);
				nr.Bottom = nr.Top + nr.Height;
			}

			return nr;
		}
	}
}
