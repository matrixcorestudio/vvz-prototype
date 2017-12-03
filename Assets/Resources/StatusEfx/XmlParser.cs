using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using System.Collections.Generic;

public class XmlParser : MonoBehaviour 
{
	public TextAsset xmlRawFile; // contenedor para el archivo data.xml
	public Text uiText; //descripcion del status seleccionado
	public Text statusName; //nombre del status
	public List<Status> statusList = new List<Status>(); //contener de todos los status leidos de data.xml
	public StatusDropdown statusReference;

	// Levanta el archivo.
	void Start () 
	{
		string data = xmlRawFile.text;
		parseXmlFile (data);
		statusReference.PopulateList ();
	}

	void parseXmlFile(string xmlData)
	{
		string totVal = "";
		string tempID = "";
		string tempName = "";
		string tempParameter = "";
		string tempTarget = "";
		string tempClasification = "";
		string tempDescription = "";
		string temptroubleshooting = "";

		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load ( new StringReader(xmlData));


		string xmlPathPattern = "//ItemCollection/Item"; //raiz del xml y el nombre de los elementos
		XmlNodeList myNodeList = xmlDoc.SelectNodes (xmlPathPattern);

		//Se lee el primer nodo que es id y se almacena de manera temporal.
		//es necesario tomar referencia al primer nodo, ya que todas las referencias subsequetes se tomaran a partir de la posicion del primero.
		foreach(XmlNode node in myNodeList)
		{
			XmlNode id = node.FirstChild; 
			tempID = id.InnerXml;
			XmlNode name = id.NextSibling;
			tempName = name.InnerXml;
			XmlNode parameter = name.NextSibling;
			tempParameter = parameter.InnerXml;
			XmlNode target = parameter.NextSibling;
			tempTarget = target.InnerXml;
			XmlNode clasification = target.NextSibling;
			tempClasification = clasification.InnerXml;
			XmlNode description = clasification.NextSibling;
			tempDescription = description.InnerXml;
			XmlNode troubleshooting = description.NextSibling;
			temptroubleshooting = troubleshooting.InnerXml;

			//agrega todo el show a la lista (las listas son cool)
			statusList.Add(new Status(tempID, tempName, tempParameter, tempTarget, tempClasification, tempDescription, temptroubleshooting));
			

		}

	}
}