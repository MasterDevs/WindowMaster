using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;
using WindowMasterLib;
using System.Windows.Forms;


namespace WindowMasterLib.Actions {
	public class WindowMemory {

		private static SDictionary<int, SDictionary<string, Window.WINDOWPLACEMENT>> Memory;
		private static XmlSerializer serializer;
		private const string ConfigFileName = "WindowMasterWindowMemoryConfig.xml";

		static WindowMemory() {
			serializer =
				new XmlSerializer(typeof(SDictionary<int, SDictionary<string, Window.WINDOWPLACEMENT>>)); 
			Load();
		}

		/// <summary>
		/// Loads the memory from the configuration file
		/// </summary>
		/// <exception cref="System.Security.SecurityException" />
		/// <exception cref="System.ArgumentException" />
		/// <exception cref="System.IO.FileNotFoundException />
		/// <exception cref="System.IO.IsolatedStorage.IsolatedStorageException" />
		public static void Load() {
			Memory = new SDictionary<int, SDictionary<string, Window.WINDOWPLACEMENT>>();
			try {
				//-- Get a storage location
				using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
				//-- Get a handle to the config file stream
				using (IsolatedStorageFileStream isoFileStream =
					new IsolatedStorageFileStream(ConfigFileName, FileMode.OpenOrCreate, FileAccess.Read, isoStore)) {
					//-- Deserialize the memory
					Memory = (SDictionary<int, SDictionary<string, Window.WINDOWPLACEMENT>>)
						serializer.Deserialize(isoFileStream);
					//-- Close the file stream
					isoFileStream.Close();
				}
			} catch (Exception) { }
		}

		/// <summary>
		/// Saves the current memory to the configuration file
		/// </summary>
		/// <exception cref="System.Security.SecurityException" />
		/// <exception cref="System.ArgumentException" />
		/// <exception cref="System.IO.FileNotFoundException />
		/// <exception cref="System.IO.IsolatedStorage.IsolatedStorageException" />
		public static void Save() {
			//-- We are getting an isolated storage file for this application / user.
			using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForAssembly())
			//-- Get a handle to the config file stream
			using (IsolatedStorageFileStream isoStream =
				new IsolatedStorageFileStream(ConfigFileName, FileMode.Create, FileAccess.Write, isoStore)) {

				//-- Serialize the memory to the file
				serializer.Serialize(isoStream, Memory);

				//-- Close the stream
				isoStream.Close();

				//-- Success
			}
		}
		
		/// <summary>
		/// Checks to see if we have a saved size and position for a given 
		/// windowMemoryID and executable path pair.
		/// </summary>
		/// <param name="windowMemoryID">The set of stored positions to look for the title</param>
		/// <param name="executiblePath">Full path to the application the window corresponds to</param>
		/// <returns>true if found, false otherwise</returns>
		public static bool Lookup(int windowMemoryID, string executiblePath) {
			if (Memory.ContainsKey(windowMemoryID))
				return Memory[windowMemoryID].ContainsKey(executiblePath);
			return false;
		}

		/// <summary>
		/// Remembers a saved window size and position.
		/// </summary>
		/// <param name="windowMemoryID">The set of stored positions to look for the title</param>
		/// <param name="executiblePath">Full path to the application the window corresponds to</param>
		/// <returns>A WindowPlacement structure representing the size & position of the window</returns>
		/// <exception cref="ArgumentException" />
		public static Window.WINDOWPLACEMENT Remember(int windowMemoryID, string executiblePath) {
			if (Lookup(windowMemoryID, executiblePath))
				return Memory[windowMemoryID][executiblePath];
			else {
				if (Memory.ContainsKey(windowMemoryID))
					throw new ArgumentException("Title is invalid in this set. Please make sure you enter the correct title for this set");
				else
					throw new ArgumentException("Set is invalid. Please enter a correct set");
			}
		}

		/// <summary>
		/// Saves a windows location to memory.
		/// </summary>
		/// <param name="setID">The Memory set to store this location to</param>
		/// <param name="window">The window who's title / position will be saved to meemory</param>
		public static void Memorize(int windowMemoryID, Window window) {
			if (Memory.ContainsKey(windowMemoryID)) {
				Dictionary<string, Window.WINDOWPLACEMENT> wpd = Memory[windowMemoryID];
				if (wpd.ContainsKey(window.ExecutiblePath))
					wpd[window.ExecutiblePath] = window.GetWindowPlacement();
				else
					wpd.Add(window.ExecutiblePath, window.GetWindowPlacement());
			} else {
				SDictionary<string, Window.WINDOWPLACEMENT> wpd = new SDictionary<string, Window.WINDOWPLACEMENT>(){
					{ window.ExecutiblePath, window.GetWindowPlacement()} };
				Memory.Add(windowMemoryID, wpd);
			}

			//-- Save Memory
			Save();
		}

	}
}
