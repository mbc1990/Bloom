using UnityEngine;
using System.Collections;

/*
 * This is kind of a hack, but I need an interface and Unity doens't let you be a component and implement an interface
 * Anyway, this script is attached to every module (an empty gameobject). It's used to get the module's name for GUI display purposes, and eventually will probably be used in cost/resource reporting as well.
 * 
 * 
 */
public class module_info : MonoBehaviour {
	
	//set when module is added to the probe in the mission planning GUI
	public string mod_name;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
