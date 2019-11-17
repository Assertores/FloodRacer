using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "Streat", menuName = "Game/Streat")]
	public class StreatAsset : ScriptableObject {
		[Header("References")]
		public GameObject[][] prefabs = new GameObject[(int)StreatType.SIZE][];
		[Header("Balancing")]
		public WorldType world = WorldType.MANHATTAN;

		public StreatAsset() {
			for(int i = 0; i < prefabs.Length; i++) {
				prefabs[i] = new GameObject[0];
			}
		}
	}
}