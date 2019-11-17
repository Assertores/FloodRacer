using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	public enum WorldType { MANHATTAN = 0, SAN_FRANCISCO, NEW_ORLEANS, TOKIO, BRASILIEN, AGYPTEN, CHINA_TOWN, FUTURE_TOWN, SIZE };

	public enum BuildingType { SPETIAL = 0, R1X1, R1X2, R2X2, L2X2, R5X5, SIZE };

	public enum StreatType { STARIGHT = 0, CORNER, T, CORS, SIZE }

	public enum RiverType { STRAIGHT = 0, CORNER, T, BRIDGE, SIZE }

	public class world {
		public GameObject[][] buildings; //building type array of that buildingtype
		public GameObject[][][] streats; //array of streattype arrays of that streattype
		public GameObject[][][] rivers; //array of rivertype arrays of that rivertype
	}

	class worldConverter {
		public List<GameObject>[] buildings;
		public List<GameObject[][]> streats;
		public List<GameObject[][]> river;
	}

	public class WorldHandler : Singleton<WorldHandler> {

		[Header("References")]
		[SerializeField] BuildingAsset[] buildings;
		[SerializeField] StreatAsset[] streats;
		[SerializeField] RiverAsset[] rivers;

		[Header("Balancing")]
		[SerializeField] bool doDebugLogs = false;

		public WorldType currentWorld { get; private set; }
		public GameObject[][] currentBuildings { get; private set; }
		public GameObject[][] currentStreats { get; private set; }
		public GameObject[][] currentRiver { get; private set; }

		Tuple<bool, world>[] worlds = new Tuple<bool, world>[(int)WorldType.SIZE];

		private void Start() {
			InitWorld();
			ProgressHandler.LoadProgress(StringCollection.PP_WORLDS, worlds);
			worlds[0].Item1 = true;

			if(doDebugLogs) {
				for(int i = 0; i < worlds.Length; i++) {
					print(StringCollection.WorldTypeToString((WorldType)i) + ": " + worlds[i].Item1);
				}
			}
		}

		private void OnDestroy() {
			ProgressHandler.SaveProgress(StringCollection.PP_WORLDS, worlds);
		}

		public void ChangeWorld(WorldType newWorld = WorldType.SIZE) {
			if(newWorld != WorldType.SIZE) {
				currentWorld = newWorld;
			} else {
				int index = Random.Range(0, (int)WorldType.SIZE);
				for(int i = 0; !worlds[index].Item1 && i < worlds.Length; i++) {
					index++;
					index %= worlds.Length;
				}
				currentWorld = (WorldType)index;
			}

			RefereshCurrent();
		}

		public void RefereshCurrent() {
			currentBuildings = worlds[(int)currentWorld].Item2.buildings;
			currentStreats = worlds[(int)currentWorld].Item2.streats[Random.Range(0, worlds[(int)currentWorld].Item2.streats.Length)];
			currentRiver = worlds[(int)currentWorld].Item2.rivers[Random.Range(0, worlds[(int)currentWorld].Item2.rivers.Length)];
		}

		void InitWorld() {
			worldConverter[] worldC = new worldConverter[(int)WorldType.SIZE];

			for(int i = 0; i < (int)WorldType.SIZE; i++) {
				worldC[i] = new worldConverter();
				worldC[i].buildings = new List<GameObject>[(int)BuildingType.SIZE];
				for(int j = 0; j < (int)BuildingType.SIZE; j++) {
					worldC[i].buildings[j] = new List<GameObject>();
				}
				worldC[i].streats = new List<GameObject[][]>();
				worldC[i].river = new List<GameObject[][]>();
			}

			foreach(var it in buildings) {
				worldC[(int)it.world].buildings[(int)it.type].Add(it.prefab);
			}

			foreach(var it in streats) {
				worldC[(int)it.world].streats.Add(it.prefabs);
			}

			foreach(var it in rivers) {
				worldC[(int)it.world].river.Add(it.prefabs);
			}

			buildings = null;
			streats = null;
			rivers = null;

			for(int i = 0; i < worlds.Length; i++) {
				worlds[i].Item1 = false;
				worlds[i].Item2 = new world();
				worlds[i].Item2.buildings = new GameObject[worldC[i].buildings.Length][];
				for(int j = 0; j < worlds[i].Item2.buildings.Length; j++) {
					worlds[i].Item2.buildings[j] = worldC[i].buildings[j].ToArray();
				}

				worlds[i].Item2.streats = worldC[i].streats.ToArray();
				worlds[i].Item2.rivers = worldC[i].river.ToArray();
			}
		}
	}
}