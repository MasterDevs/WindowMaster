using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowMasterLib.Util {

	/// <summary>
	/// Provides a way to access current, previous
	/// and next elements in a list. If the current
	/// element is at the end of the list, the next element
	/// is the 1st element in the list.
	/// </summary>
	/// <typeparam name="T">Type of object that will be contained in the list</typeparam>
	public class LoopList<T> : List<T> {

		public int CurrentIndex { get; set; }

		public LoopList() {
			CurrentIndex = 0;
		}

		public LoopList(int capacity)
			: base(capacity) {
			CurrentIndex = 0;
		}

		/// <summary>
		/// This method will move the current index to the
		/// next element in the list
		/// </summary>
		public void MoveNext(){
			int idx = NextIndex;
			if (idx != -1) {
				CurrentIndex = idx;
			}
		}

		/// <summary>
		/// This method will move the currrent index to the 
		/// previous element int he list
		/// </summary>
		public void MovePrev() {
			int idx = PrevIndex;
			if (idx != -1) {
				CurrentIndex = idx;
			}
		}

		public T Current { get { return this[CurrentIndex]; } }

		/// <summary>
		/// This method will retrieve the next index
		/// in the loop but will not incriment the current index
		/// </summary>
		public int PrevIndex {
			get {
				if (this.Count == 0)
					return -1;
				else if ((CurrentIndex - 1) < 0)
					return this.Count - 1;
				else
					return CurrentIndex - 1;
			}
		}

		/// <summary>
		/// This method will retrive the previous index
		/// but it will not alter the current index
		/// </summary>
		public int NextIndex {
			get {
				if (this.Count == 0)
					return -1;
				else if ((CurrentIndex + 1) == this.Count)
					return 0;
				else
					return CurrentIndex + 1;
			}
		}

		/// <summary>
		/// This method will retrieve the next item in the loop
		/// and alter the current index and current item to reflect this move.
		/// </summary>
		public T Next { get { MoveNext(); return Current; } }
		/// <summary>
		/// This method will retrieve the previous item in the loop
		/// and alter the current index and current item to reflect this move.
		/// </summary>
		public T Prev { get { MovePrev(); return Current; } }
	}
}
