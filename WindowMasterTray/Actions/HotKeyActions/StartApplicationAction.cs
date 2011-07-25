using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using WindowMasterLib.Util;
using System.Text.RegularExpressions;
using System.IO;

namespace WindowMasterLib.Actions.HotKeyActions {
	[Serializable]
	public class StartApplicationAction : HotKeyAction {

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

		[Description("Set of command-line arguments to use when starting the application.")]
		public string Arguments { get; set; }

		[Description("If true, will not set create a new window when starting the application")]
		public bool CreateNoWindow { get; set; }

		[Description("The full path to the process to be started.")]
		public string Path { get; set; }

		[Description("Redirects the output stream of the executing app and outputs all of the text to a window")]
		public bool PrintOutput { get; set; }

		[Description("If true, it will try to start a new instance of the application. If false, it will bring the currently running instance to the foreground")]
		public bool StartNewInstance { get; set; }

		[Description("The window state to use when the process is started")]
		public ProcessWindowStyle WindowStyle { get; set; }

		[Description("Initial Directory for the process to be started.")]
		public string WorkingDirectory { get; set; }


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

			psi.CreateNoWindow = CreateNoWindow;
			psi.WindowStyle = WindowStyle;

			if (this.PrintOutput) {
				RunPrintOutput(psi);
			} else
				try {
					ProcessName = Process.Start(psi).ProcessName;
				} catch (Exception) {
					MessageBox.Show(string.Format("Failed to start: {0}{0}{1}{0}{0}Please make sure you have specified the correct absolute path!", Environment.NewLine, Path), "Failed to Start Application", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
		}

		private void RunPrintOutput(ProcessStartInfo psi) {
			psi.UseShellExecute = false;
			psi.ErrorDialog = false;
			psi.CreateNoWindow = true;
			psi.RedirectStandardOutput = true;
			psi.RedirectStandardError = true;

			try {
				Process p = Process.Start(psi);
				using (StreamReader output = p.StandardOutput)
				using (StreamReader err = p.StandardError) {
					string procName = p.ProcessName;
					string stdOut = output.ReadToEnd();
					string stdErr = err.ReadToEnd();

					StartApplicationOutput sao = new StartApplicationOutput(procName, stdOut, stdErr);
					sao.ShowDialog();
				}

			} catch (Exception exp) {
				MessageBox.Show("Error starting application:\n=n" + exp.Message);
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
			StartApplicationAction saa = action as StartApplicationAction;
			if (saa != null) {
				Arguments = saa.Arguments;
				CreateNoWindow = saa.CreateNoWindow;
				Path = saa.Path;
				StartNewInstance = saa.StartNewInstance;
				WindowStyle = saa.WindowStyle;
				WorkingDirectory = saa.WorkingDirectory;
			}
		}
	}
}

