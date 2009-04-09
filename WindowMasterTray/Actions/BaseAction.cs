using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowMasterLib;

namespace WindowMaster.Actions {
	[Serializable]
	public abstract class BaseAction {

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
		public string Name { get; set; }
		public string Description { get; set; }
		protected abstract void DefaultActionHandler(object sender, KeyPressedEventArgs args);

		public BaseAction() {
			_Combos = new List<KeyCombo>();
			Name = string.Empty;
			Description = string.Empty;
		}

		public BaseAction(string actionName)
			: this() {
			Name = actionName;
		}

		public BaseAction(string name, string description)
			: this(name) {
			Description = description;
		}

		public void AddHotKey(KeyCombo hotkey) {
			_Combos.Add(hotkey);
			HotKey.RegisterHotKey(hotkey, DefaultActionHandler);
		}

		public void RemoveHotKey(KeyCombo hotkey) {
			_Combos.Remove(hotkey);
			HotKey.UnRegisterHotKey(hotkey);
		}

		public void ChangeHotKey(KeyCombo oldKC, KeyCombo newKC, bool AddIfNotFound) {
			if (_Combos.Contains(oldKC)) {
				_Combos[_Combos.IndexOf(oldKC)] = newKC;
			} else if (AddIfNotFound) {
				_Combos.Add(newKC);
			}
		}

		private void RegisterHotKeys() {
			foreach (KeyCombo kc in Combos) {
				HotKey.RegisterHotKey(kc, DefaultActionHandler);
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
