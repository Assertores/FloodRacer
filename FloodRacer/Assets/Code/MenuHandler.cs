using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class MenuHandler : Singleton<MenuHandler> {

	[SerializeField] TextMeshProUGUI r_bestScore;
	[SerializeField] TextMeshProUGUI r_lastScore;

	[SerializeField] AudioMixer p_mixer;

	public static System.Action ActivateMenu;
	public static System.Action StartGame;

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

		ActivateMenu?.Invoke();
	}

	public void FinishLevel(float score) {
		ActivateMenu?.Invoke();

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

		StartGame?.Invoke();
		gameObject.SetActive(false);

		Time.timeScale = 1;
		
		SceneManager.LoadScene(StringCollection.S_INGAME);
	}

	public void Quit() {
		Application.Quit();
	}

	bool h_isMuted;
	public void Mute() {
		h_isMuted = !h_isMuted;
		p_mixer.SetFloat(StringCollection.G_MASTER, h_isMuted ? -80 : 0);
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

		Time.timeScale = 0;
	}
}
