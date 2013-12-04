using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Interpreter;

public class Manager : MonoBehaviour {
	
	//Connect to editor
	public GameObject planet; //sphere prefab for planet with basic planet_attr script
	public GameObject star; //sphere prefab for sun with basic star_attr script
	public GameObject probe; //cube probe prefab
	
	//global vars
	public GameObject homeworld;
	
	//list of planets in the solarsystem
	public ArrayList pl_list = new ArrayList();
	
	//constants
	//starting population of the homeworld
	int STARTING_POP = 1000;
	
	
	// Use this for initialization
	void Start () {
	
		print("Starting home screen");
		
		//Generate home solar system
		GameObject s = GenerateSolarSystem(new Vector3(0,0,0), .5f);
		homeworld = GeneratePlanet(new Vector3(0,0,0), 1f);
		homeworld.transform.localScale *= 2;
		homeworld.GetComponent<planet_attrs>().population = STARTING_POP;
		
		//testing token stuff
		//TODO: remove this code
		/*
		IdentifierToken t = new IdentifierToken("futurevar");
		OperatorToken ot = new OperatorToken('+');
		List<Token> toks = new List<Token>();
		toks.Add(t);
		toks.Add(ot);
		for(int i = 0; i < 2; i++) {
			string tp = toks[i].GetTokenType();
			if(tp == "identifier") {
				IdentifierToken itk = toks[i] as IdentifierToken;
				print (itk.GetValue());
			} else if(tp == "operator") {
				OperatorToken otk = toks[i] as OperatorToken;
				print (otk.GetValue());
			}
		}
		*/
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
		float r_speed = UnityEngine.Random.Range(.5f, 3f);
		p_script.speed = r_speed;
		
		//liveability
		float live = UnityEngine.Random.Range(0, 1f);
		p_script.liveable = (live < liveable) ? true : false;
		
		//minerals amount
		p_script.minerals = (int)UnityEngine.Random.Range (0, 400f);
		
		//energy amount
		p_script.energy = UnityEngine.Random.Range(0, 10000f);

                //name
                //pl_script.planet_name = GeneratePlanetName();
		
		//random size
	//	float r_size = UnityEngine.Random.Range(.5f, 3);
	//	p.transform.localScale *= r_size;
		
		//add to list
		pl_list.Add(p);
		
		return p;
	}

         
        string GeneratePlanetName() {
//            int ran = UnityEngine.Random.Range(0, greeks.length()) as int;     
//            print("Random greek number: "+ran);
			return "";
        }
	
	
}
