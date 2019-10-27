using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Controle : Singleton<Controle> {

	[SerializeField] Rigidbody r_rb;
	[SerializeField] AnimationCurve m_exelerationDump;
	[SerializeField] float m_exelerationMultiplyer;
	[SerializeField] AnimationCurve m_turningForce;
	[SerializeField] float m_turningForceMultiplyer;
	[SerializeField] Transform r_car;
	[SerializeField] AnimationCurve m_tsunamiNearingExeleration;

	[SerializeField] TextMeshProUGUI r_score;
	[SerializeField] TextMeshProUGUI r_tsunamiDist;

	[SerializeField] AudioSource r_source;
	[SerializeField] AudioSource r_crash;
	[SerializeField] AudioClip m_speedUp;
	[SerializeField] AudioClip m_break;

	Vector2 inputDir;
	void Start() {
		if(!r_rb) {
			Debug.LogError("no rigitbody");
			Destroy(this);
			return;
		}
		if(!r_source) {
			Debug.LogError("no audio souce for car");
			Destroy(this);
			return;
		}
		if(!r_crash) {
			Debug.LogError("no crash audio source for car");
			Destroy(this);
			return;
		}
		if(!m_speedUp) {
			Debug.LogError("no speed up sound set");
			Destroy(this);
			return;
		}
		if(!m_break) {
			Debug.LogError("no break sound set");
			Destroy(this);
			return;
		}
		//m_rb.velocity = new Vector3(0, 0, 1);

		MenuHandler.ActivateMenu += OnLoss;
		MenuHandler.StartGame += OnLevelStart;
	}

	private void OnDestroy() {
		print("i got destroied");
	}

	void OnLevelStart() {
		transform.position = new Vector3(0, 0, 0);
		r_car.rotation = Quaternion.LookRotation(transform.forward);
		r_source.enabled = true;
		r_crash.enabled = true;
		r_rb.isKinematic = false;
	}

	void OnLoss() {
		r_source.enabled = false;
		r_crash.enabled = false;
		r_rb.isKinematic = true;
	}

	void Update() {
		r_score.text = transform.position.z.ToString("F2") + "m";
		float dist = transform.position.z -FloodHandler.s_instance.transform.position.z;
		if(dist < 98)
			r_tsunamiDist.text = dist.ToString("F2") + "m";
		else
			r_tsunamiDist.text = "Far Away";

		inputDir = new Vector2(Input.GetAxis(StringCollection.A_VERTICAL), Input.GetAxis(StringCollection.A_HORIZONTAL)).normalized;
		Vector2 vel = new Vector2(r_rb.velocity.z, r_rb.velocity.x);

		if(inputDir.magnitude < 0.01f)
			return;
		if(vel.magnitude < 0.01f)
			vel = new Vector2(transform.forward.z, transform.forward.x) * 0.2f;

		float alongVel = Vector2.Dot(inputDir, vel.normalized) * m_exelerationMultiplyer;
		float ortogonalVel = Vector2.Dot(inputDir, new Vector2(-vel.y, vel.x).normalized) * m_turningForceMultiplyer;

		alongVel *= m_exelerationDump.Evaluate(vel.magnitude) * m_tsunamiNearingExeleration.Evaluate(dist);
		ortogonalVel *= m_turningForce.Evaluate(vel.magnitude);

		Vector2 a = alongVel * vel.normalized;
		Vector2 o = ortogonalVel * new Vector2(-vel.y, vel.x).normalized;

		Vector2 acselleration = a+o;

		r_rb.velocity += new Vector3(acselleration.y,0,acselleration.x) * Time.deltaTime;

		r_car.rotation = Quaternion.LookRotation(new Vector3(acselleration.y, 0, acselleration.x));


		Debug.DrawRay(new Vector3(0, 0, -10) + new Vector3(o.y, 0, o.x), new Vector3(a.y, 0, a.x), Color.green);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(o.y, 0, o.x), Color.blue);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(vel.y, 0, vel.x), Color.magenta);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(inputDir.y, 0, inputDir.x), Color.red);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(acselleration.y, 0, acselleration.x), Color.cyan);
	}

	private void OnCollisionEnter(Collision collision) {
		if(!r_crash.isPlaying)
			r_crash.Play();
	}
}
