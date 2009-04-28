using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowMasterLib.Actions;
using System.IO;

namespace WindowMasterLib {
	public partial class ImportExportForm : Form {

		private string InitalDir { get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal); } } 

		public ImportExportForm() {
			InitializeComponent();
			Actions = new List<HotKeyAction>();

			//-- Set Initial Directory on the Open & Save Dialogs
			openDialog.InitialDirectory = InitalDir;
			saveDialog.InitialDirectory = InitalDir;
		}

		public List<HotKeyAction> Actions;

		private void bExport_Click(object sender, EventArgs e) {
			DialogResult dr = saveDialog.ShowDialog(this);
			if (dr == DialogResult.OK || dr == DialogResult.Yes) {
				
				//-- Load the Actions
				Actions = ActionManager.LoadActions();

				try {
					//-- Save the Actions to the user specified location
					ActionManager.SaveActions(Actions, saveDialog.FileName);
				} catch (Exception) {
					MessageBox.Show("There was an error exporting the actions. Please try again, making sure you're selecting a location that you can save to.", "Error Saving Config File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					saveDialog.InitialDirectory = InitalDir;
					return;
				}

				MessageBox.Show("Actions Exported!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

				//-- Unregister all hotkeys
				foreach (HotKeyAction  a in Actions) {
					a.RemoveAllHotKeys();
				}

				//-- Set DialogResult to Close form and return to owner
				DialogResult = DialogResult.OK;
			}

		}

		private void bImport_Click(object sender, EventArgs e) {
			DialogResult dr = openDialog.ShowDialog(this);
			if (dr == DialogResult.OK || dr == DialogResult.Yes) {

				try {
					//-- Load the actions from user specified location
					Actions = ActionManager.LoadActions(openDialog.FileName);
				} catch (Exception) {
					MessageBox.Show("There was an error importing the actions. Please try again, making sure you're selecting a valid WindowMaster configuration file.", "Error Loading Config File", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					openDialog.InitialDirectory = InitalDir;
					return;
				}

				//-- Save the actions
				if (ActionManager.SaveActions(Actions))
					MessageBox.Show("Actions Imported!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

				//-- Unregister all hotkeys
				foreach (HotKeyAction a in Actions) {
					a.RemoveAllHotKeys();
				}

				//-- Set DialogResult to Close form and return to owner
				DialogResult = DialogResult.OK;
			}
		}
	}
}
