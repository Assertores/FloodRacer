using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FloodRacer {
	public class SaveStart : MonoBehaviour {

		private void Awake() {
			if(!DDOL.isLoaded)
				SceneManager.LoadScene(StringCollection.S_CONST);
		}
	}
}