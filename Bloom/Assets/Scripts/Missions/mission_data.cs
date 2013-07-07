using UnityEngine;
using System.Collections;

public class mission_data : MonoBehaviour {
	
	//mission code, initialized and set by GUI when the probe is "launched"
	public string code;
	
	//current destination
	public GameObject dest;
	
	//list of gameobjects w/ module script & "interface" like module script that has the module name
	public ArrayList modules = new ArrayList();
	
	// Use this for initialization
	void Start () {
		print("data added");
		
	}
	
	// Update is called once per frame
	void Update () {
	
		//TODO: movement toward destination
	
	}
	
	//called from update, this method interprets the code and calls the various functions associated with the attached modules
	void run_code() {
		
	}
}
