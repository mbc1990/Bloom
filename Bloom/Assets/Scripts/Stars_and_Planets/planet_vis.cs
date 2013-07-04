using UnityEngine;
using System.Collections;

public class planet_vis : MonoBehaviour {
	//main camera
	GameObject cam;
	
	//distance from planet to activate info box
	float ACTIVATION_DISTANCE = 100f;
	
	//distance from box the cursor can be before box disappears
	float BOX_PERSIST = 2000f;
	
	//toggle info box
	bool info_box = false;
	//info box location (where the planet was when the box was triggered)
	Vector3 info_box_loc;
	//box location unadjusted (for relative location to other onscreen objects)
	Vector3 info_box_loc_unadj;
	
	//attribute script
	planet_attrs attr_script;

	
	// Use this for initialization
	void Start () {
		cam = GameObject.Find("Main Camera");	
		attr_script = gameObject.GetComponent<planet_attrs>();
	}
	
	// Update is called once per frame
	void Update () {
		//detect when cursor is on planet
		Vector3 mouse = Input.mousePosition;
		mouse = cam.camera.ScreenToWorldPoint(mouse);
		mouse = new Vector3(mouse.x,mouse.y,0);
		
		//enable info box
		//TODO: fix magic number box y pos adjustment
		if(!info_box && Vector3.Distance(mouse,transform.position) < ACTIVATION_DISTANCE) {
			info_box = true;
			info_box_loc = cam.camera.WorldToScreenPoint(new Vector2(mouse.x,Screen.height-1000 - mouse.y));
			info_box_loc_unadj = mouse; 
		}
		
		//disable info box when mouse is out of range
		if(info_box 
			&& Vector3.Distance(mouse, transform.position) >= ACTIVATION_DISTANCE 
			&& Vector3.Distance(info_box_loc_unadj, mouse) >= BOX_PERSIST) {
			info_box = false;
		}
		
	}
	
	void OnGUI() {
		//new box
		if(info_box) {
			GUI.Box(new Rect(info_box_loc.x,info_box_loc.y,100,90), attr_script.pl_name.ToString());
			if(GUI.Button(new Rect(info_box_loc.x,info_box_loc.y+20,100,20), "Send Mission")) {
				print ("button pressed");
			}
		}
	}
}
