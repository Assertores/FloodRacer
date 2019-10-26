using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : Singleton<MenuHandler> {

	[SerializeField] TextMeshProUGUI r_bestScore;
	[SerializeField] TextMeshProUGUI r_lastScore;

	float m_bestScore;
	float m_lastScore;

	void Start() {
		if(!r_bestScore) {
			Debug.LogError("no best score reference set");
			Destroy(this);
			return;
		}
		if(!r_lastScore) {
			Debug.LogError("no last score reference set");
			Destroy(this);
			return;
		}

		m_bestScore = PlayerPrefs.GetFloat("m_bestScore");

		r_bestScore.text = m_bestScore.ToString("F2") + "m";
		r_lastScore.text = m_lastScore.ToString("F2") + "m";
		Time.timeScale = 0;
	}

	public void FinishLevel(float score) {
		gameObject.SetActive(true);
		StartCoroutine(IESlowDown(1));

		m_lastScore = score;
		if(m_lastScore > m_bestScore) {
			m_bestScore = m_lastScore;
			PlayerPrefs.SetFloat("m_bestScore", m_bestScore);
		}

		r_bestScore.text = m_bestScore.ToString("F2") + "m";
		r_lastScore.text = m_lastScore.ToString("F2") + "m";
	}

	public void StartLevel() {
		Time.timeScale = 1;
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
		m_bestScore = 0;
		m_lastScore = 0;

		r_bestScore.text = m_bestScore.ToString("F2") + "m";
		r_lastScore.text = m_lastScore.ToString("F2") + "m";
	}

	IEnumerator IESlowDown(float duration) {
		float startTime = Time.unscaledTime;

		while(startTime + duration > Time.unscaledTime) {
			Time.timeScale = Mathf.Lerp(1, 0, (Time.unscaledTime - startTime) / duration);

			yield return null;
		}
	}
}
