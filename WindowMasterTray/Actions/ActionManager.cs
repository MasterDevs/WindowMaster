﻿using System;
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
