using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FloodRacer {
	public class DDOL : MonoBehaviour {
		public static bool isLoaded { get; private set; } = false;

		[SerializeField] string nextSzene;

		private void Awake() {
			DontDestroyOnLoad(this.gameObject);
			isLoaded = true;

			if(nextSzene != null)
				SceneManager.LoadScene(nextSzene);

			Destroy(this);
		}
	}
}