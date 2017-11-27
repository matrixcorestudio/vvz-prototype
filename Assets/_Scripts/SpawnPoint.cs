using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Utilities;

public class SpawnPoint : MonoBehaviour {
	public Board board;

	void Start ()
	{
		board = FindObjectOfType<Board>();
		StartCoroutine(SetSpawnPoints());
	}

	IEnumerator SetSpawnPoints ()
	{
		if(board != null)
		{
			yield return new WaitUntil(() => board.AllTiles == null);
			yield return new WaitForSeconds(2f);
			foreach (var tile in board.AllTiles) 
			{
				if(tile.tileType == Enums.TileType.VikingSpawn)
				{
					GameObject sp = new GameObject("VikingSpawn");
					sp.transform.SetParent(transform);
				}	
			}
		}
	}
}
