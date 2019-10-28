using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	[SerializeField] GameObject p_startLevel;

	public static System.Action EnterNewLevel;

	int m_levelCount = 0;

	LevelHandler m_currentLevel;
	public string m_currentLevelName { get; private set; }
	public float m_currentZero { get; private set; } = 0;
	float m_currentLength;

	Queue<GameObject> m_loadedLevels = new Queue<GameObject>();

	void Start() {
		if(p_startLevel == null) {
			Debug.LogError("no start level selected");
			Destroy(this);
			return;
		}
		if(!p_startLevel.GetComponent<LevelHandler>()) {
			Debug.LogError("start level has no level handler atached");
			Destroy(this);
			return;
		}

		GameObject tmp = Instantiate(p_startLevel);
		m_currentLevel = CheckNextLevel(tmp);
		m_currentLength = m_currentLevel.m_levelLength;

		m_loadedLevels.Enqueue(tmp);
	}


	int h_random;
	string h_name;
	void FixedUpdate() {
		if(Controle.s_instance.transform.position.z > (m_currentLevel.transform.position.z + m_currentLevel.m_levelLength/2)) {
			if(m_loadedLevels.Count > 2)
				Destroy(m_loadedLevels.Dequeue());

			h_random = Random.Range(0, m_currentLevel.p_nextLevels.Length);
			GameObject tmp = Instantiate(m_currentLevel.p_nextLevels[h_random]);
			
			tmp.transform.position = new Vector3(0, 0, m_currentLevel.transform.position.z + m_currentLevel.m_levelLength);
			h_name = m_currentLevel.p_nextLevels[h_random].name;

			m_currentLevel = CheckNextLevel(tmp);
			m_loadedLevels.Enqueue(tmp);
		}if(Controle.s_instance.transform.position.z > m_currentZero + m_currentLength) {
			m_currentZero = m_currentLevel.transform.position.z;
			m_currentLevelName = h_name;
			m_currentLength = m_currentLevel.m_levelLength;
			EnterNewLevel?.Invoke();
		}
	}

	LevelHandler CheckNextLevel(GameObject target) {
		target.name = "LevelPart: " + m_levelCount;
		m_levelCount++;

		LevelHandler value = target.GetComponent<LevelHandler>();

		if(value == null) {
			Debug.LogError("next level has no LevelHandler");
			return null;
		}
		if(value.m_levelLength <= 0) {
			Debug.LogError("next level has no length");
			return null;
		}
		if(value.p_nextLevels == null) {
			Debug.LogError("next level has no next levels");
			return null;
		}
		foreach(var it in value.p_nextLevels) {
			if(it == null) {
				Debug.LogError("an element in the next level list is missing");
				return null;
			}
			if(!it.GetComponent<LevelHandler>()) {
				Debug.LogError("the level " + it.name + " in next levels has no level handler");
				return null;
			}
		}

		return value;
	}
}
