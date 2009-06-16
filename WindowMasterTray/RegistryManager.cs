using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WindowMasterLib {
	public class RegistryManager {

		private static RegistryKey runSubKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
		private static string Startup_KeyName { get { return Application.ProductName; } }
		private static string Startup_KevValue {
			get {
				return string.Format("\"{0}\" -hide", Application.ExecutablePath);
			}
		}

		public static bool StartWithWindows {
			get {
				object val = runSubKey.GetValue(Startup_KeyName, null);

				//-- If the value is not null, then the key exists!
				return val != null;
			}
			set {
				if (value)
					runSubKey.SetValue(Startup_KeyName, Startup_KevValue);
				else
					runSubKey.DeleteValue(Startup_KeyName, false);
			}
		}
	}
}
