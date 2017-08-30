using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RGBCardHolder : MonoBehaviour {

	public enum State {
		green,blue,red
	}

	public State CardType;
	public GameObject cardPrefab;

	public GameObject GreenCardSelectPlayerHead;
	public Transform GreenCardSelectPlayerScrollContain;


	public void CardClicked (){
		


		if (CardType == State.green) {

			instantiateGreenCard ();

			// can be clicked only once per turn:
			GameManager.instance.DisableGreenCards(true);
			GameManager.instance.DisableRedCards(true);
			GameManager.instance.DisableBlueCards(true);
		}
		else if(CardType == State.red){


			instantiateRedCard ();

			// can be clicked only once per turn:
			GameManager.instance.DisableGreenCards(true);
			GameManager.instance.DisableRedCards(true);
			GameManager.instance.DisableBlueCards(true);
			
		}
		else if(CardType == State.blue){

			instantiateBlueCard ();

			// can be clicked only once per turn:
			GameManager.instance.DisableGreenCards(true);
			GameManager.instance.DisableRedCards(true);
			GameManager.instance.DisableBlueCards(true);
		}



	}

	public void instantiateGreenCard(){
		GameObject gcard;
		gcard = Instantiate(cardPrefab) as GameObject;

		gcard.transform.SetParent (GameObject.Find ("GreenCardsHolder").transform, false);

		gcard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (gcard.GetComponent<RectTransform>().anchoredPosition.x, gcard.GetComponent<RectTransform>().anchoredPosition.y, 0);

		gcard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		Sprite gsprite = GameManager.instance.GetRandomCard ("green");

		gcard.GetComponent<GreenCardScript> ().GreenCardID = gsprite.name;
		gcard.GetComponent<GreenCardScript> ().G1 = GreenCardSelectPlayerHead;
		gcard.GetComponent<GreenCardScript> ().G2 = GreenCardSelectPlayerScrollContain;

		gcard.GetComponent<CardSpriteHandling> ().FrontSprite = gsprite;
		gcard.GetComponent<CardSpriteHandling> ().Flip();

		gcard.GetComponent<GreenCardScript> ().CardClicked ();

		string myname = PhotonNetwork.player.name;
		GetComponent<PhotonView>().RPC("instantiateGreenCard_RPC", PhotonTargets.Others, gsprite.name, myname);



	}

	[PunRPC]
	public void instantiateGreenCard_RPC(string spritename, string playername){

		GameObject gcard;
		gcard = Instantiate(cardPrefab) as GameObject;
		gcard.transform.SetParent (GameObject.Find ("GreenCardsHolder").transform, false);

		gcard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (gcard.GetComponent<RectTransform>().anchoredPosition.x, gcard.GetComponent<RectTransform>().anchoredPosition.y, 0);

		gcard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		Sprite gsprite = GameManager.instance.GetCardFromSpriteName ("Greens", spritename);

		gcard.GetComponent<GreenCardScript> ().GreenCardID = gsprite.name;

		gcard.GetComponent<CardSpriteHandling> ().FrontSprite = gsprite;
		gcard.GetComponent<CardSpriteHandling> ().Flip();

		gcard.GetComponent<GreenCardScript> ().CardClickedNetwork (playername);
	}


	public void instantiateRedCard(){
		GameObject rcard;
		rcard = Instantiate(cardPrefab) as GameObject;
		rcard.transform.SetParent (GameObject.Find ("RedCardsHolder").transform, false);

		rcard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (rcard.GetComponent<RectTransform>().anchoredPosition.x, rcard.GetComponent<RectTransform>().anchoredPosition.y, 0);

		rcard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		Sprite rsprite = GameManager.instance.GetRandomCard ("red");


		string[] redSplitArray =  rsprite.name.Split(new char[]{'#'}); 

		rcard.GetComponent<RedCardScript> ().RedCardID = redSplitArray[0];
		rcard.GetComponent<RedCardScript> ().type = redSplitArray[1];
		rcard.GetComponent<CardSpriteHandling> ().FrontSprite = rsprite;
		rcard.GetComponent<CardSpriteHandling> ().Flip();

		rcard.GetComponent<RedCardScript> ().CardClicked ();

		string myname = PhotonNetwork.player.name;

		GetComponent<PhotonView>().RPC("instantiateRedCard_RPC", PhotonTargets.Others, rsprite.name, myname);
		foreach (Player p in GameManager.instance.players) {
			if (p.PlName == myname) {
				if(redSplitArray[1] == "eksoplismos"){
					if (p.eksoplismoi.Contains (redSplitArray [0]) == false || (p.eksoplismoi.Contains (redSplitArray [0]) && redSplitArray[0]=="Reds_5") ) {
						p.eksoplismoi.Add (redSplitArray[0]);
					}


					if (redSplitArray [0] == "Reds_7") {
						
						if(GameManager.instance.GetPlayersInOppositeAreas(p.destinationText)>0 && (p.destinationText != "none") && p.PlName == PhotonNetwork.player.name){ 
							
							if (!GameManager.instance.AttackStarted) {
								GameManager.instance.AttackButtonGO.GetComponent<Button> ().interactable = true;
							}


						}

						if (GameManager.instance.AttackButtonGO.GetComponent<Button> ().interactable && GameManager.instance.GetPlayersInOppositeAreas (p.destinationText) == 0) {
							GameManager.instance.AttackButtonGO.GetComponent<Button> ().interactable = false;
						}

					}



				}
			}
		}

	}

	[PunRPC]
	public void instantiateRedCard_RPC(string spritename, string playername){

		GameObject rcard;
		rcard = Instantiate(cardPrefab) as GameObject;
		rcard.transform.SetParent (GameObject.Find ("RedCardsHolder").transform, false);

		rcard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (rcard.GetComponent<RectTransform>().anchoredPosition.x, rcard.GetComponent<RectTransform>().anchoredPosition.y, 0);

		rcard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		Sprite rsprite = GameManager.instance.GetCardFromSpriteName ("Reds", spritename);

		string[] redSplitArray =  rsprite.name.Split(new char[]{'#'}); 

		rcard.GetComponent<RedCardScript> ().RedCardID = redSplitArray[0];
		rcard.GetComponent<RedCardScript> ().type = redSplitArray[1];

		rcard.GetComponent<CardSpriteHandling> ().FrontSprite = rsprite;
		rcard.GetComponent<CardSpriteHandling> ().Flip();

		rcard.GetComponent<RedCardScript> ().CardClickedNetwork (playername);

//		foreach (Player p in GameManager.instance.players) {
//			if (p.PlName == playername) {
//				if(redSplitArray[1] == "eksoplismos"){
//					if (p.eksoplismoi.Contains (redSplitArray [0]) == false || (p.eksoplismoi.Contains (redSplitArray [0]) && redSplitArray[0]=="Reds_5") ) {
//						p.eksoplismoi.Add (redSplitArray[0]);
//					}
//						
//				}
//			}
//		}

	}


	public void instantiateBlueCard(){
		Debug.Log ("instantiateBlueCard run");
		GameObject bcard;
		bcard = Instantiate(cardPrefab) as GameObject;
		bcard.transform.SetParent (GameObject.Find ("BlueCardsHolder").transform, false);

		//bcard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (bcard.GetComponent<RectTransform>().anchoredPosition.x, bcard.GetComponent<RectTransform>().anchoredPosition.y, 0);
		bcard.GetComponent<RectTransform> ().position = GameManager.instance.blueCardButton.GetComponent<RectTransform> ().position;


		bcard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		Sprite bsprite = GameManager.instance.GetRandomCard ("blue");
		string[] blueSplitArray =  bsprite.name.Split(new char[]{'#'}); 

		bcard.GetComponent<BlueCardScript> ().BlueCardID = blueSplitArray[0];
		bcard.GetComponent<BlueCardScript> ().type = blueSplitArray[1];

		bcard.GetComponent<CardSpriteHandling> ().FrontSprite = bsprite;
		bcard.GetComponent<CardSpriteHandling> ().Flip();

		bcard.GetComponent<BlueCardScript> ().CardClicked ();


		string myname = PhotonNetwork.player.name;
		GetComponent<PhotonView>().RPC("instantiateBlueCard_RPC", PhotonTargets.Others, bsprite.name, myname);




		foreach (Player p in GameManager.instance.players) {
			if (p.PlName == myname) {
				if(blueSplitArray[1] == "eksoplismos"){
					if (p.eksoplismoi.Contains (blueSplitArray [0])==false) {
						p.eksoplismoi.Add (blueSplitArray[0]);	
					}

				}
			}
		}

	}

	[PunRPC]
	public void instantiateBlueCard_RPC(string spritename, string playername){
		Debug.Log ("instantiateBlueCard_RPC run");
		GameObject bcard;
		bcard = Instantiate(cardPrefab) as GameObject;
		bcard.transform.SetParent (GameObject.Find ("BlueCardsHolder").transform, false);

		bcard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (bcard.GetComponent<RectTransform>().anchoredPosition.x, bcard.GetComponent<RectTransform>().anchoredPosition.y, 0);

		bcard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		Sprite bsprite = GameManager.instance.GetCardFromSpriteName ("Blues", spritename);

		string[] blueSplitArray =  bsprite.name.Split(new char[]{'#'}); 

		bcard.GetComponent<BlueCardScript> ().BlueCardID = blueSplitArray[0];
		bcard.GetComponent<BlueCardScript> ().type = blueSplitArray[1];

		bcard.GetComponent<CardSpriteHandling> ().FrontSprite = bsprite;
		bcard.GetComponent<CardSpriteHandling> ().Flip();

		bcard.GetComponent<BlueCardScript> ().CardClickedNetwork (playername);


//		foreach (Player p in GameManager.instance.players) {
//			if (p.PlName == playername) {
//				if(blueSplitArray[1] == "eksoplismos"){
//					if (p.eksoplismoi.Contains (blueSplitArray [0])==false) {
//
//						p.eksoplismoi.Add (blueSplitArray[0]);	
//					}
//
//
//				}
//			}
//		}

	}


}
