using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class StartApplicationAction : HotKeyAction {

		[Description("Arguments that will be passed to the appliction when it starts")]
		public string Arguments { get; set; }
		[Description("Set of command-line arguments to use when starting the application.")]
		public string Path { get; set; }
		[Description("Initial Directory for the process to be started.")]
		public string WorkingDirectory { get; set; }
		[Description("The window state to use when the process is started")]
		public ProcessWindowStyle WindowStyle { get; set; }
		[Description("If true, it will try to start a new instance of the application. If false, it will bring the currently running instance to the foreground")]
		public bool StartNewInstance { get; set; }
		private string ProcessName { get; set; }

		private Process CurrentProcessInstance {
			get {
				foreach (Process p in Process.GetProcesses()) {
					if (p.ProcessName == ProcessName) {
						return p;
					}
				}
				return null;
			}
		}

		protected override void ActionMethod(object sender, EventArgs args) {
			Process currentProc = CurrentProcessInstance;
			if (StartNewInstance || CurrentProcessInstance == null)
				StartApplication();
			else
				BringCurrentInstanceToForeground(currentProc);
		}

		private void BringCurrentInstanceToForeground(Process p) {
			Window w = new Window(p.MainWindowHandle);
			w.SetAsForegroundWindow();
		}

		private void StartApplication() {
			ProcessStartInfo psi = new ProcessStartInfo();
			psi.FileName = Path;
			if (!string.IsNullOrEmpty(Arguments))
				psi.Arguments = Arguments;
			if (!string.IsNullOrEmpty(WorkingDirectory))
				psi.WorkingDirectory = WorkingDirectory;

			psi.WindowStyle = WindowStyle;
			try {
				ProcessName = Process.Start(psi).ProcessName;
			} catch (Exception) {
				MessageBox.Show(string.Format("Failed to start: {0}{0}{1}{0}{0}Please make sure you have specified the correct absolute path!", Environment.NewLine, Path), "Failed to Start Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public StartApplicationAction() {
			Name = "Start Application";
			Description = "This method will start an application.";
			Arguments = string.Empty;
			Path = string.Empty;
			WorkingDirectory = string.Empty;
			WindowStyle = ProcessWindowStyle.Normal;
		}

		public StartApplicationAction(KeyCombo hotKey)
			: this() {
			AddHotKey(hotKey);
		}

		public override void Initialize(HotKeyAction action) {
			base.Initialize(action);
			if (action is StartApplicationAction) {
				StartApplicationAction saa = (StartApplicationAction)action;
				Arguments = saa.Arguments;
				Path = saa.Path;
				WorkingDirectory = saa.WorkingDirectory;
				WindowStyle = saa.WindowStyle;
				StartNewInstance = saa.StartNewInstance;
			}
		}
	}
}

