using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data {
	public string level;
	public List<Vector3> path = new List<Vector3>();
	public List<Vector3> crashes = new List<Vector3>();
	public Vector3 death = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
}

[System.Serializable]

struct JsonWrapper {
	public Data[] backingArray;
}
public class LogCreator : Singleton<LogCreator> {

	[SerializeField] Sprite m_crashSprite;
	[SerializeField] Sprite m_deathSprite;

	List<Data> m_log = new List<Data>();
	public Data CurrentLog { get; private set; } = new Data();

	private void Start() {
		MenuHandler.StartGame += StartLog;
		MenuHandler.ActivateMenu += StopLog;
		GameManager.EnterNewLevel += NextLevel;
	}

	private void OnDestroy() {
		MenuHandler.StartGame -= StartLog;
		MenuHandler.ActivateMenu -= StopLog;
		GameManager.EnterNewLevel -= NextLevel;
	}

	void StartLog() {
		m_log = new List<Data>();
		CurrentLog = new Data();
		m_log.Add(CurrentLog);
		doLog = true;
	}

	void StopLog() {
		doLog = false;
		JsonWrapper jsonWrapper = new JsonWrapper();
		jsonWrapper.backingArray = m_log.ToArray();
		string json = JsonUtility.ToJson(jsonWrapper);

		System.IO.Directory.CreateDirectory(Application.dataPath + "/LOG");
		System.IO.File.WriteAllText(Application.dataPath + "/LOG/" + System.DateTime.Now.ToFileTime() + ".log", json);
	}

	void NextLevel() {
		CurrentLog = new Data();
		m_log.Add(CurrentLog);
		CurrentLog.level = GameManager.s_instance.m_currentLevelName;
	}

	public bool doLog { get; private set; } = false;
	private void FixedUpdate() {
		if(doLog) {
			CurrentLog.path.Add(Controle.s_instance.transform.position);
			CurrentLog.path[CurrentLog.path.Count - 1] -= new Vector3(0, 0, GameManager.s_instance.m_currentZero);
		}
	}

	public static void ShowLog(string key, string path, GameObject parent = null) {
		if(parent == null) {
			parent = new GameObject("LOG_" + key);
		}

		foreach(var file in System.IO.Directory.GetFiles(path, "*.log")) {
			JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(System.IO.File.ReadAllText(file));
			foreach(var it in wrapper.backingArray) {
				if(it.level == key) {
					LineRenderer line = new GameObject("path").AddComponent<LineRenderer>();
					line.transform.parent = parent.transform;
					line.positionCount = it.path.Count;
					line.SetPositions(it.path.ToArray());

					foreach(var jt in it.crashes) {
						GameObject tmp = new GameObject("crash");
						line = tmp.AddComponent<LineRenderer>();
						line.SetPositions(new Vector3[] {new Vector3(0,0,0), new Vector3(0,100,0) });
						line.useWorldSpace = false;
						line.startColor = Color.cyan;
						line.endColor = Color.cyan;
						tmp.transform.parent = parent.transform;
						tmp.transform.position = jt;
						//tmp.AddComponent<SpriteRenderer>().sprite = s_instance.m_crashSprite;
					}

					if(it.death.y < float.MaxValue) {
						GameObject tmp = new GameObject("death");
						tmp.transform.parent = parent.transform;
						tmp.transform.position = it.death;
						line = tmp.AddComponent<LineRenderer>();
						line.SetPositions(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 100, 0) });
						line.useWorldSpace = false;
						line.startColor = Color.red;
						line.endColor = Color.red;
					}
				}
			}
		}
	}
}
