using UnityEngine;
using System.Collections;

public class mission_planning_gui : MonoBehaviour {

	// Use this for initialization
	void Start () {
		print ("opening new box");
		//draw box when first added (when button was clicked)
	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Box(new Rect(Screen.width/8,Screen.height/10,6 * Screen.width/8,8*Screen.height/10), "new mission");
	}
}
