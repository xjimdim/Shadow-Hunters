using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	public Vector3 moveDestination;
	public string destinationText = "none";
	public float moveSpeed = 10.0f;
	public string currentLocation = "none";
	public bool moveStarted = false;

	public string PlName = "PLAYER";
	public string PlayerCharacter = "";
	public string PlayerColor = "";
	public string PlayerRace ="";
	public int DiesAt = -1;
	public int DamagePoints = 0;
	public Vector3 realPosition;
	public string killedBy = "";
	public bool AbilityActivated = false;
	public bool AbilityDisabled = false;
	public int extraRounds = 0;

	public Tile fromPlace;
	public Tile place;
	public bool movedone = false;

	public bool isRevealed = false;
	public List <string> eksoplismoi = new List<string>();

	void Awake () {
		moveDestination = transform.position;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

//	public virtual void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
//
//	}


	
	public virtual void TurnUpdate () {
		// afth i methodos ginete override sto userplayer srript ;)
	}


}
