using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WindowMasterLib.Util {
	
	[Serializable, StructLayout(LayoutKind.Sequential)]
	[DebuggerDisplay("X = {Left} Y = {Top} Width = {Width} = Height {Height} Ratio = {Ratio}")]
	public struct RECT {
		public int Left;
		public int Top;
		public int Right;
		public int Bottom;
		private string Ratio {
			get {
				return string.Format("{0:0.000}", (double)Width / (double)Height);
			}
		}

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

		/// <summary>
		/// This method will determine if the parameter is
		/// inside of this rectangle
		/// </summary>
		public bool IsInside(RECT r) {

			bool left = r.Left >= Left;
			bool right = r.Right <= Right;
			bool top = r.Top >= Top;
			bool bottom = r.Bottom <= Bottom;

			//-- Simple Case -- r is fully inside this instance
			if (left && right && top && bottom)
				return true;

			if (left && right && top) {
				return (r.Bottom - (r.Height / 2)) <= Bottom;
			} else if (left && right && bottom) {
				return (r.Top + (r.Height / 2)) >= Top;
			} else if (bottom && top && left) {
				return (r.Right - (r.Width / 2)) <= Right;
			} else
				return (r.Left + (r.Width / 2)) >= Left;
		}

		/// <summary>
		/// This method will re-initialze all four corners of this
		/// RECT with the values of the inputted RECT
		/// </summary>
		private void ReInit(RECT r) {
			Top = r.Top; Left = r.Left; Right = r.Right; Bottom = r.Bottom;
		}

		/// <summary>
		/// Moves the current RECT instance to the location by adjusting Left and Top
		/// to equal location.X and location.Y respectively. Bottom and Right are modified
		/// to keep the RECT size in tact.
		/// </summary>
		public void MoveToLocation(POINT location) { ReInit(MoveToLocation(this, location)); }

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

		public void Translate(RECT from, RECT to) {ReInit(Translate(this, from, to));}
		
		public static RECT Translate(RECT r, RECT from, RECT to) {
			int width = (r.Width * to.Width) / from.Width;
			int height = (r.Height * to.Height) / from.Height;
			int left = r.Left + (to.Left - from.Left);
			int top = r.Top + (to.Top - from.Top);
			return new RECT(left, top, left + width, top + height);
		}
		
		public void Slide(RECT from, RECT to) {ReInit(Slide(this, from, to));}
		
		public static RECT Slide(RECT r, RECT from, RECT to) {
			int left = r.Left + (to.Left - from.Left);
			int top = r.Top + (to.Top - from.Top);
			return new RECT(left, top, left + r.Width, top + r.Height);
		}

		public void SlideInBounds(RECT from, RECT to, bool center) { 
			ReInit(SlideInBounds(this, from, to, center)); 
		}

		public static RECT SlideInBounds(RECT r, RECT from, RECT to, bool center) {
			int left = r.Left + (to.Left - from.Left);
			int top = r.Top + (to.Top - from.Top);
			int width = r.Width;
			int height = r.Height;
			
			//-- Make sure new RECT is not larger then the box it's going to
			if (width > to.Width)
				width = ((r.Width * to.Width) / from.Width);
			if (height > to.Height)
				height = ((r.Height * to.Height) / from.Height);

			int right = left + width;
			int bottom = top + height;

			if (center) {
				//-- Make sure the new RECT is inside the box it's going to
				//-- nr is hanging over right or the left
				if ((right > to.Right) || (left < to.Left)) {
					left = to.Left + ((to.Width - width) / 2);
					right = left + width;
				}
				//-- nr is haning over the bottom or the top
				if ((top < to.Top) || (bottom > to.Bottom)) {
					top = to.Top + ((to.Height - height) / 2);
					bottom = top + height;
				}
			} else {
				if (right > to.Right) {
					right = to.Right;
					left = right - width;
				}
				if (left < to.Left) {
					left = to.Left;
					right = to.Left + width;
				}
				if (top < to.Top) {
					top = to.Top;
					bottom = top + height;
				}
				if (bottom > to.Bottom) {
					bottom = to.Bottom;
					top = bottom - height;
				}
			}
			
			return new RECT(left, top, right, bottom);
		}


		public override string ToString() {
			return string.Format("{X {0} Y {1} Width {2} Height {3} }", Top, Left, Width, Height);
		}

		public override int GetHashCode() {
			return Left ^ ((Top << 13) | (Top >> 0x13))
				^ ((Width << 0x1a) | (Width >> 6))
				^ ((Height << 7) | (Height >> 0x19));
		}

		public void PlaceInisde(RECT r) {
			Left = Left < r.Left ? r.Left : Left;
			Right = Right > r.Right ? r.Right : Right;
			Top = Top < r.Top ? r.Top : Top; 
			Bottom = Bottom > r.Bottom ? r.Bottom : Bottom;
		}
	}

}
