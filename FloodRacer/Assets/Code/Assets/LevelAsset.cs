using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "Level", menuName = "Game/Level")]
	public class LevelAsset : ScriptableObject {
		[Header("References")]
		public GameObject prefab;
		[Header("Balancing")]
		public WorldType world = WorldType.MANHATTAN;
		public float length;
		public LevelAsset[] nextLevels;
	}
}