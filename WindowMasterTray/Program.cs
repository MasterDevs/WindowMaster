using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMasterLib;
using System.Drawing;
using WindowMasterLib.Actions;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowMasterLib {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			bool firstInstance = true;
			using (Mutex mutex = new Mutex(true, "WindowMaster", out firstInstance)) {
				if (firstInstance) {
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					settings = new SettingsWindow();
					InitItems();
					//-- Only show the settings form if we don't have the -hide argument
					if (!ContainsArgument(args, "hide"))
						miSettings_Click(null, null);
					Application.Run();
				} else {
					mutex.ReleaseMutex();
					Application.Exit();
				}
			}
		}

		static SettingsWindow settings;
		static NotifyIcon notify;
		static ContextMenu cm;
		static MenuItem miAbout;
		static MenuItem miSettings;
		static MenuItem miClose;
		
		static void InitItems() {
			miSettings = new MenuItem();
			miSettings.Text = "Settings";
			miSettings.Click += new EventHandler(miSettings_Click);

			miAbout = new MenuItem();
			miAbout.Text = "About";
			miAbout.Click += new EventHandler(miAbout_Click);

			miClose = new MenuItem();
			miClose.Text = "Exit";
			miClose.Click += new EventHandler(miClose_Click);

			cm = new ContextMenu();
			cm.MenuItems.Add(miSettings);
			cm.MenuItems.Add(miAbout);
			cm.MenuItems.Add(miClose);
			cm.Name = "cm";

			notify = new NotifyIcon();
			notify.Visible = true;
			notify.Icon = Icon.FromHandle(Properties.Resources.Monitor_16.GetHicon());
			notify.ContextMenu = cm;
			notify.DoubleClick += new EventHandler(miSettings_Click);

			notify.Text = "Window Master";
		}

		static void miAbout_Click(object sender, EventArgs e) {
			About a = new About();
			a.ShowDialog();
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

		static bool ContainsArgument(string[] args, string argument) {
			argument = argument.ToLower();
			foreach (string arg in args) {
				if (arg.ToLower().Contains(argument))
					return true; //-- We found the argument!
			}
			//- Argument not found
			return false;
		}
	}
}
