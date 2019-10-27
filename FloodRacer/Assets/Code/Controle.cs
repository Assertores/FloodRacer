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

	[SerializeField] GameObject r_ui;
	[SerializeField] TextMeshProUGUI r_score;
	[SerializeField] Color m_overHighScoreColor;
	[SerializeField] TextMeshProUGUI r_tsunamiDist;
	[SerializeField] Gradient m_color;
	Color m_normalColor;
	Color m_scoreStartColor;

	[SerializeField] AudioSource r_skid;
	[SerializeField] AudioSource r_crash;
	[SerializeField] AudioClip[] m_crashs;
	[SerializeField] float m_angle = 40.0f;

	Vector2 inputDir;
	void Start() {
		if(!r_rb) {
			Debug.LogError("no rigitbody");
			Destroy(this);
			return;
		}
		if(!r_skid) {
			Debug.LogError("no audio souce for car");
			Destroy(this);
			return;
		}
		if(!r_crash) {
			Debug.LogError("no crash audio source for car");
			Destroy(this);
			return;
		}
		if(m_crashs == null || m_crashs.Length == 0) {
			Debug.LogError("no chrash sounds set");
			Destroy(this);
			return;
		}
		//m_rb.velocity = new Vector3(0, 0, 1);

		m_normalColor = m_color.Evaluate(1);
		m_scoreStartColor = r_score.color;

		MenuHandler.ActivateMenu += OnLoss;
		MenuHandler.StartGame += OnLevelStart;
	}

	private void OnDestroy() {
		print("i got destroied");
	}

	void OnLevelStart() {
		r_ui.SetActive(true);
		transform.position = new Vector3(0, 0, 0);
		r_car.rotation = Quaternion.LookRotation(transform.forward);
		r_crash.enabled = true;
		r_rb.isKinematic = false;
		r_score.color = m_scoreStartColor;
	}

	void OnLoss() {
		r_ui.SetActive(false);
		r_skid.enabled = false;
		r_crash.enabled = false;
		r_rb.isKinematic = true;
	}

	void Update() {
		r_score.text = transform.position.z.ToString("F2") + "m";
		if(transform.position.z > MenuHandler.s_instance.m_bestScore)
			r_score.color = m_overHighScoreColor;

		float dist = transform.position.z -FloodHandler.s_instance.transform.position.z;
		if(dist < FloodHandler.s_instance.maxDist - 2) {
			r_tsunamiDist.text = "! " + dist.ToString("F2") + "m !";
			r_tsunamiDist.color = m_color.Evaluate(dist/(FloodHandler.s_instance.maxDist - 2));
		} else {
			r_tsunamiDist.text = "Far Away";
			r_tsunamiDist.color = m_normalColor;
		}

		inputDir = new Vector2(Input.GetAxis(StringCollection.A_VERTICAL), Input.GetAxis(StringCollection.A_HORIZONTAL)).normalized;
		if(Input.touchCount > 0) {
			Touch touch = Input.GetTouch(0);

			inputDir = (touch.position - new Vector2(Screen.width / 2, Screen.height / 2)).normalized;
			inputDir = new Vector2(inputDir.y, inputDir.x);
		}

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

		float angle = Vector2.Angle(vel, acselleration);

		Debug.Log(angle);

		r_rb.velocity += new Vector3(acselleration.y,0,acselleration.x) * Time.deltaTime;

		r_car.rotation = Quaternion.LookRotation(new Vector3(acselleration.y, 0, acselleration.x));

		if(angle > m_angle) {
			r_skid.enabled = true;
		} else {
			r_skid.enabled = false;
		}

		Debug.DrawRay(new Vector3(0, 0, -10) + new Vector3(o.y, 0, o.x), new Vector3(a.y, 0, a.x), Color.green);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(o.y, 0, o.x), Color.blue);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(vel.y, 0, vel.x), Color.magenta);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(inputDir.y, 0, inputDir.x), Color.red);
		Debug.DrawRay(new Vector3(0, 0, -10), new Vector3(acselleration.y, 0, acselleration.x), Color.cyan);
	}

	private void OnCollisionEnter(Collision collision) {
		if(!r_crash.isPlaying)
			r_crash.clip = m_crashs[Random.Range(0, m_crashs.Length)];
			r_crash.Play();
	}
}
