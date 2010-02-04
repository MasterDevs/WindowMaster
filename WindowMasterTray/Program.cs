using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowMasterLib;
using System.Drawing;
using WindowMasterLib.Actions;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Ipc;

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
					ListenForOtherInstances();

					//-- Only show the settings form if we don't have the -hide argument
					if (!ContainsArgument(args, "hide"))
						miSettings_Click(null, null);
					else
						settings.LoadActions(); //-- Load actions if we don't show the form
					Application.Run();
				} else {
					//-- Display the SettingsWindow of the First Instance
					NotifyFirstInstance();
					//-- Exit this instance as the user's already using the first instance.
					Application.Exit();
				}
			}
		}

		public static void Exit() {
			notify.Visible = false;
			//-- Dispose Necessary Forms
			settings.Dispose();
			//-- Hotkeys Is Disposable
			HotKey.CleanUp();
			Application.Exit();
		}

		/// <summary>
		/// Displays a balloon icon of all active hotkeys.
		/// </summary>
		/// <param name="timeout">Length of balloon pop-up, in milliseconds</param>
		public static void DisplayActiveHotKeys(int timeout) {
			string activeHotKeys = settings.ActiveActionsString;

			//-- If the text of the balloon is empty, a runtime error will be thrown
			if (string.IsNullOrEmpty(activeHotKeys))
				notify.BalloonTipText = "No Active HotKeys!";
			else
				notify.BalloonTipText = activeHotKeys;

			notify.ShowBalloonTip(timeout);
		}

		static SettingsWindow settings;
		static NotifyIcon notify;
		static ContextMenu cm;
		static MenuItem miAbout;
		static MenuItem miSettings;
		static MenuItem miExit;
		
		static void InitItems() {
			miSettings = new MenuItem();
			miSettings.Text = "Settings";
			miSettings.Click += new EventHandler(miSettings_Click);

			miAbout = new MenuItem();
			miAbout.Text = "About";
			miAbout.Click += new EventHandler(miAbout_Click);

			miExit = new MenuItem();
			miExit.Text = "Exit";
			miExit.Click += new EventHandler(miExit_Click);

			cm = new ContextMenu();
			cm.MenuItems.Add(miSettings);
			cm.MenuItems.Add(miAbout);
			cm.MenuItems.Add(miExit);
			cm.Name = "cm";

			notify = new NotifyIcon();
			notify.Visible = true;
			notify.Icon = Properties.Resources.Monitor_256;
			notify.ContextMenu = cm;
			notify.BalloonTipClicked += new EventHandler(miSettings_Click);
			notify.DoubleClick += new EventHandler(miSettings_Click);
			notify.BalloonTipIcon = ToolTipIcon.Info;
			notify.BalloonTipTitle = "Active HotKeys";
			notify.Text = "Window Master";
		}

		static void miAbout_Click(object sender, EventArgs e) {
			About a = new About();
			a.ShowDialog();
		}

		static void miExit_Click(object sender, EventArgs e) {
			Exit();
		}

		static void miSettings_Click(object sender, EventArgs e) {
			settings.Visible = true;
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

		#region IPC for Single Instance Application
		private const string ServerChannelName = "WindowMaster";
		private const string FirstInstanceName = "FirstInstance";
		private const string RemoteMessageFS = "ipc://{0}/{1}";

		/// <summary>
		/// Sets up the Server Channel to listen for other instances
		/// remote message.
		/// </summary>
		static void ListenForOtherInstances() {
			//-- Create the Server Channel
			IpcServerChannel serverChannel = new IpcServerChannel(ServerChannelName);
			ChannelServices.RegisterChannel(serverChannel, false);

			RemoteMessage remoteMessage = new RemoteMessage(settings);
			RemotingServices.Marshal(remoteMessage, FirstInstanceName);
		}

		/// <summary>
		/// When another instance of WindowMaster starts up, it
		/// will call this method to open up an IPC channel
		/// to the first instance and show the settings window.
		/// </summary>
		static void NotifyFirstInstance() {
			IpcClientChannel clientChannel = new IpcClientChannel();
			ChannelServices.RegisterChannel(clientChannel, false);

			RemoteMessage message = (RemoteMessage)Activator.GetObject(typeof(RemoteMessage),
				string.Format(RemoteMessageFS, ServerChannelName, FirstInstanceName));

			if (!message.Equals(null)) {
				message.ShowSettingsWindow();
			}
		}

		/// <summary>
		/// This small class is used to show the SettingsWindow
		/// by containing a reference to it.
		/// </summary>
		class RemoteMessage : MarshalByRefObject {
			private SettingsWindow mainForm;

			public RemoteMessage(SettingsWindow mainForm) {
				this.mainForm = mainForm;
			}

			public void ShowSettingsWindow() {
				mainForm.Show();
				mainForm.Activate();
			}
		} 
		#endregion
	}
}
