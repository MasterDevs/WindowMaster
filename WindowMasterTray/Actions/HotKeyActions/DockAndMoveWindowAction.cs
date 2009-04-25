﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;
using System.ComponentModel;
using System.Windows.Forms;
using WindowMasterLib.Util;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class DockAndMoveWindowAction : HotKeyAction {


		private double _Percentage;
		[Description("The amount of screen the window will take up when it is docked. Valid values: (0, 1)")]
		public double Percentage {
			get { return _Percentage; }
			set {
				if (value > 0 && value < 1)
					_Percentage = value;
			}
		}
		
		
		protected override void ActionMethod(object sender, EventArgs args) {
			if (Enabled) {

				Window w = Window.ForeGroundWindow;
				Screen curScreen = w.CurrentScreen;

				//-- Make sure we are docked at either the right or left
				if (!(w.IsDocked && (w.CurrentDockPosition == DockStyle.Left || w.CurrentDockPosition == DockStyle.Right))) {
					w.UnDock();
				}


				//-- We are moving to the left
				if (MoveDirection == Direction.Left) {
					//-- Window is Docked on the Left
					if (w.IsDocked && w.CurrentDockPosition == DockStyle.Left) {
						w.UnDock();
						w.Dock(NextScreen(curScreen, MoveDirection), DockStyle.Right, Percentage);
					}//-- Window is Docked on the Right 
					else if (w.IsDocked && w.CurrentDockPosition == DockStyle.Right) {
						w.UnDock();
						w.MoveToScreen(curScreen, false);
					}
					else //-- Window is not Docked 
					{
						w.Dock(w.CurrentScreen, DockStyle.Left, Percentage);
					}
				} else { //-- We are moving to the right
					//-- Window is Docked on the right
					if (w.IsDocked && w.CurrentDockPosition == DockStyle.Right) {
						w.UnDock();
						w.Dock(NextScreen(curScreen, MoveDirection), DockStyle.Left, Percentage);
					} //-- Window is Docked on the Left
					else if (w.IsDocked && w.CurrentDockPosition == DockStyle.Left) {
						w.UnDock();
						w.MoveToScreen(curScreen, false);
					} //-- Window is not Docked
					else {
						w.Dock(curScreen, DockStyle.Right, Percentage);
					}
				}
			}
		}


		private Screen NextScreen(Screen curScreen, Direction dr) {
			foreach (Screen s in Screen.AllScreens) {
				if (dr == Direction.Left && s.WorkingArea.Right == curScreen.WorkingArea.Left)
					return s;
				else if (dr == Direction.Right && s.WorkingArea.Left == curScreen.WorkingArea.Right)
					return s;
			}
			
			//-- We are at the left / right most screen. So now find the right / left most screen. 
			// (We are looping to the other side
			Screen otherSide = curScreen;
			foreach (Screen s in Screen.AllScreens) {
				if (dr == Direction.Left && s.WorkingArea.Right > otherSide.WorkingArea.Right)
					otherSide = s;
				else if (dr == Direction.Right && s.WorkingArea.Left < otherSide.WorkingArea.Left)
					otherSide = s;
			}

			return otherSide;
		}

		public enum Direction {
			Left,
			Right
		}
		private Direction _MoveDirection;
		[Description("The direction in which the window will be moved.")]
		public Direction MoveDirection {
			get { return _MoveDirection; }
			set { _MoveDirection = value; }
		}

		private bool _PreserveSize = false;
		[Description("When set to true, the window will be relocated but it's size (in pixels) will remain the same.")]
		public bool PreserveSize {
			get { return _PreserveSize; }
			set { _PreserveSize = value; }
		}

		public DockAndMoveWindowAction() {
			Name = "Dock and Move Window";
			Description = "This action will get the current foreground window and move it to the next screen. If there is only one screen, it will not move the window.";
		}

		public DockAndMoveWindowAction(KeyCombo kc)
			: this() {
			AddHotKey(kc);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);

			if (action is DockAndMoveWindowAction) {
				DockAndMoveWindowAction dmwa = (DockAndMoveWindowAction)action;
				PreserveSize = dmwa.PreserveSize;
				Percentage = dmwa.Percentage;
				MoveDirection = dmwa.MoveDirection;
			}
		}

	}
}
