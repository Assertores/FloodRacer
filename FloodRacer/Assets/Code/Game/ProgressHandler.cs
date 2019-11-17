using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	public static class ProgressHandler {
		public static void SaveProgress<U>(string name, Tuple<bool, U>[] elements) {
			int value = 0;
			for(int i = 0; i < elements.Length; i++) {
				value = value << 1;
				value |= elements[i].Item1 ? 1 : 0;
			}
			PlayerPrefs.SetInt(StringCollection.PP_WORLDS, value);
		}

		public static void LoadProgress<U>(string name, Tuple<bool, U>[] elements) {
			int value = PlayerPrefs.GetInt(StringCollection.PP_WORLDS);
			for(int i = elements.Length - 1; i >= 0; i--) {
				elements[i].Item1 = (value & 1) == 1;
				value = value >> 1;
			}
		}

		public static U[] GetProgress<U>(Tuple<bool, U>[] elements) {
			List<U> value = new List<U>(elements.Length);

			for(int i = 0; i < elements.Length; i++) {
				if(elements[i].Item1)
					value.Add(elements[i].Item2);
			}

			return value.ToArray();
		}

		public static int[] GetProgressIndex<U>(Tuple<bool, U>[] elements) {
			List<int> value = new List<int>(elements.Length);

			for(int i = 0; i < elements.Length; i++) {
				if(elements[i].Item1)
					value.Add(i);
			}

			return value.ToArray();
		}
	}
}