using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.Utilities;

public class Tile : MonoBehaviour 
{
	public int xIndex;
	public int yIndex;
	public Enums.TileType tileType = Enums.TileType.Empty;
	public Renderer rend;
    private string clickedTile = string.Empty;
	Board m_board;

	public void Init(int type, int x,int y, Board board)
	{
		tileType = (Enums.TileType)type;
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
            m_board.RefreshTileText(clickedTile);
		}
	}

    void SetMaterial()
	{
		rend.material = m_board.boardData.tileMaterials.Find(n => n.name == tileType.ToString()+"Mat");
	}

	public void ResetProperties()
	{
		tileType = Enums.TileType.Empty;
		SetMaterial();
	}
		
	public void ChangeProperties(int optionTile)
	{
		tileType = (Enums.TileType)Mathf.Pow (2f, optionTile);
		SetMaterial();
		Debug.Log (gameObject.name+"; Changed to type: "+tileType);
	}

	public void ChangeProperties(Enums.TileType type)
	{
		tileType = type;
		SetMaterial();
		Debug.Log (gameObject.name+"; Changed to type: "+tileType);
	}
}
