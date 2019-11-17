﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FloodRacer {
	[CustomEditor(typeof(River))]
	public class RiverEditor : Editor {
		public override void OnInspectorGUI() {
			River element = (target as River);

			EditorGUILayout.LabelField("References", EditorStyles.boldLabel);
			for(int i = 0; i < element.prefabs.Length; i++) {
				int newSize = EditorGUILayout.DelayedIntField(((RiverType)i).ToString(), element.prefabs[i].Length);
				if(newSize != element.prefabs[i].Length) {
					GameObject[] tmp = new GameObject[newSize];
					for(int index = 0; index < (newSize < element.prefabs[i].Length ? newSize : element.prefabs[i].Length); index++) {
						tmp[index] = element.prefabs[i][index];
					}
					element.prefabs[i] = tmp;
				}
				for(int j = 0; j < element.prefabs[i].Length; j++) {
					element.prefabs[i][j] = (GameObject)EditorGUILayout.ObjectField(element.prefabs[i][j], typeof(GameObject), false);
				}
			}

			EditorGUILayout.LabelField("Balancing", EditorStyles.boldLabel);
			element.world = (WorldType)EditorGUILayout.EnumPopup("World", element.world);
		}
	}
}