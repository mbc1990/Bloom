using UnityEngine;
using System.Collections;

/*
 * This shows the HUD GUI that appears on the side of the screen and provides info about missions, resources, and other things (opponents? threats?)
 * 
 */ 
public class hud : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
		//background boxes
		//Resources box
		GUI.Box(new Rect(-10,Screen.height/10,110,Screen.height/5), "Resources");
		
		//Mission box
		GUI.Box(new Rect(-10,Screen.height/10 + Screen.height/5 + 20,110,Screen.height/5), "Missions");
		
		//Third yet unused box
		GUI.Box(new Rect(-10,Screen.height/10 + Screen.height/5 * 2 + 40,110,Screen.height/5), "Threats?");
		
		
	}
}
