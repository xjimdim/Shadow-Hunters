using UnityEngine;
using System.Collections;
using UnityEngine.UI; 
using System.Collections.Generic;
using System;
using Facebook.Unity;


public class NetworkManager : MonoBehaviour {


	public static NetworkManager instance;

	public Text ServerStatusText;
	public GameObject RoomEntryPanel;
	public GameObject ScrollContain;

	public GameObject CreateRoomsButton;
	public GameObject CreateRoomPanel;
	public GameObject FBLoggedInUI;

	public List<RoomNameINFO> RoomNames;

	// ROOM UI STUFF

	public GameObject RoomUI;
	public GameObject PlayerEntryPrefab;
	public GameObject PlayerList;
	public GameObject ReadyButton;
	public GameObject LeaveButton;
	public GameObject GoButton;
	bool GoButtonDisabled = true;
	public Text EverybodyReadyText;

	public Text MaxPlayersText;

	int roomsBeforeUpdate = 0;

	public GameObject ErrorPanel;

	public List <string> PlayerNames = new List<string>();

	//chat stuff

	public Text ChatText;
	public GameObject PleaseWaitText;

	public bool inGame;
	public string LastRoomTriedToJoin;

	public Sprite myProfilePic;

	public GameObject fullscreenButtonGO;

	bool fullscreenFix = false;


	// Use this for initialization

	void Awake () {
		DontDestroyOnLoad(transform.gameObject);
		inGame = false;
	}

	void Start () {
		instance = this;
		Debug.Log ("peerstate: "+PhotonNetwork.networkingPeer.PeerState.ToString());
		if (PhotonNetwork.networkingPeer.PeerState.ToString() != "Connected") {
			Connect ();
		}
		 
		RoomNames = new List<RoomNameINFO>();

		Debug.Log ("screen res: "+Screen.currentResolution.ToString ());

		if (Screen.currentResolution.height < 1080f) {
			fullscreenButtonGO.SetActive (true);
			//Screen.SetResolution(Screen.width - 256, Screen.height - 256, false);
		}

	}

	void Update(){
		if (!inGame) {
			ServerStatusText.text = "Server Status: " + PhotonNetwork.connectionStateDetailed.ToString ();
			
			if (PhotonNetwork.connectionStateDetailed.ToString () != "JoinedLobby") {
				CreateRoomsButton.GetComponent<Button>().interactable = false;
			}
			else{
				CreateRoomsButton.GetComponent<Button>().interactable = true;
			}
			
			
			if(PhotonNetwork.insideLobby && PhotonNetwork.countOfRooms != roomsBeforeUpdate){
				RoomGenerator();
				Debug.Log("Room generator fired from update, count of rooms: "+PhotonNetwork.countOfRooms + " rooms before: " + roomsBeforeUpdate);
			}
			
			roomsBeforeUpdate = PhotonNetwork.countOfRooms;
			


		}

		if (Screen.fullScreen && !fullscreenFix && Screen.currentResolution.height == 1080f) {
			GameObject.FindGameObjectWithTag ("UICanvas").GetComponent<CanvasScaler> ().scaleFactor = 1.8f;
			fullscreenFix = true;
		}

		if (!Screen.fullScreen && fullscreenFix && Screen.currentResolution.height == 1080f) {
			GameObject.FindGameObjectWithTag ("UICanvas").GetComponent<CanvasScaler> ().scaleFactor = 1.278f;
			fullscreenFix = false;
		}

	}


	void OnDisconnectedFromPhoton (){
		if (PhotonNetwork.isMasterClient) {
			// here destroy the room somehow;
		}
		PhotonNetwork.LeaveRoom ();
		if (!inGame) {
			GetComponent<PhotonView>().RPC("OnLeftRoom_RPC", PhotonTargets.AllBuffered, FBHolder.instance.MyID);		
		}

	}

