﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowMasterLib;
using WindowMasterLib.Actions;
using System.Xml.Serialization;
using System.IO;
using WindowMasterLib.Actions.HotKeyActions;

namespace WindowMasterLib {
	public partial class SettingsWindow : Form {

		public List<HotKeyAction> Actions;
		public KeyCombo[] Combos;
		
		private HotKeyAction SelectedAction { get { return (HotKeyAction)lbActions.SelectedItem; } }
		private KeyCombo SelectedCombo { get { return (KeyCombo)lbHotKeys.SelectedItem; } }
		private bool HasKeyCombo { get { return lbHotKeys.Items.Count != 0; } }

		public SettingsWindow() {
			InitializeComponent();
		}

		/// <summary>
		/// Initialzes all of the Actions from the Config file
		/// and populates the Actions CheckedListBox
		/// </summary>
		private void LoadActions() {
			//-- Start from Scratch
			RemoveAllHotKeys();
			
			//-- Initialize Actions
			Actions = ActionManager.LoadActions();
			      
			//-- Add Actions to the Actions List Box
			lbActions.Items.Clear();
			foreach (HotKeyAction act in Actions) {
				lbActions.Items.Add(act, act.Enabled);
			}

			//-- Set the ListBox HotKey Datasource to nothing. 
      //[When a Action is clicked, the list will be populated]
			Combos = null;
			lbHotKeys.DataSource = Combos;

			//-- Initialize Buttons
			bApply.Enabled = false;
			bAddHotKey.Enabled = false;
			bRemoveHotKey.Enabled = false;
		}

		/// <summary>
		/// Saves the actions to the Config File
		/// </summary>
		private void SaveActions() { ActionManager.SaveActions(Actions); }

		/// <summary>
		/// Unregisters all current hotkeys
		/// </summary>
		private void RemoveAllHotKeys() {
			if (Actions != null)
				foreach (HotKeyAction a in Actions)
					a.RemoveAllHotKeys();
		}

		/// <summary>
		/// Refreshes the HotKeys List Box
		/// </summary>
		private void RefreshHotKeys() {
			if (SelectedAction != null)
				Combos = SelectedAction.Combos;
			else
				Combos = new KeyCombo[0];
			lbHotKeys.DataSource = Combos;
		}

		/// <summary>
		/// Displays a message to the user that there have been changes made,
		/// but they haven't been saved. The user can then save those changes
		/// or not save the changes.
		/// </summary>
		private DialogResult PromptForSave() {
			DialogResult dr = MessageBox.Show("You have modified some actions. Do you want to save your actions?", "Actions not saved!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
			if (dr == DialogResult.Yes) {
				SaveActions();
			}

			return dr;
		}

		/// <summary>
		/// Method will set the following buttons enabled value to false:
		/// bRemoveHotKey, bAddHotKey, bRemoveAction & bModifyAction
		/// </summary>
		private void DisableButtons() {
			bRemoveHotKey.Enabled = 
				bAddHotKey.Enabled = 
				bRemoveAction.Enabled = 
				bModifyAction.Enabled = false;
		}

		#region Form Events

		/// <summary>
		/// Ensures that we're only hiding the form. The only time
		/// we'll close the form is when the CloseReason is ApplicationExitCall.
		/// </summary>
		private void SettingsWindow_FormClosing(object sender, FormClosingEventArgs e) {
			//-- Only close this form if we are exiting. Else, we'll hide it.
			if (e.CloseReason != CloseReason.ApplicationExitCall) {
				if (bApply.Enabled) {
					DialogResult dr = PromptForSave();
					if (dr == DialogResult.Cancel) return;
				}
				Hide();
				e.Cancel = true;
			}
		}

		/// <summary>
		/// If we are showing the form, this method will call LoadActions.
		/// <para>If we are closing the form, this method will remove
		/// all of the current HotKeys (in case some were changed) and
		/// the Actions will be reloaded from the configuration file.</para>
		/// </summary>
		private void SettingsWindow_VisibleChanged(object sender, EventArgs e) {
			ShowInTaskbar = Visible;

			if (Visible) {
				LoadActions();
			} else {
				//-- Remove All Actions, in case some have been changed
				RemoveAllHotKeys();

				//-- Don't enable these buttons when nothing is selected
				bRemoveHotKey.Enabled = false;
				bModifyAction.Enabled = false;
				bRemoveAction.Enabled = false;
				//-- Load Actions from ConfigFile
				Actions = ActionManager.LoadActions();
			}
		}

		/// <summary>
		/// If Escape is pressed, the form will be hidden.
		/// </summary>
		private void SettingsWindow_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Escape) {
				e.Handled = true;
				if (bApply.Enabled) {
					DialogResult dr = PromptForSave();
					if (dr == DialogResult.Cancel) return;
				}
				Hide();
			} else if (e.KeyCode == Keys.Delete) { //-- Delete Action or KeyCode
				if (lbActions.Focused && SelectedAction != null) {
					bRemoveAction_Click(sender, e);
				} else if (lbHotKeys.Focused && SelectedCombo != null) {
					bRemoveKey_Click(sender, e);
				}
			}
		}

		/// <summary>
		/// Shows the About form
		/// </summary>
		private void SettingsWindow_HelpButtonClicked(object sender, CancelEventArgs e) {
			About a = new About();
			a.ShowDialog();
			e.Cancel = true;
		}

		#endregion

		#region ListBoxes Actions

