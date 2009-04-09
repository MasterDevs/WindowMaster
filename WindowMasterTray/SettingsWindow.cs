using System;
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

		private string ConfigPath { get { return Application.StartupPath + Path.DirectorySeparatorChar + Properties.Settings.Default.ConfigFile; } }
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
			//-- Initialize Actions
			Actions = ActionManager.LoadActions(ConfigPath);
			
			//-- Add Actions to the Actions List Box
			lbActions.Items.Clear();
			foreach (HotKeyAction act in Actions) {
				lbActions.Items.Add(act, act.Enabled);
			}

			//-- Set the ListBox HotKey Datasource
			lbHotKeys.DataSource = Combos;
		}
		
		/// <summary>
		/// Ensures that we're only hiding the form. The only time
		/// we'll close the form is when the CloseReason is ApplicationExitCall.
		/// </summary>
		private void SettingsWindow_FormClosing(object sender, FormClosingEventArgs e) {
			//-- Only close this form if we are exiting. Else, we'll hide it.
			if (e.CloseReason != CloseReason.ApplicationExitCall) {
				Hide();
				e.Cancel = true;
			}
		}

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
				bDeleteHotKey.Enabled = HasKeyCombo;
				
			}

		}

		/// <summary>
		/// Refreshes the HotKeys List Box
		/// </summary>
		private void RefreshHotKeys() {
			Combos = SelectedAction.Combos;
			lbHotKeys.DataSource = Combos;
		}

		/// <summary>
		/// If we are showing the form, this method will call LoadActions.
		/// <para>If we are closing the form, this method will remove
		/// all of the current HotKeys (in case some were changed) and
		/// the Actions will be reloaded from the configuration file.</para>
		/// </summary>
		private void SettingsWindow_VisibleChanged(object sender, EventArgs e) {
			if (Visible) {
				LoadActions();
			} else {
				//-- Remove All Actions that might've been changed
				foreach (HotKeyAction a in Actions) {
					a.RemoveAllHotKeys();
				}
				//-- Load Actions from ConfigFile
				Actions = ActionManager.LoadActions(ConfigPath);
			}
		}

		/// <summary>
		/// Saves the actions to the config file and hides the form.
		/// </summary>
		private void bSave_Click(object sender, EventArgs e) {
			ActionManager.SaveActions(Actions, ConfigPath);
			Hide();
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
			}
			hkf.Dispose();
		}

		/// <summary>
		/// Removes the selected hotkey. If there are not hotkeys left
		/// after the remove, the bDeleteHotKey button will be disabled.
		/// </summary>
		private void bDeleteHotKey_Click(object sender, EventArgs e) {
			KeyCombo kc = SelectedCombo;
			SelectedAction.RemoveHotKey(kc);
			RefreshHotKeys();

			if (lbHotKeys.Items.Count == 0) {
				bDeleteHotKey.Enabled = false;
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
			}
		}

		/// <summary>
		/// If Escape is pressed, the form will be hidden.
		/// </summary>
		private void SettingsWindow_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Escape) {
				e.Handled = true;
				Hide();
			}
		}
		
	}
}
