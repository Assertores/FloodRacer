using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[System.Serializable]
	public struct Tuple<T, U>{
		public T Item1;
		public U Item2;
	}
}