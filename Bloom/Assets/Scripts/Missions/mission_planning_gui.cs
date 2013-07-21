using UnityEngine;
using System.Collections;

public class mission_planning_gui : MonoBehaviour {
	
	//TODO: Instantiate probe from a prefab (maybe hooked into the manager obj?) instead of creating a new gameobject
	
	string control_code = "";
	GameObject probe; //Data & behavior that corresponds to what the GUI is doing
	mission_data data;
	mission_manager mis_man;
	public GameObject target; //Planet that was clicked on

	// Use this for initialization
	void Start () {
		GUI_locks.PLAN_MISSION = true;
		probe = new GameObject();
		//attach probe script
		probe.AddComponent<mission_data>();
		data = probe.GetComponent<mission_data>();
		
		//get mission manager
		//TODO: modularize this for multiple solar systems - each system that has a command station has its own mission manager 
		GameObject man = GameObject.Find("Manager_Obj");
		mis_man = man.GetComponent<mission_manager>();
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
		GUI.Box(new Rect(Screen.width/8,Screen.height/10,6 * Screen.width/8,8*Screen.height/10), "Mission to "+target.GetComponent<planet_attrs>().pl_name);
		
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
			data.code = control_code; //mission code written by player
			data.dest = target; //planet whose "send mission" button was clicked on
			mis_man.Launch(probe); //add probe to active mission list and start it on its way
			Exit();
		}
		
		//code editing box
		control_code = GUI.TextArea(new Rect(Screen.width/8 + 200,Screen.height/10 + 20, Screen.width - Screen.width/8 * 2 - 300,8*Screen.height/10 - 20), control_code);
	
		//stats text
		GUI.Label(new Rect(Screen.width - Screen.width/8 - 100,Screen.height/10 + 50,100,30), "Energy: ");
		GUI.Label(new Rect(Screen.width - Screen.width/8 - 100,Screen.height/10 + 80,100,30), "Cost: ");
		
		//TODO: Prevent duplicates from being added
		//buttons for adding a module
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+40,100,20), "Nav")) {
			if(probe.GetComponent<mod_nav>() == null) {
				probe.AddComponent<mod_nav>();
			}
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+60,100,20), "Battery")) {
			if(probe.GetComponent<mod_bat>() == null) {
				probe.AddComponent<mod_bat>();
			}
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+80,100,20), "Energy Station")) {
			if(probe.GetComponent<mod_est>() == null) {
				probe.AddComponent<mod_est>();
			}
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+100,100,20), "Extractor")) {
			if(probe.GetComponent<mod_ext>() == null) {
				probe.AddComponent<mod_ext>();
			}
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+120,100,20), "Sensor")) {
			if(probe.GetComponent<mod_sensor>() == null) {
				probe.AddComponent<mod_sensor>();
			}
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+140,100,20), "Transport")) {
			if(probe.GetComponent<mod_transport>() == null) {
				probe.AddComponent<mod_transport>();
			}
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+160,100,20), "Comm")) {
			if(probe.GetComponent<mod_comm>() == null) {
				probe.AddComponent<mod_comm>();
			}
		}
		
		//buttons for deleting a module 
		int button_offset = 1;
		if(probe.GetComponent<mod_nav>() != null) {
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), "Nav")) {
				//delete the component
				Destroy(probe.GetComponent<mod_nav>());
			}
			button_offset++;
		}
		if(probe.GetComponent<mod_bat>() != null) {
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), "Battery")) {
				//delete the component
				Destroy(probe.GetComponent<mod_bat>());
			}
			button_offset++;
		}
		if(probe.GetComponent<mod_est>() != null) {
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), "Energy Station")) {
				//delete the component
				Destroy(probe.GetComponent<mod_est>());
			}
			button_offset++;
		}
		if(probe.GetComponent<mod_ext>() != null) {
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), "Extractor")) {
				//delete the component
				Destroy(probe.GetComponent<mod_ext>());
			}
			button_offset++;
		}
		if(probe.GetComponent<mod_sensor>() != null) {
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), "Sensor")) {
				//delete the component
				Destroy(probe.GetComponent<mod_sensor>());
			}
			button_offset++;
		}
		if(probe.GetComponent<mod_transport>() != null) {
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), "Transport")) {
				//delete the component
				Destroy(probe.GetComponent<mod_transport>());
			}
			button_offset++;
		}
		if(probe.GetComponent<mod_comm>() != null) {
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), "Comm")) {
				//delete the component
				Destroy(probe.GetComponent<mod_comm>());
			}
			button_offset++;
		}
		
	}
}
