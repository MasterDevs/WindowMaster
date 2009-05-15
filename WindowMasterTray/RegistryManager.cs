using System;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WindowMasterLib {
	public class RegistryManager {

		public static RegistryKey runSubKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
		public static string ApplicationPath {
			get {
				return string.Format("\"{0}\" -hide", Application.ExecutablePath);
			}
		}
		public static void StartWithWindows_Add() {
			runSubKey.SetValue(Application.ProductName, ApplicationPath );
		}
		public static void StartWithWindows_Remove() {
			runSubKey.DeleteValue(Application.ProductName, false);
		}
	}
}
