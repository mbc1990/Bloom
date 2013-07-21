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
		
		//buttons for each available module that attach the corresponding named module_info script & module_behavior script to the mission_data's list of modules
		//TODO: fix the parenting problem, or add iterating over all children 
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+40,100,20), "Nav")) {
			probe.AddComponent<mod_nav>();
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+60,100,20), "Battery")) {
		/*	GameObject module = new GameObject(); //a module (in the game design sense) consists of a module gameobject with module specific scripts attached, as well as the module_info scrpt
			module.AddComponent<module_info>();
			module.GetComponent<module_info>().mod_name = "Battery";
			data.modules.Add(module); //add the newly created module to the probe's list of modules
			module.transform.parent = data.transform;
			*/
			//probe.AddComponent<mod_nav>();
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+80,100,20), "Energy Station")) {
			GameObject module = new GameObject(); //a module (in the game design sense) consists of a module gameobject with module specific scripts attached, as well as the module_info scrpt
			module.AddComponent<module_info>();
			module.GetComponent<module_info>().mod_name = "Energy Station";
			data.modules.Add(module); //add the newly created module to the probe's list of modules
			module.transform.parent = data.transform;
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+100,100,20), "Extractor")) {
			GameObject module = new GameObject(); //a module (in the game design sense) consists of a module gameobject with module specific scripts attached, as well as the module_info scrpt
			module.AddComponent<module_info>();
			module.GetComponent<module_info>().mod_name = "Extractor";
			data.modules.Add(module); //add the newly created module to the probe's list of modules
			module.transform.parent = data.transform;
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+120,100,20), "Sensor")) {
			probe.AddComponent<mod_sensor>();
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+140,100,20), "Transport")) {
			GameObject module = new GameObject(); //a module (in the game design sense) consists of a module gameobject with module specific scripts attached, as well as the module_info scrpt
			module.AddComponent<module_info>();
			module.GetComponent<module_info>().mod_name = "Transport";
			data.modules.Add(module); //add the newly created module to the probe's list of modules
			module.transform.parent = data.transform;
		}
		if(GUI.Button(new Rect(Screen.width/8,Screen.height/10+160,100,20), "Comm")) {
			GameObject module = new GameObject(); //a module (in the game design sense) consists of a module gameobject with module specific scripts attached, as well as the module_info scrpt
			module.AddComponent<module_info>();
			module.GetComponent<module_info>().mod_name = "Comm";
			data.modules.Add(module); //add the newly created module to the probe's list of modules
			module.transform.parent = data.transform;
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
		if(probe.GetComponent<mod_sensor>() != null) {
			if(GUI.Button(new Rect(Screen.width/8 + 100,Screen.height/10+20 + 20*button_offset,100,20), "Sensor")) {
				//delete the component
				Destroy(probe.GetComponent<mod_sensor>());
			}
			button_offset++;
		}
		
	}
}
