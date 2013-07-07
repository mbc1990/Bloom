using UnityEngine;
using System.Collections;

/*
 * This script keeps track of missions launched from a particular planet
 * If the "command" mechanic is damaged in some way, it might be important to have a list of probes
 * 
 */
public class mission_manager : MonoBehaviour {
	
	//list of active missions
	public ArrayList active_missions = new ArrayList();
	Manager m;
	

	// Use this for initialization
	void Start () {
		//get manager (for probe prefab)
		GameObject man = GameObject.Find("Manager_Obj") as GameObject;
		m = man.GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//called when a mission is launched from mision_planning_gui
	//does any initialization (such as calling a special launch visual effect in mission_vis) and adds the probe to the list of active missions 
	public void Launch(GameObject probe) {
		//give it a cube prefab for temporarily visualization purposes
		GameObject pref = Instantiate(m.probe) as GameObject;
		//TODO: Instantiate the probecube at the right place
		pref.transform.position = new Vector3(0, 500, 0);
		probe.transform.parent = pref.transform;
		active_missions.Add(probe);
	}
}
