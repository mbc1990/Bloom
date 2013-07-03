using UnityEngine;
using System.Collections;
using System;

public class Manager : MonoBehaviour {
	
	//Connect to editor
	public GameObject planet; //sphere prefab for planet with basic planet_attr script
	public GameObject sun; //sphere prefab for sun with basic planet_attr script
	
	
	// Use this for initialization
	void Start () {
		print("Starting home screen");
		
		//Generate home solar system
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Randomly generates a solar system centered around a sun at this location with liveable percent liveable planets
	//Returns the "sun" of the solar system
	GameObject GenerateSolarSystem(Vector3 sun_location, float liveable) {
		//instantiate sun
		//generate sun attributes (heat, volume)
		
		//pick number of planets
		//for each number
			//instantiate planet
			//generate planet attributes (liveability, size, orbit speed, gas vs rocky)
			//set parent location (for orbitting)
			//make planet child of sun
		
	}
	
	
}
