using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	[CreateAssetMenu(fileName = "Car", menuName = "Game/Car")]
	public class Car : ScriptableObject {
		[Header("References")]
		public GameObject[] skins;
		[Header("Balancing")]
		public AnimationCurve acceleration;
		public float accelerationScalar = 100;
		public AnimationCurve stearing;
		public float stearingScalar = 100;
		public AnimationCurve tsunami;
	}
}