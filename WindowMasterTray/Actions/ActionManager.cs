using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using WindowMasterLib.Actions.HotKeyActions;
using System.IO.IsolatedStorage;

namespace WindowMasterLib.Actions {
	public class ActionManager {

		private static XmlSerializer serializer =
			new XmlSerializer(typeof(List<HotKeyAction>), ActionTypes);

		public static List<HotKeyAction> Actions { get; set; }

		private const string ConfigFileName = "WindowMasterConfig.xml";

		static ActionManager() { LoadActions(); }

		public static void LoadActions(string xmlPath) {
			Actions = new List<HotKeyAction>();
			using (TextReader reader = new StreamReader(xmlPath)) {
				Actions = (List<HotKeyAction>)serializer.Deserialize(reader);
				reader.Close();
			}
		}

		public static void LoadActions() {
			Actions = new List<HotKeyAction>();
			try {
				//-- Get a storage location
				using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
				//-- Get a handle to the config file stream
				using (IsolatedStorageFileStream isoFileStream =
					new IsolatedStorageFileStream(ConfigFileName, FileMode.Open, FileAccess.Read, isoStore)) {
					//-- Deserialize the actions
					Actions = (List<HotKeyAction>)serializer.Deserialize(isoFileStream);
					//-- Close the file stream
					isoFileStream.Close();
				}
			} catch (Exception) { }
		}

		public static bool SaveActions() {

			try {

				//-- We are getting an isolated storage file for this application / user.
				using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
				//-- Get a handle to the config file stream
				using (IsolatedStorageFileStream isoStream =
					new IsolatedStorageFileStream(ConfigFileName, FileMode.Create, FileAccess.Write, isoStore)) {

					//-- Serialize the actions to the file
					serializer.Serialize(isoStream, Actions);

					//-- Close the stream
					isoStream.Close();

					//-- Success
					return true;
				}
			} catch (Exception) { return false; }
		}

		public static void SaveActions(string xmlPath) {
			using (TextWriter writer = new StreamWriter(xmlPath)) {
				serializer.Serialize(writer, Actions);
				writer.Close();
			}
		}

		/// <summary>
		/// This array contains all action types. If new actions are created, this 
		/// property needs to be modified in order for the Action De Serializer to work.
		/// </summary>
		public static Type[] ActionTypes {
			get {
				List<Type> aTypes = new List<Type>();
				aTypes.Add(typeof(ChangeOpacityAction));
				aTypes.Add(typeof(DockAndMoveWindowAction));
				aTypes.Add(typeof(DockWindowAction));
				aTypes.Add(typeof(MakeInvisibleAction));
				aTypes.Add(typeof(MakeOpaqueAction));
				aTypes.Add(typeof(MaximizeWindowAction));
				aTypes.Add(typeof(MediaKeyAction));
				aTypes.Add(typeof(MemorizeWindowLocationAction));
				aTypes.Add(typeof(MinimizeRestoreOtherWindowsAction));
				aTypes.Add(typeof(MinimizeToTray));
				aTypes.Add(typeof(MinimizeWindowAction));
				aTypes.Add(typeof(MoveWindowAction));
				aTypes.Add(typeof(PureTextAction));
				aTypes.Add(typeof(RecoverOrphanWindowsAction));
				aTypes.Add(typeof(RememberWindowLocationAction));
				aTypes.Add(typeof(RestoreDownAction));
				aTypes.Add(typeof(RestoreUpAction));
				aTypes.Add(typeof(RestoreWindowAction));
				aTypes.Add(typeof(SetWindowPlacement));
				aTypes.Add(typeof(ShowActiveActionsAction));
				aTypes.Add(typeof(ShowWindowPlacement));
				aTypes.Add(typeof(StartApplicationAction));
				aTypes.Add(typeof(StretchWindowAction));
				return aTypes.ToArray();
			}
		}
	}
}
