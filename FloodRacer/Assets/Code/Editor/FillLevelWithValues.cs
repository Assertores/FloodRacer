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

		size = EditorGUILayout.IntField(size);
		if(size != obj.Length)
			obj = new GameObject[size];

		for(int i = 0; i < size; i++) {
			obj[i] = (GameObject)EditorGUILayout.ObjectField(obj[i], typeof(GameObject));
		}

		if(GUILayout.Button("Randomize")) {
			for(int i = 0; i < area.width / 10; i++) {
				for(int j = 0; j < area.height / 10; j++) {
					GameObject tmp = Instantiate(obj[Random.Range(0, obj.Length)]);
					tmp.transform.position = new Vector3(area.x + i * 10, 0, area.y + j * 10);
				}
			}
		}
	}
}
