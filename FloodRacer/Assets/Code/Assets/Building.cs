using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "Building", menuName = "Game/Building")]
	public class Building : ScriptableObject {
		[Header("References")]
		public GameObject prefab;
		[Header("Balancing")]
		public WorldType world = WorldType.NON;
		public BuildingType type = BuildingType.NON;
	}
}