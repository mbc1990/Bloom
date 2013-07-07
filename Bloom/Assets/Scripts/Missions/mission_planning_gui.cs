using UnityEngine;
using System.Collections;

public class mission_planning_gui : MonoBehaviour {
	
	string control_code = "";
	GameObject probe; //Data & behavior that corresponds to what the GUI is doing
	mission_data data;

	// Use this for initialization
	void Start () {
		GUI_locks.PLAN_MISSION = true;
		probe = new GameObject();
		//attach probe script
		probe.AddComponent<mission_data>();
		data = probe.GetComponent<mission_data>();
		
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
		
		//stats background
		GUI.Box(new Rect(Screen.width - Screen.width/8 - 100,Screen.height/10+20,100,8*Screen.height/10 - 20), "Info");
		
		
		//cancel button
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10,100,20), "Cancel")) {
			Destroy(probe); //probe isn't being sent, so destroy it
			Exit ();
		}
		
		//send button
		if(GUI.Button(new Rect(Screen.width - Screen.width/8 - 100,Screen.height/10,100,20), "Launch")) {
			print (control_code);
			Exit();
		}
		
		//code editing box
		control_code = GUI.TextArea(new Rect(Screen.width/8 + 200,Screen.height/10 + 20, Screen.width - Screen.width/8 * 2 - 300,8*Screen.height/10 - 20), control_code);
	
		//stats text
		GUI.Label(new Rect(Screen.width - Screen.width/8 - 100,Screen.height/10 + 50,100,30), "Energy: ");
		GUI.Label(new Rect(Screen.width - Screen.width/8 - 100,Screen.height/10 + 80,100,30), "Cost: ");
		
		//buttons for each available module that attach the corresponding named module_info script & module_behavior script to the mission_data's list of modules
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+40,100,20), "Nav")) {
			GameObject module = new GameObject(); //a module (in the game design sense) consists of a module gameobject with module specific scripts attached, as well as the module_info scrpt
			module.AddComponent<module_info>();
			module.GetComponent<module_info>().mod_name = "Nav";
			data.modules.Add(module); //add the newly created module to the probe's list of modules
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+60,100,20), "Battery")) {
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+80,100,20), "Energy Station")) {
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+100,100,20), "Extractor")) {
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+120,100,20), "Sensor")) {
		}
		
		//buttons for each module already attached to the mission_data's list of modules, to remove them from the list
		int button_offset = 1;
		ArrayList to_remove = new ArrayList();
		foreach (GameObject g in data.modules) {
			module_info inf = g.GetComponent<module_info>();
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), inf.mod_name)) {
				//add to removal list to avoid concurrent modification exception (or whatever C# calls that)
				to_remove.Add(g);
			}
			button_offset ++;
		}
		//Every frame (actually, more like twice per frame, since this is in OnGUI) remove the gameobjects (modules) that the use has selected to remove in the GUI
		for (int i = 0; i < to_remove.Count; i++) {
			GameObject g = to_remove[i] as GameObject; 
			data.modules.Remove(g);
			Destroy(g);
		}
	}
}
