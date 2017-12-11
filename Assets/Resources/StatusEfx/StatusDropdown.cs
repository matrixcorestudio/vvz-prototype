using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDropdown : MonoBehaviour
{

    public Dropdown dropdown;
    public Text selectedName;
    public Text selectedText;
    public XmlParser statusReference;

    public void Dropdown_IndexChanged(int index)
    {
        string totVal = "";
        --index;// Se hace el -- para contrarestar el espacio vacio en el dropdown
        if (index >= 0)
        {
            selectedName.text = statusReference.statusList[index].Name;
            //selectedText.text = statusReference.statusList[index].Description;

            totVal += "parameter : " + statusReference.statusList[index].Parameter + "\n target : " + statusReference.statusList[index].Target + "\n clasification : " + statusReference.statusList[index].Clasification + "\n description : " + statusReference.statusList[index].Description + "\n troubleshooting : " + statusReference.statusList[index].Troubleshooting + "\n\n";
            selectedText.text = totVal; 
        }
        else
        {
            selectedName.text = "Select Status";
            selectedText.text = "No Status Selected";
        }
    }

    public void PopulateList()
    {
        List<string> names = new List<string>();
        int totalEntries = statusReference.statusList.Count;
        for (int i = 0; i < totalEntries; i++)
        {
            names.Add(statusReference.statusList[i].Name);

        }
        dropdown.AddOptions(names);
    }




}
