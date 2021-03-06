﻿using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

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

		public RECT(int left, int top, int right, int bottom) {
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
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
		/// This method will determine if this rectangle is fully
		/// inside of the passed in parameter
		/// <param name="r">The bounding rectangle</param>
		/// <returns>True if this instance is fits inside of the parameter R.</returns>
		/// </summary>
		public bool IsInsideOf(RECT r) {
			return
				r.Left <= Left && r.Right >= Right &&
				r.Top <= Top && r.Bottom >= Bottom;
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
		/// parameter location
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

		/// <summary>
		/// This method will move the window from it's current location
		/// to a new location. The window will be repositioned such that the
		/// ratio of the distance between the RECT edges and the from
		/// RECT edges are equal to the distance between the RECT edges
		/// and the to RECT edges.
		/// <param name="preserveSize">When true, the rectangle will be moved, but it's size
		/// will remain the same as before. The rectangle will be positioned based
		/// on the Top, Left corner and it's original height and width will extend from
		/// the relocated Top, Left point</param>
		/// </summary>
		public void Relocate(RECT from, RECT to, bool preserveSize) {
			ReInit(Relocate(this, from, to, preserveSize));
		}

		public RECT RelocateInBounds(RECT from, RECT to, bool preserveSize) {
			RECT r = Relocate(this, from, to, preserveSize);
			int orig_width = r.Width;
			int orig_height = r.Height;
			if (r.Left < to.Left) { r.Left = to.Left; }
			if (r.Right > to.Right) { r.Right = to.Right; }
			if (r.Top < to.Top) { r.Top = to.Top; }
			if (r.Bottom > to.Bottom) { r.Bottom = to.Bottom; }

			if (preserveSize) { //-- Maintain Original Size

				//-- We will only resize if the newly created window
				//will fit in the new bounds.

				//-- Horizontal Positioning
				if ((r.Left + orig_width) <= to.Right)
					r.Right = r.Left + orig_width;
				else if (orig_width <= to.Width)
					r.Left = r.Right - orig_width;
				else { //-- Width is too large. Resize to max bounds
					r.Left = to.Left;
					r.Right = to.Right;
				}

				//-- Vertical Positioning
				if ((r.Top + orig_height) <= to.Bottom)
					r.Bottom = r.Top + orig_height;
				else if (orig_height <= to.Height)
					r.Top = r.Bottom - orig_height;
				else { //-- Height is too large. Resize ot max bounds
					r.Top = to.Top;
					r.Bottom = to.Bottom;
				}
			}

			ReInit(r);
			return r;
		}

		private static RECT Relocate(RECT r, RECT from, RECT to, bool preserveSize) {
			//-- Find Height & Width Multiplier
			double height = (double)to.Height / (double)from.Height;
			double width = (double)to.Width / (double)from.Width;

			//-- Calculate the new location of the window
			int left = to.Left + (int)Math.Round(((double)(r.Left - from.Left) * width));
			int right = to.Right - (int)Math.Round((double)(from.Right - r.Right) * width);
			int top = to.Top + (int)Math.Round((double)(r.Top - from.Top) * height);
			int bottom = to.Bottom - (int)Math.Round((double)(from.Bottom - r.Bottom) * height);

			//-- Create the new rectangle
			RECT ret = new RECT(left, top, right, bottom);

			//-- ReAdjust the size if we are preserving it.
			if (preserveSize) {
				ret.Right = ret.Left + r.Width;
				ret.Bottom = ret.Top + r.Height;
			}

			//-- Return the newly relocated rectangle
			return ret;
		}
 
		public void PlaceInisde(RECT r) {
			Left = Left < r.Left ? r.Left : Left;
			Right = Right > r.Right ? r.Right : Right;
			Top = Top < r.Top ? r.Top : Top; 
			Bottom = Bottom > r.Bottom ? r.Bottom : Bottom;
		}

		public static RECT GetDockedRECT(RECT container, DockStyle ds, double percentage) {
			RECT rect = container;
			int height = (int)((double)container.Height * percentage);
			int width = (int)((double)container.Width * percentage);
			switch (ds) {
				case DockStyle.Bottom:
					rect.Top = container.Bottom - height;;
					break;
				case DockStyle.Fill:
					rect = container;
					break;
				case DockStyle.Left:
					rect.Right = container.Left + width;
					break;
				case DockStyle.Right:
					rect.Left = container.Right - width;
					break;
				case DockStyle.Top:
					rect.Bottom = container.Top + height;
					break;
				case DockStyle.None:
				default:
					break;
			}
			return rect;
		}


		#region Overrides
		public override string ToString() {
			return string.Format("X {0} Y {1} Width {2} Height {3} ", Left, Top, Width, Height);
		}

		public override int GetHashCode() {
			return Left ^ ((Top << 13) | (Top >> 0x13))
				^ ((Width << 0x1a) | (Width >> 6))
				^ ((Height << 7) | (Height >> 0x19));
		}

		private static bool CompareValues(RECT a, RECT b) {
			return a.Top == b.Top && a.Bottom == b.Bottom && a.Left == b.Left && a.Right == b.Right;
		}
		private bool CompareValues(RECT r) {
			return CompareValues(this, r);
		}
		public override bool Equals(object obj) {
			if (obj == null)
				return false;

			return obj is RECT && Equals((RECT)obj);
		}

		public bool Equals(RECT r) {
			if ((object)r == null)
				return false;

			return CompareValues(r);
		}

		public bool Equals(Rectangle r) {
			if((object)r == null)
				return false;

			RECT rect = new RECT(r);
			return this.Equals(rect);
		}

		public static bool operator ==(RECT a, RECT b) {
			// If both are null, or both are teh same instance, return true
			if (object.ReferenceEquals(a, b))
				return true;

			// If one is null, but not both, return false
			if (((object)a == null) || (object)b == null)
				return false;

			// Return true only if all parameters are equal
			return CompareValues(a, b);
		}

		public static bool operator !=(RECT a, RECT b) {
			return !(a == b);
		}

		#endregion

	}

}
