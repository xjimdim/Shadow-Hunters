using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserPlayer : Player {

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

//	public override void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
//		if (stream.isWriting)
//		{
//			// We own this player: send the others our data
//			stream.SendNext(transform.position);
//			Debug.Log ("Writing");
//			
//			
//			
//		}
//		else
//		{
//			// Network player, receive data
//			Debug.Log ("Receiving");
//			realPosition = (Vector3)stream.ReceiveNext();
//			
//			
//		}
//	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(PlName);
			stream.SendNext(destinationText);
			stream.SendNext(moveDestination);
			stream.SendNext(movedone);
			stream.SendNext(DamagePoints);
			stream.SendNext(moveStarted);

			
		}
		else
		{
			// Network player, receive data
			PlName  = (string)stream.ReceiveNext();
			destinationText = (string)stream.ReceiveNext();
			moveDestination = (Vector3)stream.ReceiveNext();
			movedone = (bool)stream.ReceiveNext();
			DamagePoints = (int)stream.ReceiveNext();
			moveStarted = (bool)stream.ReceiveNext();

		}
	}

	public void moveCurrentPlayer(Tile destTile, string dt) { //called onUpdate
		

		if(!moveStarted){
		//if(!moveStarted && (fromPlace == null || (fromPlace != null && (place.transform.name != fromPlace.transform.name)))){
			if(fromPlace == null || (fromPlace != null && place.transform.name != fromPlace.transform.name)){
				moveDestination = destTile.getEmptyPlace() + 0.5f * Vector3.up;
			}

			//Debug.Log ("get empty called from moveCurrentPlayer method");

			if(fromPlace == null){
				destinationText = dt; 
				currentLocation = destinationText;  //the from destination;
			}
			else{
				currentLocation = destinationText;  //the from destination;
				destinationText = dt; 
			}

		}
		
		// for the deactivation of the cards when someone is in the destination tile 
		GameManager.instance.DestinationTextSet = true;
		GameManager.instance.MyDestinationText = dt;
		
	}

	public void SetPlace(Tile t, string dtxt){
//		place = t;
		GetComponent<PhotonView> ().RPC ("SetPlace_RPC", PhotonTargets.AllBufferedViaServer, t.transform.name+'_'+dtxt);
	}

	[PunRPC]
	public void SetPlace_RPC(string n){
		string[] splitArray = n.Split(new char[]{'_'});
		place = GetTileFromName(splitArray [0]);
		string dt = splitArray [1];

		moveCurrentPlayer (place, dt);

	}

	public void SetFromPlace(Tile t){
//		fromPlace = t;
		GetComponent<PhotonView> ().RPC ("SetFromPlace_RPC", PhotonTargets.AllBufferedViaServer, t.transform.name);
	}
	
	[PunRPC]
	public void SetFromPlace_RPC(string n){
		Debug.Log ("trying to set from place to : " + n);
		fromPlace = GetTileFromName(n);
	}



	Tile GetTileFromName(string n){

		Tile[] tiles = FindObjectsOfType(typeof(Tile)) as Tile[];

		foreach (Tile t in tiles) {
			if(t.transform.name == n){
				return t;
			}		
		}

		return null;
	}



	public void CorrectColor(){
	
		transform.GetComponent<Renderer>().material.color = GameManager.instance.GetColorFromString (PlayerColor);

	}


	public override void TurnUpdate ()
	{
		movedone = false;
		if (Vector3.Distance(moveDestination, transform.position) > 0.1f) {
			moveStarted=true;

			if(fromPlace == null){
				//this is first move
				transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;
			}	

			if(fromPlace != null && place.transform.name != fromPlace.transform.name ){
				//second++ move and only if its not the same
				transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;
			}	


			if (Vector3.Distance(moveDestination, transform.position) <= 0.1f) {
				// almost finished moving

				transform.position = moveDestination;
				//GameManager.instance.place.NumberOfPlayersInArea++;
				//place.GetComponent<PhotonView>().RPC ("IncreaseNumOfPlayers_RPC", PhotonTargets.AllBuffered, null);

				if(fromPlace == null || (fromPlace != null && place.transform.name != fromPlace.transform.name)){
					place.GetComponent<PhotonView>().RPC ("TakePlace_RPC", PhotonTargets.AllBuffered, PlName);
				}

				

			    
				//GameManager.instance.place.takePlace(GameManager.instance.players[GameManager.instance.currentPlayerIndex].gameObject);

				Debug.Log("onoma: "+GameManager.instance.players[GameManager.instance.currentPlayerIndex].gameObject.name);

				if(fromPlace != null && place.transform.name != fromPlace.transform.name ){
					//fromPlace.GetComponent<PhotonView>().RPC ("DecreaseNumOfPlayers_RPC", PhotonTargets.AllBuffered, null);
					//GameManager.instance.fromPlace.NumberOfPlayersInArea--;

					fromPlace.GetComponent<PhotonView>().RPC ("LeavePlace_RPC", PhotonTargets.AllBuffered, PlName);
					//GameManager.instance.fromPlace.leavePlace(GameManager.instance.players[GameManager.instance.currentPlayerIndex].gameObject);
				}
				else{
					Debug.Log("That was the first movement for that player");
				}

				GameManager.instance.waitfordicedelete=false;

//				if (GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName == PhotonNetwork.player.name){
//					GameManager.instance.DisableCardsForArea(destinationText);
//				}
				if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].PlName == PhotonNetwork.player.name) {

					if(GameManager.instance.GetPlayersInArea(destinationText)>1 && (destinationText != "none")){  
						//there are more than 1 players (1 is ourselves) here so we can attack them
						GameObject fs = GameObject.Find ("FourSidedDie(Clone)");
						GameObject ss = GameObject.Find ("SixSidedDie(Clone)");

						if (fs == null && ss == null) {
							GameObject AttackButtonGO = GameObject.Find ("AttackButton");
							AttackButtonGO.GetComponent<Button> ().interactable = true; 
						}
					}




				
				}



				//GameManager.instance.nextTurn ();





				//wait for end turn.

			}
			moveStarted=false;  //move finished
		}
		
		base.TurnUpdate ();
	}
}
