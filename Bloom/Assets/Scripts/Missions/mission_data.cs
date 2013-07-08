using UnityEngine;
using System.Collections;

public class mission_data : MonoBehaviour {
	
	//mission code, initialized and set by GUI when the probe is "launched"
	public string code;
	
	//current destination
	public GameObject dest;
	
	//list of gameobjects w/ module script & "interface" like module script that has the module name
	public ArrayList modules = new ArrayList();
	
	//true when in orbit at a destination
	bool in_orbit = false;
	
	//wait until the mission has been launched to begin moving
	public bool mission_started = false;	
	
	//constants
	//distance from target distination at which point the probe will stop travelling and begin orbitting
	float ORBIT_DIST = 10;
	
	//rate at which probe orbits planet
	float ORBIT_SPEED = 5;
	
	//speed moving through non-interstellar-space (this will probably be modifed by various modules)
	float SPEED = 100;
	
	// Use this for initialization
	void Start () {
		print("data added");
	}
	
	// Update is called once per frame
	void Update () {
		
		//do these things once the mission is underway (not while it's being constructed in the mission planning gui)
		//TODO: fix bug in which orbit does not move with moving planet
		//TODO: adjust ORBIT_DIST (may be a bug)
		if(mission_started) {
			if (in_orbit) {
				//rotate around destination 
				transform.RotateAround(new Vector3(0,0,1),ORBIT_SPEED*Time.deltaTime);
				
			} else {
				if(Vector3.Distance(transform.position, dest.transform.position) < ORBIT_DIST) {
					//arrive at destination, begin orbit
					in_orbit = true;	
				} else {
					print("moving toward destination");
					//move towards destination
					print ("cube position: "+transform.position+" desination position: "+dest.transform.position);
					//TODO: fix movement bug in which probe does not move in the right direction
					transform.position = Vector3.MoveTowards(transform.position, dest.transform.position, SPEED * Time.deltaTime);
				}
			}
		}
	}
	
	//called from update, this method interprets the code and calls the various functions associated with the attached modules
	void run_code() {
		
	}
}
