using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System.Collections.Generic;

public class XMLCharacterLoader : MonoBehaviour
{
    public TextAsset xmlRawFile; // contenedor para el archivo data.xml
    public Text uiText; //descripcion del status seleccionado
	public List<UiCharacterInfo> vikingList = new List<UiCharacterInfo>(); //contener de todos los status leidos de data.xml
	public List<UiCharacterInfo> zombieList = new List<UiCharacterInfo>(); //contener de todos los status leidos de data.xml
    // Levanta el archivo.
    void Start()
    {
        string data = xmlRawFile.text;
		ParseXmlFile(data,vikingList);
		ParseXmlFile(data,zombieList);
    }

	void ParseXmlFile(string xmlData, List<UiCharacterInfo> charList)
    {
        string totVal = "";
        string tempID = "";
        string tempName = "";
        string tempDice = "";
        string tempPassive = "";
        string tempActive = "";


        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));


        string xmlPathPattern = "//ItemCollection/Item"; //raiz del xml y el nombre de los elementos
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

        //Se lee el primer nodo que es id y se almacena de manera temporal.
        //es necesario tomar referencia al primer nodo, ya que todas las referencias subsequetes se tomaran a partir de la posicion del primero.
        foreach (XmlNode node in myNodeList)
        {
            XmlNode id = node.FirstChild;
            tempID = id.InnerXml;
            XmlNode name = id.NextSibling;
            tempName = name.InnerXml;
            XmlNode dice = name.NextSibling;
			tempDice = dice.InnerXml;
            XmlNode passive = dice.NextSibling;
			tempPassive = passive.InnerXml;
            XmlNode active = passive.NextSibling;
			tempActive = active.InnerXml;


            //agrega todo el show a la lista (las listas son cool)
			charList.Add(new UiCharacterInfo(tempID, tempName, tempDice, tempPassive, tempActive));

        }

    }
}