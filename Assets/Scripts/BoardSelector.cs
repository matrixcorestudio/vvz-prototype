using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardSelector : MonoBehaviour {

	public int gameSceneIndex = 2;
	public SceneFader sceneFader;
	public RectTransform contentPanel;
	public Button buttonPrefab;
	Object[] textAssets;
	List<string> boardNames = new List<string>();

	void Start ()
	{
		textAssets = Resources.LoadAll("",typeof(TextAsset));
		foreach (var item in textAssets)
		{
			TextAsset t = item as TextAsset;
			boardNames.Add(t.name);
			Button button = Instantiate(buttonPrefab,contentPanel);
			button.GetComponentInChildren<Text>().text = t.name;
			button.onClick.AddListener(() => {SelectBoard(t.text);});
		}

		List<string> boardFiles = SaveLoadManager.LoadBoardsNames();
		foreach (var name in boardFiles) 
		{
			if(!boardNames.Contains(name))
			{
				boardNames.Add(name);
				Button button = Instantiate(buttonPrefab,contentPanel);
				button.GetComponentInChildren<Text>().text = name;
				button.onClick.AddListener(() => {SelectBoard(SaveLoadManager.ReadCSVFile(name));});
			}
		}
	}

	void SelectBoard (string boardCSV)
	{
		PlayerPrefs.SetString("BoardCSV",boardCSV);
		sceneFader.FadeTo(gameSceneIndex);
	}
}
