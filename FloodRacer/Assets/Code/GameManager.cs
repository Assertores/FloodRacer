using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	[SerializeField] GameObject p_startLevel;

	[SerializeField] GameObject r_flood;
	[SerializeField] float m_floodaccseleration = 10;

	int m_levelCount = 0;

	LevelHandler m_currentLevel;

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
		if(!r_flood) {
			Debug.LogError("no flood reference set");
			Destroy(this);
			return;
		}

		GameObject tmp = Instantiate(p_startLevel);
		m_currentLevel = CheckNextLevel(tmp);

		m_loadedLevels.Enqueue(tmp);
	}

	private void Update() {
		r_flood.transform.position += new Vector3(0, 0, Mathf.Sqrt(1 / m_floodaccseleration * Time.timeSinceLevelLoad) * Time.deltaTime);
	}

	void FixedUpdate() {
		if(Controle.s_instance.transform.position.z > (m_currentLevel.transform.position.z + m_currentLevel.m_levelLength/2)) {
			if(m_loadedLevels.Count > 2)
				Destroy(m_loadedLevels.Dequeue());

			GameObject tmp = Instantiate(m_currentLevel.p_nextLevels[Random.Range(0, m_currentLevel.p_nextLevels.Length)]);
			tmp.transform.position = new Vector3(0, 0, m_currentLevel.transform.position.z + m_currentLevel.m_levelLength);
			m_currentLevel = CheckNextLevel(tmp);
			m_loadedLevels.Enqueue(tmp);
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
			if(!it.GetComponent<LevelHandler>()) {
				Debug.LogError("the level " + it.name + " in next levels has no level handler");
				return null;
			}
		}

		return value;
	}
}
