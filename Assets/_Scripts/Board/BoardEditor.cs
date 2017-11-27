using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Prototype.Utilities;

[System.Serializable]
public struct BoardChange
{
	public Tile modifiedTile;
	public Enums.TileType oldType;

	public BoardChange(Tile tile, Enums.TileType type)
	{
		modifiedTile = tile;
		oldType = type;
	}
}

public class BoardEditor : MonoBehaviour 
{
	public Dropdown tilesDropdownMenu;
	public RectTransform loadableBoardsPanel;
	public Button buttonPrefab;
	public Text saveNameText;
	public SceneFader sceneFader;
	public Board board;

	List<string> boardNames = new List<string>();

	Stack<BoardChange> undoStack = new Stack<BoardChange>();
	Stack<BoardChange> redoStack = new Stack<BoardChange>();

	void Awake()
	{
		board = FindObjectOfType<Board>() as Board;
		if(board == null)
		{
			Debug.LogWarning("Board not found in hierarchy! shavo...");
		}
	}

	void Start()
	{
		if(!board)
		{
			return;
		}

		List<string> tileOptions = new List<string>();
		foreach (var type in Enum.GetValues(typeof(Enums.TileType)))
		{
			tileOptions.Add(type.ToString());
		}
		tilesDropdownMenu.ClearOptions();
		tilesDropdownMenu.AddOptions(tileOptions);

		LoadBoardsToPanel();
	}

	public void SaveBoard()
	{
		if(string.IsNullOrEmpty(saveNameText.text))
		{
			SaveBoardToCSV("no_name");
			AddLoadButton("no_name");
		}
		else
		{
			SaveBoardToCSV(saveNameText.text);
			AddLoadButton(saveNameText.text);
		}
		//LoadBoardsToPanel();

	}

	void AddLoadButton (string boardName)
	{
		if(boardNames.Contains(boardName))
		{
			return;
		}
		boardNames.Add(boardName);
		Button button = Instantiate(buttonPrefab, loadableBoardsPanel);
		button.GetComponentInChildren<Text>().text = boardName;
		button.onClick.AddListener(() => {LoadBoard(SaveLoadManager.ReadCSVFile(boardName));});
	}

	void LoadBoardsToPanel()
	{
		UnityEngine.Object[] textAssets = Resources.LoadAll("",typeof(TextAsset));
		foreach (var item in textAssets)
		{
			TextAsset t = item as TextAsset;
			boardNames.Add(t.name);
			Button button = Instantiate(buttonPrefab,loadableBoardsPanel);
			button.GetComponentInChildren<Text>().text = t.name;
			button.onClick.AddListener(() => {LoadBoard(t.text);});
		}

		List<string> boardFiles = SaveLoadManager.LoadBoardsNames();
		foreach (var name in boardFiles) 
		{
			if(!boardNames.Contains(name))
			{
				boardNames.Add(name);
				Button button = Instantiate(buttonPrefab, loadableBoardsPanel);
				button.GetComponentInChildren<Text>().text = name;
				button.onClick.AddListener(() => {LoadBoard(SaveLoadManager.ReadCSVFile(name));});
			}
		}
	}

	void Update()
	{
		if(!board)
		{
			return;
		}
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray mouse = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(mouse, out hit, 50f))
			{
				Tile hitTile = hit.collider.GetComponent<Tile>();
				if (hitTile != null) 
				{
					undoStack.Push(new BoardChange(hitTile,hitTile.tileType));
					hitTile.ChangeProperties(tilesDropdownMenu.value);
				}
			}
		}
	}

	void LoadBoard (string boardCSV)
	{
		DestroyBoard();
		//board.CsvString = SaveLoadManager.ReadCSVFile(name);
		board.CsvString = boardCSV;
		board.SetupBoard();
	}

	void DestroyBoard()
	{
		for (int j = 0; j < board.Height; j++) {
			for (int i = 0; i < board.Width; i++) {
				if (board.AllTiles [i, j] != null) {
					Destroy(board.AllTiles [i, j].gameObject);
				}
			}
		}
	}
		
	public void ResetBoard()
	{
		Debug.Log ("Clearing board-> " + board.Width + " : " + board.Height);
		for (int j = 0; j < board.Height; j++) {
			for (int i = 0; i < board.Width; i++) {
				if (board.AllTiles [i, j] != null) {
					undoStack.Push(new BoardChange(board.AllTiles [i, j],board.AllTiles [i, j].tileType));
					board.AllTiles [i, j].ResetProperties ();
				}
				else {
					Debug.LogWarning ("null tile");
				}
			}
		}
	}

	public void SaveBoardToCSV(string name = "Mapilla")
	{
		List<string[]> dataRows = new List<string[]>();
		string[] rowDataTemp;
		Debug.Log("Saving...");
		for (int j = board.Height - 1; j >= 0; j--) 
		{
			rowDataTemp = new string[board.Width];
			for (int i = 0; i < board.Width; i++) 
			{
				if (board.AllTiles [i, j] != null) 
				{
					rowDataTemp [i] = ((int)board.AllTiles [i, j].tileType).ToString ();
				}
			}
			Debug.Log("j: "+j+"rowTempLen: "+rowDataTemp.Length);
			if(rowDataTemp.Length > 0)
			{
				dataRows.Add (rowDataTemp);
			}
		}
		SaveLoadManager.WriteBoardFile(CSVManager.ConvertToCsv (dataRows), name);
	}

	public void UndoTileChange ()
	{
		if(undoStack.Count < 1)
			return;
		BoardChange change = undoStack.Pop();
		redoStack.Push(new BoardChange (change.modifiedTile,change.modifiedTile.tileType));
		change.modifiedTile.ChangeProperties(change.oldType);
	}

	public void RedoTileChange ()
	{
		if(redoStack.Count < 1)
			return;
		BoardChange change = redoStack.Pop();
		undoStack.Push(new BoardChange (change.modifiedTile,change.modifiedTile.tileType));
		change.modifiedTile.ChangeProperties(change.oldType);
	}

	public void BackToMenu ()
	{
		sceneFader.FadeTo(0);
	}
}