		/// <summary>
		/// Populates the Description Text & HotKeys List Box when we change
		/// the selected action.
		/// </summary>
		private void lbActions_SelectedIndexChanged(object sender, EventArgs e) {
			if (lbActions.SelectedItem != null) {

				//-- Populate lbHotKeys
				HotKeyAction a = (HotKeyAction)lbActions.SelectedItem;
				Combos = a.Combos;

				//-- Set description text
				tbActionDescription.Text = a.Description;

				RefreshHotKeys();

				//-- Initialize Add / Delete HotKey Buttons
				bAddHotKey.Enabled = true;
				bRemoveHotKey.Enabled = HasKeyCombo;

				//-- Enable Modify & Remove Buttons
				bModifyAction.Enabled = true;
				bRemoveAction.Enabled = true;
			}

		}

		/// <summary>
		/// Launches the HotKeyForm with the current hotkey passed
		/// into it's constructor. If the form's dialog result
		/// is OK, it'll change the old hotkey with the new one.
		/// </summary>
		private void lbHotKeys_DoubleClick(object sender, EventArgs e) {
			if (lbHotKeys.SelectedItem != null) {
				KeyCombo oldKC = SelectedCombo;
				HotKeyForm hkf = new HotKeyForm(oldKC);
				DialogResult result = hkf.ShowDialog();
				if (result == DialogResult.OK) {
					SelectedAction.ChangeHotKey(oldKC, hkf.HotKey, false);
					RefreshHotKeys();
					bApply.Enabled = true;
				}
				hkf.Dispose();
			}
		}

		/// <summary>
		/// Sets the HotKeyAction.Enabled value to the new checkbox value
		/// </summary>
		private void lbActions_ItemCheck(object sender, ItemCheckEventArgs e) {
			if (SelectedAction != null) {
				SelectedAction.Enabled = (e.NewValue == CheckState.Checked) ? true : false;
				bApply.Enabled = true;
			}
		}

		#endregion

		#region Button Events
		/// <summary>
		/// Launches a new form that will allow a user to define a new action
		/// and add it to the actions list.
		/// </summary>
		private void bAddAction_Click(object sender, EventArgs e) {
			ActionForm af = new ActionForm();
			DialogResult dr = af.ShowDialog();
			if (dr == DialogResult.OK) {
				Actions.Add(af.Action);
				lbActions.Items.Add(af.Action);
				lbActions.SetItemChecked(lbActions.Items.Count - 1, af.Action.Enabled);
				bApply.Enabled = true;
			}
			af.Dispose();
		}

		/// <summary>
		/// Opens up the action form to modify the setting for the
		/// selected action
		/// </summary>
		private void bModify_Click(object sender, EventArgs e) {
			ActionForm af = new ActionForm(SelectedAction);
			DialogResult dr = af.ShowDialog();
			if (dr == DialogResult.OK) {
				//-- Update Checked state
				int index = lbActions.SelectedIndex;
				lbActions.SetItemChecked(index, SelectedAction.Enabled);
				tbActionDescription.Text = SelectedAction.Description;
				lbActions.Refresh();
				bApply.Enabled = true;
			}
			af.Dispose();
		}

		/// <summary>
		/// Removes the selected action
		/// </summary>
		private void bRemoveAction_Click(object sender, EventArgs e) {
			SelectedAction.RemoveAllHotKeys();
			Actions.Remove(SelectedAction);
			lbActions.Items.Remove(SelectedAction);
			bApply.Enabled = true;
			DisableButtons();
		}

		/// <summary>
		/// Opens a new HotKeyForm. If the dialog result is OK,
		/// the new HotKey will be added to the SelectedAction
		/// </summary>
		private void bAddHotKey_Click(object sender, EventArgs e) {

			HotKeyForm hkf = new HotKeyForm();
			DialogResult result = hkf.ShowDialog();
			if (result == DialogResult.OK) {
				SelectedAction.AddHotKey(hkf.HotKey);
				RefreshHotKeys();
				bApply.Enabled = true;
				bRemoveHotKey.Enabled = true;
			}
			hkf.Dispose();
		}

		/// <summary>
		/// Removes the selected hotkey. If there are not hotkeys left
		/// after the remove, the bDeleteHotKey button will be disabled.
		/// </summary>
		private void bRemoveKey_Click(object sender, EventArgs e) {
			KeyCombo kc = SelectedCombo;
			SelectedAction.RemoveHotKey(kc);
			RefreshHotKeys();

			if (lbHotKeys.Items.Count == 0) {
				bRemoveHotKey.Enabled = false;
			}

			bApply.Enabled = true;
		}
		
		/// <summary>
		/// Saves the actions to the config file and hides the form.
		/// </summary>
		private void bOK_Click(object sender, EventArgs e) {
			SaveActions();
			Hide();
		}

		/// <summary>
		/// Saves the actions to the config file.
		/// </summary>
		private void bApply_Click(object sender, EventArgs e) {
			SaveActions();
			bApply.Enabled = false;
		}

		/// <summary>
		/// Hides the form.
		/// </summary>
		private void bCancel_Click(object sender, EventArgs e) {
			if (bApply.Enabled) {
				DialogResult dr = PromptForSave();
				if (dr == DialogResult.Cancel) return;
			}
			Hide();
		}

		/// <summary>
		/// If there are unsaved actions, the user is prompted to save.
		/// If they choose to save, then are then presented with the 
		/// Import / Export form
		/// </summary>
		private void bImportExport_Click(object sender, EventArgs e) {

			//-- Make sure all actions are saved
			if (bApply.Enabled) {
				DialogResult dr = MessageBox.Show("Before Importing / Exporting you must save your actions. Would you like to save now?", "Actions not saved!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (dr == DialogResult.Yes) {
					SaveActions();
					bApply.Enabled = false;
				} else
					return;
			}

			ImportExportForm ief = new ImportExportForm();
			//-- We must unregister & remove all hotkeys
			RemoveAllHotKeys();

			//-- Block on the dialog
			ief.ShowDialog(this);
			
			//-- Load Actions
			LoadActions();
		}

		#endregion
	}
}
