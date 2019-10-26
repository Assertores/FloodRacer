using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConstBootleg : MonoBehaviour {

	[SerializeField] string m_nextScene;

	void Start() {
		DontDestroyOnLoad(this.gameObject);
		Destroy(this);
		SceneManager.LoadScene(m_nextScene);
	}
}
