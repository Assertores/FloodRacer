using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodHandler : MonoBehaviour {

	[SerializeField] float m_floodaccseleration = 10;
	[SerializeField] float m_startSpeed = 100;

	private void Update() {
		transform.position += new Vector3(0, 0,( m_floodaccseleration * Time.timeSinceLevelLoad + m_startSpeed) * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other) {
		print("1 " + other.tag + " : " + other.transform.root.tag);
		if(other.transform.root.tag == StringCollection.T_PLAYER) {
			if(MenuHandler.Exists())
				MenuHandler.s_instance.FinishLevel(Controle.s_instance.transform.position.z);
		}
	}
}
