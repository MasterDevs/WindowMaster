using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowMasterLib.Actions;
using WindowMasterLib.Actions.HotKeyActions;

namespace WindowMasterLib {
	public partial class ActionForm : Form {
		private HotKeyAction _originalAction;
		private HotKeyAction _Action;
		public HotKeyAction Action {
			get {
				return _Action;
			}
		}

		private struct ActionType {
			public HotKeyAction Action;
			public string Name;
			
			public ActionType(string name, HotKeyAction action) {
				Name = name;
				Action = action;
			}

			public override string ToString() {
				return Name;
			}
		}

		public ActionForm() {
			InitializeComponent();
			
			foreach (Type action in ActionManager.ActionTypes) {
				HotKeyAction ac = (HotKeyAction)System.Activator.CreateInstance(action);
				lbActions.Items.Add(new ActionType(ac.Name, ac));
			}
			propertyGrid.SelectedObject = Action;
		}


		public ActionForm(HotKeyAction action){
			InitializeComponent();
			_originalAction = action;
			_Action = (HotKeyAction)System.Activator.CreateInstance(action.GetType());
			_Action.Initialize(action);
			lbActions.Visible = false;

			bOK.Text = "&Save";
			propertyGrid.SelectedObject = Action;

			Height = Height - lbActions.Height;
			ResetNameDescr();
		}

		private void bOK_Click(object sender, EventArgs e) {
			
			//-- Make sure we selected an action
			if (Action == null) {
				DialogResult = DialogResult.Cancel;
				return;
			}

			//-- If we were modifying an action, Initialize it with the new values
			if (_originalAction != null) {
				_originalAction.Initialize(Action);
			}
		}

		private void lbActions_SelectedIndexChanged(object sender, EventArgs e) {
			_Action = ((ActionType)lbActions.SelectedItem).Action;
			propertyGrid.SelectedObject = Action;
			ResetNameDescr();
		}

		private void ResetNameDescr() {
			tbDescription.Text = Action.Description;
			lActionName.Text = Action.Name;
		}
	}
}
