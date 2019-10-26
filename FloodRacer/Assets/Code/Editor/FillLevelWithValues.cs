using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FillLevelWithValues : EditorWindow {

	Rect area;
	int size = 0;
	GameObject[] obj = new GameObject[0];

	[MenuItem("Window/Randomice")]
	public static void ShowWindow() {
		GetWindow<FillLevelWithValues>("Randomice");
	}

	void OnGUI() {
		area = EditorGUILayout.RectField(area);

		size = EditorGUILayout.DelayedIntField(size);

		if(size != obj.Length) {
			GameObject[] tmp = new GameObject[obj.Length];
			for(int i = 0; i < obj.Length; i++) {
				tmp[i] = obj[i];
			}
			obj = new GameObject[size];
			for(int i = 0; i < ((obj.Length < tmp.Length) ? obj.Length : tmp.Length); i++) {
				obj[i] = tmp[i];
			}
		}

		for(int i = 0; i < size; i++) {
			obj[i] = (GameObject)EditorGUILayout.ObjectField(obj[i], typeof(GameObject));
		}

		if(GUILayout.Button("Randomize")) {
			for(int i = 0; i < area.width / 10; i++) {
				for(int j = 0; j < area.height / 10; j++) {
					GameObject tmp = (GameObject)PrefabUtility.InstantiatePrefab(obj[Random.Range(0, obj.Length)]);
					tmp.transform.position = new Vector3(area.x + i * 10, 0, area.y + j * 10);
				}
			}
		}
	}
}
