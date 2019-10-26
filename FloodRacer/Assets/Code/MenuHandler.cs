using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {

	float m_bestScore;
	float m_lastScore;
	void Start() {
		m_bestScore = PlayerPrefs.GetFloat("m_bestScore");
	}

	public void FinishLevel() {
		m_lastScore = Controle.s_instance.transform.position.z;
		if(m_lastScore > m_bestScore) {
			m_bestScore = m_lastScore;
			PlayerPrefs.SetFloat("m_bestScore", m_bestScore);
		}
	}

	public void StartLevel() {
		gameObject.SetActive(false);
		SceneManager.LoadScene(StringCollection.S_INGAME);
	}

	public void Quit() {
		Application.Quit();
	}

	public void Mute() {
		//TODO: mute Master
	}

	public void ResetScore() {
		PlayerPrefs.SetFloat("m_bestScore", 0);
	}
}
