using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "River", menuName = "Game/River")]
	public class River : ScriptableObject {
		[Header("References")]
		public GameObject[][] prefabs = new GameObject[(int)RiverType.SIZE][];
		[Header("Balancing")]
		public WorldType world = WorldType.NON;

		public River() {
			for(int i = 0; i < prefabs.Length; i++) {
				prefabs[i] = new GameObject[0];
			}
		}
	}
}