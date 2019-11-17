using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	public static class StringCollection {
		//===== ===== Scenes ===== =====
		public const string S_CONST = "Const";
		public const string S_INGAME = "InGame";
		public const string S_MENU = "Menu";
		public const string S_SHOP = "Shop";

		//===== ===== PlayerPrefs ===== =====
		public const string PP_WORLDS = "Worlds";
		public const string PP_CARS = "Cars";

		//===== ===== Axis ===== =====
		public const string A_HORIZONTAL = "Horizontal";
		public const string A_VERTICAL = "Vertical";

		//===== ===== enum to string ===== =====
		public static string WorldTypeToString(WorldType type) {
			switch(type) {
			case WorldType.MANHATTAN:
				return "Manhattan";
			case WorldType.SAN_FRANCISCO:
				return "San francisco";
			case WorldType.NEW_ORLEANS:
				return "New Orleans";
			case WorldType.TOKIO:
				return "Tokio";
			case WorldType.BRASILIEN:
				return "Brasilien";
			case WorldType.AGYPTEN:
				return "Ägypten";
			case WorldType.CHINA_TOWN:
				return "China Town";
			case WorldType.FUTURE_TOWN:
				return "Future Town";
			case WorldType.SIZE:
			default:
				return "";
			}
		}

		public static string BuildingTypeToString(BuildingType type) {
			switch(type) {
			case BuildingType.SPETIAL:
				return "Spetial";
			case BuildingType.R1X1:
				return "1 by 1";
			case BuildingType.R1X2:
				return "1 by 2";
			case BuildingType.R2X2:
				return "2 by 2";
			case BuildingType.L2X2:
				return "2 by 2 L-shape";
			case BuildingType.R5X5:
				return "5 by 5";
			case BuildingType.SIZE:
			default:
				return "";
			}
		}

		public static string StreatTypeToString(StreatType type) {
			switch(type) {
			case StreatType.STARIGHT:
				return "Straight";
			case StreatType.CORNER:
				return "Corner";
			case StreatType.T:
				return "T";
			case StreatType.CORS:
				return "Corossing";
			case StreatType.SIZE:
			default:
				return "";
			}
		}

		public static string RiverTypeToString(RiverType type) {
			switch(type) {
			case RiverType.STRAIGHT:
				return "Straight";
			case RiverType.CORNER:
				return "Corner";
			case RiverType.T:
				return "T";
			case RiverType.BRIDGE:
				return "Bridge";
			case RiverType.SIZE:
			default:
				return "";
			}
		}
	}
}