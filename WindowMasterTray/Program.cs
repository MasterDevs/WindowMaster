using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMasterLib;
using System.Drawing;
using WindowMasterLib.Actions;

namespace WindowMasterLib {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			settings = new SettingsWindow();
			InitItems();
			miSettings_Click(null, null);
			Application.Run();
		}

		static SettingsWindow settings;
		static NotifyIcon notify;
		static ContextMenu cm;
		static MenuItem miSettings;
		static MenuItem miClose;

		static void InitItems() {
			miSettings = new MenuItem();
			miSettings.Text = "Settings";
			miSettings.Click += new EventHandler(miSettings_Click);

			miClose = new MenuItem();
			miClose.Text = "Exit";
			miClose.Click += new EventHandler(miClose_Click);

			cm = new ContextMenu();
			cm.MenuItems.Add(miSettings);
			cm.MenuItems.Add(miClose);
			cm.Name = "cm";

			notify = new NotifyIcon();
			notify.Visible = true;
			notify.Icon = Icon.FromHandle(Properties.Resources.Monitor_16.GetHicon());
			notify.ContextMenu = cm;
			notify.DoubleClick += new EventHandler(miSettings_Click);

			notify.Text = "Window Master";
		}

		static void miClose_Click(object sender, EventArgs e) {
			notify.Visible = false;
			settings.Dispose();
			Application.Exit();
		}

		static void miSettings_Click(object sender, EventArgs e) {
			settings.Show();
			settings.Activate();
		}
	}
}
