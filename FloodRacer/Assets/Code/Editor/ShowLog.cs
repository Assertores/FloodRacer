using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelHandler))]
public class ShowLog : Editor {

	static string levelname;
	static string path = "D:/Workspace/FloodRacer/FloodRacer/Assets/LOG";
	static GameObject refObject;
	static GameObject parentObject;
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		refObject = ((LevelHandler)target).gameObject;
		levelname = refObject?.name;

		parentObject = (GameObject)EditorGUILayout.ObjectField(parentObject, typeof(GameObject));

		levelname = EditorGUILayout.TextField(levelname);
		path = EditorGUILayout.TextField(path);

		if(levelname != null && path != null) {
			if(GUILayout.Button("Show Log")) {
				LogCreator.ShowLog(levelname, path, parentObject);
			}
		}
	}
}
