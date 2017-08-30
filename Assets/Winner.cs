using UnityEngine;
using System.Collections;

public class Winner {

	public string PlName = "PLAYER";
	public string PlayerCharacter = "";
	public string PlayerColor = "";
	public string PlayerRace ="";
	public bool isDead = false;

	public Winner (string upname, string upchar, string upcolor, string uprace){
		this.PlName = upname; 
		this.PlayerCharacter = upchar;
		this.PlayerColor = upcolor;
		this.PlayerRace = uprace;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
