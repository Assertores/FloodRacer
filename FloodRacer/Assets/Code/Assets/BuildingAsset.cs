using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "Building", menuName = "Game/Building")]
	public class BuildingAsset : ScriptableObject {
		[Header("References")]
		public GameObject prefab;
		[Header("Balancing")]
		public WorldType world = WorldType.MANHATTAN;
		public BuildingType type = BuildingType.R1X1;
	}
}