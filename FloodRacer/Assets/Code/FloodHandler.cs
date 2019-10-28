using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodHandler : Singleton<FloodHandler> {

	[SerializeField] AnimationCurve m_acelleration;
	[SerializeField] float m_maxDistToPlayer = 100;
	[SerializeField] AudioSource r_source;

	public float maxDist {
		get {
			return m_maxDistToPlayer;
		}
	}

	float pos;
	private void Start() {
		pos = transform.position.z;

		MenuHandler.StartGame += DoRestart;
	}

	void DoRestart() {
		transform.position = new Vector3(0,0, pos);
		h_death = false;
	}

	private void Update() {
		
		transform.position += new Vector3(0, 0, m_acelleration.Evaluate(Time.timeSinceLevelLoad) * Time.deltaTime);
	}

	bool h_death = false;
	private void FixedUpdate() {
		float dist = Controle.s_instance.transform.position.z - transform.position.z;
		if(dist > m_maxDistToPlayer) {
			transform.position = new Vector3(0, 0, Controle.s_instance.transform.position.z - m_maxDistToPlayer);
		}

		if(!h_death && dist < 0) {
			h_death = true;
			MenuHandler.s_instance.FinishLevel(Controle.s_instance.transform.position.z);
#if UNITY_EDITOR
			if(LogCreator.s_instance.doLog) {
				LogCreator.s_instance.CurrentLog.death = new Vector3(Controle.s_instance.transform.position.x, Controle.s_instance.transform.position.y, Controle.s_instance.transform.position.z - GameManager.s_instance.m_currentZero);
			}
#endif
		}

		r_source.volume = Mathf.Lerp(1, 0, dist / m_maxDistToPlayer);
	}
}
