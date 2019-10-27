using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animation))]
public class CollapsBuilding : MonoBehaviour {

	[SerializeField] Animation anim;

	private void Start() {
		if(anim == null)
			anim = GetComponent<Animation>();
	}

	bool h_collapsing = false;
	private void FixedUpdate() {
		if(!h_collapsing && transform.position.z < FloodHandler.s_instance.transform.position.z) {
			h_collapsing = true;
			StartCoroutine(IEPlayAnim());
		}

		IEnumerator IEPlayAnim() {
			yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
			anim.Play();
			Destroy(this);
		}
	}
}
