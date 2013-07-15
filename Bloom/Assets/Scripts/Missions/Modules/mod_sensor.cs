using UnityEngine;
using System.Collections;


/*
 * 
 * Sensor module API
 * Used to get information about surroundings and planets
 * 
 */ 
public class mod_sensor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*API*/
	//If probe is in orbit, returns an atmosphere reading whose accuracy & precision is based on the amount of energy passed in (between 0 and 1) 
	public ArrayList GetAtmosphere(float power) {
		return null;
	}
	
	//if probe is in orbit, returns a list of resources whos accuracy & precision...
	//arr[0] = energy
	//arr[1] = rocks
	public ArrayList GetResources(float power) {
		return null;	
	}
	
	//mechanic not yet developed, this has to do with pre-existing alien life on planets being explored
	public ArrayList GetLifeforms(float power) {
		return null;	
	}
	
	//another yet to be decided mechanic
	public float GetTemperature(float power) {
		return 0;	
	}
	
	/*END API*/
}
