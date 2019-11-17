using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloodRacer {
	public class Car : MonoBehaviour {
		[Header("References")]
		[SerializeField] Rigidbody rb;
		[SerializeField] Transform car;

		[Header("Balancing")]
		[SerializeField] CarAsset[] carAssets;

		Tuple<bool, CarAsset>[] cars;

		void Start() {
			if(rb == null) {
				Debug.LogError("rigitbody for car not set");
			}

			InitCars();
			ProgressHandler.LoadProgress(StringCollection.PP_CARS, cars);
			for(int i = 0; i < cars.Length; i++) {
				ProgressHandler.LoadProgress(StringCollection.PP_CARS + cars[i].Item2.label, cars[i].Item2.carSkins);
			}
			cars[0].Item1 = true;
			cars[0].Item2.carSkins[0].Item1 = true;
		}
		private void OnDestroy() {
			ProgressHandler.SaveProgress(StringCollection.PP_CARS, cars);
			for(int i = 0; i < cars.Length; i++) {
				ProgressHandler.SaveProgress(StringCollection.PP_CARS + cars[i].Item2.label, cars[i].Item2.carSkins);
			}
		}

		// Update is called once per frame
		void Update() {
			Vector3 inDir = CalcInput();

			Vector3 vel = rb.velocity;
			if(vel.magnitude < 0.01f) {
				vel = new Vector3(0, 0, 1);
			}
			if(inDir.magnitude < 0.01f) {
				inDir = vel;
			}

			Vector3 acceleration = CalcAceleration(inDir, vel, cars[0].Item2);//TODO: get set car

			rb.velocity += acceleration * Time.deltaTime;

			car.rotation = Quaternion.LookRotation(acceleration);
		}

		void InitCars() {
			cars = new Tuple<bool, CarAsset>[carAssets.Length];
			for(int i = 0; i < carAssets.Length; i++) {
				cars[i].Item2 = carAssets[i];
			}

			carAssets = null;
		}

		Vector2 h_startTouchPosition;
		Vector3 CalcInput() {
			if(Input.touchCount > 0) {
				Touch touch = Input.GetTouch(0);

				if(touch.phase == TouchPhase.Began)
					h_startTouchPosition = touch.position;
				Vector3 value = (touch.position - h_startTouchPosition).normalized;

				return new Vector3(value.x, 0, value.y);
			}
			
			return new Vector3(Input.GetAxis(StringCollection.A_HORIZONTAL), 0, Input.GetAxis(StringCollection.A_VERTICAL)).normalized;
		}

		Vector3 CalcAceleration(Vector3 inDir, Vector3 vel, CarAsset car) {
			Vector3 velDir = vel.normalized;

			float accValue = Vector3.Dot(inDir, velDir) * car.accelerationScalar;
			float stearValue = Vector3.Dot(inDir, new Vector3(-velDir.z, 0, velDir.x)) * car.stearingScalar;

			accValue *= car.acceleration.Evaluate(vel.magnitude); //TODO: tsunami distence
			stearValue *= car.stearing.Evaluate(vel.magnitude);

			return velDir * accValue + new Vector3(-velDir.z, 0, velDir.x) * stearValue;
		}
	}
}