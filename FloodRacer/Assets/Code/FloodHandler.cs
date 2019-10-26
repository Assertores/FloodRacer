using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodHandler : Singleton<FloodHandler> {

	[SerializeField] AnimationCurve m_acelleration;
	[SerializeField] float m_maxDistToPlayer = 100;

	private void Update() {
		
		transform.position += new Vector3(0, 0, m_acelleration.Evaluate(Time.timeSinceLevelLoad) * Time.deltaTime);
	}

	private void FixedUpdate() {
		if(Mathf.Abs(Controle.s_instance.transform.position.z - transform.position.z) > m_maxDistToPlayer) {
			transform.position = new Vector3(0, 0, Controle.s_instance.transform.position.z - m_maxDistToPlayer);
		}
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
		anim.Play();
	}
}
