using UnityEngine;
using System.Collections;
using System;

public class Manager : MonoBehaviour {
	
	//Connect to editor
	public GameObject planet; //sphere prefab for planet with basic planet_attr script
	public GameObject star; //sphere prefab for sun with basic star_attr script
	
	//global vars
	public GameObject homeworld;
	
	
	// Use this for initialization
	void Start () {
		print("Starting home screen");
		
		//Generate home solar system
		GameObject s = GenerateSolarSystem(new Vector3(0,0,0), .5f);
		homeworld = GeneratePlanet(new Vector3(0,0,0), 1f);
		homeworld.transform.localScale *= 2;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Randomly generates a solar system centered around a sun at this location with liveable percent liveable planets
	//Returns the "sun" of the solar system
	GameObject GenerateSolarSystem(Vector3 sun_location, float liveable) {
		//instantiate sun
		GameObject s = Instantiate(star) as GameObject;
		
		//size and position
		//TODO: generate size randomly
		s.transform.position = sun_location;
		s.transform.localScale += new Vector3(600f,600f,600f);
		//generate sun attributes (heat, volume)
		
		
		//pick number of planets
		float num_planets = UnityEngine.Random.Range(7f,14f);
		num_planets = Mathf.Floor(num_planets);
		for(int i = 0; i < (int)num_planets; i++) {
			GeneratePlanet(sun_location,liveable);	
		}
		
		return s;
	}
	
	//Instantiates a planet within the solar system, sets and picks attributes, picks location
	//TODO: Random selection of attributes
	GameObject GeneratePlanet(Vector3 sun_location, float liveable) {
		GameObject p = Instantiate(planet) as GameObject;
		planet_attrs p_script = p.GetComponent<planet_attrs>();
		p_script.sun_location = sun_location;
		
		//distance
		float dist = UnityEngine.Random.Range(800f, 10000f);
		p.transform.position = new Vector3(dist,0,0);
		p.transform.localScale += new Vector3(50f,50f,50f);
		
		//rotation speed
		//TODO: adjust angle (speed) based on distance so that farther planets don't orbit faster than closer ones
		float r_speed = UnityEngine.Random.Range(2f, 8f);
		p_script.speed = r_speed;
		
		//liveability
		float live = UnityEngine.Random.Range(0, 1f);
		p_script.liveable = (live < liveable) ? true : false;
		
		//resource X amount
		p_script.resource_x = (int)UnityEngine.Random.Range (0, 400f);
		
		return p;
	}
	
	
}
