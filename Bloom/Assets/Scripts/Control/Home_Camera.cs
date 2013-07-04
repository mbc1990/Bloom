using UnityEngine;
using System.Collections;

public class Home_Camera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//position camera at default starting position
		transform.position = new Vector3(0,0,-1000f);
		
		//orthographic size and scale
		camera.orthographicSize = 5000;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
