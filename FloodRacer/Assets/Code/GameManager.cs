using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	[SerializeField] GameObject[] p_levels;
	[SerializeField] GameObject p_startLevel;
	[SerializeField] float m_levelLength = 200;

	int m_levelCount = 0;

	Queue<GameObject> m_loadedLevels = new Queue<GameObject>();

	void Start() {
		if(p_levels == null) {
			Debug.LogError("no Levels to select");
			Destroy(this);
			return;
		}
		if(p_startLevel == null) {
			Debug.LogError("no start level selected");
			Destroy(this);
			return;
		}

		DontDestroyOnLoad(this.gameObject);

		m_loadedLevels.Enqueue(Instantiate(p_startLevel));
	}

	void FixedUpdate() {
		if(Controle.s_instance.transform.position.z > (m_levelCount * m_levelLength - m_levelLength/2)) {
			if(m_loadedLevels.Count > 2)
				Destroy(m_loadedLevels.Dequeue());

			GameObject tmp = Instantiate(p_levels[Random.Range(0, p_levels.Length - 1)]);
			tmp.transform.position = new Vector3(0, 0, m_levelCount * m_levelLength);

			m_loadedLevels.Enqueue(tmp);

			m_levelCount++;
		}
	}
}
