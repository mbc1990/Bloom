using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Interpreter;
using System.Text.RegularExpressions;

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
			run_code_new();
			Debug.Break();
		}
	}
	
	
	//attempt no. 2 @ an interpreter
	string run_code_new() {
		List<Token> tokens = lexer(code);
		for(var i = 0; i < tokens.Count; i++) {
			if(tokens[i].GetTokenType() == "operator") {
				OperatorToken o = tokens[i] as OperatorToken;	
				print("Operator: "+o.GetValue());
			} else if(tokens[i].GetTokenType() == "number") {
				NumberToken n = tokens[i] as NumberToken;
				print ("Number: "+n.GetValue());
			} else if(tokens[i].GetTokenType() == "identifier") {
				IdentifierToken t = tokens[i] as IdentifierToken;
				print ("Identifier: "+t.GetValue());
			}
		}
		return " ";	
	}
	
	bool is_whitespace(string text) {
		string[] whitespace = { " ", "\n" };
		for(int i=0; i < whitespace.Length; i++) {
			if(whitespace[i] == text) {
				return true;
			}
		}
		return false;
	}
	
	bool is_operator(string text) {
		string[] op_arr = { "+", "-", "(", ")", "/", "*", "%", "^"}; //valid operators
		for(int i=0; i < op_arr.Length; i++) {
			if(op_arr[i] == text) {
				return true;
			}
		}
		return false;
	}
	
	bool is_digit(string text) {
		string[] dig_arr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",};
		for(int i=0; i < dig_arr.Length; i++) {
			if(dig_arr[i] == text) {
				return true;
			}
		}
		return false;
	}
	
	bool is_identifier(string text) {
		string[] letters = {"a", "b", "c", "d", "e"};	
		for(int i=0; i < letters.Length; i++) {
			if(letters[i] == text) {
				return true;
			}
		}
		return false;
	}
	
	List<Token> lexer(string program) {
		List<Token> toks = new List<Token>();
		int loc = 0;
		while(loc < program.Length) {
			char next_character = program[loc];
			if(is_whitespace(next_character.ToString())) {	
				//do nothing
				loc++;
			} else if(is_operator(next_character.ToString())) {
				OperatorToken o = new OperatorToken(next_character);	
				toks.Add(o);
				loc++;
			} else if(is_digit(next_character.ToString())) {
				string temp = next_character.ToString();
				loc++;
				while(loc < program.Length && is_digit(program[loc].ToString())) {
					temp += program[loc].ToString();
					loc++;
				}
				NumberToken n = new NumberToken(float.Parse(temp));
				toks.Add (n);
			} else if(is_identifier(next_character.ToString())) {
				string temp = next_character.ToString();
				loc++;
				while(loc < program.Length && is_identifier(program[loc].ToString())) {
					temp += program[loc].ToString();
					loc++;
				}
				IdentifierToken t = new IdentifierToken(temp);
				toks.Add (t);
			}
		}
		return toks;
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
				ArrayList exp = new ArrayList();
				int tok_count = exe_pos + 3;
				do {
					exp.Add(toks[tok_count]);
					tok_count++;
				} while (toks[tok_count] as string != ";");
				
				string apicall = exp[1] as string;
				string[] sp = apicall.Split(new char[] {'.'}, 2); //split by the first '.' character
				string func = sp[1] as string;
				string[] fargs = func.Split(new char[] {'(',')'});
				//get attached API script
				print("sp0: "+sp[0]);
				print ("fargs0: "+fargs[0]);
				print ("fargs1: "+fargs[1]);
				if(sp[0] == "nav") {
					mod_nav api = gameObject.GetComponent<mod_nav>();
					switch (fargs[0]) {
						case "AddOne":	
							print("adding one");
							print(api.AddOne(float.Parse(fargs[1])));
							break;
					}
				} 
				
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
				
			} else if (cur == "mod"){
				//case of treating a module api call as a number
				//so get what the api call returns
				//this is accomplished by parsing the method name and arguments, then using reflection to invoke the method name by the parsed string
				string apicall = exp[1] as string;
				string[] sp = apicall.Split(new char[] {'.'}, 2); //split by the first '.' character
				string func = sp[1] as string;
				string[] fargs = func.Split(new char[] {'(',')'});
				//get attached API script
				print("sp0: "+sp[0]);
				print ("fargs0: "+fargs[0]);
				print ("fargs1: "+fargs[1]);
				if(sp[0] == "nav") {
					mod_nav api = gameObject.GetComponent<mod_nav>();
					switch (fargs[0]) {
						case "AddOne":	
							print("adding one");
							print(api.AddOne(float.Parse(fargs[1])));
							break;
					}
				} 
				
				Debug.Break();
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
		if(cond.Count == 1) {
			//check var tables
		} else {
			//case of an equality check - compare the value before the == to the value after the ==
			//split at the == and call eval_cond on each hal
			
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
