using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public struct Status {

    public string ID;
	public string Name;
	public string Parameter;
	public string Target;
	public string Clasification;
	public string Description;
	public string Troubleshooting;

	public Status(string id, string name, string parameter, string target, string clasification, string description, string troubleshooting){
		ID = id;
		Name = name;
		Parameter = parameter;
		Target = target;
		Clasification = clasification;
		Description = description;
		Troubleshooting = troubleshooting;
	}

    

}
