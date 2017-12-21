using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public struct UiCharacterInfo {

    public string ID;
	public string Name;
	public string Dice;
	public string Passive;
	public string Active;


	public UiCharacterInfo(string id, string name, string dice, string passive, string active){
		ID = id;
		Name = name;
		Dice = dice;
		Passive = passive;
		Active = active;

	}

    

}