	public void JoinThisRoom(string IDOfRoom){

		foreach (RoomNameINFO RI in RoomNames) {
			if(RI.RoomID == IDOfRoom){
				RoomInfo[] rooms = PhotonNetwork.GetRoomList ();

				foreach(RoomInfo rm in rooms){
					ExitGames.Client.Photon.Hashtable rmInfo = rm.customProperties;

					if(rmInfo["MasterRightNow"].ToString() == RI.RoomNameWithID){
						if(rm.playerCount < rm.maxPlayers){
							FBLoggedInUI.SetActive (false);
							
							RoomUI.SetActive (true);
							Debug.Log("trying to join room " + rmInfo["OriginalMaster"].ToString());

							LastRoomTriedToJoin=rmInfo["OriginalMaster"].ToString ();

							PhotonNetwork.JoinRoom(rmInfo["OriginalMaster"].ToString());

						}
						else{
							PopupErrorWindow("Can't join this room because it is full");
						} 
					}
				}

			}
		}
	}

	public void RoomGenerator(){
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();

		foreach (Transform oldrooms in ScrollContain.transform) {
			Destroy (oldrooms.gameObject);		
		}

		Debug.Log ("rooms length at room generator: " + rooms.Length.ToString());
		foreach (RoomInfo room in rooms) {
			if (room.visible) {
				GameObject RoomPanel;
				RoomPanel = Instantiate(RoomEntryPanel) as GameObject;
				RoomPanel.transform.SetParent(ScrollContain.transform);


				RoomNameINFO rm = new RoomNameINFO();
				//rm.RoomNameWithID = room.name;
				// new way with properties:
				ExitGames.Client.Photon.Hashtable hashInfo = room.customProperties;
				Debug.Log("HASH INFOOOOO: " + hashInfo["MasterRightNow"].ToString());

				rm.RoomNameWithID = hashInfo["MasterRightNow"].ToString();

				string NameAndID = hashInfo["MasterRightNow"].ToString();
				string[] splitArray =  NameAndID.Split(new char[]{'_'}); //Here we're passing the splitted string to array by that char

				string RoomName = splitArray[0]; //Here we assign the first part to the name


				string ID = splitArray[1]; //Here we assing the second part to the ID
				rm.RoomID = ID;

				RoomNames.Add (rm);

				Debug.Log ("ID IS : _" + ID+"_");
				//handling the picture:
				FB.API (Util.GetPictureURL(ID.ToString(), 50, 50), HttpMethod.GET, delegate(IGraphResult pictureResult) {
					if(pictureResult.Error != null) { // in case there was an error
						Debug.Log (pictureResult.Error);
					}
					else { //we got the image

						Image FriendAvatar = RoomPanel.transform.Find ("FriendAvatar").GetComponent<Image>(); 
						FriendAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect (0, 0, 50, 50), new Vector2 (0, 0));
					}
				});


				RoomPanel.transform.Find ("RoomNameText").GetComponent<Text>().text = "Room of: " + RoomName;


				RoomPanel.transform.Find ("PlayersOnlineText").GetComponent<Text>().text = "Players Online: " + room.playerCount +"/"+ room.maxPlayers;
				RoomPanel.transform.Find ("PlayerId").GetComponent<Text>().text = ID;

				RoomPanel.GetComponent<Button>().onClick.AddListener(() => RoomPanel.GetComponent<RoomClicked>().ClicekdRoom());
			}

		}
	}

	void OnJoinedLobby() {
		Debug.Log ("Joinned Lobby!!!!!!!!!!!!!!");
		//RoomGenerator();
		Debug.Log ("Number of rooms in lobby right now: " + PhotonNetwork.countOfRooms);
	}

	void OnReceivedRoomListUpdate(){
		Debug.Log ("Received room list");
		RoomGenerator();
	}
	
	public void CreateRoom(){

		//PhotonNetwork.CreateRoom (FBHolder.instance.MyName + '_' + FBHolder.instance.MyID, true, true, int.Parse (MaxPlayersText.text));
		//PhotonNetwork.CreateRoom (FBHolder.instance.MyName + '_' + FBHolder.instance.MyID, true, true, int.Parse (MaxPlayersText.text), new ExitGames.Client.Photon.Hashtable() { { "MasterRightNow", FBHolder.instance.MyName + '_' + FBHolder.instance.MyID },}, new string[] { "MasterRightNow" });
		//PhotonNetwork.CreateRoom (FBHolder.instance.MyName + '_' + FBHolder.instance.MyID, new RoomOptions ( maxPlayers = int.Parse (MaxPlayersText.text), new ExitGames.Client.Photon.Hashtable () { { "MasterRightNow", FBHolder.instance.MyName + '_' + FBHolder.instance.MyID },}), null);

		string roomFullName = FBHolder.instance.MyName + '_' + FBHolder.instance.MyID;
		RoomOptions newRoomOptions = new RoomOptions ();
		newRoomOptions.isOpen = true;
		newRoomOptions.isVisible = true;
		newRoomOptions.maxPlayers = byte.Parse (MaxPlayersText.text);
		newRoomOptions.customRoomProperties = new ExitGames.Client.Photon.Hashtable();
		newRoomOptions.customRoomProperties.Add ("MasterRightNow", roomFullName);
		newRoomOptions.customRoomProperties.Add ("OriginalMaster", roomFullName);
		newRoomOptions.customRoomProperties.Add ("PlayerNamesWithID", "");
		newRoomOptions.customRoomPropertiesForLobby = new string[] {"MasterRightNow", "OriginalMaster"}; 

		PhotonNetwork.CreateRoom(roomFullName, newRoomOptions, null);


		FBLoggedInUI.SetActive (false);

		RoomUI.SetActive (true);
		GoButton.SetActive (true);
		GoButton.GetComponent<Button>().interactable = false;

		CloseCreateRoomPanel ();
	}

	void Connect(){
		PhotonNetwork.ConnectUsingSettings ("1.5");
	}


	public void OnJoinedRoom(){
		//create my tile: 
		LeaveButton.GetComponent<Button> ().interactable = true;
		ReadyButton.GetComponent<Button> ().interactable = true;
		PleaseWaitText.SetActive (false);
		PhotonNetwork.player.name = FBHolder.instance.MyName + '_' + FBHolder.instance.MyID; //thats for the chat 

		ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
		customProperties.Add ("PlayerName", FBHolder.instance.MyName + '_' + FBHolder.instance.MyID);

		PhotonNetwork.player.SetCustomProperties (customProperties);


		ExitGames.Client.Photon.Hashtable rmInfo2 = PhotonNetwork.room.customProperties;

		ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable ();

		if (rmInfo2 ["PlayerNamesWithID"].ToString() == "") {
			ht.Add ("PlayerNamesWithID", '#' + FBHolder.instance.MyName + '_' + FBHolder.instance.MyID);	
			Debug.Log ("playerproperty WAS null, adding player: " + FBHolder.instance.MyName + '_' + FBHolder.instance.MyID);
		} 
		else {
			ht.Add ("PlayerNamesWithID", rmInfo2 ["PlayerNamesWithID"].ToString() + '#' + FBHolder.instance.MyName + '_' + FBHolder.instance.MyID);
			Debug.Log ("playerproperty was not null, adding player: " + FBHolder.instance.MyName + '_' + FBHolder.instance.MyID);
		}
		PhotonNetwork.room.SetCustomProperties (ht);



		Debug.Log ("Player name on joined room: "+ PhotonNetwork.player.name);

		Debug.Log ("Players online in this room: " + PhotonNetwork.room.playerCount);


		GetComponent<PhotonView>().RPC("OnJoinedRoom_RPC", PhotonTargets.AllBuffered, FBHolder.instance.MyName + '_' + FBHolder.instance.MyID + '_' + PhotonNetwork.player.ID);

		ChatText.text = "";
	}

	[PunRPC]
	void OnJoinedRoom_RPC(string NameWithID){


		string[] splitArray =  NameWithID.Split(new char[]{'_'}); //Here we're passing the splitted string to array by that char
		
		string name = splitArray[0]; //Here we assign the first part to the name
		
		string ID = splitArray[1]; //Here we assing the second part to the ID
		string PhotonID = splitArray[2];


		GameObject PlayerEntry;
		PlayerEntry = Instantiate(PlayerEntryPrefab) as GameObject;
		PlayerEntry.transform.SetParent (PlayerList.transform);

		//PlayerList.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (PlayerList.transform.parent.GetComponent<RectTransform>().sizeDelta.x ,PlayerList.GetComponent<GridLayoutGroup>().cellSize.y);
		
		PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = name;
		PlayerEntry.transform.Find ("RoomPlayerID").GetComponent<Text>().text = ID;
		PlayerEntry.transform.Find ("RoomPlayerPhotonID").GetComponent<Text>().text = PhotonID;


		GetPictureForPlayerEntry(ID.ToString(), PlayerEntry);

	}

	void GetPictureForPlayerEntry(string id, GameObject PlayerEntry){
		FB.API (Util.GetPictureURL(id, 50, 50), HttpMethod.GET, delegate(IGraphResult pictureResult) {
			if(pictureResult.Error != null) { // in case there was an error
				Debug.Log (pictureResult.Error);
				GetPictureForPlayerEntry(id, PlayerEntry);
			}
			else { //we got the image

				Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
				PlayerAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect (0, 0, 50, 50), new Vector2 (0, 0));
			}
		});
	}



	public void AddChatMessage(string m){
		string[] splitArray =  PhotonNetwork.player.name.Split(new char[]{'_'}); 
		
		string ChatName = splitArray[0]; 


		GetComponent<PhotonView>().RPC("AddChatMessage_RPC", PhotonTargets.AllViaServer, "<b>" + ChatName + "</b>: " +  m);
	}
	
	
	[PunRPC]
	void AddChatMessage_RPC(string m){

		ChatText.text = ChatText.text + Environment.NewLine + m;
		
	}



	public void ReadyClicked(){
		GetComponent<PhotonView>().RPC("ReadyClicked_RPC", PhotonTargets.AllBuffered, FBHolder.instance.MyID);
		ReadyButton.GetComponent<Button> ().interactable = false;
	}

	public void GoClicked(){
		Debug.Log ("Starting Game!! ");

		PhotonNetwork.room.visible = false;
		GetComponent<PhotonView>().RPC("LoadTheGame", PhotonTargets.AllBuffered, null);

	}

	[PunRPC]
	void LoadTheGame(){

		string MastersNameAndID = PhotonNetwork.masterClient.name;

		Debug.Log ("MastersNameAndID: " + MastersNameAndID); // here i get "MAsterNameAndID: " so there MasterNameAndID is null (?)
		
		//we need to have the list ordered for every player , so we have the room-master first and then all the others in alphabetical order...

		ExitGames.Client.Photon.Hashtable hashInfo = PhotonNetwork.room.customProperties;


		string NamesHash = hashInfo["PlayerNamesWithID"].ToString();

		Debug.Log (NamesHash);
		string[] splitArray =  NamesHash.Split(new char[]{'#'}); 
		


		for (int i = 1; i<=PhotonNetwork.playerList.Length; i++) {
			PlayerNames.Add(splitArray[i]);
			Debug.Log ("split array element in load the game: " + splitArray[i]);
		}

		List<string> tempPls = new List<string>();
		
		for (int i=0; i<PlayerNames.Count; i++) {
			tempPls.Add (PlayerNames[i]);
		}
		
		tempPls.Sort ();
		
		for (int i=0; i<tempPls.Count; i++) {
			for(int j=0; j<PlayerNames.Count; j++){
				if(tempPls[i] == PlayerNames[j]){
					
					PlayerNames.Insert (i,PlayerNames[j]);
					if(i<=j){
						PlayerNames.RemoveAt (j+1);
					}
					else{
						PlayerNames.RemoveAt (j);
					}
					
					
				}
			}
		}
		
		
		
		
		
		
		ExitGames.Client.Photon.Hashtable rht = PhotonNetwork.room.customProperties;
		string MasterName = rht ["MasterRightNow"].ToString ();
		
		for (int x=0; x<PlayerNames.Count; x++) {
			if(PlayerNames[x] == MasterName){
				
				PlayerNames.Insert (0, PlayerNames[x]);
				PlayerNames.RemoveAt(x+1);
			}				
		}




//		List <string> unsortedOtherPlayersAndMe = new List<string>();		
//		if(PhotonNetwork.playerName == MastersNameAndID){
//			PlayerNames.Add (MastersNameAndID);
//		}
//		else{
//			PlayerNames.Add (MastersNameAndID);
//			unsortedOtherPlayersAndMe.Add(PhotonNetwork.playerName);
//		}
//		
//		foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers){
//			
//			string txt = pl.name;   
//			
//			
//			if(txt != MastersNameAndID && txt != ""){
//				Debug.Log("Unsorted in txt: "+ txt);
//				unsortedOtherPlayersAndMe.Add (txt);
//			}
//		}
//		
//		unsortedOtherPlayersAndMe.Sort ();
//		
//		foreach (string otherP in unsortedOtherPlayersAndMe) {
//			Debug.Log("Sorted in otherP: "+ otherP);
//			if(otherP != ""){
//				PlayerNames.Add(otherP);		
//			}
//			
//		}
		



		inGame = true;
		myProfilePic = FBHolder.instance.UserAvatar.sprite;
		PhotonNetwork.LoadLevel ("_project");
	}


	[PunRPC]
	void ReadyClicked_RPC(string m){
		foreach (Transform child in PlayerList.transform){
			Debug.Log ("trying to find id: " + m);
			if(child.transform.Find("RoomPlayerID").GetComponent<Text>().text == m){
				GameObject ReadyState = child.transform.Find("ReadyState").gameObject;
				ReadyState.GetComponent<Text>().enabled = true;
				ReadyState.GetComponentInChildren<Image>().enabled = true;

				GameObject NotReadyState = child.transform.Find("NotReadyState").gameObject;
				NotReadyState.SetActive(false);
			}
		}

		if (PhotonNetwork.isMasterClient) {
			CheckGOButton();	
			Debug.Log("readyyyy clicked");
		}
	}

	void CheckGOButton(){
		if (PhotonNetwork.isMasterClient) {
			int countOfReady = 0;
			int countOfPlayers = 0;
			foreach (Transform child in PlayerList.transform)
			{
				countOfPlayers++;
				if(child.transform.Find("ReadyState").gameObject.GetComponent<Text>().enabled){
					countOfReady++;
				}
			}
			
			Debug.Log("Count of ready: " + countOfReady + ", count of players: " + countOfPlayers);
			
			if(countOfReady == countOfPlayers && countOfReady > 1){ //we have 2 or more players that are all ready
				GoButton.GetComponent<Image>().color = Color.green;
				GoButton.GetComponent<Button>().interactable = true;
				
				
				EverybodyReadyText.text = "Everybody is Ready!!";
				EverybodyReadyText.color = Color.red;
			}
			else{
				GoButton.GetComponent<Image>().color = Color.gray;
				GoButton.GetComponent<Button>().interactable = false;
				
				EverybodyReadyText.text = "Waiting for everyone to get ready";
				EverybodyReadyText.color = Color.black;
			}
		}
	}

	public void LeaveRoomClicked(){


		ExitGames.Client.Photon.Hashtable rmInfo = PhotonNetwork.room.customProperties;
		
		ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable ();



		//remove player from customproperties
		string NamesHash = rmInfo ["PlayerNamesWithID"].ToString ();
		Debug.Log ("NameHash before: " + NamesHash);
		string MyNameAndID = FBHolder.instance.MyName + '_' + FBHolder.instance.MyID;  
		string toRemove = "#" + MyNameAndID;
		Debug.Log ("Trying to erase: " + toRemove);
		string NamesHashFixed = NamesHash.Replace (toRemove, "");
		Debug.Log ("NameHash after: " + NamesHashFixed);
		ht.Add ("PlayerNamesWithID", NamesHashFixed);
		PhotonNetwork.room.SetCustomProperties (ht);


		GetComponent<PhotonView>().RPC("OnLeftRoom_RPC", PhotonTargets.AllBuffered, FBHolder.instance.MyID);
		PhotonNetwork.LeaveRoom ();
		LeaveButton.GetComponent<Button> ().interactable = false;

		if (PhotonNetwork.isMasterClient) {
			GoButton.SetActive(false);
			GoButtonDisabled = true;		
		}


	}

	void OnMasterClientSwitched (PhotonPlayer newMasterClient){

		string[] splitArray =  newMasterClient.name.Split(new char[]{'_'}); 

		string newMasterName =  splitArray[0]; 
		string newMasterID = splitArray[1]; 

		//GetComponent<PhotonView>().RPC("ChangeRoomName_RPC", PhotonTargets.AllBuffered, newMasterName + '_' + newMasterID);


		ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable ();
		ht.Add("MasterRightNow", newMasterName + '_' + newMasterID);

		PhotonNetwork.room.SetCustomProperties (ht);
		Debug.Log ("Master Client: " + newMasterClient.name + " Room Name: " + PhotonNetwork.room.name);
	}


	[PunRPC]
	void ChangeRoomName_RPC(string m){
		PhotonNetwork.room.name = m;
		Debug.Log ("rpc called with name: " + m); 
	}


	void OnLeftRoom(){
		if (!inGame) {
			FBLoggedInUI.SetActive (true);		
		}

		PleaseWaitText.SetActive (true);
		RoomUI.SetActive (false);
		//Clean The room for next use:
		GoButton.SetActive (false);
		LeaveButton.GetComponent<Button> ().interactable = false;
		ReadyButton.GetComponent<Button> ().interactable = false;
		ChatText.text = "";




		foreach (Transform pl in PlayerList.transform) {
			Destroy (pl.gameObject);		
		}

		RoomGenerator ();

	}


	[PunRPC]
	void OnLeftRoom_RPC(string m){
		Debug.Log ("On Left Room RPC fired for ID: " + m);
		if (!inGame) {
			foreach (Transform child in PlayerList.transform)
			{
				if(child.transform.Find("RoomPlayerID").GetComponent<Text>().text == m){
					Destroy(child.gameObject);
				}
				
				if(PhotonNetwork.isMasterClient){
					CheckGOButton();
				}
			}		
		}


	}

	[PunRPC]
	void OnLeftRoomWithPhotonID_RPC(string m){
		Debug.Log ("On Left Room RPC fired for photon ID: " + m);
		foreach (Transform child in PlayerList.transform)
		{
			if(child.transform.Find("RoomPlayerPhotonID").GetComponent<Text>().text == m){
				Destroy(child.gameObject);
			}
			
			if(PhotonNetwork.isMasterClient){
				CheckGOButton();
			}
		}
		
	}




	void OnPhotonJoinRoomFailed (object[ ] codeAndMsg){

		Debug.Log ("cant join room of + " + LastRoomTriedToJoin + " trying to join again...");
		PhotonNetwork.JoinRoom(LastRoomTriedToJoin);


		//PopupErrorWindow (codeAndMsg [1].ToString());
		//Debug.Log (codeAndMsg [1].ToString ());

	}

	void PopupErrorWindow(string msg){
		ErrorPanel.SetActive (true);
		ErrorPanel.transform.Find ("ErrorMessage").GetComponent<Text> ().text = "Error Message: " + msg;
		Debug.Log(msg);
	}

	public void CloseErrorPanel(){
		ErrorPanel.SetActive (false);
	}

	public void MaxPlayersChanged(){
		MaxPlayersText.text = MaxPlayersText.GetComponentInParent<Slider> ().value.ToString(); 
	}


	public void OpenCreateRoomPanel(){
		CreateRoomPanel.SetActive (true);
	}

	public void CloseCreateRoomPanel(){
		CreateRoomPanel.SetActive (false);
	}



	void OnPhotonPlayerDisconnected (PhotonPlayer otherPlayer){


		GetComponent<PhotonView>().RPC("OnLeftRoomWithPhotonID_RPC", PhotonTargets.AllBuffered, otherPlayer.ID.ToString ());
		Debug.Log ("disc player info: " + otherPlayer.ID.ToString ());

	}


	public void InstructionsButtonClicked(){
			Application.OpenURL ("https://dimitriskalogirou.com/shadowhunters/Instructions.html");

	}

	public void GoFullscreenClicked(){

		if (!Screen.fullScreen) {
			Screen.fullScreen = true;
			fullscreenButtonGO.GetComponentInChildren<Text> ().text = "EXIT FULLSCREEN";
		}
		else {
			Screen.fullScreen = false;
			fullscreenButtonGO.GetComponentInChildren<Text> ().text = "GO FULLSCREEN";
		}
	}






}

public class RoomNameINFO {
	public string RoomNameWithID { get; set; }
	public string RoomID { get; set; }
}
