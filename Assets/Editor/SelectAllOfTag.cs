using System.Collections;
using UnityEngine;
using UnityEditor;

public class SelectAllOfTag : ScriptableWizard {

	public string searchTag = "Tag Here!";

	[MenuItem("VvZ Tools/Select All Of Tag...")]
	static void SelectAllOfTagWizard()
	{
		ScriptableWizard.DisplayWizard<SelectAllOfTag> ("Select All Of Tag...","Make Selection");
	}

	void OnWizardCreate()
	{
		GameObject[] gameobjects = GameObject.FindGameObjectsWithTag(searchTag);
		Selection.objects = gameobjects;
	}
}
