using UnityEngine;
using System.Collections;

/*
 * This shows the HUD GUI that appears on the side of the screen and provides info about missions, resources, and other things (opponents? threats?)
 * 
 */ 
public class hud : MonoBehaviour {

	Manager man;

	// Use this for initialization
	void Start () {
		man = gameObject.GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		
		//background boxes
		//Resources box
		GUI.Box(new Rect(-10,Screen.height/10,110,Screen.height/5), "Resources");
		
		//Mission box
		GUI.Box(new Rect(-10,Screen.height/10 + Screen.height/5 + 20,110,Screen.height/5), "Missions");
		
		//Third yet unused box
		GUI.Box(new Rect(-10,Screen.height/10 + Screen.height/5 * 2 + 40,110,Screen.height/5), "Threats?");
		
		//list resources
		int total_pop = 0;
		foreach(GameObject pl in man.pl_list) {
			planet_attrs p = pl.GetComponent<planet_attrs>();	
			total_pop += p.population;
		}
		GUI.Label(new Rect(20, Screen.height/10 + 20, 110, 20), "Pop: "+total_pop);
		
	}
}
