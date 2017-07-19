using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
	Empty = 1,
	Regular = 2,
	Radioactive = 4,
	ZombieSpawn = 8,
	VikingSpawn = 16,
	Finish = 32,
	Trap = 64,
	PowerUp = 128,
	Event = 256,
	Highground = 512,
	WarpForced = 1024,
	WarpOptional = 2048,
	GreenHillZone = 4096,
	Switch = 8192,
	DangerZone = 16384,
	ConditionalGate = 32768
}

public class Tile : MonoBehaviour 
{
	public int xIndex;
	public int yIndex;
	public TileType tileType = TileType.Empty;
	public Renderer rend;
    private string clickedTile = string.Empty;
	Board m_board;

//	void Awake()
//	{
//		rend = GetComponent<Renderer>();
//	}

	public void Init(int type, int x,int y, Board board)
	{
		tileType = (TileType)type;
		xIndex = x;
		yIndex = y;
		m_board = board;
		SetMaterial();
	}

	void OnMouseDown()
	{
		if(m_board != null)
		{
            clickedTile = gameObject.name + "; Type: " + tileType;
            Debug.Log(clickedTile);
		}
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 250, 20), clickedTile);
    }

    void SetMaterial()
	{
		rend.material = m_board.boardData.tileMaterials.Find(n => n.name == tileType.ToString()+"Mat");
	}

	public void ResetProperties()
	{
		tileType = TileType.Empty;
		SetMaterial();
	}
		
	public void ChangeProperties(int optionTile)
	{
		tileType = (TileType)Mathf.Pow (2f, optionTile);
		SetMaterial();
		Debug.Log (gameObject.name+"; Changed to type: "+tileType);
	}
}
