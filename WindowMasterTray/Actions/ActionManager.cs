using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using WindowMasterLib.Actions.HotKeyActions;

namespace WindowMasterLib.Actions {
	public class ActionManager {

		private static XmlSerializer serializer = 
		  new XmlSerializer(typeof(List<HotKeyAction>), ActionTypes); 

		public static List<HotKeyAction> LoadActions(string xmlPath) {
			List<HotKeyAction> actions = new List<HotKeyAction>();
			using (TextReader reader = new StreamReader(xmlPath)) {
				actions = (List<HotKeyAction>)serializer.Deserialize(reader);
				reader.Close();
			}
			return actions;
		}

		public static void SaveActions(List<HotKeyAction> actions, string xmlPath) {
			using (TextWriter writer = new StreamWriter(xmlPath)) {
				serializer.Serialize(writer, actions);
				writer.Close();
			}
		}


		private static Type[] ActionTypes {
			get {
				List<Type> aTypes = new List<Type>();
				aTypes.Add(typeof(MoveWindowAction));
				aTypes.Add(typeof(MaximizeWindowAction));
				aTypes.Add(typeof(MinimizeWindowAction));
				aTypes.Add(typeof(RestoreWindowAction));
				aTypes.Add(typeof(RestoreDown));
				aTypes.Add(typeof(RestoreUp));
				return aTypes.ToArray();
			}
		}
	}
}
