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
	public float speed;
	
	//TODO: Decide on/implement attributes

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(sun_location, new Vector3(0,0,1), speed*Time.deltaTime);
	}
}
