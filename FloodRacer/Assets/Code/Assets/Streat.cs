using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "Streat", menuName = "Game/Streat")]
	public class Streat : ScriptableObject {
		[Header("References")]
		public GameObject[][] prefabs = new GameObject[(int)StreatType.SIZE][];
		[Header("Balancing")]
		public WorldType world = WorldType.NON;

		public Streat() {
			for(int i = 0; i < prefabs.Length; i++) {
				prefabs[i] = new GameObject[0];
			}
		}
	}
}