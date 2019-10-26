using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodHandler : MonoBehaviour {

	[SerializeField] AnimationCurve m_acelleration;

	private void Update() {
		transform.position += new Vector3(0, 0, m_acelleration.Evaluate(Time.timeSinceLevelLoad) * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other) {
		print("1 " + other.tag + " : " + other.transform.root.tag);
		if(other.transform.root.tag == StringCollection.T_PLAYER) {
			if(MenuHandler.Exists())
				MenuHandler.s_instance.FinishLevel(Controle.s_instance.transform.position.z);
		}else if(other.transform.root.tag == StringCollection.T_BUILDINGS) {
			Animation tmp = other.transform.root.GetComponent<Animation>();
			if(tmp == null) {
				Debug.LogError("building " + other.transform.root.gameObject.name + " has no animation");
				return;
			}

			tmp.Play();
		}
	}
}
