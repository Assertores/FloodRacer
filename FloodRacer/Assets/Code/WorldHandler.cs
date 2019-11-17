﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	public enum WorldType { NON, MANHATTAN, SAN_FRANCISCO, NEW_ORLEANS, TOKIO, BRASILIEN, AGYPTEN, CHINA_TOWN, FUTURE_TOWN, SIZE };

	public enum BuildingType { NON, SPETIAL, R1X1, R1X2, R2X2, L2X2, R5X5, SIZE };

	public enum StreatType { NON, STARIGHT, CORNER, T, CORS, SIZE }

	public enum RiverType { NON, STRAIGHT, CORNER, T, BRIDGE, SIZE }

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
		[SerializeField] Building[] buildings;
		[SerializeField] Streat[] streats;
		[SerializeField] River[] rivers;

		world[] worlds = new world[(int)WorldType.SIZE];

		private void Start() {
			InitWorld();
		}

		void InitWorld() {
			worldConverter[] worldC = new worldConverter[(int)WorldType.SIZE];

			foreach(var it in buildings) {
				if(worldC[(int)it.world].buildings == null) {
					worldC[(int)it.world].buildings = new List<GameObject>[(int)BuildingType.SIZE];
					for(int i = 0; i < (int)BuildingType.SIZE; i++) {
						worldC[(int)it.world].buildings[i] = new List<GameObject>();
					}
				}

				worldC[(int)it.world].buildings[(int)it.type].Add(it.prefab);
			}

			foreach(var it in streats) {
				if(worldC[(int)it.world].streats == null) {
					worldC[(int)it.world].streats = new List<GameObject[][]>();
				}
				worldC[(int)it.world].streats.Add(it.prefabs);
			}

			foreach(var it in rivers) {
				if(worldC[(int)it.world].river == null) {
					worldC[(int)it.world].river = new List<GameObject[][]>();
				}
				worldC[(int)it.world].river.Add(it.prefabs);
			}

			for(int i = 0; i < worlds.Length; i++) {
				worlds[i].buildings = new GameObject[worldC[i].buildings.Length][];
				for(int j = 0; j < worlds[i].buildings.Length; j++) {
					worlds[i].buildings[j] = worldC[i].buildings[j].ToArray();
				}

				worlds[i].streats = worldC[i].streats.ToArray();
				worlds[i].rivers = worldC[i].river.ToArray();
			}
		}
	}
}