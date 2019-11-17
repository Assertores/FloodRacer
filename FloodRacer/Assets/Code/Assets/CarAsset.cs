using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "Car", menuName = "Game/Car")]
	public class CarAsset : ScriptableObject {
		[Header("References")]
		public string label;
		GameObject[] skins;
		[HideInInspector] public Tuple<bool, GameObject>[] carSkins;
		[Header("Balancing")]
		public AnimationCurve acceleration;
		public float accelerationScalar = 100;
		public AnimationCurve stearing;
		public float stearingScalar = 100;
		public AnimationCurve tsunami;

		public CarAsset() {
			carSkins = new Tuple<bool, GameObject>[skins.Length];
			for(int i = 0; i < skins.Length; i++) {
				carSkins[i].Item2 = skins[i];
			}
		}
	}
}