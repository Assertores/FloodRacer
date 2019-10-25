using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToVelocity : MonoBehaviour {

	[SerializeField] Rigidbody m_rb;

	void Start() {
		if(!m_rb) {
			Debug.LogError("no RigitBoddy");
			Destroy(this);
			return;
		}
	}

	// Update is called once per frame
	void Update() {
		if(m_rb.velocity.magnitude < 0.01f)
			return;

		transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(m_rb.velocity), transform.rotation, 0.5f);
	}
}
