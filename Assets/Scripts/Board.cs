using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour 
{
	public BoardData boardData;
	public TextAsset boardCsvAsset;
	public int borderSize = 1;

	string csvString;
	public string CsvString {get {return csvString;} set{csvString = value;}}

	int m_width;
	public int Width {get {return m_width;}}

	int m_height;
	public int Height {get {return m_height;}}

	Tile[,] m_allTiles;
	public Tile[,] AllTiles {get {return m_allTiles;}}

    public Text tileText;
    public bool refreshTileText = false;
    private string clickedTile = string.Empty;

	void Start () 
	{
		csvString = PlayerPrefs.GetString("BoardCSV",boardCsvAsset.text);
        if (tileText != null)
        {
            tileText.text = "Last clicked Tile";
        }
		SetupBoard();
	}

    private void Update()
    {
        if (refreshTileText && tileText != null)
        {
            tileText.text = clickedTile;
            refreshTileText = false;
        }
    }

    public void RefreshTileText(string text)
    {
        clickedTile = "Last clicked " + text;
        refreshTileText = true;
    }

    public void SetupBoard ()
	{
		SetupTilesFromCSV();
		//SetupOrtographicCamera();
	}

	void SetupOrtographicCamera ()
	{
		Camera.main.transform.position = new Vector3((float)(m_width - 1)/2f, (float) (m_height-1) /2f, -10f);
		float aspectRatio = (float) Screen.width / (float) Screen.height;
		float verticalSize = (float) m_height / 2f + (float) borderSize;
		float horizontalSize = ((float) m_width / 2f + (float) borderSize ) / aspectRatio;
		//Debug.Log("Aspect: "+aspectRatio+", vertical: "+verticalSize+", horizontal: "+horizontalSize);
		Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize: horizontalSize;
	}

	bool IsWithinBounds (int x, int y)
	{
		return (x >= 0 && x < m_width && y >= 0 && y < m_height);
	}

	void MakeTile (GameObject prefab, int tileType, int x, int y, int z = 0)
	{
		if(prefab != null && IsWithinBounds(x,y))
		{
			GameObject tile = Instantiate (prefab, new Vector3 (x, y, z), Quaternion.identity, transform) as GameObject;
			tile.name = "Tile (" + x + "," + y + ")";
			m_allTiles [x, y] = tile.GetComponent<Tile> ();
			m_allTiles [x, y].Init (tileType, x, y, this);
		}
		else
		{
			Debug.LogWarning("BOARD.MakeTile invalid prefab!");
		}
	}

	void SetupTilesFromCSV ()
	{
		string[,] grid = CSVManager.SplitCsvGrid(csvString);
		m_width = grid.GetUpperBound(0);
		m_height = grid.GetUpperBound(1);
		m_allTiles = new Tile[m_width,m_height];

		for (int y = 0; y < m_height; y++)
		{
			for (int x = 0; x < m_width; x++)
			{
				if(m_allTiles[x,y] == null)
				{
					int tileType;
					Int32.TryParse(grid[x,m_height-y-1], out tileType);
					MakeTile (boardData.baseTilePrefab, tileType, x, y);
				}
			}
		}
	}
}
