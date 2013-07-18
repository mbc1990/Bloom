using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 
 * This is one of the more important scripts.
 * This is the core of the probe/mission logic. It's where the code and module attributes (list of references to modules) are stored
 * It's also where probecode is executed
 * 
 */ 
public class mission_data : MonoBehaviour {
	
	//mission code, initialized and set by GUI when the probe is "launched"
	public string code;
	
	//current destination
	public GameObject dest;
	
	//list of gameobjects w/ module script & interface-like module script that has the module name
	public ArrayList modules = new ArrayList();
	
	//true when in orbit at a destination
	bool in_orbit = false;
	
	//wait until the mission has been launched to begin moving
	public bool mission_started = false;
	
	//Used for correcting oval shaped orbit around moving planets (position of destination planet LAST frame)
	Vector3 lastdestpos;
	
	//symbol tables for interpreter
	//table of type floats
	Dictionary<string,float> fl_table = new Dictionary<string,float>();
	
	//constants
	//distance from target distination at which point the probe will stop travelling and begin orbitting
	float ORBIT_DIST = 100;
	
	//rate at which probe orbits planet
	float ORBIT_SPEED = 250;
	
	//speed moving through non-interstellar-space (this will probably be modifed by various modules)
	float SPEED = 1000;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		//do these things once the mission is underway (not while it's being constructed in the mission planning gui)
		if(mission_started) {
			if (in_orbit) {
				//rotate around destination 
				transform.parent.RotateAround(dest.transform.position,new Vector3(0,0,1),ORBIT_SPEED*Time.deltaTime);
				
				//adjust rotation for changing position of destination
				transform.parent.position += dest.transform.position - lastdestpos;
				//update lastdestpos to reflect new position of destination
				lastdestpos = dest.transform.position;
				
			} else {
				if(Vector3.Distance(transform.parent.position, dest.transform.position) < ORBIT_DIST) {
					//arrive at destination, begin orbit
					in_orbit = true;
					//give lastdestpos a starting value
					lastdestpos = dest.transform.position;
				} else {
					//move towards destination
					transform.parent.position = Vector3.MoveTowards(transform.parent.position, dest.transform.position, SPEED * Time.deltaTime);
				}
			}
			
			//execute the probecode
			run_code();
		}
	}
	
	//called from update, this method interprets the code and calls the various functions associated with the attached modules
	//TODO: Exception handling & incorrect probe code handling 
	string run_code() {
		//tokenize the code
		//TODO: run this once, not every time the code is executed
		ArrayList toks = Tokenize ();
		/* debugging */ /* 
		foreach(string e in toks) {
			print (e);	
		}
		Debug.Break();
		*/
		
		//Iterate over tokens, carrying out commands
		for(int exe_pos = 0; exe_pos < toks.Count; exe_pos++) {
			//reading in the token
			string cur = toks[exe_pos] as string;
			if(cur == "if") {
				//evaluate if condition, move exe_pos to end of if block if the condition is false
			} else if(cur == "float") {
				//variable change, use float table
				//Variable name
				string v_name = toks[exe_pos+1] as string;
				
				//syntax error
				if (toks[exe_pos + 2] as string != "=") {
					return "syntax error around token "+exe_pos.ToString()+". Equal sign out of order";
				}
				
				//get expression to evaluate (all tokens between "=" and ";")
				ArrayList exp = new ArrayList();
				int tok_count = exe_pos + 3;
				do {
					exp.Add(toks[tok_count]);
					tok_count++;
				} while (toks[tok_count] as string != ";");
				
				//nullable type - this lets null be returned in the error case that the expression passed in cannot be evaluated to a number
				float? val = eval_math_exp(exp);
				if(val.HasValue) {
					fl_table[v_name] = val.Value;	
				} else {
					return "a math expression around token "+exe_pos.ToString()+" was invalid";	
				}
				
				//float has been evaluated and the symbol table has been updated
				//update the execution position to the token following the expression that was just evaluated
				exe_pos = tok_count + 1; // the +1 is because the execution position is currently at the semicolon ending the expression 
				
				/*testing*/ /*
				print ("fl table: "+fl_table[v_name].ToString());
				Debug.Break(); */
				
				
			} else if(cur == "body") {
				//needs more thought, but this will be used to keep track of planets (for example, the nav module will return a list of local planets)
			} else if (cur == "mod") {
				//a module api is about to be called, get the module name and method call from the next token, then get the results
				
			} else {
				//shouldn't happen?
			}
		}
		
		return "success";
	}
	
	/*
	 * Evaluates a mathematical expression (floats only)
	 * Recursive left-to-right evaluation (?)
	 * Must also be able to evaluate module things to float values
	 */
	//float is returned as a nullable type
	float? eval_math_exp(ArrayList exp) {
		print ("evaluating math expression...");
		/* debugging */ /*
		foreach(string tok in exp) {
			print (tok);	
		}
		Debug.Break ();
		*/
		
		//base case: arraylist of length 1
		if(exp.Count == 1) {
			float val = new float();
			if (float.TryParse(exp[0] as string,out val)) {
				return val;	
			} else {
				//float parsing failed, return null as error indicator
				return null;
			}
		} else {
			//recursively return null if the recursive call returns null, otherwise evaluate
			string cur = exp[0] as string;
			//recursively call the spliced array (1:)
			if(cur == "(") {
				
			} else if(cur == ")") {
				
			} else if(cur == "*") {
			
			} else if (cur == "/") {
				
			} else if (cur == "%") {
				
			} else if (cur == "+") {
				
			} else if (cur == "-") {
				
			} else{
				
			}
		}
		
		return null;
	}
	
	/*
	 * Takes a list of strings and returns true or false based on whether or not the input evaluates true or false
	 */ 
	bool eval_cond(ArrayList cond) {
		//A single variable is evaluated (base case)
		//TODO: Fix this 
		if(cond.Count == 2) {
			//check var table
			if (cond[0] as string == "float") {
				//look up value of variable
				//return true if it's nonzero
			}
			//other var table cases here
		} else {
			//case of an equality check - compare the value before the == to the value after the ==
			//probably use eval_math_exp here
		}
		return true;
	}
	
	//this splits the code at whitespace to provide a list of tokens
	//it also may do some pre-processing of the code (such as giving a jump-to index to if statements)
	private ArrayList Tokenize() {
		//split at spaces
		char[] delim = new char[2];
		delim[0] = ' ';
		delim[1] = '\n';
		string[] sp = code.Split(delim);
		ArrayList toks = new ArrayList();
		toks.AddRange(sp);
		
		//remove indentation
		ArrayList to_remove = new ArrayList();
		foreach(string e in toks) {
			if(e.Length == 0) {
				to_remove.Add (e);	
			}
		}
		foreach(string e in to_remove) {
			toks.Remove(e);	
		}
		return toks;
		
	}
}
