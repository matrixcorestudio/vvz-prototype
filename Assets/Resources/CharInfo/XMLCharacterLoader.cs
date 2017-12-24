using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System.Collections.Generic;

public class XMLCharacterLoader : MonoBehaviour
{
    public TextAsset xmlRawFileV; // contenedor para el archivo data.xml
    public TextAsset xmlRawFileZ; // contenedor para el archivo data.xml
    public Text uiText; //descripcion del status seleccionado
    private string Zdata, Vdata;

    // Levanta el archivo.
    void Start()
    {
        Zdata = xmlRawFileZ.text;
        Vdata = xmlRawFileV.text;
        ParseXmlFile(Vdata, Color.blue);
    }

    void ParseXmlFile(string xmlData, Color textColor)
    {
        string temp;//pare remover el primer id
        string totalValue = "";

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(new StringReader(xmlData));

        string xmlPathPattern = "//ItemCollection/Item"; //raiz del xml y el nombre de los elementos
        XmlNodeList myNodeList = xmlDoc.SelectNodes(xmlPathPattern);

        //Se lee el primer nodo que es id y se almacena de manera temporal.
        //es necesario tomar referencia al primer nodo, ya que todas las referencias subsequetes se tomaran a partir de la posicion del primero.
        foreach (XmlNode node in myNodeList)
        {

            XmlNode id = node.FirstChild;
            temp = id.InnerXml;
            XmlNode name = id.NextSibling;
            totalValue += "<b>Name:</b>\n";
            totalValue += name.InnerXml + "\n";
            XmlNode dice = name.NextSibling;
            totalValue += "<b>Dice and range:</b>\n";
            totalValue += dice.InnerXml + "\n";
            XmlNode passive = dice.NextSibling;
            totalValue += "<b>Passive:</b>\n";
            totalValue += passive.InnerXml + "\n";
            XmlNode active = passive.NextSibling;
            totalValue += "<b>Active:</b>\n";
            totalValue += active.InnerXml;

            //agrega todo el show a la lista (las listas son cool)
            //charList.Add(new UiCharacterInfo(tempID, tempName, tempDice, tempPassive, tempActive));
            totalValue += "\n*******************************************\n";
        }
        uiText.text = totalValue;
        uiText.color = textColor;
    }

    /*Segun se detecte un int change en el dropwpdn, se carga la respectiva informaci'on al panel;*/
    public void changeCharInfo(int dropValue)
    {
        if (dropValue == 1)
        {
            ParseXmlFile(Zdata, new Color(139f / 250f, 0 / 250f, 139f / 250f));
        }
        else
        {
            ParseXmlFile(Vdata, Color.blue);
        }
    }

}