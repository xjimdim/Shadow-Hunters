using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GreenCardScript : MonoBehaviour {

	public string GreenCardID;
	public bool isEnabled = false;

	public GameObject OkPanel;
	public GameObject OkButton;
	public GameObject CardResultPanel;
	public GameObject OkButtonNet;
	public GameObject WaitingNetText;

	public GameObject CardResultNotForMePanel;
	public GameObject CardResultNotForMeButton;

	public GameObject G1;
	public Transform G2;
	public GameObject GreenCardPlayerEntry;

	public bool imIntrested = false;



	public void CardClicked(){

		iTween.MoveTo (gameObject, iTween.Hash ("y", 360, "x", 580, "z", -4.5, "easetype","spring","oncomplete", "displayOkButton"));
		iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "y", 180, "easetype","spring"));
		iTween.ScaleTo (gameObject, iTween.Hash ("x", -0.6, "y", 0.6, "z", 0.6,  "easetype","spring"));
	}


	public void CardClickedNetwork(string m){
		string[] splitArray = m.Split(new char[]{'_'});

		this.GetComponent<Image>().color = Color.white;
		iTween.MoveTo (gameObject, iTween.Hash ("y", 360, "x", 580, "z", -4.5, "easetype","spring","oncomplete", "displayNetText","oncompleteparams",splitArray[0]));
		iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "y", 0, "easetype","spring"));
		iTween.ScaleTo (gameObject, iTween.Hash ("x", -0.6, "y", 0.6, "z", 0.6,  "easetype","spring"));


	}

	public void displayNetText(string m){

		WaitingNetText.SetActive (true);
		WaitingNetText.transform.Find ("Text").GetComponent<Text> ().text = "Waiting for "+ m + " to give this card to someone";



	}

	public void displayOkButtonNetwork()
	{
		OkButtonNet.SetActive (true);
		OkButtonNet.transform.SetParent (this.transform.parent);
		OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0,0,0));
		OkButtonNet.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

	}



	public void displayOkButton()
	{


		OkPanel.SetActive (true);
		OkPanel.transform.SetParent (this.transform.parent);
		OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0,0,0));
		OkPanel.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);


	}

	public void GreenCardOK(){
		Debug.Log ("Green ok clicked");

		ShowGreenCardPanel ();


//
//		OkPanel.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
//		OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0,180f,90f));
//		OkPanel.transform.SetParent (this.transform);
//
//		Destroy (gameObject);


	}

	public void ShowGreenCardPanel(){


		G1.SetActive (true);


		foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in GameManager.instance.players){
				if(ht["PlayerName"].ToString() == localp.PlName){
					GameObject PlayerEntry = Instantiate(GreenCardPlayerEntry) as GameObject;
					PlayerEntry.transform.SetParent (G2);

					string[] splitArray = localp.PlName.Split(new char[]{'_'});

					string EnedraPlayerName = splitArray[0];
					string EnedraPlayerID = splitArray[1];


					PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = EnedraPlayerName;
					PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = EnedraPlayerID;
					PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GameManager.instance.GetColorFromString(localp.PlayerColor);

					PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromGreenCard>().PlayerClickedFromGreenCard());
					PlayerEntry.GetComponent<Button> ().interactable = true;

					//handle images:
					foreach(Transform t in GameManager.instance.PlayerScrollContain.transform){
						if(t.Find ("PlayerName").GetComponent<Text>().text == EnedraPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == EnedraPlayerID){
							Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
							PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

							PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
						} 
					}
				}	
			}
		}

	}

	public void ClearGreenCardPanel(){
		foreach (Transform t in G2) {
			Destroy (t.gameObject);		
		}

		G1.SetActive (false);
	}

	public void GreenCardResult(string receiverName){
		if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].PlName != PhotonNetwork.player.name) {
			//if we are not the active player, we are possible receivers

			if (PhotonNetwork.player.name == receiverName) {
				//if my name == the receiver name DO RECEIVED CODE
				//swap the card and call the method when swap is complete:



				WaitingNetText.SetActive(false);
				iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "y", 180, "easetype","spring", "oncomplete", "ResultsPositiveForThisPlayer", "oncompleteparams",receiverName));

			} 
			else {
				//display info text "this card was given to this player" and ok to close
				WaitingNetText.SetActive(false);

				CardResultNotForMePanel.SetActive (true);
				CardResultNotForMeButton.transform.SetParent (this.transform.parent);
				CardResultNotForMeButton.transform.rotation = Quaternion.Euler (new Vector3 (0,0,0));
				CardResultNotForMeButton.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

				string[] senderName = GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName.Split(new char[]{'_'});
				string[] recName = receiverName.Split(new char[]{'_'});

				CardResultNotForMePanel.transform.Find ("IntroText").GetComponent<Text> ().text = senderName [0] + " gave this card to "+recName[0];


			}


		}
		else {
			//display that the receiver got the card and ok
			OkPanel.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
			OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0,180f,90f));
			OkPanel.transform.SetParent (this.transform);
			OkPanel.SetActive(false);


			CardResultPanel.SetActive (true);
			OkButtonNet.transform.SetParent (this.transform.parent);
			OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0,0,0));
			OkButtonNet.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
			//CardResultPanel.transform.Rotate (new Vector3 (0f,0f,180f));

			string[] recName = receiverName.Split(new char[]{'_'});

			CardResultPanel.transform.Find ("IntroText").GetComponent<Text> ().text = "You gave this card to " + recName [0];
		}

		
	}

	public void ResultsPositiveForThisPlayer(string rec){

		Player thePlayer = null;
		foreach(Player pl in GameManager.instance.players){
			if (pl.PlName == rec) {
				thePlayer = pl;
			}
		}
		CardResultPanel.SetActive (true);
		OkButtonNet.transform.SetParent (this.transform.parent);
		OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0,0,0));
		OkButtonNet.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		if (checkIfIntrested (thePlayer.PlayerCharacter, thePlayer.PlayerRace)) {
			//player is intrested
			//form the button text based on card id
			string[] senderName = GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName.Split(new char[]{'_'});

			CardResultPanel.transform.Find ("IntroText").GetComponent<Text> ().text = senderName [0] + " gave YOU this card";


			Text buttonText = OkButtonNet.GetComponentInChildren<Text> ();

			string[] splitArray = thePlayer.PlName.Split(new char[]{'_'});

			buttonText.text = "Me endiaferei";

			if (GreenCardID == "Greens_7" || GreenCardID == "Greens_10" || GreenCardID == "Greens_12") {
				if (GameManager.instance.GetEquipmentNumberForPlayerGreen (splitArray[0],  splitArray[1]) > 0) {  //player name and player id
					buttonText.text = "Give equipment";
				}
			}

			imIntrested = true;

		}
		else {
			//player not intrested 
			//form the button to be just ok and close, TODO: add log message
			string[] senderName = GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName.Split(new char[]{'_'});

			CardResultPanel.transform.Find ("IntroText").GetComponent<Text> ().text = senderName [0] + " gave YOU this card";

			Text buttonText = OkButtonNet.GetComponentInChildren<Text> ();
			buttonText.text = "OK/Not Intrested";

		}
	}

	public void GreenOkClickedAfterResultNet(){
		Debug.Log ("GreenOkClickedAfterResultNet");
		Player thePlayer = null;
		foreach(Player pl in GameManager.instance.players){
			if (pl.PlName == PhotonNetwork.player.name) {
				thePlayer = pl;
			}
		}
			

		if(GreenCardID == "Greens_1" || GreenCardID == "Greens_2" || GreenCardID == "Greens_8"){
			//1 ponto zimias
			GameManager.instance.DoDamageTo (1, GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName);
			DestroyGreenNet ();
		}
		if(GreenCardID == "Greens_4" || GreenCardID == "Greens_6"){
			//2 pontous zimias

			GameManager.instance.DoDamageTo (2, GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName);
			DestroyGreenNet ();
		}
		if(GreenCardID == "Greens_5" || GreenCardID == "Greens_9" || GreenCardID == "Greens_11"){
			if (thePlayer.DamagePoints > 0) {
				GameManager.instance.DoHealTo (1, GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName);
			}
			else {
				GameManager.instance.DoDamageTo (1, GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName);
			}
			DestroyGreenNet ();
		}
		if(GreenCardID == "Greens_7" || GreenCardID == "Greens_10" || GreenCardID == "Greens_12"){
			Debug.Log ("green test run");
			string[] splitArray = thePlayer.PlName.Split(new char[]{'_'});
			if (GameManager.instance.GetEquipmentNumberForPlayerGreen (splitArray [0], splitArray [1]) > 0) {  //player name and player id
				
				//do eq panel
				string[] senderName = GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName.Split(new char[]{'_'});
				GameManager.instance.ShowSelectCardToGiveToPlayerFromGreen(senderName[0], senderName[1]);


			}
			else {
				GameManager.instance.DoDamageTo (1, GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName);
			}
			DestroyGreenNet ();
		}



	}








	public bool checkIfIntrested(string charName, string charRace){
		bool result = false;


		if ((GreenCardID == "Greens_1" || GreenCardID == "Greens_6" || GreenCardID == "Greens_11") && charRace == "Vamp") {

			return true;

		}
		if (GreenCardID == "Greens_2") {

			if (GetCharactersFirstLetterGreen (charName) == "A" || GetCharactersFirstLetterGreen (charName) == "K" || GetCharactersFirstLetterGreen (charName) == "X" 
				|| GetCharactersFirstLetterGreen (charName) == "E" || GetCharactersFirstLetterGreen (charName) == "O" ) {

				return true;
			}

		}

		// greens 3 is missing because its a whole different card 
		if (GreenCardID == "Greens_4") {

			if (GetCharactersFirstLetterGreen (charName) == "R" || GetCharactersFirstLetterGreen (charName) == "F" || GetCharactersFirstLetterGreen (charName) == "G" 
				|| GetCharactersFirstLetterGreen (charName) == "V" || GetCharactersFirstLetterGreen (charName) == "M" ) {

				return true;
			}

		}
		if ((GreenCardID == "Greens_5" || GreenCardID == "Greens_8") && charRace == "Lycan") {

			return true;

		}
		if (GreenCardID == "Greens_7" && (charRace == "Vamp" || charRace == "Lycan" )) {

			return true;

		}

		if (GreenCardID == "Greens_9" && charRace == "Human") {

			return true;

		}
		if (GreenCardID == "Greens_10" && (charRace == "Human" || charRace == "Lycan" )) {

			return true;

		}
		if (GreenCardID == "Greens_12" && (charRace == "Human" || charRace == "Vamp" )) {

			return true;

		}

		return result;

	}

	public string GetCharactersFirstLetterGreen(string fullcharname){
		string first = "none";

		first = fullcharname.Substring (0, 1);
		return first;
	}

	public void GreenCardOKNet(){
		string[] splitArray = PhotonNetwork.player.name.Split(new char[]{'_'});
		if (imIntrested) {
			GreenOkClickedAfterResultNet ();


			GameManager.instance.GetComponent<PhotonView>().RPC("AddChatMessage_RPC", PhotonTargets.AllViaServer, "<color=green><b>" + splitArray[0] + " said he/she is intrested</b></color>");
		}
		else {
			OkButtonNet.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
			OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0,180f,90f));
			OkButtonNet.transform.SetParent (this.transform);

			//check if we are not the curr player
			if(PhotonNetwork.player.name != GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlName){
				GameManager.instance.GetComponent<PhotonView>().RPC("AddChatMessage_RPC", PhotonTargets.AllViaServer, "<color=green><b>" + splitArray[0] + " said he/she is NOT intrested</b></color>");	
			}


			Destroy (gameObject);
		}

	}

	public void DestroyGreenNet(){
		OkButtonNet.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0,180f,90f));
		OkButtonNet.transform.SetParent (this.transform);


		Destroy (gameObject);
	}

	public void GreenCardOKNetNotForMe(){
		

		CardResultNotForMeButton.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
		CardResultNotForMeButton.transform.rotation = Quaternion.Euler (new Vector3 (0,180f,90f));
		CardResultNotForMeButton.transform.SetParent (this.transform);
		Destroy (gameObject);
	}
}