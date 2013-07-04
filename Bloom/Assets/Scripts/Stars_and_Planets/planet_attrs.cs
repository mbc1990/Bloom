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
	
	//**attributes**\\
	//TODO: Base this mechanic on real factors such as atmosphere, temperature, and resources
	//whether or not population can spread here
	public bool liveable;
	
	//starts at 0, if it's liveable population may spread here
	public int population;
	
	//amount of resource x in the planet
	public int resource_x;
	
	//TODO: native alien population mechanic
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(sun_location, new Vector3(0,0,1), speed*Time.deltaTime);
	}
}
