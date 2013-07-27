using UnityEngine;
using System.Collections;

/*
 * Contains planet attributes such as atmosphere composition, size, population, 
 * 
 * 
 */ 
public class planet_attrs : MonoBehaviour {
	
	//rotates around this
	public Vector3 sun_location;
	
	//rotation speed
	public float speed;
	
	//name (random float)
	public float pl_name;
	
	//**attributes**\\
	//TODO: Base this mechanic on real factors such as atmosphere, temperature, and resources
	//whether or not population can spread here
	public bool liveable;
	
	//starts at 0, if it's liveable population may spread here
	public int population;
	
	//amount of minerals in the planet
	public int minerals;
	
	//amount of energy in the planet
	public float energy;
	
	//last time p/e/r was updated
	float last_update;
	
	//constants 
	//how often population/energy/rocks are updated (in seconds)
	static float TICK_SIZE = 5;
	
	// Use this for initialization
	void Start () {		
		pl_name = UnityEngine.Random.Range(100000f,700000f);
		last_update = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//orbit sun
		transform.RotateAround(sun_location, new Vector3(0,0,1), speed*Time.deltaTime);
		
		//updating of energy/minerals/population based on functions
		if(!GUI_locks.PLAN_MISSION && Time.time - last_update >= TICK_SIZE) {
			//update last_update
			last_update = Time.time;
			
			//TODO: design the functions	
			//
			if(population > 0 && minerals > 0) {
				minerals--;
				energy++;
			} 
			
			if(minerals <= 0) {
				population--;
				energy = energy > 0 ? energy-1 : energy;
			}
			
		}
	}
}
