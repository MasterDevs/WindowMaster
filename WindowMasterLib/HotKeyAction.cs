using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;

namespace WindowMasterLib {
	[Serializable]
	public abstract class HotKeyAction {

		private List<KeyCombo> _Combos;
		public KeyCombo[] Combos {
			get { return _Combos.ToArray(); }
			set {
				UnRegisterHotKeys();
				_Combos.Clear();
				foreach (KeyCombo kc in value) {
					AddHotKey(kc);
				}
			}
		}
		private bool _Enabled;
		
		/// <summary>
		/// When value is set to true, all current hotkeys are registerd.
		/// <para>When value is set to false, all current hotkeys are unregistered.</para>
		/// </summary>
		public bool Enabled {
			get {
				return _Enabled;
			}
			set {
				UnRegisterHotKeys();
				if (value) {
					RegisterHotKeys();
					_Enabled = value;
				}
			}
		}
		/// <summary>
		/// Consise name of the action
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Explains the function of the action
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// When overridden in a derived class, this method will be called
		/// when any of the HotKeys are pressed.
		/// </summary>
		protected abstract void ActionMethod(object sender, EventArgs args);
		
		public HotKeyAction() {
			_Combos = new List<KeyCombo>();
			Name = string.Empty;
			Description = string.Empty;
			_Enabled = true;
		}

		/// <summary>
		/// Adds and registers the hotkey
		/// </summary>
		public bool AddHotKey(KeyCombo hotkey) {
			//-- Make sure it's not already added
			if (!_Combos.Contains(hotkey)) {
				if (HotKey.RegisterHotKey(hotkey, ActionMethod)) {
					_Combos.Add(hotkey);
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// UnRegisters and removes the hotkey
		/// </summary>
		public void RemoveHotKey(KeyCombo hotkey) {
			_Combos.Remove(hotkey);
			HotKey.UnRegisterHotKey(hotkey);
		}

		/// <summary>
		/// UnRegisters and removes all hotkeys
		/// </summary>
		public void RemoveAllHotKeys() {
			UnRegisterHotKeys();
			_Combos.Clear();
		}

		/// <summary>
		/// Replaces a current hotkey with a new hotkey
		/// </summary>
		/// <param name="oldKC">Value of a hotkey in this action</param>
		/// <param name="newKC">Value of the new hotkey</param>
		/// <param name="AddIfNotFound">When true, the new KC will be added if oldKC 
		/// not found in this action</param>
		public bool ChangeHotKey(KeyCombo oldKC, KeyCombo newKC, bool AddIfNotFound) {
			if (_Combos.Contains(oldKC)) {
				//-- Try to Register New HotKey
				if (HotKey.RegisterHotKey(newKC, ActionMethod)) {
					_Combos[_Combos.IndexOf(oldKC)] = newKC; //-- Replace oldKC with newKC
					HotKey.UnRegisterHotKey(oldKC); //-- UnRegister old HotKey
				} else { //-- Can't Register, so return false
					return false;
				}
			} else if (AddIfNotFound) {
				return AddHotKey(newKC);
			}
			return false;
		}

		private void RegisterHotKeys() {
			foreach (KeyCombo kc in Combos) {
				HotKey.RegisterHotKey(kc, ActionMethod);
			}
		}

		private void UnRegisterHotKeys() {
			foreach (KeyCombo kc in Combos) {
				HotKey.UnRegisterHotKey(kc);
			}
		}

		public override string ToString() {
			return Name;
		}

	}
}
