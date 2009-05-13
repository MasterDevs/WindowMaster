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

		private const string ConfigFileName = "WindowMasterConfig.xml";

		public static List<HotKeyAction> LoadActions(string xmlPath) {
			List<HotKeyAction> actions = new List<HotKeyAction>();
			using (TextReader reader = new StreamReader(xmlPath)) {
				actions = (List<HotKeyAction>)serializer.Deserialize(reader);
				reader.Close();
			}
			return actions;
		}

		public static List<HotKeyAction> LoadActions() {

			List<HotKeyAction> actions = new List<HotKeyAction>();
			try {

				//-- Get a storage location
				using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
				//-- Get a handle to the config file stream
				using (IsolatedStorageFileStream isoFileStream =
					new IsolatedStorageFileStream(ConfigFileName, FileMode.Open, FileAccess.Read, isoStore)) {
					//-- Deserialize the actions
					actions = (List<HotKeyAction>)serializer.Deserialize(isoFileStream);
					//-- Close the file stream
					isoFileStream.Close();
				}
			} catch (Exception) { }
			return actions;
		}

		public static bool SaveActions(List<HotKeyAction> actions) {

			try {

				//-- We are getting an isolated storage file for this application / user.
				using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
				//-- Get a handle to the config file stream
				using (IsolatedStorageFileStream isoStream =
					new IsolatedStorageFileStream(ConfigFileName, FileMode.Create, FileAccess.Write, isoStore)) {

					//-- Serialize the actions to the file
					serializer.Serialize(isoStream, actions);

					//-- Close the stream
					isoStream.Close();

					//-- Success
					return true;
				}
			} catch (Exception) { return false; }
		}

		public static void SaveActions(List<HotKeyAction> actions, string xmlPath) {
			using (TextWriter writer = new StreamWriter(xmlPath)) {
				serializer.Serialize(writer, actions);
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
				aTypes.Add(typeof(MinimizeRestoreOtherWindowsAction));
				aTypes.Add(typeof(MinimizeWindowAction));
				aTypes.Add(typeof(MoveWindowAction));
				aTypes.Add(typeof(RestoreDownAction));
				aTypes.Add(typeof(RestoreUpAction));
				aTypes.Add(typeof(RestoreWindowAction));
				aTypes.Add(typeof(StretchWindowAction));
				return aTypes.ToArray();
			}
		}
	}
}
