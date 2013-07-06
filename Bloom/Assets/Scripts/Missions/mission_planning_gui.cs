using UnityEngine;
using System.Collections;

public class mission_planning_gui : MonoBehaviour {
	
	string control_code = "";

	// Use this for initialization
	void Start () {
		GUI_locks.PLAN_MISSION = true;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Exit() {
		Destroy(gameObject);
		GUI_locks.PLAN_MISSION = false;
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
			Exit ();
		}
		
		//send button
		if(GUI.Button(new Rect(Screen.width - Screen.width/8 - 100,Screen.height/10,100,20), "Launch")) {
			print (control_code);
			Exit();
		}
		
		//code editing box
		control_code = GUI.TextArea(new Rect(Screen.width/8 + 200,Screen.height/10 + 20, Screen.width - Screen.width/8 * 2 - 300,8*Screen.height/10 - 20), control_code);
	}
}
