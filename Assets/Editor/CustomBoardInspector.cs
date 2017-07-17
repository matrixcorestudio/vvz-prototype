using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardEditor))]
public class CustomBoardEditorInspector : Editor 
{
	
	public override void OnInspectorGUI()
	{
		BoardEditor m_boardEditor = target as BoardEditor;
		DrawDefaultInspector();
		if(GUILayout.Button("Reset Board Tiles"))
		{
			m_boardEditor.ResetBoard();
			Debug.Log("BOARD RESET!");
		}

		if(GUILayout.Button("Save Board"))
		{
			m_boardEditor.SaveBoard();
			Debug.Log("BOARD SAVED!");
		}
	}
}
