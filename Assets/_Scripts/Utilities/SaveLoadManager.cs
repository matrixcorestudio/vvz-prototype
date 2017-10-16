﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;
using System.Text;
using System;

public class SaveLoadManager 
{
	public static void WriteBoardFile(string csv, string boardName)
	{
		string filePath = GetPath() + boardName + ".csv";
		StreamWriter outStream = System.IO.File.CreateText(filePath);
		outStream.Write(csv);
		outStream.Close();

		#if UNITY_EDITOR
		AssetDatabase.ImportAsset("Assets/Resources/Maps/" + boardName + ".csv");
		#endif
	}

	public static List<string> LoadBoardsNames()
	{
		string[] txtfiles = Directory.GetFiles(GetPath(), "*.csv");
		List<string> boardNames = new List<string>();

		for (int i = 0; i < txtfiles.Length; i++)
		{
			boardNames.Add(Path.GetFileNameWithoutExtension(txtfiles[i]));
			Debug.Log(boardNames[i]);
		}
		return boardNames;
	}

	public static string ReadCSVFile(string name)
	{
		StreamReader sr = new StreamReader(GetPath()+name+".csv");
		return sr.ReadToEnd();
	}


	public static string GetPath()
	{
		#if UNITY_EDITOR
		return Application.dataPath +"/Resources/Maps/";
		#elif UNITY_ANDROID
		return Application.persistentDataPath+"/Maps/";
		#elif UNITY_IPHONE
		return Application.persistentDataPath+"/Maps/";
		#else
		return Application.dataPath+"/Maps/";
		#endif
	}
}