using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle : Singleton<Controle> {

	[SerializeField] Rigidbody r_rb;
	[SerializeField] AnimationCurve m_exelerationDump;
	[SerializeField] float m_exelerationMultiplyer;
	[SerializeField] AnimationCurve m_turningForce;
	[SerializeField] float m_turningForceMultiplyer;
	[SerializeField] Transform r_car;

	Vector2 inputDir;
	void Start() {
		if(!r_rb) {
			Debug.LogError("no rigitbody");
			Destroy(this);
			return;
		}
		//m_rb.velocity = new Vector3(0, 0, 1);
	}

	void Update() {
		inputDir = new Vector2(Input.GetAxis(StringCollection.A_VERTICAL), Input.GetAxis(StringCollection.A_HORIZONTAL)).normalized;
		Vector2 vel = new Vector2(r_rb.velocity.z, r_rb.velocity.x);

		if(inputDir.magnitude < 0.01f)
			return;
		if(vel.magnitude < 0.01f)
			vel = new Vector2(transform.forward.z, transform.forward.x) * 0.2f;

		float alongVel = Vector2.Dot(inputDir, vel.normalized) * m_exelerationMultiplyer;
		float ortogonalVel = Vector2.Dot(inputDir, new Vector2(-vel.y, vel.x).normalized) * m_turningForceMultiplyer;

		alongVel *= m_exelerationDump.Evaluate(vel.magnitude);
		ortogonalVel *= m_turningForce.Evaluate(vel.magnitude);

		Vector2 a = alongVel * vel.normalized;
		Vector2 o = ortogonalVel * new Vector2(-vel.y, vel.x).normalized;

		Vector2 acselleration = a+o;

		//Debug.Log(vel.magnitude);
		Debug.DrawRay(new Vector3(0, 0, -10) + new Vector3(o.y, 0, o.x), new Vector3(a.y,0,a.x), Color.green);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(o.y,0,o.x), Color.blue);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(vel.y, 0, vel.x), Color.magenta);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(inputDir.y, 0, inputDir.x), Color.red);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(acselleration.y, 0, acselleration.x), Color.cyan);

		r_rb.velocity += new Vector3(acselleration.y,0,acselleration.x) * Time.deltaTime;

		r_car.rotation = Quaternion.LookRotation(new Vector3(acselleration.y, 0, acselleration.x));
	}
}
