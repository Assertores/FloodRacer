using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "Level", menuName = "Game/Level")]
	public class Level : ScriptableObject {
		[Header("References")]
		public GameObject prefab;
		[Header("Balancing")]
		public WorldType world = WorldType.NON;
		public float length;
		public Level[] nextLevels;
	}
}