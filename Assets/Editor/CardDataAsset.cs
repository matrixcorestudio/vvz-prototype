﻿using UnityEngine;
using UnityEditor;

public class CardDataAsset
{
	[MenuItem("Assets/Create/Card Data")]
	[MenuItem("VvZ Tools/Create/Card Data")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CardData> ();
	}
}
