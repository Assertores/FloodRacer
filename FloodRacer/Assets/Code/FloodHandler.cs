using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodHandler : Singleton<FloodHandler> {

	[SerializeField] AnimationCurve m_acelleration;
	[SerializeField] float m_maxDistToPlayer = 100;
	[SerializeField] AudioSource r_source;

	float pos;
	private void Start() {
		pos = transform.position.z;

		MenuHandler.StartGame += DoRestart;
	}

	void DoRestart() {
		transform.position = new Vector3(0,0, pos);
	}

	private void Update() {
		
		transform.position += new Vector3(0, 0, m_acelleration.Evaluate(Time.timeSinceLevelLoad) * Time.deltaTime);
	}

	private void FixedUpdate() {
		float dist = Mathf.Abs(Controle.s_instance.transform.position.z - transform.position.z);
		if(dist > m_maxDistToPlayer) {
			transform.position = new Vector3(0, 0, Controle.s_instance.transform.position.z - m_maxDistToPlayer);
		}

		r_source.volume = Mathf.Lerp(1, 0, dist / m_maxDistToPlayer);
	}

	private void OnTriggerEnter(Collider other) {
		print("trigger " + other.tag);
		if(other.tag == StringCollection.T_PLAYER) {
			if(MenuHandler.Exists())
				MenuHandler.s_instance.FinishLevel(Controle.s_instance.transform.position.z);
		}else if(other.tag == StringCollection.T_BUILDINGS) {
			print(true);
			Animation tmp = other.GetComponent<Animation>();
			if(tmp == null) {
				Debug.LogError("building " + other.gameObject.name + " has no animation");
				return;
			}

			StartCoroutine(IEPlayAnim(tmp));
		}
	}

	IEnumerator IEPlayAnim(Animation anim) {
		yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
		if(anim)
			anim.Play();
	}
}
