using UnityEngine;
using System.Collections;

/*
 * 
 * Nav module API script
 * Exposes the nav module API to be called by the probecode
 * 
 */ 
public class mod_nav : MonoBehaviour {
	
	public string mod_name = "Nav";
	mission_data md;

	// Use this for initialization
	void Start () {
		md = gameObject.GetComponent<mission_data>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	/*API*/
	
	//fake method for testing
	public float AddOne(float f) {
		return f + 1;	
	}
	//returns a list of references to planets in the solar system and their distances 
	public ArrayList GetLocalPlanets() {
		return null;	
	}
	
	//Changes the 'target' planet and sets 'in_orbit' to false, sending the probe to that planet
	public void MoveToPlanet(GameObject target) {
		md.dest = target;
	}
	
	//returns a reference to the current planet ('target')
	public GameObject GetCurrentPlanet() {
		return null;
	}
	
	//gets absolute location in space
	public Vector3 GetAbsLocation() {
		return new Vector3();	
	}
	/*END API*/
}
