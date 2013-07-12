using UnityEngine;
using System.Collections;

/*
 * 
 * This is one of the more important scripts.
 * This is the core of the probe/mission logic. It's where the code and module attributes (list of references to modules) are stored
 * It's also where probecode is executed
 * 
 */ 
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
	
	//Used for correcting oval shaped orbit around moving planets (position of destination planet LAST frame)
	Vector3 lastdestpos;
	
	//constants
	//distance from target distination at which point the probe will stop travelling and begin orbitting
	float ORBIT_DIST = 100;
	
	//rate at which probe orbits planet
	float ORBIT_SPEED = 250;
	
	//speed moving through non-interstellar-space (this will probably be modifed by various modules)
	float SPEED = 1000;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//do these things once the mission is underway (not while it's being constructed in the mission planning gui)
		if(mission_started) {
			if (in_orbit) {
				//rotate around destination 
				transform.parent.RotateAround(dest.transform.position,new Vector3(0,0,1),ORBIT_SPEED*Time.deltaTime);
				
				//adjust rotation for changing position of destination
				transform.parent.position += dest.transform.position - lastdestpos;
				//update lastdestpos to reflect new position of destination
				lastdestpos = dest.transform.position;
				
			} else {
				if(Vector3.Distance(transform.parent.position, dest.transform.position) < ORBIT_DIST) {
					//arrive at destination, begin orbit
					in_orbit = true;
					//give lastdestpos a starting value
					lastdestpos = dest.transform.position;
				} else {
					//move towards destination
					transform.parent.position = Vector3.MoveTowards(transform.parent.position, dest.transform.position, SPEED * Time.deltaTime);
				}
			}
		}
	}
	
	//called from update, this method interprets the code and calls the various functions associated with the attached modules
	void run_code() {
		
	}
}
