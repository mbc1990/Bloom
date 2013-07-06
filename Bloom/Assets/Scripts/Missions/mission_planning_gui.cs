using UnityEngine;
using System.Collections;

public class mission_planning_gui : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		//background
		GUI.Box(new Rect(Screen.width/8,Screen.height/10,6 * Screen.width/8,8*Screen.height/10), "new mission");
		
		//available modules background
		GUI.Box(new Rect(Screen.width/8,Screen.height/10+20,100,8*Screen.height/10 - 20), "Available");
		
		//currently on probe background
		GUI.Box(new Rect(Screen.width/8 + 100,Screen.height/10+20,100,8*Screen.height/10 - 20), "On probe");
		
		//cancel button
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10,100,20), "Cancel")) {
			Destroy(gameObject);
		}
		
		//send button
		if(GUI.Button(new Rect(Screen.width - Screen.width/8 - 100,Screen.height/10,100,20), "Launch")) {
			print ("launching");
			Destroy(gameObject);
		}
		
		
		
		
		
		
	}
}
