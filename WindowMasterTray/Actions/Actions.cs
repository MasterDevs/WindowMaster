using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace WindowMaster.Actions {
	public class ActionManager {

		private static XmlSerializer serializer = 
			new XmlSerializer(typeof(List<BaseAction>), ActionTypes); 

		public static List<BaseAction> LoadActions(string xmlPath) {
			List<BaseAction> actions = new List<BaseAction>();
			using (TextReader reader = new StreamReader(xmlPath)) {
				actions = (List<BaseAction>)serializer.Deserialize(reader);
				reader.Close();
			}
			return actions;
		}

		public static void SaveActions(List<BaseAction> actions, string xmlPath) {
			using (TextWriter writer = new StreamWriter(xmlPath)) {
				serializer.Serialize(writer, actions);
				writer.Close();
			}
		}


		private static Type[] ActionTypes {
			get {
				List<Type> aTypes = new List<Type>();
				aTypes.Add(typeof(MoveForegroundWindowAction));
				return aTypes.ToArray();
			}
		}
	}
}
