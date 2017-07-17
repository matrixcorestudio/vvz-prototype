using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardData : ScriptableObject 
{
	public GameObject baseTilePrefab;
	public List<Material> tileMaterials = new List<Material>();
}