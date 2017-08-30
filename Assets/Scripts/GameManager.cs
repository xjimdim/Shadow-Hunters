using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;


	public Text ServerStatusTextInGame;

	public int NumberOfPlayers;
	public GameObject PlayerPrefab; 
	public GameObject FSD;
	public GameObject SSD; 
	public GameObject[] HPSpots;

	public GameObject AttackPanelHead;
	public GameObject AttackPanel;
	public Transform AttackScrollContain;
	public GameObject AttackPlayerEntry;
	public GameObject AttackPlayerEntryEkriksi;
	public Text CDMTSPText;

	public GameObject DeadPlayerPanel;

	public GameObject MagicPanelHead;
	public GameObject MagicPanel;
	public Transform MagicForestScrollContain;
	public GameObject MagicForestPlayerEntry;
	public Text MagicForestSelectText;

	public GameObject StealCardHead;
	public Transform StealCardScrollContain;
	public GameObject StealCardPlayerEntry;
	public GameObject SelectCardToStealPanel;
	public Transform AvailableEqScrollContain;
	public GameObject EquipCardToStealPrefab;

	public GameObject EquipCardPrefab;

	public GameObject PagidaHead;
	public Transform PagidaScrollContain;
	public GameObject PagidaPlayerEntry;
	public GameObject SelectCardToGivePanel;
	public Transform AvailableEqToGiveScrollContain;
	public GameObject EquipCardToGivePrefab;


	public GameObject NyxteridaHead;
	public Transform NyxteridatScrollContain;
	public GameObject NyxteridaPlayerEntry;
	public Text NyxteridaHealText;

	public GameObject MatomeniAraxniHead;
	public Transform MatomeniAraxniScrollContain;
	public GameObject MatomeniAraxniPlayerEntry;

	public GameObject EnedraHead;
	public Transform EnedraScrollContain;
	public GameObject EnedraPlayerEntry;

	public GameObject GregoryAbilityHead;
	public Transform GregoryAbilityScrollContain;
	public GameObject GregoryAbilityPlayerEntry;

	public GameObject TherapiaApoMakriaHead;
	public Transform TherapiaApoMakriaScrollContain;
	public GameObject TherapiaApoMakriaPlayerEntry;

	public GameObject MatomenoFegariHead;
	public Transform MatomenoFegariScrollContain;
	public GameObject MatomenoFegariPlayerEntry;

	public GameObject ElenaAbilityHead;
	public GameObject WarningText;

	public GameObject MalburcaHead;
	public String MalburcaPlayerToAttackBack = "none";
	public bool WaitingForMalburca = false;

	public bool[] buttonState = new bool[10];

	public bool rollForMalburcaAttack = false;


	public bool rollForEnedra = false;
	public bool enedraDone = false;

	public string EnedraTargetedPlayer = "none";

	public bool rollForGregoryAbility = false;
	public bool gregoryAbilityDone = false;

	public string GregoryAbilityTargetedPlayer = "none";

	public bool RollForEkriksi = false;

	public bool RollForTherapiaApoMakria = false;
	public string NameOfPLayerToHealApoMakria = "none";


	public GameObject GameOverPanel;
	public GameObject EndGamePlayerEntry;

	public bool GameOver = false;

	public GameObject LoadingPanel;

	public string MyDestinationText = "none";

	public Text ExtraRoundsText;

	public GameObject PiksidaHead;

	// FLAGS

	public bool firstmovedone = false;
	public bool bothsleeping = false;
	public bool bothcreated = false;
	public bool bothdeleted = true;
	public bool waitfordicedelete = true;
	public bool waitForPlayerToRoll = true;
	public bool DestinationTextSet = false;

	public bool cardsActivated = false;

	public bool moveFromPiksida = false;
	public string piksidaarea = "none";

	public bool isBeginningOfRound = false;

	public bool AbilityActivatedOnce = false;


	//attack flags
	public bool RollDiceForAttack = false; //so don't makemove
	public bool DamagePointsCreated = false;
	public bool AttackStarted = false;
	public bool AttackPanelCleared = true;

	public bool GreenClicked = false;
	public int ActiveGreenCardID = -1;
	public GameObject PlayerTileSelectedGO; 
	public bool PlayerTileClicked = false;

	public bool RedClicked = false;
	public int ActiveRedCardID = -1;

	public bool BlueClicked = false;
	public int ActiveBlueCardID = -1;


	//Magic Forest Stuff
	public int MagicForestWhatToDo = -1; // 0 = dmg, 1=heal
	public Button MFDamageButton;
	public Button MFHealButton;


	//FOR CARDS GENERATOR

	public GameObject greenPrefab;
	public GameObject greenCardButton;
	public List <Sprite> GreenSprites = new List<Sprite>();
	List <GameObject> GreenCards = new List<GameObject>();

	public GameObject redPrefab;
	public GameObject redCardButton;
	public List <Sprite> RedSprites = new List<Sprite>();
	List <GameObject> RedCards = new List<GameObject>();

	public GameObject bluePrefab;
	public GameObject blueCardButton;
	public List <Sprite> BlueSprites = new List<Sprite>();
	List <GameObject> BlueCards = new List<GameObject>();

	public GameObject CharCardPrefab;
	public List <Sprite> CharSprites = new List<Sprite>();
	public GameObject MyCharCardGO;

	List <Sprite> HumanSprites = new List<Sprite>();
//	List <GameObject> HumanCards = new List<GameObject>();
	//List<Sprite> SelectedHumans = new List<Sprite>();

	List <Sprite> LycanSprites = new List<Sprite>();
//	List <GameObject> LycanCards = new List<GameObject>();
	//List<Sprite> SelectedLycans = new List<Sprite>();

	List <Sprite> VampSprites = new List<Sprite>();
//	List <GameObject> VampCards = new List<GameObject>();
	//List<Sprite> SelectedVamps = new List<Sprite>();


	public List <Player> players = new List<Player>();
	public List <HPPlayer> HPplayers = new List<HPPlayer>();
	public List <Player> deadPlayers = new List<Player>();

	public int currentPlayerIndex = 0;

	public Tile place = null;
	public Tile fromPlace = null;

	public Text ChatText;

	string[] PlayerColor;
	//PLAYER LIST STUFF
	public GameObject PlayerScrollContain;
	public GameObject PlayerEntryPrefab; 

	public Transform[] SpawnSpots;

	// private vars
	bool playersSorted = false;
	GameObject placeobj2 = null;
	GameObject CurrPlayerTextGO;

	GameObject EndTurnButtonGO;
	public GameObject AttackButtonGO;

	public Button PetrinosKiklosButton;
	GameObject MagicForestButtonGO;
	GameObject RollButtonGO;

	public Button RevealCharacterButton;
	public Button ActivateAbilityButton;
	public Button RollToAttackButton;
	
	void Awake() {
		instance = this;

		//DUMMIES FOR PLAYER NAMES
		//for (int i = 0; i<5; i++){
		//	PlayerInfo.PlayerNames.Add ("Player "+(i+1));
		//}


		//this needs reverse to get the correct order if dummies are not used 
		//PlayerInfo.PlayerNames.Reverse (); 


		PlayerColor = new string[] {"gray", "blue", "yellow", "red", "black"};
		NumberOfPlayers = NetworkManager.instance.PlayerNames.Count;

		HPSpots = new GameObject[] {GameObject.Find("Player1HPSpots"),GameObject.Find("Player2HPSpots"), GameObject.Find("Player3HPSpots"), GameObject.Find("Player4HPSpots"), GameObject.Find("Player5HPSpots")};

		for(int i = 1; i<CharSprites.Count; i++){
			if(i>=1 && i<=5){
				HumanSprites.Add (CharSprites[i]);
			}
			else if(i>=6 && i<=11){
				LycanSprites.Add (CharSprites[i]);

			}
			else if(i>=12 && i<=16){
				VampSprites.Add (CharSprites[i]);
			}
			
		}
		if (PhotonNetwork.isMasterClient) {
			HandleCharacters();		
		}
		generatePlayers();

		//generateCards(); RIP GENERATECARDS :(

		CurrPlayerTextGO = GameObject.Find ("CurrentPlayerText");





		EndTurnButtonGO = GameObject.Find ("EndTurnButton");
		AttackButtonGO = GameObject.Find ("AttackButton");
		MagicForestButtonGO = GameObject.Find ("MagikoDassosButton");
		RollButtonGO = GameObject.Find ("RollButton");

		blueCardButton.GetComponent<Button>().onClick.AddListener(() => blueCardButton.GetComponent<RGBCardHolder>().CardClicked());
		redCardButton.GetComponent<Button>().onClick.AddListener(() => redCardButton.GetComponent<RGBCardHolder>().CardClicked());
		greenCardButton.GetComponent<Button>().onClick.AddListener(() => greenCardButton.GetComponent<RGBCardHolder>().CardClicked());

		DisableBlueCards (true);
		DisableRedCards (true);
		DisableGreenCards (true);

		StartCoroutine (GetMyPlayer (3));
		//StartCoroutine (RemoveLoadingScreenIn (6));




	}

	void FixedUpdate(){
		if (players.Count == NumberOfPlayers && !GameOver) {
			if (players [currentPlayerIndex].GetComponent<UserPlayer> () != null) { // if == null tote mallon o player einai dead

				players [currentPlayerIndex].TurnUpdate ();	
			}
				
		}

		GameObject fs = GameObject.Find ("FourSidedDie(Clone)");
		GameObject ss = GameObject.Find ("SixSidedDie(Clone)");
		
		
		//checking dice status and updating flags
		if (fs != null && ss != null) {
			bothcreated = true;
			if (fs.GetComponent<Rigidbody>().IsSleeping () && ss.GetComponent<Rigidbody>().IsSleeping () && waitfordicedelete) {
				
				bothsleeping = true;
				
			}
			else{
				bothsleeping = false;
			}
		}
		else {
			bothcreated = false;
		}

		if (moveFromPiksida) {
			NewMakeMove ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		ServerStatusTextInGame.text = "Status: " + PhotonNetwork.connectionStateDetailed.ToString ();
		if(players.Count == NumberOfPlayers && !GameOver){

			if (!playersSorted) {
				if (PhotonNetwork.isMasterClient) {
					GetComponent<PhotonView> ().RPC ("SortPlayerList_rpc", PhotonTargets.AllBufferedViaServer, null);
				}




				if (players [currentPlayerIndex].PlName != PhotonNetwork.player.name) {

					RollButtonGO.GetComponent<Button> ().interactable = false; 

					AttackButtonGO.GetComponent<Button> ().interactable = false; 
					

					EndTurnButtonGO.GetComponent<Button> ().interactable = false; 
					RevealCharacterButton.interactable = false;
				}

			}
			ChangeCurrentPlayerText ();
			 
			//players [currentPlayerIndex].TurnUpdate (); //calls userplayer script
			//NewMakeMove ();
			//makeMove ();
		}


			


	}

	[PunRPC]
	void SortPlayerList_rpc(){

		List<string> tempPls = new List<string>();

		for (int i=0; i<players.Count; i++) {
			tempPls.Add (players[i].PlName);
		}

		tempPls.Sort ();

		for (int i=0; i<tempPls.Count; i++) {
			for(int j=0; j<players.Count; j++){
				if(tempPls[i] == players[j].PlName){

					players.Insert (i,players[j]);
					HPplayers.Insert (i,HPplayers[j]);
					if(i<=j){
						players.RemoveAt (j+1);
						HPplayers.RemoveAt (j+1);
					}
					else{
						players.RemoveAt (j);
						HPplayers.RemoveAt (j);
					}


				}
			}
		}



		ExitGames.Client.Photon.Hashtable rht = PhotonNetwork.room.customProperties;
		string MasterName = rht ["MasterRightNow"].ToString ();

		for (int x=0; x<players.Count; x++) {
			if(players[x].PlName == MasterName){

				players.Insert (0, players[x]);
				players.RemoveAt(x+1);

//				HPplayers.Insert (0, HPplayers[x]);
//				HPplayers.RemoveAt(x+1);

				HPplayers.Insert (0, HPplayers[x]);
				HPplayers.RemoveAt(x+1);
			}				
		}


		playersSorted = true;

//		foreach (Player upl in players) {
//			if(upl.PlName == MasterName){
//				players.Remove (upl);
//				players.Insert (0, upl);
//			}		
//		}


	}

	public void EndTurnClicked(){

		//handling the delay "glitch"
		//we have to call nextturn locally and then to all the other players





		RollButtonGO.GetComponent<Button> ().interactable = false; 

		AttackButtonGO.GetComponent<Button> ().interactable = false; 
		AttackStarted = false;
		ClearAttackPanel (); //se periptosi pou patisame apla x kai den kaname clear

		MagicForestButtonGO.GetComponent<Button> ().interactable = false;
		PetrinosKiklosButton.interactable = false;
		EndTurnButtonGO.GetComponent<Button> ().interactable = false; 
		RevealCharacterButton.interactable = false;
		ActivateAbilityButton.interactable = false;

		DisableGreenCards (true);
		DisableRedCards (true);
		DisableBlueCards (true);

		cardsActivated = false;

		//if dice still exist while we end our turn lets destroy them
//		if (bothcreated) {
//			GetComponent<PhotonView>().RPC("DestroyDiceWithNoDelay_RPC", PhotonTargets.AllBuffered, null);		
//		}


		if(RollDiceForAttack){
			RollDiceForAttack = false;
		}
		if (enedraDone) {
			enedraDone = false;
		}

		if (players [currentPlayerIndex].extraRounds > 0) {
			ExtraRoundsText.enabled = true;
		}
		else {
			ExtraRoundsText.enabled = false;
		}

		WarningText.SetActive (false); //in case it is active

		isBeginningOfRound = false;

		GetComponent<PhotonView> ().RPC ("nextTurn_RPC", PhotonTargets.AllBufferedViaServer, null);
	}



	[PunRPC]
	public void nextTurn_RPC() {

		DestinationTextSet = false;
		if (players [currentPlayerIndex].extraRounds == 0) {
			if (currentPlayerIndex + 1 < players.Count) {
				currentPlayerIndex++;
			} else {
				currentPlayerIndex = 0;
			}
		}
		else {
			players [currentPlayerIndex].extraRounds--;

		}




		if (players [currentPlayerIndex].PlName == PhotonNetwork.player.name) {
			//only the current player will run this methods

			if(players[currentPlayerIndex].eksoplismoi.Contains("Blues_15")){
				string[] splitArray =  players [currentPlayerIndex].PlName.Split(new char[]{'_'});

				GetComponent<PhotonView>().RPC("RemoveEquipCardFromPlayer_RPC", PhotonTargets.AllBuffered, "Blues", "Blues_15", splitArray[0], splitArray[1]);

			}

			Player currPl = players [currentPlayerIndex];

			if (currPl.PlayerCharacter == "Gandolf" && currPl.AbilityActivated && !currPl.AbilityDisabled) {
				//that means we are gandolf, and we activated the ability last round, so its time to disable it
				currPl.AbilityDisabled = true;
				GetComponent<PhotonView> ().RPC ("DisableAbilityOfPlayer", PhotonTargets.AllBufferedViaServer, players [currentPlayerIndex].PlName, players [currentPlayerIndex].PlName);
			}


			RollButtonGO.GetComponent<Button> ().interactable = true; 
			EndTurnButtonGO.GetComponent<Button>().interactable = true;
			isBeginningOfRound = true;
			if (players [currentPlayerIndex].isRevealed) {
				string ch = players [currentPlayerIndex].PlayerCharacter;


				if(ch=="Gregor" || ch=="Frederic" || ch=="Flora" || ch=="Etta" || ch == "Mentour"){
					//mia fora sto paixnidi sto ksekinima
					if (!AbilityActivatedOnce && !players[currentPlayerIndex].AbilityDisabled) {
						ActivateAbilityButton.interactable = true;
					}
					else {
						ActivateAbilityButton.interactable = false;
					}

				}
				if (ch == "Elena" || ch == "Xloey") {
					if (!players [currentPlayerIndex].AbilityDisabled) {
						ActivateAbilityButton.interactable = true;
					}
				}
				if (ch == "Ouriel") {
					
					if (!players [currentPlayerIndex].AbilityDisabled && GetPlayersInArea ("8")>0) {
						ActivateAbilityButton.interactable = true;
					}
				}

				if (ch == "Volco" || ch == "Anta") {
					
					if (!players [currentPlayerIndex].AbilityDisabled && !players [currentPlayerIndex].AbilityActivated) {
						ActivateAbilityButton.interactable = true;
					}
					else {
						ActivateAbilityButton.interactable = false;
					}
				}

				//Klerry has passive ability so donothing about her

			}

			if (players [currentPlayerIndex].isRevealed == false) {

				RevealCharacterButton.interactable = true;
			}

		}
	}

	public void addExtraRoundToCurrentPlayer(){
		GetComponent<PhotonView> ().RPC ("addExtraRoundToCurrentPlayer_RPC", PhotonTargets.AllBufferedViaServer, null);
	}

	[PunRPC]
	public void addExtraRoundToCurrentPlayer_RPC(){
		players [currentPlayerIndex].extraRounds++;
	}

	public void addExtraRoundsToCurrentPlayer(int numRounds){
		GetComponent<PhotonView> ().RPC ("addExtraRoundsToCurrentPlayer_RPC", PhotonTargets.AllBufferedViaServer, numRounds);
	}

	[PunRPC]
	public void addExtraRoundsToCurrentPlayer_RPC(int rounds){
		players [currentPlayerIndex].extraRounds += rounds;
	}

	public void ChangeCurrentPlayerText(){

		if (players [currentPlayerIndex].PlName == PhotonNetwork.player.name && !firstmovedone) {

			RollButtonGO.GetComponent<Button> ().interactable = true; 
			EndTurnButtonGO.GetComponent<Button>().interactable = true;
			isBeginningOfRound = true;
			if (players [currentPlayerIndex].isRevealed == false) {

				RevealCharacterButton.interactable = true;
			}

			GetComponent<PhotonView> ().RPC ("SetFirstMoveDoneFlag_RPC", PhotonTargets.AllBuffered, null);
		}

		string[] splitArray =  players [currentPlayerIndex].PlName.Split(new char[]{'_'}); //Here we're passing the splitted string to array by that char
		
		string CurrentPlayerName = splitArray[0]; //Here we assign the first part to the name

		CurrPlayerTextGO.GetComponent<Text> ().text = CurrentPlayerName + " Plays"; 
	}

	[PunRPC]
	void SetFirstMoveDoneFlag_RPC(){
		firstmovedone = true;
		LoadingPanel.SetActive (false);
	}

	public void DoApokalipsi(){

		if (players [currentPlayerIndex].PlayerRace == "Lycan" || players [currentPlayerIndex].PlayerRace == "Vamp") {
			if (players [currentPlayerIndex].isRevealed == false) {
				RevealButtonClicked ();
			}
		}
	}

	public void RollButtonClicked(){
		//GameManager.instance.waitForPlayerToRoll = false; //player rolled ^^
		isBeginningOfRound=false;

		//no longer the beggining of the round TODO: depends on the player - not for all:
		if (ActivateAbilityButton.interactable) {
			string ch = players [currentPlayerIndex].PlayerCharacter;
			if(ch == "Gregor" || ch == "Frederic" || ch == "Flora" || ch == "Xloey" || ch == "Etta" || ch == "Ouriel"){
				ActivateAbilityButton.interactable = false;	
			}

		}

		RollButtonGO.GetComponent<Button> ().interactable = false; 
		EndTurnButtonGO.GetComponent<Button> ().interactable = false;

		if (players [currentPlayerIndex].eksoplismoi.Contains ("Blues_8")) {
			//do move? HOOOW?
			PiksidaHead.SetActive(true);

		}
		else {

			RollDice ();
		}

	}


	public void ClicekdFromPiksidaPanel(string areatogo){

		moveFromPiksida = true;
		piksidaarea = areatogo;
		PiksidaHead.SetActive (false);
	}

	public void RevealButtonClicked(){
		

		players [currentPlayerIndex].isRevealed = true;
		Player pl = players [currentPlayerIndex];
		if (isBeginningOfRound && !pl.AbilityDisabled && pl.PlayerCharacter != "Klaudios"  && pl.PlayerCharacter != "Raphael" && pl.PlayerCharacter != "Klerry" && pl.PlayerCharacter != "Valkyria" && pl.PlayerCharacter != "Gandolf" ) {
			if(pl.PlayerCharacter == "Ouriel"){
				if (GetPlayersInArea ("8") > 0) {
					
					ActivateAbilityButton.interactable = true;
				}
				else {
					ActivateAbilityButton.interactable = false;
				}
			}
			else{
				ActivateAbilityButton.interactable = true;
			}

		}

		//activate passive abilities:
		if (pl.PlayerCharacter == "Klerry" || pl.PlayerCharacter == "Valkyria" || pl.PlayerCharacter == "Mentour" || pl.PlayerCharacter == "Malburca") {
			ActivateAbilityClicked ();
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Xloey") {
			ActivateAbilityButton.GetComponentInChildren<Text> ().text = "Heal 1pt";
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Mentour") {
			ActivateAbilityButton.GetComponentInChildren<Text> ().text = "Activate, Extra Rounds: "+deadPlayers.Count.ToString();
		}

		GetComponent<PhotonView>().RPC("RevealCharacter_RPC", PhotonTargets.OthersBuffered, PhotonNetwork.player.name);

		RevealCharacterButton.interactable = false;


	}

	[PunRPC]
	public void RevealCharacter_RPC(string playernameandID){
		
		players [currentPlayerIndex].isRevealed = true;

		string[] splitArray = playernameandID.Split(new char[]{'_'});
		string playername = splitArray [0];
		string playerID = splitArray [1];

		foreach(Transform t in PlayerScrollContain.transform){
			
			if (t.Find ("PlayerName").GetComponent<Text> ().text == playername && t.Find ("PlayerID").GetComponent<Text> ().text == playerID) {
				t.transform.Find("PlayerCharCard").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
				//TODO itween instead of regular rotation change


			}
			else {
				Debug.Log ("ERROR: could not find the player in scroll contain, playername is: " + t.Find ("PlayerName").GetComponent<Text> ().text + " player ID is: " + t.Find ("PlayerID").GetComponent<Text> ().text);
			}
		}



	}

	public void ActivateAbilityClicked(){
		Debug.Log ("Activating ability...");

		AbilityActivatedOnce = true;
		GetComponent<PhotonView> ().RPC ("AbilityActivatedForPlayer", PhotonTargets.AllBuffered, players [currentPlayerIndex].PlName);


		if (players [currentPlayerIndex].PlayerCharacter == "Gregor" || players [currentPlayerIndex].PlayerCharacter == "Frederic" || players [currentPlayerIndex].PlayerCharacter == "Etta") {
			//once again gregory panel is a general panel, not just for gregor
			ShowGregoryAbilityPanel ();
			ActivateAbilityButton.interactable = false;
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Flora") {
			ShowMatomenoFegariPanel ();
			ActivateAbilityButton.interactable = false;
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Elena") {
			ActivateAbilityButton.GetComponentInChildren<Text> ().text = "Move left or right";
			ActivateAbilityButton.onClick.AddListener(() => this.ElenaAbility());
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Volco") {
			ActivateAbilityButton.GetComponentInChildren<Text> ().text = "Ability Activated";
			ActivateAbilityButton.interactable = false;
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Anta") {
			DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);
			ActivateAbilityButton.GetComponentInChildren<Text> ().text = "Ability Done";
			ActivateAbilityButton.interactable = false;
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Klerry" || players [currentPlayerIndex].PlayerCharacter == "Valkyria" || players [currentPlayerIndex].PlayerCharacter == "Malburca") {
			ActivateAbilityButton.GetComponentInChildren<Text> ().text = "Ability Active";
			ActivateAbilityButton.interactable = false;
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Xloey") {
			
			DoHealTo (1, players [currentPlayerIndex].PlName);
			ActivateAbilityButton.interactable = false;
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Ouriel") {
			//check for sure:
			if(GetPlayersInArea("8")>0){

				ShowGregoryAbilityPanel (); //reminder: this is a general select player panel not just for gregor
			}

			ActivateAbilityButton.interactable = false;
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Mentour") {


			addExtraRoundsToCurrentPlayer (deadPlayers.Count);

			ActivateAbilityButton.interactable = false;
		}

		if (players [currentPlayerIndex].PlayerCharacter == "Gandolf") {
			//end the round:
			AttackButtonGO.GetComponent<Button> ().interactable = false;
			MagicForestButtonGO.GetComponent<Button>().interactable = false;
			PetrinosKiklosButton.interactable = false;


			ActivateAbilityButton.interactable = false;
		}


	}

	[PunRPC]
	public void AbilityActivatedForPlayer (string pName){
		foreach (Player pl in players) {
			if (pl.PlName == pName) {
				pl.AbilityActivated = true;
			}
		}
	}

	public void ElenaAbility(){
		Debug.Log ("ElenaAbility");
		if (players[currentPlayerIndex].destinationText == "none") {
			WarningText.SetActive (true);
			WarningText.GetComponent<Text> ().text = "On your first move you can ONLY roll the dice";
		}
		else {
			ElenaAbilityHead.SetActive (true);
			RollButtonGO.GetComponent<Button> ().interactable = false;
			EndTurnButtonGO.GetComponent<Button> ().interactable = false;
		}


		ActivateAbilityButton.interactable = false;
	}

	public void ElenaMoveLeft(){
		moveFromPiksida = true;
		piksidaarea = GetLeftArea(players[currentPlayerIndex].destinationText);
		Debug.Log ("Elenamove LEFT piksidaarea: " + piksidaarea);
		ElenaAbilityHead.SetActive (false);
	}

	public void ElenaMoveRight(){
		moveFromPiksida = true;
		piksidaarea = GetRightArea(players[currentPlayerIndex].destinationText);
		Debug.Log ("Elenamove Right piksidaarea: " + piksidaarea);
		ElenaAbilityHead.SetActive (false);
	}


	public string GetLeftArea(string currentArea){
		string leftarea="none";

		if (currentArea == "23") {
			leftarea = "10";
		}
		else if(currentArea == "45"){
			leftarea = "10";
		}
		else if(currentArea == "6"){
			leftarea = "45";
		}
		else if(currentArea == "7"){
			leftarea = "23";
		}
		else if(currentArea == "8"){
			leftarea = "23";
		}
		else if(currentArea == "9"){
			leftarea = "8";
		}
		else if(currentArea == "10"){
			leftarea = "23";
		}

		return leftarea;
	}

	public string GetRightArea(string currentArea){
		string rightarea="none";

		if (currentArea == "23") {
			rightarea = "8";
		}
		else if(currentArea == "45"){
			rightarea = "6";
		}
		else if(currentArea == "6"){
			rightarea = "9";
		}
		else if(currentArea == "7"){
			rightarea = "45";
		}
		else if(currentArea == "8"){
			rightarea = "9";
		}
		else if(currentArea == "9"){
			rightarea = "6";
		}
		else if(currentArea == "10"){
			rightarea = "45";
		}

		return rightarea;
	}

	public void MalburcaAttackBackClicked(){
		
		GetComponent<PhotonView> ().RPC ("MalburcaResult", PhotonTargets.AllBuffered, true, PhotonNetwork.player.name, MalburcaPlayerToAttackBack);
	}

	public void MalubrcaDoNothingClicked(){
		
		GetComponent<PhotonView> ().RPC ("MalburcaResult", PhotonTargets.AllBuffered, false, PhotonNetwork.player.name, "DoNothing");
	}

	[PunRPC]
	public void MalburcaResult (bool attackBack, string malburcaPlayerName, string attackBackPlName){
		if (attackBack) {

			foreach (Player p in players) {
				if (p.PlName == PhotonNetwork.player.name) {
					if (p.PlName == malburcaPlayerName) {
						MalburcaHead.SetActive (false);
						RollDiceForAttack=true;
						rollForMalburcaAttack=true;
						RollDice ();
						
					} 
					else if (p.PlName == attackBackPlName) {
						//the damager 
						WarningText.SetActive(false);
						WaitingForMalburca = false;
						EnableButtons ();


					}
					else {
						//everyone else
						WarningText.SetActive(false);
						WaitingForMalburca = false;
					}

				}
			}

		}
		else {

			foreach (Player p in players) {
				if (p.PlName == PhotonNetwork.player.name) {
					if (p.PlName == malburcaPlayerName) {
						//do nothing
						MalburcaHead.SetActive (false);
					} 
					else if (p.PlName == attackBackPlName) {
						//the damager 
						WarningText.SetActive(false);
						EnableButtons ();

					}
					else {
						//everyone else
						WarningText.SetActive(false);
					}

				}
			}
		
		}
		
	}

	public void MalburcaDoDamage(string points){
		GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
		int integerPoints = int.Parse (points);
		if (DamagePointsCreated) {
			DoDamageTo (integerPoints, MalburcaPlayerToAttackBack);
		}

		rollForMalburcaAttack = false;
	}


	public void NewMakeMove(){
		//this method should actually only check if the dice are sleeping and change the dstination text 

		if (moveFromPiksida && !DestinationTextSet && !RollDiceForAttack) {
			Debug.Log("piksida move");
			string cloc = players [currentPlayerIndex].currentLocation;

			if (piksidaarea == "none") {
				Debug.LogWarning ("Piksida area is set to none");
			}

			if (!cardsActivated) {
				DisableCardsForArea(piksidaarea);
				cardsActivated = true;
			}

			if (piksidaarea == "9") {
				MagicForestButtonGO.GetComponent<Button> ().interactable = true; 
			}

			if (piksidaarea == "10") {
				if (GameManager.instance.GetEquipmentNumberOfOtherPlayers () > 0) {
					PetrinosKiklosButton.interactable = true;
				}

			}


			GameObject placeobj = GameObject.Find (piksidaarea);

			if (cloc != "none") {  // if this is not the first move of the player find FROM location
				placeobj2 = GameObject.Find (cloc);
				Debug.Log ("placeobj2 name: " + placeobj2.transform.name);
			} else {
				//Debug.Log ("cloc = none");
			}
			if (placeobj != null) {

				players [currentPlayerIndex].GetComponent<UserPlayer> ().SetPlace (placeobj.GetComponent<Tile> (), piksidaarea);

				if (placeobj2 != null) {
					players [currentPlayerIndex].GetComponent<UserPlayer> ().SetFromPlace (placeobj2.GetComponent<Tile> ());		
				}

			}


			GetComponent<PhotonView> ().RPC ("UnsetPiksidaFlag_RPC", PhotonTargets.AllBufferedViaServer, null);

		}


		if (bothsleeping && waitfordicedelete && !DestinationTextSet && !RollDiceForAttack && !moveFromPiksida ) {  // afto simenei pws ta zaria exoune rixtei gia na pame se mia perioxi
			Debug.Log("normal move");


			//evresi topothesias
			string dtext = GameObject.Find ("DiceText").GetComponent<Text> ().text;

			
			if (dtext == "4" || dtext == "5")
				dtext = "45";
			if (dtext == "2" || dtext == "3")
				dtext = "23";
			if (dtext == "7" || dtext == "1") {

				if (!PiksidaHead.activeSelf) {
					PiksidaHead.SetActive(true);
					GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);

				}
				return;

				//dtext = "10";
			}


			if (!cardsActivated) {
				DisableCardsForArea(dtext);
				cardsActivated = true;
			}

			if (dtext == "9") {
				MagicForestButtonGO.GetComponent<Button> ().interactable = true; 
			}

			if (dtext == "10") {
				if (GameManager.instance.GetEquipmentNumberOfOtherPlayers () > 0) {
					PetrinosKiklosButton.interactable = true;
				}

			}
			
			
			
			
			GameObject placeobj = GameObject.Find (dtext);
			
			string cloc = players [currentPlayerIndex].currentLocation;
			
			if (cloc != "none") {  // if this is not the first move of the player find FROM location
				placeobj2 = GameObject.Find (cloc);
				Debug.Log ("placeobj2 name: " + placeobj2.transform.name);
			} else {
				//Debug.Log ("cloc = none");
			}
			if (placeobj != null) {
				
				players [currentPlayerIndex].GetComponent<UserPlayer> ().SetPlace (placeobj.GetComponent<Tile> (), dtext);
				
				if (placeobj2 != null) {
					players [currentPlayerIndex].GetComponent<UserPlayer> ().SetFromPlace (placeobj2.GetComponent<Tile> ());		
				}
				
			}
			

			GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
			
			
			
			
		} else if ( (bothsleeping || ((players[currentPlayerIndex].eksoplismoi.Contains("Reds_4") || (players [currentPlayerIndex].PlayerCharacter == "Valkyria" && players [currentPlayerIndex].AbilityActivated)) 
			&& GameObject.Find("FourSidedDie(Clone)").GetComponent<Rigidbody> ().IsSleeping ()))  && waitfordicedelete && RollDiceForAttack && !moveFromPiksida) {   // << thats a really complicated contition :P 



			GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
			
			
			if (DamagePointsCreated) {
				CDMTSPText.text = "Points Created, please select a player";
				CDMTSPText.color = Color.blue;

				if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_9")) {
					//insert prefab gia tin ekriksi kai min kaneis enable ola ta alla
//					GameObject EkriksiEntry = Instantiate(AttackPlayerEntryEkriksi) as GameObject;
//					EkriksiEntry.transform.SetParent (AttackScrollContain.transform);
//					EkriksiEntry.transform.SetSiblingIndex (0); //this sets it to be the first button
//					EkriksiEntry.name = "EkriksiButton";
//
//					EkriksiEntry.GetComponent<Button>().onClick.AddListener (() => EkriksiEntry.GetComponent<ClickedToAttack>().PlayerClickedToAttack());
//					EkriksiEntry.GetComponent<Button> ().interactable = true;	




					foreach (Transform t in AttackScrollContain.transform) {
						t.GetComponent<Button> ().interactable = true;
						t.GetComponent<ClickedToAttack> ().isEkriksi = true;
					}


				}
				else {
					
					foreach (Transform t in AttackScrollContain.transform) {
						t.GetComponent<Button> ().interactable = true;
					}

				}



			} else {
				CDMTSPText.text = "Create Damage Points to select a player";
				CDMTSPText.color = Color.red;
			}
		}



	}


//	public void makeMove(){ //called onUpdate
//
//	
//
//
//		ChangeCurrentPlayerText();
//
//		if (!bothcreated && !waitForPlayerToRoll){ // afto simenei pws o paiktis molis ekane click gia na riksei zaria
//			RollDice ();
//			waitForPlayerToRoll = true;
//
//		}	
//
//		if (AttackStarted) {
//			foreach(Transform child in AttackScrollContain){
//				if(child.gameObject.GetComponent<Button>().interactable && !DamagePointsCreated){
//					child.gameObject.GetComponent<Button>().interactable=false;
//				}
//				else if(!child.gameObject.GetComponent<Button>().interactable && DamagePointsCreated){
//					child.gameObject.GetComponent<Button>().interactable=true;
//				}
//			}		
//		}
//
//		if (bothsleeping && waitfordicedelete && bothcreated && !RollDiceForAttack) {  // afto simenei pws ta zaria exoune rixtei gia na pame se mia perioxi
//
//			//evresi topothesias
//			GameObject dieTextGameObject = GameObject.Find ("DieText");
//			string dtext = dieTextGameObject.GetComponent<TextMesh> ().text;
//			 
//			if (dtext == "4" || dtext == "5")
//				dtext = "45";
//			if (dtext == "2" || dtext == "3")
//				dtext = "23";
//			if (dtext == "7" ){
//				dtext = "10";
//			}
//			if (dtext == "1"){
//				RollDice();    //se periptosi sfalmatos, not optimal vevea 
//			}
//
//
//
//
//
//
//
//			GameObject placeobj = GameObject.Find (dtext);
//
//			string cloc = players[currentPlayerIndex].currentLocation;
//
//			if(cloc != "none"){  // if this is not the first move of the player find FROM location
//				placeobj2 = GameObject.Find (cloc);
//				Debug.Log ("placeobj2 name: " + placeobj2.transform.name);
//			}
//			else{
//				Debug.Log ("cloc = none");
//			}
//			if (placeobj != null) {
//
//				players[currentPlayerIndex].GetComponent<UserPlayer>().SetPlace(placeobj.GetComponent<Tile> (), dtext);
//
//
//
//				if (placeobj2 != null) {
//					players[currentPlayerIndex].GetComponent<UserPlayer>().SetFromPlace(placeobj2.GetComponent<Tile> ());
//
//				}
//
//				if(bothsleeping){
//					moveCurrentPlayer (players[currentPlayerIndex].place, dtext);
//				}
//
//
//
//
//			}
//
//
//
//			GetComponent<PhotonView>().RPC("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
//
//
//
//						
//		}
//		else if (bothsleeping && waitfordicedelete && RollDiceForAttack){
//			GetComponent<PhotonView>().RPC("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
//
//
//			if(DamagePointsCreated){
//				CDMTSPText.text = "Points Created, please select player";
//				CDMTSPText.color = Color.blue;
//			}
//			else{
//				CDMTSPText.text = "Create Damage Points to select player";
//				CDMTSPText.color = Color.red;
//			}
//		}
//
//	}



	public void moveCurrentPlayer(Tile destTile, string dt) { //called onUpdate




		if (!players[currentPlayerIndex].moveStarted ) {

			players [currentPlayerIndex].currentLocation = players [currentPlayerIndex].destinationText; 
			if(place.transform.name != fromPlace.transform.name){
				players [currentPlayerIndex].moveDestination = destTile.getEmptyPlace() + 0.5f * Vector3.up;
			}


			 //the from destination;
			players [currentPlayerIndex].destinationText = dt;
		}

		  // for the deactivation of the cards when someone is in the destination tile 
		MyDestinationText = dt;

	}



	public void DisableCardsForArea(string area){
		if (area == "23") {
			DisableBlueCards(true);
			DisableRedCards(true);
			DisableGreenCards(false);
		}
		if (area == "8") {
			DisableGreenCards(true);
			DisableBlueCards(true);
			DisableRedCards(false);
		}
		if (area == "6") {
			DisableGreenCards(true);
			DisableRedCards(true);
			DisableBlueCards(false);
		}
		if (area == "10" || area == "9") {
			DisableGreenCards(true);
			DisableRedCards(true);
			DisableBlueCards(true);
		}
		if (area == "45") {
			DisableBlueCards(false);
			DisableRedCards(false);
			DisableGreenCards(false);
		}


	}

	public void DisableGreenCards(bool ac){

		if (ac) {
			
			greenCardButton.GetComponent<Image>().color = Color.gray;
			//greenCardButton.GetComponent<Button>().onClick.RemoveAllListeners();
			greenCardButton.GetComponent<Button>().interactable = false;
		}
		else {
			
			greenCardButton.GetComponent<Image>().color = Color.white;
			//greenCardButton.GetComponent<Button>().onClick.AddListener(() => greenCardButton.GetComponent<RGBCardHolder>().CardClicked());
			greenCardButton.GetComponent<Button>().interactable = true;
		}

//		if(ac){
//			foreach (GameObject gcardz in GreenCards) {
//				gcardz.GetComponent<GreenCardScript>().isEnabled = false;	
//				gcardz.GetComponent<Image>().color = Color.gray;
//				gcardz.GetComponent<Button>().onClick.RemoveAllListeners();
//			}
//		}
//		else{
//			foreach (GameObject gcardz in GreenCards) {
//				gcardz.GetComponent<GreenCardScript>().isEnabled = true;	
//				gcardz.GetComponent<Image>().color = Color.white;
//				gcardz.GetComponent<Button>().onClick.AddListener(() => gcardz.GetComponent<GreenCardScript>().CardClicked());
//			}
//		}
//	
	}

	public void DisableRedCards(bool ac){

		if (ac) {
			
			redCardButton.GetComponent<Image>().color = Color.gray;
			//redCardButton.GetComponent<Button>().onClick.RemoveAllListeners();
			redCardButton.GetComponent<Button>().interactable = false;
		}
		else {
			
			redCardButton.GetComponent<Image>().color = Color.white;
			//redCardButton.GetComponent<Button>().onClick.AddListener(() => redCardButton.GetComponent<RGBCardHolder>().CardClicked());
			redCardButton.GetComponent<Button>().interactable = true;
		}


//		if(ac){
//			foreach (GameObject rcardz in RedCards) {
//				rcardz.GetComponent<RedCardScript>().isEnabled = false;
//				rcardz.GetComponent<Image>().color = Color.gray;
//				rcardz.GetComponent<Button>().onClick.RemoveAllListeners();
//			}
//		}
//		else{
//			foreach (GameObject rcardz in RedCards) {
//				rcardz.GetComponent<RedCardScript>().isEnabled = true;
//				rcardz.GetComponent<Image>().color = Color.white;
//				rcardz.GetComponent<Button>().onClick.AddListener(() => rcardz.GetComponent<RedCardScript>().CardClicked());
//			}
//		}
	}

	public void DisableBlueCards(bool ac){

		if (ac) {

			blueCardButton.GetComponent<Image>().color = Color.gray;
			//blueCardButton.GetComponent<Button>().onClick.RemoveAllListeners();
			blueCardButton.GetComponent<Button>().interactable = false;
		}
		else {

			blueCardButton.GetComponent<Image>().color = Color.white;
			//blueCardButton.GetComponent<Button>().onClick.AddListener(() => blueCardButton.GetComponent<RGBCardHolder>().CardClicked());
			blueCardButton.GetComponent<Button>().interactable = true;
		}


//		if(ac){
//			
//			foreach (GameObject bcardz in BlueCards) {
//				bcardz.GetComponent<BlueCardScript>().isEnabled = false;
//				bcardz.GetComponent<Image>().color = Color.gray;
//				bcardz.GetComponent<Button>().onClick.RemoveAllListeners();
//			}
//		}
//		else{
//			foreach (GameObject bcardz in BlueCards) {
//				bcardz.GetComponent<BlueCardScript>().isEnabled = true;
//				bcardz.GetComponent<Image>().color = Color.white;
//				bcardz.GetComponent<Button>().onClick.AddListener(() => bcardz.GetComponent<BlueCardScript>().CardClicked());
//			}
//		}
	}

	public void SkoteiniTeletiHealClicked(){
		//be sure curr pl is vamp

		Debug.Log ("Skoteini Teleti Heal Clicked");
		if(players[currentPlayerIndex].PlayerRace == "Vamp"){

			if (players [currentPlayerIndex].isRevealed) {
				//then just heal
				DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);
			}
			else {
				//reveal and then heal
				Debug.Log("not revealed trying to reveal and heal");
				RevealButtonClicked();
				DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);

			}

		}

	}

	public void TherapeiaHealClicked(){
		//be sure curr pl is lycan

		Debug.Log ("Therapeia Heal Clicked");
		if(players[currentPlayerIndex].PlayerRace == "Lycan"){

			if (players [currentPlayerIndex].isRevealed) {
				//then just heal
				DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);
			}
			else {
				//reveal and then heal
				Debug.Log("not revealed trying to reveal and heal");
				RevealButtonClicked();
				DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);

			}

		}

	}

	public void EnergiaClicked(){

		if (players [currentPlayerIndex].isRevealed) {
			//then just heal
			DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);
		}
		else {
			//reveal and then heal
			Debug.Log("not revealed trying to reveal and heal");
			RevealButtonClicked();
			DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);

		}

	}

	public void EvlogiaClicked(){

		DoHealTo (2, players [currentPlayerIndex].PlName);

	}

	public Sprite GetRandomCard(string type){
		Sprite result = null;

		if (type == "green") {
			result = GreenSprites [UnityEngine.Random.Range(0, GreenSprites.Count)];
		}
		else if(type == "red"){
			result = RedSprites [UnityEngine.Random.Range(0, RedSprites.Count)];
		}
		else if(type == "blue"){
			//result = BlueSprites [16];
			result = BlueSprites [UnityEngine.Random.Range(0, BlueSprites.Count)];
		}

		return result;

	}


	public Sprite GetCardFromSpriteName(string type, string name){
		Sprite result = null;

		if (type == "Greens") {
			
			foreach (Sprite s in GreenSprites) {
				if (s.name == name) {
					result = s;
				}
			}
	
		}
		else if(type == "Reds"){

			foreach (Sprite s in RedSprites) {
				if (s.name == name) {
					result = s;
				}
			}

		}

		else if(type == "Blues"){

			foreach (Sprite s in BlueSprites) {
				if (s.name == name) {
					result = s;
				}
			}

		}



		if (result == null) {
			
			Debug.LogWarning ("Could not find sprite of type "+type+" with name: " + name);

		}

		return result;

	}


	void HandleDmgText(){
		foreach (Transform t in PlayerScrollContain.transform) {
			string plName = t.transform.Find("PlayerName").GetComponent<Text>().text;
			string plID = t.transform.Find("PlayerID").GetComponent<Text>().text;
			foreach(Player pl in players){
				if(pl.PlName == plName + '_' + plID){
					t.transform.Find("DmgPointsText").GetComponent<Text>().text = pl.DamagePoints.ToString();
				}
			}
		}

		if (AttackStarted) {
			foreach (Transform ta in AttackScrollContain.transform) {
				if(ta.name != "EkriksiButton"){
					string plName = ta.transform.Find("PlayerName").GetComponent<Text>().text;
					string plID = ta.transform.Find("PlayerID").GetComponent<Text>().text;
					foreach(Player pl in players){
						if(pl.PlName == plName + '_' + plID){
							ta.transform.Find("DmgPointsText").GetComponent<Text>().text = pl.DamagePoints.ToString();
						}
					}
				}

			}	
		}
	}

	public void ClearAttackPanel(){
		AttackButtonGO.GetComponent<Button> ().interactable = false;
		GameObject dmgtdtext = GameObject.Find ("DmgToDoText");
		if (dmgtdtext != null) {
			dmgtdtext.GetComponent<Text> ().text = "0";	
		}

			
		CDMTSPText.text = "Create Damage Points to select player";
		CDMTSPText.color = Color.red;

		foreach (Transform t in AttackScrollContain) {
			Destroy (t.gameObject);		
		}

		GameObject ARB = GameObject.Find ("AttackRollButton");

		if (ARB != null) {
			ARB.GetComponent<Button> ().interactable = true;		
		}

		if (GameObject.Find ("IronFistText") != null) {
			Text sidereniagrothiatext = GameObject.Find ("IronFistText").GetComponent<Text> ();
			sidereniagrothiatext.enabled = false;
		}

		if(GameObject.Find ("MandiasText") != null){
			Text prostateftikosmandiastext = GameObject.Find ("MandiasText").GetComponent<Text> ();
			prostateftikosmandiastext.enabled = false;	
		}

		if (GameObject.Find ("KsorkiFotiasTextAc") != null) {
			Text ksorkifotiasactivetxt = GameObject.Find ("KsorkiFotiasTextAc").GetComponent<Text> ();
			ksorkifotiasactivetxt.enabled = false;
		}

		RollToAttackButton.interactable = true;
		AttackPanelCleared = true;


		CloseAttackPanel ();

	}

	public void AttackButtonClicked(){

		AttackButtonGO.GetComponent<Button>().interactable=false;
		AttackPanelHead.SetActive (true);
		AttackPanelCleared = false;

		foreach(Transform child in AttackScrollContain){
			if(child.gameObject.GetComponent<Button>().interactable && !DamagePointsCreated){
				child.gameObject.GetComponent<Button>().interactable=false;
			}
			else if(!child.gameObject.GetComponent<Button>().interactable && DamagePointsCreated){
				child.gameObject.GetComponent<Button>().interactable=true;
			}
		}	

		AttackStarted = true;

		if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_9")) {
			//TODO a visual to let the user know about ksorki fotias
			Text ksorkifotiasactivetxt = GameObject.Find ("KsorkiFotiasTextAc").GetComponent<Text> ();
			ksorkifotiasactivetxt.enabled = true;
		}

		foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {
			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;
			
			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){


					if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_7")) {

						if(localp.destinationText != MyDestinationText || localp.destinationText != GetNeighboor(MyDestinationText)){
							GameObject PlayerEntry = Instantiate(AttackPlayerEntry) as GameObject;
							PlayerEntry.transform.SetParent (AttackScrollContain.transform);

							string[] splitArray = localp.PlName.Split(new char[]{'_'});

							string AttackPlayerName = splitArray[0];
							string AttackPlayerID = splitArray[1];


							PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = AttackPlayerName;
							PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = AttackPlayerID;
							PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

							if (localp.eksoplismoi.Contains ("Blues_15") || (localp.PlayerCharacter == "Gandolf" && localp.AbilityActivated && !localp.AbilityDisabled)) {  
								PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
							}

							PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedToAttack>().PlayerClickedToAttack());
							PlayerEntry.GetComponent<Button> ().interactable = false;

							foreach(Transform t in PlayerScrollContain.transform){
								if(t.Find ("PlayerName").GetComponent<Text>().text == AttackPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == AttackPlayerID){
									Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
									PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

									PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
								} 
							}

							if (localp.eksoplismoi.Contains ("Blues_9") && !localp.eksoplismoi.Contains("Blues_15")) {  //TODO: seperate those too, someone may have both
								PlayerEntry.transform.Find ("MandiasPlayerText").GetComponent<Text> ().enabled = true;
							}
						}

					}
					else {
						
						if(localp.destinationText == MyDestinationText || localp.destinationText==GetNeighboor(MyDestinationText)){
							GameObject PlayerEntry = Instantiate(AttackPlayerEntry) as GameObject;
							PlayerEntry.transform.SetParent (AttackScrollContain.transform);

							string[] splitArray = localp.PlName.Split(new char[]{'_'});

							string AttackPlayerName = splitArray[0];
							string AttackPlayerID = splitArray[1];


							PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = AttackPlayerName;
							PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = AttackPlayerID;
							PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

							if (localp.eksoplismoi.Contains ("Blues_15") || (localp.PlayerCharacter == "Gandolf" && localp.AbilityActivated && !localp.AbilityDisabled)) {  
								PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
							}

							PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedToAttack>().PlayerClickedToAttack());
							PlayerEntry.GetComponent<Button> ().interactable = false;

							foreach(Transform t in PlayerScrollContain.transform){
								if(t.Find ("PlayerName").GetComponent<Text>().text == AttackPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == AttackPlayerID){
									Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
									PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

									PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
								} 
							}

							if (localp.eksoplismoi.Contains ("Blues_9") && !localp.eksoplismoi.Contains("Blues_15")) {  //TODO: seperate those too, someone may have both
								PlayerEntry.transform.Find ("MandiasPlayerText").GetComponent<Text> ().enabled = true;
							}
						}


					
					}
						






				}	
			}
		}


//		PhotonPlayer[] phpl = PhotonNetwork.otherPlayers;
//		ExitGames.Client.Photon.Hashtable hashtable = phpl[UnityEngine.Random.Range(0, PhotonNetwork.otherPlayers.Length)].customProperties;
//		string dn = hashtable ["PlayerName"].ToString ();
//		int DmgPoint = 1;
//
//		DoDamageTo (DmgPoint, dn);


	}

	public string GetCharactersFirstLetter(string fullcharname){
		string first = "none";

		first = fullcharname.Substring (0, 1);
		return first;
	}

	public void DoPagida(){

		string[] splitArray = players[currentPlayerIndex].PlName.Split(new char[]{'_'});

		string MyName = splitArray[0];
		string MyID = splitArray[1];

		if (GetEquipmentNumberForPlayer (MyName, MyID) > 0) {
			PagidaHead.SetActive (true);

			foreach (Transform pl in PlayerScrollContain.transform) {

				string trapPlayerName = pl.Find("PlayerName").GetComponent<Text>().text;
				string trapPlayerID  = pl.Find("PlayerID").GetComponent<Text>().text;	


				//this player has at least one equip card so instantiate him in the list and add copy all his eq cards
				GameObject PlayerEntry = Instantiate(PagidaPlayerEntry) as GameObject;
				PlayerEntry.transform.SetParent (PagidaScrollContain);




				PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = trapPlayerName;
				PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = trapPlayerID;
				PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = pl.Find ("ColorCircleImage").GetComponent<Image>().color;

				PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromPagidaPanel>().PlayerClickedFromPagidaPanel());



				Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
				PlayerAvatar.sprite = pl.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

				if(GetEquipmentNumberForPlayer(trapPlayerName, trapPlayerID) > 0){
					

					//we know the player has one or more eq cards so lets find them and copy them to the new player entry
					foreach(Transform t in pl.Find("PlayerEqTamplo").transform){
						GameObject eqinplEntry = Instantiate (t.gameObject);
						eqinplEntry.transform.SetParent (PlayerEntry.transform.Find ("PlayerEqTamplo").transform);
						eqinplEntry.GetComponent<ZoomOnHover> ().enabled = false;

					}

				}


			}	
		}

		else {

			DoDamageTo (1, MyName + "_" + MyID);
		}




	}

	public void ShowSelectCardToGiveToPlayer(string PlNameToGive, string PlID){
		SelectCardToGivePanel.SetActive (true);
		SelectCardToGivePanel.transform.Find ("InstructionsInGivePanel").GetComponent<Text> ().text = "Please select a card to give to " + PlNameToGive + ":";

		foreach(Transform t in GameObject.Find ("HandDownPanel").transform.Find("TamploEksoplismon").transform){
			
			GameObject eqinSelect = Instantiate (EquipCardToGivePrefab);

			eqinSelect.transform.SetParent (AvailableEqToGiveScrollContain);

			eqinSelect.GetComponent<Image> ().sprite = t.gameObject.GetComponent<Image> ().sprite;


			eqinSelect.GetComponent<GiveThisCard> ().DestinationPlayerName = PlNameToGive;
			eqinSelect.GetComponent<GiveThisCard> ().DestinationPlayerID = PlID;

		}


	}

	public void ShowSelectCardToGiveToPlayerFromGreen(string PlNameToGive, string PlID){
		
		SelectCardToGivePanel.SetActive (true);
		SelectCardToGivePanel.transform.Find ("InstructionsInGivePanel").GetComponent<Text> ().text = "Please select a card to give to " + PlNameToGive + ":";

		foreach(Transform t in GameObject.Find ("HandDownPanel").transform.Find("TamploEksoplismon").transform){

			GameObject eqinSelect = Instantiate (EquipCardToGivePrefab);

			eqinSelect.transform.SetParent (AvailableEqToGiveScrollContain);

			eqinSelect.GetComponent<Image> ().sprite = t.gameObject.GetComponent<Image> ().sprite;


			eqinSelect.GetComponent<GiveThisCard> ().DestinationPlayerName = PlNameToGive;
			eqinSelect.GetComponent<GiveThisCard> ().DestinationPlayerID = PlID;
			eqinSelect.GetComponent<GiveThisCard> ().isForGreenCard = true;

		}


	}

	public void ClearCardToGiveFromGreen(){


		foreach (Transform t in AvailableEqToGiveScrollContain) {
			Destroy (t.gameObject);		
		}


		SelectCardToGivePanel.SetActive (false);


	}


	public void ClearPagidaPanel(){


		foreach (Transform t in AvailableEqToGiveScrollContain) {
			Destroy (t.gameObject);		
		}



		foreach (Transform t2 in PagidaScrollContain) {
			Destroy (t2.gameObject);		
		}

		SelectCardToGivePanel.SetActive (false);

		PagidaHead.SetActive (false);



	}

	public void PetrinosKiklosClicked(){
		StealCardHead.SetActive (true);
		PetrinosKiklosButton.interactable = false;


		foreach (Transform pl in PlayerScrollContain.transform) {


			string ascPlayerName = pl.Find("PlayerName").GetComponent<Text>().text;
			string ascPlayerID  = pl.Find("PlayerID").GetComponent<Text>().text;	

			if(GetEquipmentNumberForPlayer(ascPlayerName, ascPlayerID) > 0){
				//this player has at least one equip card so instantiate him in the list and add copy all his eq cards
				GameObject PlayerEntry = Instantiate(StealCardPlayerEntry) as GameObject;
				PlayerEntry.transform.SetParent (StealCardScrollContain);




				PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = ascPlayerName;
				PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = ascPlayerID;
				PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = pl.Find ("ColorCircleImage").GetComponent<Image>().color;

				PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromStealCard>().PlayerClickedFromStealCardPanel());



				Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
				PlayerAvatar.sprite = pl.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

				//we know the player has more than one eq cards so lets find them and copy them to the new player entry
				foreach(Transform t in pl.Find("PlayerEqTamplo").transform){
					GameObject eqinplEntry = Instantiate (t.gameObject);
					eqinplEntry.transform.SetParent (PlayerEntry.transform.Find ("PlayerEqTamplo").transform);
					eqinplEntry.GetComponent<ZoomOnHover> ().enabled = false;
				}

			}


		}



	}

	public void ShowSelectCardToStealFromPlayer(string PlName, string PlID){
		SelectCardToStealPanel.SetActive (true);
		SelectCardToStealPanel.transform.Find ("InstructionsInStealPanel").GetComponent<Text> ().text = "Please select a card to steal from " + PlName + ":";

		foreach (Transform pl in PlayerScrollContain.transform) {
			string ascPlayerName = pl.Find("PlayerName").GetComponent<Text>().text;
			string ascPlayerID  = pl.Find("PlayerID").GetComponent<Text>().text;	

			if (ascPlayerName == PlName && ascPlayerID == PlID) {

				foreach(Transform t in pl.Find("PlayerEqTamplo").transform){
					GameObject eqinSelect = Instantiate (EquipCardToStealPrefab);

					eqinSelect.transform.SetParent (AvailableEqScrollContain);

					eqinSelect.GetComponent<Image> ().sprite = t.gameObject.GetComponent<Image> ().sprite;
					eqinSelect.GetComponent<StealThisCard> ().OwnerName = PlName;
					eqinSelect.GetComponent<StealThisCard> ().OwnerID = PlID;
				}
			
			}
		}
	}

	public void ClearStealCardPanel(){


		foreach (Transform t in AvailableEqScrollContain) {
			Destroy (t.gameObject);		
		}



		foreach (Transform t2 in StealCardScrollContain) {
			Destroy (t2.gameObject);		
		}

		SelectCardToStealPanel.SetActive (false);

		StealCardHead.SetActive (false);



	}


	public void AddEquipCardToPlayer (string cardType, string cardID, bool fromCardButton){
		Debug.Log ("AddEquipCardToPlayer run for cardid " + cardID);

		//check if we need to stack?
		foreach(Transform t in GameObject.Find ("HandDownPanel").transform.Find("TamploEksoplismon").transform){
			if (t.GetComponent<Image> ().sprite.name == cardID + "#eksoplismos") {

				//stack it
				if(t.GetComponent<Image> ().sprite.name == "Reds_5#eksoplismos"){
					//stack only siderenia grothia, ola ta alla apla min kaneis tpt  ...for now
					t.Find("EqStar").gameObject.SetActive(true);
					Text stacktxt = t.Find("EqStar").Find("Stacknumber").GetComponent<Text>();
					int stacknumber = int.Parse(t.Find("EqStar").Find("Stacknumber").GetComponent<Text>().text);
					stacknumber++;
					stacktxt.text = stacknumber.ToString ();
				}

				return;
			}

		}

		if (cardID == "Blues_8") {

			RollButtonGO.transform.Find ("Text").GetComponent<Text> ().text = "Make a move";

		}

		GameObject eqCard = Instantiate (EquipCardPrefab) as GameObject;

		eqCard.transform.SetParent(GameObject.Find ("HandDownPanel").transform.Find("TamploEksoplismon").transform);

		eqCard.GetComponent<Image> ().sprite = GetCardFromSpriteName (cardType, cardID+"#eksoplismos");


		eqCard.GetComponent<ZoomOnHover> ().isInMyEquipTamplo = true;


		if (fromCardButton) {
			//this method was called from the called buttons and not from steal or give card
			//check for klerry win
			Debug.Log ("Checking for Klerry");
			foreach (Player pl in players) {
				if (pl.PlayerCharacter == "Klerry" && pl.eksoplismoi.Count >= 4) {
					Debug.Log ("Klerry Won");
					Winner klerrywinner = new Winner (pl.PlName, pl.PlayerCharacter, pl.PlayerColor, pl.PlayerRace);
					List <Winner> winners = new List<Winner>();
					winners.Add (klerrywinner);
					GlobalGameOver (winners);

				}
			}
		}




	}

	[PunRPC]
	public void AddEquipCardToPlayerNetwork_RPC(string cType, string cID, string pName, string pID){
		// this method is called (for now) from the StealThisCard script

		AddEquipCardToPlayerNetwork(cType, cID, pName, pID);

	}

	public void AddEquipCardToPlayerNetwork (string cardType, string cardID, string playername, string playerID){
		Debug.Log ("AddEquipCardToPlayerNetwork run for cardid " + cardID);
		Debug.Log("photon name: " + PhotonNetwork.player.name + "method name: " + playername + "_" + playerID);

		if (PhotonNetwork.player.name == playername + "_" + playerID) {
			//add it to my local player
			//this will run from steal or give card
			Debug.Log("steal card add to my local player");
			AddEquipCardToPlayer(cardType, cardID, false);


		}
		else {
			//add it on another (network) player 
			GameObject eqCard = Instantiate (EquipCardPrefab) as GameObject;
			eqCard.GetComponent<LayoutElement> ().preferredWidth = 11.7f;
			eqCard.GetComponent<LayoutElement> ().preferredHeight = 18.4f;
			eqCard.GetComponent<RectTransform> ().sizeDelta = new Vector2 (11.7f, 18.4f);

			foreach(Transform t1 in PlayerScrollContain.transform){
				if (t1.Find ("PlayerName").GetComponent<Text> ().text == playername && t1.Find ("PlayerID").GetComponent<Text> ().text == playerID) {


					//check for stack
					foreach(Transform t2 in t1.Find ("PlayerEqTamplo").transform){
						if (t2.GetComponent<Image> ().sprite.name == cardID + "#eksoplismos") {


							if (t2.GetComponent<Image> ().sprite.name == "Reds_5#eksoplismos") {
								//add it either way 
								eqCard.transform.SetParent (t1.Find ("PlayerEqTamplo").transform);
								eqCard.GetComponent<Image> ().sprite = GetCardFromSpriteName (cardType, cardID+"#eksoplismos");
								eqCard.GetComponent<ZoomOnHover> ().isInPlayerScrollc = true;

								foreach (Player p in players) {
									if (p.PlName == playername+"_"+playerID) {

										if (p.eksoplismoi.Contains (cardID) == false || (p.eksoplismoi.Contains (cardID) && cardID=="Reds_5") ) {
											p.eksoplismoi.Add (cardID);
										}
									}
								}
							}

							//if not just delete it like nothing happened
							Destroy(eqCard);
							return;


						}

					}

					//if it doesnt exist then add it 
					eqCard.transform.SetParent (t1.Find ("PlayerEqTamplo").transform);
				}
				else {
					Debug.Log ("ERROR: could not find the player in scroll contain, playername is: " + t1.Find ("PlayerName").GetComponent<Text> ().text + " player ID is: " + t1.Find ("PlayerID").GetComponent<Text> ().text);
				}
			}

			//check if klerry just won all clients will check that
			Debug.Log ("Check Klerry");
			foreach (Player pl in players) {
				if (pl.PlayerCharacter == "Klerry" && pl.eksoplismoi.Count >= 4) {
					Debug.Log ("Klerry Won");
					Winner klerrywinner = new Winner (pl.PlName, pl.PlayerCharacter, pl.PlayerColor, pl.PlayerRace);
					List <Winner> winners = new List<Winner>();
					winners.Add (klerrywinner);
					GlobalGameOver (winners);

				}
			}



			eqCard.GetComponent<Image> ().sprite = GetCardFromSpriteName (cardType, cardID+"#eksoplismos");
			eqCard.GetComponent<ZoomOnHover> ().isInPlayerScrollc = true;

		
		}

		foreach (Player p in players) {
			if (p.PlName == playername+"_"+playerID) {

				if (p.eksoplismoi.Contains (cardID) == false || (p.eksoplismoi.Contains (cardID) && cardID=="Reds_5") ) {
					p.eksoplismoi.Add (cardID);
				}
			}
		}

	}


	[PunRPC]
	public void RemoveEquipCardFromPlayer_RPC(string cType, string cID, string pName, string pID){
		// this method is called (for now) from the StealThisCard script

		RemoveEquipCardFromPlayer(cType, cID, pName, pID);

	}

	public void RemoveEquipCardFromPlayer (string cardType, string cardID, string playername, string playerID){
		Debug.Log ("RemoveEquipCardFromPlayer run for cardid " + cardID);

		if (PhotonNetwork.player.name == playername + "_" + playerID) {
			//this means the card will be removed from the eqtamplo not from the player scroll contain



			foreach (Transform t in GameObject.Find ("HandDownPanel").transform.Find("TamploEksoplismon").transform) {
				if (t.GetComponent<Image> ().sprite.name == cardID + "#eksoplismos") {

					//remove one stack if stacked
					if (t.GetComponent<Image> ().sprite.name == "Reds_5#eksoplismos") {
						if (t.Find ("EqStar").gameObject.activeSelf) {
							// this means we have reds5 and also its stacked with at least 2 cards

							Text stacktxt = t.Find ("EqStar").Find ("Stacknumber").GetComponent<Text> ();
							int stacknumber = int.Parse (t.Find ("EqStar").Find ("Stacknumber").GetComponent<Text> ().text);
							if (stacknumber <= 2) {
								//just disable stack star and set stack number to 1
								stacknumber = 1;
								t.Find ("EqStar").gameObject.SetActive (false);

							}
							else {
								stacknumber--;
							}

							stacktxt.text = stacknumber.ToString ();
							foreach (Player p in players) {
								if (p.PlName == playername+"_"+playerID) {

									if (p.eksoplismoi.Contains (cardID) == true ) {
										p.eksoplismoi.Remove (cardID);
									}
								}
							}

							return;
						}


					}

					if (cardID == "Blues_8") {

						RollButtonGO.transform.Find ("Text").GetComponent<Text> ().text = "Roll";

					}

					foreach (Player p in players) {
						if (p.PlName == playername+"_"+playerID) {

							if (p.eksoplismoi.Contains (cardID) == true ) {
								p.eksoplismoi.Remove (cardID);
							}
						}
					}
					Destroy (t.gameObject);
					return;



				}

			}
		}
		else {
			//this means the card will be removed from the player scroll contain

			foreach(Transform t in PlayerScrollContain.transform){
				if (t.Find ("PlayerName").GetComponent<Text> ().text == playername && t.Find ("PlayerID").GetComponent<Text> ().text == playerID) {

					foreach (Transform t2 in t.Find ("PlayerEqTamplo").transform){
						if (t2.GetComponent<Image> ().sprite.name == cardID + "#eksoplismos") {
							//no stacks here yet so just destroy TODO: stacks maybe? we will see at the debugging stage

							Destroy (t2.gameObject);
							foreach (Player p in players) {
								if (p.PlName == playername+"_"+playerID) {

									if (p.eksoplismoi.Contains (cardID) == true ) {
										p.eksoplismoi.Remove (cardID);
									}
								}
							}
							return;
						}


					}
				}
				else {
					Debug.Log ("ERROR: could not find the player in scroll contain, playername is: " + t.Find ("PlayerName").GetComponent<Text> ().text + " player ID is: " + t.Find ("PlayerID").GetComponent<Text> ().text);
				}
			}


		
		}





	}








	public int GetEquipmentNumberOfOtherPlayers(){
		int counter = 0;
		foreach (Transform t in PlayerScrollContain.transform) {
			foreach(Transform t2 in t.Find("PlayerEqTamplo").transform){
				counter++;
			}
		}

		Debug.Log ("GetEquipmentNumberOfOtherPlayers returning: " + counter);
		return counter;

	}

	public int GetEquipmentNumberForPlayer(string PlayerName, string PlayerID){
		int counter = 0;

		if (players[currentPlayerIndex].PlName == PlayerName + "_" + PlayerID) {
			//search my player
			Debug.Log ("search my player");
			foreach(Transform t in GameObject.Find ("HandDownPanel").transform.Find("TamploEksoplismon").transform){
				counter++;
			}

			Debug.Log ("GetEquipmentNumberForPlayer returning: " + counter + " for player " + PlayerName);
			return counter;
		}

		//the player wasn't local

		//search the net players
		foreach (Transform t in PlayerScrollContain.transform) {
			string tPlName = t.Find ("PlayerName").GetComponent<Text> ().text;
			string tPlID = t.Find ("PlayerID").GetComponent<Text> ().text;

			if (tPlName == PlayerName && tPlID == PlayerID) {
				foreach(Transform t2 in t.Find("PlayerEqTamplo").transform){
					counter++;
				}
			}
		}



		Debug.Log ("GetEquipmentNumberForPlayer returning: " + counter + " for player " + PlayerName);
		return counter;

	}

	public int GetEquipmentNumberForPlayerGreen(string PlayerName, string PlayerID){
		int counter = 0;

		if (PhotonNetwork.player.name == PlayerName + "_" + PlayerID) {
			//search my player
			Debug.Log ("search my player");
			foreach(Transform t in GameObject.Find ("HandDownPanel").transform.Find("TamploEksoplismon").transform){
				counter++;
			}

			Debug.Log ("GetEquipmentNumberForPlayer returning: " + counter + " for player " + PlayerName);
			return counter;
		}

		//the player wasn't local

		//search the net players
		foreach (Transform t in PlayerScrollContain.transform) {
			string tPlName = t.Find ("PlayerName").GetComponent<Text> ().text;
			string tPlID = t.Find ("PlayerID").GetComponent<Text> ().text;

			if (tPlName == PlayerName && tPlID == PlayerID) {
				foreach(Transform t2 in t.Find("PlayerEqTamplo").transform){
					counter++;
				}
			}
		}



		Debug.Log ("GetEquipmentNumberForPlayer returning: " + counter + " for player " + PlayerName);
		return counter;

	}

	public void MagicForestDamageClicked(){
		
		MagicForestWhatToDo = 0;
		MFDamageButton.interactable = false;
		MFHealButton.interactable = false;

		MagicForestSelectText.text = "Select Player To Damage 2pts";
		foreach(Transform t in MagicForestScrollContain.transform){
			t.GetComponent<Button> ().interactable = true;
		}

	}

	public void MagicForestHealClicked(){
		
		MagicForestWhatToDo = 1;
		MFDamageButton.interactable = false;
		MFHealButton.interactable = false;
		MagicForestSelectText.text = "Select Player To Heal 1pt";
		foreach(Transform t in MagicForestScrollContain.transform){
			t.GetComponent<Button> ().interactable = true;
		}
	}


	public void MagicForestButtonClicked(){
		//do 1 damage to a random other player
		MagicForestButtonGO.GetComponent<Button>().interactable=false;
		MagicPanelHead.SetActive (true);


		foreach(Transform child in MagicForestScrollContain){
			if(child.gameObject.GetComponent<Button>().interactable && !DamagePointsCreated){
				child.gameObject.GetComponent<Button>().interactable=false;
			}
			else if(!child.gameObject.GetComponent<Button>().interactable && DamagePointsCreated){
				child.gameObject.GetComponent<Button>().interactable=true;
			}
		}	


		foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
			//we also want our player thats why we choose playerList and not otherplayers

			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){
					
					GameObject PlayerEntry = Instantiate(MagicForestPlayerEntry) as GameObject;
					PlayerEntry.transform.SetParent (MagicForestScrollContain.transform);

					string[] splitArray = localp.PlName.Split(new char[]{'_'});

					string MagicPlayerName = splitArray[0];
					string MagickPlayerID = splitArray[1];


					PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = MagicPlayerName;
					PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = MagickPlayerID;
					PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

					if (localp.eksoplismoi.Contains ("Blues_16")) {  //he is protected from magic forest
						PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
					}

					PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromMagicForest>().PlayerClickedFromMagicForest());
					PlayerEntry.GetComponent<Button> ().interactable = false;

					foreach(Transform t in PlayerScrollContain.transform){
						if(t.Find ("PlayerName").GetComponent<Text>().text == MagicPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == MagickPlayerID){
							Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
							PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

							PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
						} 
					}

					if (players [currentPlayerIndex].PlName == MagicPlayerName + "_" + MagickPlayerID) {
						Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
						PlayerAvatar.sprite = NetworkManager.instance.myProfilePic;

						PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = players [currentPlayerIndex].DamagePoints.ToString ();
					}
				}	
			}
		}



	}

	public void CloseMagicForestPanel(){
		MagicPanelHead.SetActive (false);

	}

	public void ClearMagicForestPanel(){
		MagicForestButtonGO.GetComponent<Button> ().interactable = false;



		MagicForestSelectText.text = "Available Players:";


		foreach (Transform t in MagicForestScrollContain) {
			Destroy (t.gameObject);		
		}


		MFDamageButton.GetComponent<Button> ().interactable = true;		
		MFHealButton.GetComponent<Button> ().interactable = true;	


		CloseMagicForestPanel ();

	}

	public void ShowNyxteridaPanel(){
		

		NyxteridaHead.SetActive (true);
		Button NyxteridaHealButton = NyxteridaHead.transform.Find ("NyxteridaPanel").Find ("NyxteridaHealButton").GetComponent<Button> ();



		if (players [currentPlayerIndex].DamagePoints > 0) {
			NyxteridaHealText.text = "You can heal yourself:";
			NyxteridaHealButton.interactable = true;
		}
		else {
			NyxteridaHealText.text = "Your health is full!";
			NyxteridaHealButton.interactable = false;
		}



//		foreach(Transform child in NyxteridatScrollContain){
//			if(child.gameObject.GetComponent<Button>().interactable && !DamagePointsCreated){
//				child.gameObject.GetComponent<Button>().interactable=false;
//			}
//			else if(!child.gameObject.GetComponent<Button>().interactable && DamagePointsCreated){
//				child.gameObject.GetComponent<Button>().interactable=true;
//			}
//		}	


		foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){
					GameObject PlayerEntry = Instantiate(NyxteridaPlayerEntry) as GameObject;
					PlayerEntry.transform.SetParent (NyxteridatScrollContain.transform);

					string[] splitArray = localp.PlName.Split(new char[]{'_'});

					string NyxteridaPlayerName = splitArray[0];
					string NyxteridaPlayerID = splitArray[1];


					PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = NyxteridaPlayerName;
					PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = NyxteridaPlayerID;
					PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

					if (localp.eksoplismoi.Contains ("Blues_13")) {  
						PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
					}

					PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromNyxterida>().PlayerClickedFromNyxterida());
					PlayerEntry.GetComponent<Button> ().interactable = true;

					//handle images:
					foreach(Transform t in PlayerScrollContain.transform){
						if(t.Find ("PlayerName").GetComponent<Text>().text == NyxteridaPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == NyxteridaPlayerID){
							Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
							PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

							PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
						} 
					}
				}	
			}
		}

		//check if they are all protected:
		foreach(Transform t in NyxteridatScrollContain){
			if (!t.Find ("ProtectedText").GetComponent<Text> ().enabled) {
				//if at least one of them is not protected then just return
				return;
			}
		}

		//if we reach this point everyone is protected so enable the x button
		NyxteridaHead.transform.Find ("NyxteridaPanel").Find ("CloseNyxteridaPanelWindowImg").GetComponent<Button> ().enabled = true;
		NyxteridaHead.transform.Find ("NyxteridaPanel").Find ("CloseNyxteridaPanelWindowImg").GetComponent<Image> ().enabled = true;

	}

	public void CloseNyxteridaClicked(){
		ClearNyxteridaPanel ();
	}

	public void NyxteridaHeal(){
		DoHealTo (1, players [currentPlayerIndex].PlName);

		NyxteridaHead.transform.Find ("NyxteridaPanel").Find ("NyxteridaHealButton").GetComponent<Button> ().interactable=false;
	}


	public void ClearNyxteridaPanel(){
		




		foreach (Transform t in NyxteridatScrollContain) {
			Destroy (t.gameObject);		
		}


		NyxteridaHead.SetActive (false);

	}

	public void ShowTherapiaApoMakriaPanel(){

		TherapiaApoMakriaHead.SetActive (true);




		foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){
					GameObject PlayerEntry = Instantiate(TherapiaApoMakriaPlayerEntry) as GameObject;
					PlayerEntry.transform.SetParent (TherapiaApoMakriaScrollContain);

					string[] splitArray = localp.PlName.Split(new char[]{'_'});

					string EnedraPlayerName = splitArray[0];
					string EnedraPlayerID = splitArray[1];


					PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = EnedraPlayerName;
					PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = EnedraPlayerID;
					PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

					PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromTheApMa>().PlayerClickedFromTherapiaApoMakria());
					PlayerEntry.GetComponent<Button> ().interactable = true;

					//handle images:
					foreach(Transform t in PlayerScrollContain.transform){
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

	public void RollForTherapiaApMa (string nameToHeal){
		RollForTherapiaApoMakria = true;
		NameOfPLayerToHealApoMakria = nameToHeal;

		//roll only 6plevro
		Vector3 position2 = new Vector3(0f, 10.5f, 0.42f);
		Vector3 rotation2 = new Vector3(UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f));
		GameObject ssided = ((GameObject)PhotonNetwork.Instantiate ("SixSidedDie", position2, Quaternion.Euler (rotation2),0));
		ssided.GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.Range(150f,300f),UnityEngine.Random.Range(10f,32f),UnityEngine.Random.Range(160f,280f));

	}

	public void DoTherapiaApoMakria(int pointsAfterRoll){
		GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
		DoHealTo (pointsAfterRoll, NameOfPLayerToHealApoMakria);

		RollForTherapiaApoMakria = false;


		
	}

	public void ClearTherapiaApoMakriaPanel(){





		foreach (Transform t in TherapiaApoMakriaScrollContain) {
			Destroy (t.gameObject);		
		}


		TherapiaApoMakriaHead.SetActive (false);

	}

	public void ShowMatomeniAraxniPanel(){


		MatomeniAraxniHead.SetActive (true);


		if (players[currentPlayerIndex].eksoplismoi.Contains ("Blues_13")) { 
			Text YouWillGetDamagedTxt = MatomeniAraxniHead.transform.Find ("MatomeniAraxniPanel").Find("YouWillGetDamagedTxt").GetComponent<Text> ();

			YouWillGetDamagedTxt.text = "You are protected with Filaxto";
			YouWillGetDamagedTxt.color = Color.blue;
		}

		foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){
					GameObject PlayerEntry = Instantiate(MatomeniAraxniPlayerEntry) as GameObject;
					PlayerEntry.transform.SetParent (MatomeniAraxniScrollContain.transform);

					string[] splitArray = localp.PlName.Split(new char[]{'_'});

					string MatomeniAraxniPlayerName = splitArray[0];
					string MatomeniAraxniPlayerID = splitArray[1];


					PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = MatomeniAraxniPlayerName;
					PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = MatomeniAraxniPlayerID;
					PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

					if (localp.eksoplismoi.Contains ("Blues_13")) {  
						PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
					}


					PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromMatomeniAraxni>().PlayerClickedFromMatomeniAraxni());
					PlayerEntry.GetComponent<Button> ().interactable = true;

					//handle images:
					foreach(Transform t in PlayerScrollContain.transform){
						if(t.Find ("PlayerName").GetComponent<Text>().text == MatomeniAraxniPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == MatomeniAraxniPlayerID){
							Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
							PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

							PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
						} 
					}
				}	
			}
		}

		//check if they are all protected:
		foreach(Transform t in MatomeniAraxniScrollContain){
			if (!t.Find ("ProtectedText").GetComponent<Text> ().enabled) {
				//if at least one of them is not protected then just return
				return;
			}
		}

		//if we reach this point everyone is protected so enable the x button
		MatomeniAraxniHead.transform.Find ("MatomeniAraxniPanel").Find ("CloseMatomeniAraxniPanelWindowImg").GetComponent<Button> ().enabled = true;
		MatomeniAraxniHead.transform.Find ("MatomeniAraxniPanel").Find ("CloseMatomeniAraxniPanelWindowImg").GetComponent<Image> ().enabled = true;



	}

	public void CloseMatomeniAraxniClicked(){
		if (!players [currentPlayerIndex].eksoplismoi.Contains ("Blues_13")) { //he is NOT protected from magic forest but everyone else was so he can only click x now
			DoDamageTo (2, players [currentPlayerIndex].PlName);
		}  


		ClearMatomeniAraxniPanel ();
	}



	public void ClearMatomeniAraxniPanel(){





		foreach (Transform t in MatomeniAraxniScrollContain) {
			Destroy (t.gameObject);		
		}


		MatomeniAraxniHead.SetActive (false);

	}

	public void ShowEnedraPanel(){


		EnedraHead.SetActive (true);




		foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){
					GameObject PlayerEntry = Instantiate(EnedraPlayerEntry) as GameObject;
					PlayerEntry.transform.SetParent (EnedraScrollContain);

					string[] splitArray = localp.PlName.Split(new char[]{'_'});

					string EnedraPlayerName = splitArray[0];
					string EnedraPlayerID = splitArray[1];


					PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = EnedraPlayerName;
					PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = EnedraPlayerID;
					PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

					PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromEnedra>().PlayerClickedFromEnedra());
					PlayerEntry.GetComponent<Button> ().interactable = true;

					//handle images:
					foreach(Transform t in PlayerScrollContain.transform){
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

	public void EnedraPlayer(string plName){

		EnedraTargetedPlayer = plName;

		rollForEnedra = true;

		//roll only eksaplevro

		Vector3 position2 = new Vector3(0f, 10.5f, 0.42f);
		Vector3 rotation2 = new Vector3(UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f));
		GameObject ssided = ((GameObject)PhotonNetwork.Instantiate ("SixSidedDie", position2, Quaternion.Euler (rotation2),0));
		ssided.GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.Range(150f,300f),UnityEngine.Random.Range(10f,32f),UnityEngine.Random.Range(160f,280f));


	}

	public void EnedraFinalResult(int result){
		if (enedraDone == false) {
			

			GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);

			if (result >= 1 && result <= 4) {
				DoDamageTo (3, EnedraTargetedPlayer);
			}
			else if (result > 4 && result <= 6) {
				DoDamageTo (3, players[currentPlayerIndex].PlName);
			}

			enedraDone = true;
		}




	}

	public void ClearEnedraPanel(){





		foreach (Transform t in EnedraScrollContain) {
			Destroy (t.gameObject);		
		}


		EnedraHead.SetActive (false);

	}

	public void ShowGregoryAbilityPanel(){
		//this panel is not only for gregory TODO: Rename to general select player panel

		GregoryAbilityHead.SetActive (true);

		if (players [currentPlayerIndex].PlayerCharacter == "Ouriel") {
			// we nneed to display only players in nekrotafio

			foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

				ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

				foreach(Player localp in players){
					if(ht["PlayerName"].ToString() == localp.PlName){

						if (localp.destinationText == "8") {

							GameObject PlayerEntry = Instantiate(GregoryAbilityPlayerEntry) as GameObject;
							PlayerEntry.transform.SetParent (GregoryAbilityScrollContain);

							string[] splitArray = localp.PlName.Split(new char[]{'_'});

							string GregoryAbilityPlayerName = splitArray[0];
							string GregoryAbilityPlayerID = splitArray[1];


							PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = GregoryAbilityPlayerName;
							PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = GregoryAbilityPlayerID;
							PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

							PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromGregorAbility>().PlayerClickedFromGregorAbility());
							PlayerEntry.GetComponent<Button> ().interactable = true;

							//handle images:
							foreach(Transform t in PlayerScrollContain.transform){
								if(t.Find ("PlayerName").GetComponent<Text>().text == GregoryAbilityPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == GregoryAbilityPlayerID){
									Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
									PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

									PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
								} 
							}

						}


					}	
				}
			}
		}
		else {
			//its for the abilities of the other characters

			foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

				ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

				foreach(Player localp in players){
					if(ht["PlayerName"].ToString() == localp.PlName){
						GameObject PlayerEntry = Instantiate(GregoryAbilityPlayerEntry) as GameObject;
						PlayerEntry.transform.SetParent (GregoryAbilityScrollContain);

						string[] splitArray = localp.PlName.Split(new char[]{'_'});

						string GregoryAbilityPlayerName = splitArray[0];
						string GregoryAbilityPlayerID = splitArray[1];


						PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = GregoryAbilityPlayerName;
						PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = GregoryAbilityPlayerID;
						PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

						PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromGregorAbility>().PlayerClickedFromGregorAbility());
						PlayerEntry.GetComponent<Button> ().interactable = true;

						//handle images:
						foreach(Transform t in PlayerScrollContain.transform){
							if(t.Find ("PlayerName").GetComponent<Text>().text == GregoryAbilityPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == GregoryAbilityPlayerID){
								Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
								PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

								PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
							} 
						}
					}	
				}
			}
		}





	}

	public void GregoryAbilityPlayer(string plName){

		if (players [currentPlayerIndex].PlayerCharacter == "Etta") {
			GetComponent<PhotonView> ().RPC ("DisableAbilityOfPlayer", PhotonTargets.AllBufferedViaServer, plName, players [currentPlayerIndex].PlName);
		}
		else if(players [currentPlayerIndex].PlayerCharacter == "Ouriel"){
			DoDamageTo (3, plName);
		}
		else {
			//its from gregor


			GregoryAbilityTargetedPlayer = plName;

			rollForGregoryAbility = true;

			//roll only tetraplevro

			if (players [currentPlayerIndex].PlayerCharacter == "Gregor") {
				Vector3 position = new Vector3(-3.24f, 10.5f, 0.42f);
				Vector3 rotation = new Vector3(UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f));

				GameObject fsided = ((GameObject)PhotonNetwork.Instantiate ("FourSidedDie", position, Quaternion.Euler (rotation),0));
				fsided.GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.Range(150f,300f),UnityEngine.Random.Range(10f,32f),UnityEngine.Random.Range(160f,280f));
			}
			else if(players [currentPlayerIndex].PlayerCharacter == "Frederic"){
				Vector3 position2 = new Vector3(0f, 10.5f, 0.42f);
				Vector3 rotation2 = new Vector3(UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f));
				GameObject ssided = ((GameObject)PhotonNetwork.Instantiate ("SixSidedDie", position2, Quaternion.Euler (rotation2),0));
				ssided.GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.Range(150f,300f),UnityEngine.Random.Range(10f,32f),UnityEngine.Random.Range(160f,280f));
			}
		
		}







	}

	[PunRPC]
	public void DisableAbilityOfPlayer(string PlayerToDisable, string fromPlayer){

		string[] splitArray = fromPlayer.Split(new char[]{'_'});


		foreach (Player pl in players) {
			if(pl.PlName == PlayerToDisable){
				pl.AbilityDisabled = true;
				if (pl.PlName == PhotonNetwork.player.name) {
					//if our ability just got deactivated then disable the button and edit text
					if (PlayerToDisable == fromPlayer) {
						//that means we are Gandolf
						ActivateAbilityButton.GetComponentInChildren<Text>().text = "Ability Done";
					}
					else {
						//someone disabled my ability
						ActivateAbilityButton.GetComponentInChildren<Text>().text = "Ability Disabled by "+splitArray[0];
					}

					if (ActivateAbilityButton.interactable){
						ActivateAbilityButton.interactable = false;
					}
				}

			}

		}

		foreach (Player pl2 in players) {
			if (pl2.PlName == PhotonNetwork.player.name) {
				
			}
		}



	}


	public void GregoryAbilityFinalResult(int result){
		if (gregoryAbilityDone == false) {


			GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);

			DoDamageTo (result, GregoryAbilityTargetedPlayer);
		

			gregoryAbilityDone = true;
		}




	}

	public void ClearGregoryAbilityPanel(){





		foreach (Transform t in GregoryAbilityScrollContain) {
			Destroy (t.gameObject);		
		}


		GregoryAbilityHead.SetActive (false);

	}


	public void ShowMatomenoFegariPanel(){
		
		MatomenoFegariHead.SetActive (true);

		foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
			//we also want our player thats why we choose playerList and not otherplayers

			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){

					GameObject PlayerEntry = Instantiate(MatomenoFegariPlayerEntry) as GameObject;
					PlayerEntry.transform.SetParent (MatomenoFegariScrollContain.transform);

					string[] splitArray = localp.PlName.Split(new char[]{'_'});

					string BloodMoonPlayerName = splitArray[0];
					string BloodMoonPlayerID = splitArray[1];


					PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = BloodMoonPlayerName;
					PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = BloodMoonPlayerID;
					PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(localp.PlayerColor);

					if (localp.eksoplismoi.Contains ("Blues_3")) {  //he is protected from magic forest
						PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
					}

					PlayerEntry.GetComponent<Button>().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromMatomenoFegari>().PlayerClickedFromMatomenoFegari());
					PlayerEntry.GetComponent<Button> ().interactable = true;

					foreach(Transform t in PlayerScrollContain.transform){
						if(t.Find ("PlayerName").GetComponent<Text>().text == BloodMoonPlayerName && t.Find ("PlayerID").GetComponent<Text>().text == BloodMoonPlayerID){
							Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
							PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image>().sprite; 

							PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text>().text = t.Find ("DmgPointsText").GetComponent<Text>().text;
						} 
					}

					if (players [currentPlayerIndex].PlName == BloodMoonPlayerName + "_" + BloodMoonPlayerID) {
						Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
						PlayerAvatar.sprite = NetworkManager.instance.myProfilePic;

						PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = players [currentPlayerIndex].DamagePoints.ToString ();
					}
				}	
			}
		}


	}

	public void ClearMatomenoFegariPanel(){

		foreach (Transform t in MatomenoFegariScrollContain) {
			Destroy (t.gameObject);		
		}

		MatomenoFegariHead.SetActive (false);

	}


	public void DoIeriOrgi(){

		foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach (Player localp in players) {
				if (ht ["PlayerName"].ToString () == localp.PlName) {
					
					DoDamageTo (2, localp.PlName);
				
				}
			
			}
		}
	
	}

	public void DoEkriksi(){
		RollForEkriksi = true;
		RollDice ();

	}

	public void HandleGreenCardResult(string rName){
		
		GetComponent<PhotonView> ().RPC ("HandleGreenCardResult_RPC", PhotonTargets.AllBufferedViaServer, rName);
		GameObject.FindObjectOfType<GreenCardScript> ().ClearGreenCardPanel ();
	}

	[PunRPC]
	public void HandleGreenCardResult_RPC(string receivName){
		GameObject.FindObjectOfType<GreenCardScript> ().GreenCardResult(receivName);
	}


	public void DamageAllPlayersAtArea (string area, int dmgpoints){
		GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);

		if (area == "4" || area == "5")
			area = "45";
		if (area == "2" || area == "3")
			area = "23";
		if (area == "7") {  //Do nothing if 7
			RollForEkriksi = false;
			return;
		}

		foreach (Player pl in players) {
			if (pl.destinationText == area) {
				if (!pl.eksoplismoi.Contains ("Blues_13")) {
					DoDamageTo (dmgpoints, pl.PlName);					
				}
				else {
					//do nothing
					//TODO: add here some fancy way to let the user now that player is protected
				}
					

			}
		}

		RollForEkriksi = false;

	}



	public Color GetColorFromString(string str){
		if(str == "gray"){
			return Color.gray;
		}
		else if(str == "blue"){
			return Color.blue;
		}
		else if(str == "yellow"){
			return Color.yellow;
		}
		else if(str == "red"){
			return Color.red;
		}
		else if(str == "black"){
			return Color.black;
		}

		return Color.white; // white is the error color probably :P
	}

	public void CloseAttackPanel(){
		AttackPanelHead.SetActive (false);
		if (!AttackPanelCleared) {
			ClearAttackPanel ();
		}
	}



	public void RollToAttackClicked(){
		RollDiceForAttack=true;
		RollToAttackButton.interactable = false;
		EndTurnButtonGO.GetComponent<Button> ().interactable = false;
		RollDice ();
	}

	public void DisableAllButtons(){
		//first we have to log the button state
		//the order goes the same as they are disabled bellow
		buttonState[0] = RollButtonGO.GetComponent<Button> ().interactable;
		buttonState[1] = AttackButtonGO.GetComponent<Button> ().interactable;
		buttonState[2] = EndTurnButtonGO.GetComponent<Button> ().interactable;
		buttonState[3] = MagicForestButtonGO.GetComponent<Button> ().interactable;
		buttonState[4] = PetrinosKiklosButton.interactable;
		buttonState[5] = RevealCharacterButton.interactable;
		buttonState[6] = ActivateAbilityButton.interactable;
		buttonState[7] = greenCardButton.GetComponent<Button> ().interactable;
		buttonState[8] = redCardButton.GetComponent<Button> ().interactable;
		buttonState[9] = blueCardButton.GetComponent<Button> ().interactable;


		RollButtonGO.GetComponent<Button> ().interactable = false; 
		AttackButtonGO.GetComponent<Button> ().interactable = false; 
		EndTurnButtonGO.GetComponent<Button> ().interactable = false;

		MagicForestButtonGO.GetComponent<Button> ().interactable = false;
		PetrinosKiklosButton.interactable = false;

		RevealCharacterButton.interactable = false;
		ActivateAbilityButton.interactable = false;

		DisableGreenCards (true);
		DisableRedCards (true);
		DisableBlueCards (true);
	}

	public void EnableButtons(){
		RollButtonGO.GetComponent<Button> ().interactable = buttonState[0]; 
		AttackButtonGO.GetComponent<Button> ().interactable = buttonState[1]; 
		EndTurnButtonGO.GetComponent<Button> ().interactable = buttonState[2];

		MagicForestButtonGO.GetComponent<Button> ().interactable = buttonState[3];
		PetrinosKiklosButton.interactable = buttonState[4];

		RevealCharacterButton.interactable = buttonState[5];
		ActivateAbilityButton.interactable = buttonState[6];

		DisableGreenCards (buttonState[7]);
		DisableRedCards (buttonState[8]);
		DisableBlueCards (buttonState[9]);
	}

	public void SetDamageTo(int DmgToSet, string n){
		GetComponent<PhotonView>().RPC("SetDamageTo_RPC", PhotonTargets.AllBuffered, DmgToSet.ToString() + '#' + n, PhotonNetwork.player.name);
	}

	[PunRPC]
	public void SetDamageTo_RPC(string PointsAndName, string nameOfDamager){

		string[] splitArray = PointsAndName.Split(new char[]{'#'});

		int PointsToDmg = int.Parse(splitArray[0]);
		string NameOfPlayerToDamage = splitArray [1];
		Debug.Log ("Trying to damage: " + NameOfPlayerToDamage);
		foreach (Player pl in players) {
			if(pl.PlName == NameOfPlayerToDamage){
				pl.DamagePoints = PointsToDmg;
			}
			HandleDmgText ();
		}

		HandleDmgPositionForPlayer (NameOfPlayerToDamage, nameOfDamager);

	}


	public void DoDamageTo(int DmgPoints, string n){
		if(DmgPoints>0){
			GetComponent<PhotonView>().RPC("DoDamageTo_RPC", PhotonTargets.AllBuffered, DmgPoints.ToString() + '#' + n, PhotonNetwork.player.name);	
		}
	}



	[PunRPC]
	public void DoDamageTo_RPC(string PointsAndName, string nameOfDamager){

		string[] splitArray = PointsAndName.Split(new char[]{'#'});

		int PointsOfDmg = int.Parse(splitArray[0]);
		string NameOfPlayerToDamage = splitArray [1];
		Debug.Log ("Trying to damage: " + NameOfPlayerToDamage);
		foreach (Player pl in players) {
			if(pl.PlName == NameOfPlayerToDamage){
				pl.DamagePoints += PointsOfDmg;
			}
			HandleDmgText ();
		}

		HandleDmgPositionForPlayer (NameOfPlayerToDamage, nameOfDamager);

	}

	public void DoDamageFromAttackTo(int DmgPoints, string n){
		if (DmgPoints > 0) {
			GetComponent<PhotonView>().RPC("DoDamageFromAttackTo_RPC", PhotonTargets.AllBuffered, DmgPoints.ToString() + '#' + n, PhotonNetwork.player.name);
		}

	}

	[PunRPC]
	public void DoDamageFromAttackTo_RPC(string PointsAndName, string nameOfDamager){
		
		string[] splitArray = PointsAndName.Split(new char[]{'#'});


		int PointsOfDmg = int.Parse(splitArray[0]);
		string NameOfPlayerToDamage = splitArray [1];

		foreach (Player p in players) {
			if (p.PlName == PhotonNetwork.player.name) {
				if (p.PlName == NameOfPlayerToDamage) {
					if (p.PlayerCharacter == "Malburca" && p.AbilityActivated && !p.AbilityDisabled) {
						MalburcaHead.SetActive (true);
						MalburcaPlayerToAttackBack = nameOfDamager;
					}
				} 
				else if (p.PlName == nameOfDamager) {
					//the damager 
					bool attackingactivemalburca = false;
					//check if malburca is the player to damage and if she's activated
					foreach(Player p2 in players){
						if (p2.PlName == NameOfPlayerToDamage) {
							if (p2.PlayerCharacter == "Malburca" && p2.AbilityActivated && !p2.AbilityDisabled) {
								attackingactivemalburca = true;

							}
						}

					}
					if (attackingactivemalburca) {

						WarningText.SetActive (true);
						WarningText.GetComponent<Text> ().text = "Waiting for Malburca to attack back...";
						WaitingForMalburca = true;
						DisableAllButtons ();
					}

				}
				else{
					//everyone else
					//check if malburca is the player to damage and if she's activated
					bool attackingactivemalburca = false;
					//check if malburca is the player to damage and if she's activated
					foreach(Player p2 in players){
						if (p2.PlName == NameOfPlayerToDamage) {
							if (p2.PlayerCharacter == "Malburca" && p2.AbilityActivated && !p2.AbilityDisabled) {
								attackingactivemalburca = true;
							}
						}

					}
					if (attackingactivemalburca) {

						WarningText.SetActive (true);
						WarningText.GetComponent<Text> ().text = "Waiting for Malburca to attack back...";
						WaitingForMalburca = true;
					}

				}
			}



		}

		Debug.Log ("Trying to damage: " + NameOfPlayerToDamage);
		foreach (Player pl in players) {
			if(pl.PlName == NameOfPlayerToDamage){
				pl.DamagePoints += PointsOfDmg;
			}
			HandleDmgText ();
		}

		HandleDmgPositionForPlayer (NameOfPlayerToDamage, nameOfDamager);

	}


	public void DoHealTo(int HealPoints, string n){
		GetComponent<PhotonView>().RPC("HealPlayer_RPC", PhotonTargets.AllBuffered, HealPoints.ToString() + '#' + n);
	}

	[PunRPC]
	public void HealPlayer_RPC(string PointsAndName){

		string[] splitArray = PointsAndName.Split(new char[]{'#'});
		
		int PointsToHeal = int.Parse(splitArray[0]);
		string NameOfPlayerToHeal = splitArray [1];
		Debug.Log ("Trying to heal: " + NameOfPlayerToHeal);
		foreach (Player pl in players) {
			if(pl.PlName == NameOfPlayerToHeal){
				pl.DamagePoints -= PointsToHeal;

				if (pl.DamagePoints < 0) {
					pl.DamagePoints = 0;
				}
			}
			HandleDmgText ();
		}

		HandleDmgPositionForPlayer (NameOfPlayerToHeal);

	}

	public int GetPlayersInArea(string area){
		int PlayersInThatArea = 0;


		foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){
					if(localp.destinationText == area){
						PlayersInThatArea++;
					}
					if (GetNeighboor (area) != "none") {
						if (localp.destinationText == GetNeighboor(area)) {
							PlayersInThatArea++;
						}
					}
					else{
						Debug.Log ("Can't find neighboor, wtf?");
					}


				}
			}


		}

		return PlayersInThatArea;
	}

	public int GetPlayersInOppositeAreas(string area){
		//this method is for valistra 

		int PlayersInThoseAreas = 0;


		foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
			ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

			foreach(Player localp in players){
				if(ht["PlayerName"].ToString() == localp.PlName){
					if(localp.destinationText != area && localp.destinationText != "none" && localp.destinationText != GetNeighboor(area)){
						PlayersInThoseAreas++;
						Debug.Log ("GetPlayersInOppositeAreas: run on first if +1");
					}

//					if (GetNeighboor (area) != "none") {
//						if (localp.destinationText != GetNeighboor(area)) {
//							PlayersInThoseAreas++;
//							Debug.Log ("GetPlayersInOppositeAreas: run on second if +1");
//						}
//					}
//					else{
//						Debug.LogWarning ("Can't find neighboor, wtf?");
//					}


				}
			}


		}
		Debug.Log ("GetPlayersInOppositeAreas: total: " + PlayersInThoseAreas.ToString());
		return PlayersInThoseAreas;
	}

	public string GetNeighboor(string area){
		string neighboor="none";

		if (area == "23") {
			neighboor = "8";
		}
		else if(area == "45"){
			neighboor = "10";
		}
		else if(area == "6"){
			neighboor = "9";
		}
		else if(area == "7"){
			neighboor = "45";
		}
		else if(area == "8"){
			neighboor = "23";
		}
		else if(area == "9"){
			neighboor = "6";
		}
		else if(area == "10"){
			neighboor = "45";
		}

		return neighboor;
	}


	public void RollDice(){




		Vector3 position = new Vector3(-3.24f, 10.5f, 0.42f);
		Vector3 rotation = new Vector3(UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f));

		GameObject fsided = ((GameObject)PhotonNetwork.Instantiate ("FourSidedDie", position, Quaternion.Euler (rotation),0));
		fsided.GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.Range(150f,280f),UnityEngine.Random.Range(10f,32f),UnityEngine.Random.Range(160f,290f));


		if (RollDiceForAttack && (players[currentPlayerIndex].eksoplismoi.Contains("Reds_4") || (players [currentPlayerIndex].PlayerCharacter == "Valkyria" && players [currentPlayerIndex].AbilityActivated))) {

			return;

		}

		
		Vector3 position2 = new Vector3(0f, 10.5f, 0.42f);
		Vector3 rotation2 = new Vector3(UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f),UnityEngine.Random.Range(0f, 360f));
		GameObject ssided = ((GameObject)PhotonNetwork.Instantiate ("SixSidedDie", position2, Quaternion.Euler (rotation2),0));
		ssided.GetComponent<Rigidbody>().AddTorque(UnityEngine.Random.Range(150f,280f),UnityEngine.Random.Range(10f,32f),UnityEngine.Random.Range(160f,290f));
		
		
	}



	
	void generatePlayers() {

		//generate players in the list

		foreach (string NameWithID in NetworkManager.instance.PlayerNames) {
			if (NameWithID != PhotonNetwork.player.name) {
				Debug.Log("namewithid: " + NameWithID);
				string[] splitArray =  NameWithID.Split(new char[]{'_'}); //Here we're passing the splitted string to array by that char

				string name = splitArray[0]; //Here we assign the first part to the name

				string ID = splitArray[1]; //Here we assing the second part to the ID


				GameObject PlayerEntry;
				PlayerEntry = Instantiate(PlayerEntryPrefab) as GameObject;
				PlayerEntry.transform.SetParent (PlayerScrollContain.transform);

				PlayerEntry.transform.Find ("PlayerName").GetComponent<Text>().text = name;
				PlayerEntry.transform.Find ("PlayerID").GetComponent<Text>().text = ID;


				FB.API (Util.GetPictureURL(ID.ToString(), 50, 50), HttpMethod.GET, delegate(IGraphResult pictureResult) {
					if(pictureResult.Error != null) { // in case there was an error
						Debug.Log (pictureResult.Error);
					}
					else { //we got the image

						Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image>(); 
						PlayerAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect (0, 0, 50, 50), new Vector2 (0, 0));
					}
				});	
			}

		}






		//generate pionia kai hp pionia
		ExitGames.Client.Photon.Hashtable hashtable = PhotonNetwork.player.customProperties;

		List<string> tempNames = NetworkManager.instance.PlayerNames;
		for (int i=0; i<NetworkManager.instance.PlayerNames.Count; i++) {
			if(hashtable["PlayerName"].ToString() == tempNames[i]){
				GameObject PGO = PhotonNetwork.Instantiate ("PlayerPrefab", new Vector3 (SpawnSpots[i].position.x, 0.5f, 2.67f), Quaternion.Euler (new Vector3 (0, 0, 0)), 0);
				PGO.GetComponent<UserPlayer>().PlName = hashtable["PlayerName"].ToString();
				ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
				customProperties.Add  ("PlayerColor", PlayerColor[i]);
				PhotonNetwork.player.SetCustomProperties (customProperties);

				Vector3 HPPosition = HPSpots[i].transform.Find("0hp").position;

				GameObject HPGO = PhotonNetwork.Instantiate ("HPPrefab", new Vector3 (HPPosition.x, HPPosition.y, HPPosition.z), Quaternion.Euler (new Vector3 (-90, -180, 0)), 0);


				HPGO.GetComponent<HPPlayer>().HPName = hashtable["PlayerName"].ToString();
				HPGO.GetComponent<HPPlayer>().moveDestination = HPPosition;

				PGO.GetComponent<UserPlayer>().PlName = hashtable["PlayerName"].ToString();
			}
		}


		


		StartCoroutine (HandlePlayerNameAndColor (3,hashtable["PlayerName"].ToString()));



//		if (PhotonNetwork.isMasterClient) {
//						
//			PhotonPlayer[] pls = PhotonNetwork.playerList;
//			float temp = -6.87f;
//			for(int i = 0; i<pls.Length; i++){
//
//				foreach(string name in NetworkManager.instance.PlayerNames){
//					Debug.Log("NNNNNNNNNAME : " + name + " pls length: " + pls.Length);
//					ExitGames.Client.Photon.Hashtable ht = pls[i].customProperties;
//
//
//					if(ht["PlayerName"].ToString()==name){
//						ExitGames.Client.Photon.Hashtable rht = PhotonNetwork.room.customProperties;
//						Debug.Log ("Player name: " + ht["PlayerName"].ToString() + " master now name : " +rht["MasterRightNow"].ToString());
//
//
//						GameObject PGO = PhotonNetwork.Instantiate ("PlayerPrefab", new Vector3 (temp, 0.5f, 2.67f), Quaternion.Euler (new Vector3 (0, 0, 0)), 0);
//						if(ht["PlayerName"].ToString()!=rht["MasterRightNow"].ToString()){
//							PGO.GetComponent<PhotonView>().TransferOwnership (pls[i].ID);
//						}
//						temp = temp + 1.2f;
//						ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
//						customProperties.Add  ("PlayerColor", PlayerColor[i]);
//						pls[i].SetCustomProperties (customProperties);
//					}
//				}
//
//			}
//		}
//
//
//
//			GetComponent<PhotonView>().RPC("HandlePlayerNamesAndColors_RPC", PhotonTargets.AllBuffered, null);
//			


//			firstplayer = ((GameObject)PhotonNetwork.Instantiate ("PlayerPrefab", new Vector3 (-6.87f, 0.5f, 2.67f), Quaternion.Euler (new Vector3 (0, 0, 0)), 0)).GetComponent<UserPlayer> ();
//
//
//
//			firstplayer.gameObject.name = NetworkManager.instance.PlayerNames [0];
//			firstplayer.PlayerColor = PlayerColor [0];
//			firstplayer.PlName = NetworkManager.instance.PlayerNames [0];
//			firstplayer.CorrectColor();
//			players.Add (firstplayer);
//			
//			for (int i = 1; i<NumberOfPlayers; i++) {
//				float temp = players[i-1].transform.position.x + 1.2f;
//				player = ((GameObject)PhotonNetwork.Instantiate ("PlayerPrefab", new Vector3 (temp, 0.5f, 2.67f), Quaternion.Euler (new Vector3 (0, 0, 0)), 0)).GetComponent<UserPlayer> ();
//				player.gameObject.name = NetworkManager.instance.PlayerNames [i];
//				player.PlayerColor = PlayerColor[i];
//				player.PlName = NetworkManager.instance.PlayerNames [i];
//				player.CorrectColor();
//				players.Add (player);
//			}
//
//		}

	}

	public void HandleDmgPositionForPlayer(string NameAndID, string nameOfDamagerAndID){


		for (int i = 0; i<HPplayers.Count; i++) {
			if(HPplayers[i].HPName==NameAndID){

				foreach(Player pl in players){
					if(pl.PlName == HPplayers[i].HPName){

						if (pl.DamagePoints >= pl.DiesAt) {
							HPplayers[i].moveDestination = HPSpots[i].transform.Find(pl.DiesAt.ToString()+"hp").position;	
							DisplayGameOverText (pl.PlName);
							StartCoroutine (PlayerGameOver (pl, nameOfDamagerAndID));




							foreach (Player damager in players) {
								if (damager.PlName == nameOfDamagerAndID) {
									if (damager.eksoplismoi.Contains ("Blues_7") || (players[currentPlayerIndex].PlayerCharacter == "Klerry" && players[currentPlayerIndex].AbilityActivated)) {
										//o damager exei sakidio opote kane steal oles tis kartes tou dead player

										string[] Owner =  pl.PlName.Split(new char[]{'_'});
										string OwnerName = Owner[0];
										string OwnerID = Owner[1];

										string[] Killer =  nameOfDamagerAndID.Split(new char[]{'_'});
										string KillerName = Killer[0];
										string KillerID = Killer[1];


										foreach(String card in pl.eksoplismoi){

											string[] Card =  card.Split(new char[]{'_'});
											string cardtype = Card[0];
											string cardID = card;


											if (PhotonNetwork.player.isMasterClient) {
												//run this only once from the master
												GetComponent<PhotonView>().RPC("RemoveEquipCardFromPlayer_RPC", PhotonTargets.AllBuffered, cardtype, cardID, OwnerName, OwnerID);

												GetComponent<PhotonView>().RPC("AddEquipCardToPlayerNetwork_RPC", PhotonTargets.AllBuffered, cardtype, cardID, KillerName, KillerID);
											}

										}



									}
								}
							}





						}
						else {
							HPplayers[i].moveDestination = HPSpots[i].transform.Find(pl.DamagePoints.ToString()+"hp").position;	
						}
					
					}
				}
			}
		}


	}

	public void HandleDmgPositionForPlayer(string NameAndID){


		for (int i = 0; i<HPplayers.Count; i++) {
			if(HPplayers[i].HPName==NameAndID){

				foreach(Player pl in players){
					if(pl.PlName == HPplayers[i].HPName){

						if (pl.DamagePoints >= pl.DiesAt) {
							HPplayers[i].moveDestination = HPSpots[i].transform.Find(pl.DiesAt.ToString()+"hp").position;	
							DisplayGameOverText (pl.PlName);
							StartCoroutine (PlayerGameOver (pl, null));

						}
						else {
							HPplayers[i].moveDestination = HPSpots[i].transform.Find(pl.DamagePoints.ToString()+"hp").position;	
						}

					}
				}
			}
		}


	}

	public void DisplayGameOverText (string name){
		
		DeadPlayerPanel.SetActive (true);
		string[] splitArray =  name.Split(new char[]{'_'}); 
		string onlyname = splitArray [0];

		if (name == PhotonNetwork.player.name) {

			DeadPlayerPanel.transform.Find ("DeadText").GetComponent<Text> ().text = "You are DEAD!!!";
		} 
		else {

			DeadPlayerPanel.transform.Find ("DeadText").GetComponent<Text> ().text = onlyname + " is DEAD!!!";	
		}

		StartCoroutine(HideDeadPlayerPanel(2f));
	}

	public IEnumerator HideDeadPlayerPanel(float duration)		
	{

		yield return new WaitForSeconds(duration);

		//code
		DeadPlayerPanel.SetActive(false);	


		yield break;


	}

	public void CheckIfGlobalGameOver(){
		Debug.Log ("check if global gameover");
		Debug.Log ("dead players count: " + deadPlayers.Count);
		List <Winner> winners = new List<Winner>();

		int LycansAlive = 0;
		int LycansDead = 0;
		int VampsAlive = 0;
		int VampsDead = 0;
		int HumansAlive = 0;
		int HumansDead = 0;

		//check if chloe is the first to die
		if (deadPlayers.Count == 1) {
			
			if (deadPlayers [0].PlayerCharacter == "Xloey") {

				//"Chloe (Human) WINS"
				UserPlayer chloe = getPlayerWithCharName("Xloey");
				Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

				winners.Add (chloeWinner);
				GlobalGameOver (winners);
				return;
			}
		}

		//check if raphael is the first to die
		if (deadPlayers.Count == 1) {
			if (deadPlayers [0].PlayerCharacter == "Raphael") {

				//"Raphael (Human) WINS"
				UserPlayer raphael = getPlayerWithCharName("Raphael");
				Winner raphaelWinner = new Winner (raphael.PlName, raphael.PlayerCharacter, raphael.PlayerColor, raphael.PlayerRace);

				winners.Add (raphaelWinner);
				GlobalGameOver (winners);
				return;
			}
		}



		foreach(UserPlayer pl in players){
			if (pl.PlayerRace == "Lycan") {
				LycansAlive++;
			}
			if (pl.PlayerRace == "Vamp") {
				VampsAlive++;
			}
			if (pl.PlayerRace == "Human") {
				HumansAlive++;
			}
		}

		foreach(UserPlayer dpl in deadPlayers){
			if (dpl.PlayerRace == "Lycan") {
				LycansDead++;
			}
			if (dpl.PlayerRace == "Vamp") {
				VampsDead++;
			}
			if (dpl.PlayerRace == "Human") {
				HumansDead++;
			}
		}

		Debug.Log ("CheckIfGlobalGameOver game: Lycans Dead = " + LycansDead + ", Vamps Dead = " + VampsDead + ", Humans Dead = " + HumansDead + " NumOfPlayers: " + NumberOfPlayers + " dead players: " + deadPlayers.Count.ToString());






		//Check if Klerry just won and died at the same time
		foreach (UserPlayer deadpl in deadPlayers) {
			if (deadpl.PlayerCharacter == "Klerry" && deadpl.eksoplismoi.Count >= 4) {
				Winner klerrywinner = new Winner (deadpl.PlName, deadpl.PlayerCharacter, deadpl.PlayerColor, deadpl.PlayerRace);
				winners.Add (klerrywinner);

			}
		}





		bool ClaudiosKilled12less = false;
		//check if claudios killed someone with 12 or less 
		foreach (UserPlayer deadpl in deadPlayers) {
			if (HumansAlive == 1 && deadpl.killedBy == "Klaudios" && deadpl.DiesAt <= 12) {
				//check if claudios also gave win to other players
				ClaudiosKilled12less = true;
			}
		}


		//check if claudios killed someone with 13 or higher TODO: add exceptions checks and Debug.LogWarning later
		foreach(UserPlayer deadpl in deadPlayers){
			if (ClaudiosKilled12less == false) {
				if (HumansAlive == 1 && deadpl.killedBy == "Klaudios" && deadpl.DiesAt >= 13) {
					//check if claudios also gave win to other players
					// numberofplayers 2 and 4 means no humans therefore no claudios

					if ((NumberOfPlayers + deadPlayers.Count) == 3) { 
						UserPlayer claudios = getPlayerWithCharName("Klaudios");
						Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

						winners.Add (claudiosWinner);

						if(VampsDead == 1 ){

							//"Claudios (Human) and the Lycans WIN"

							List <UserPlayer> lycans = new List<UserPlayer>();
							lycans = getPlayersWithRace ("Lycan");
							foreach (UserPlayer lyc in lycans) {
								Winner temp = new Winner (lyc.PlName, lyc.PlayerCharacter, lyc.PlayerColor, lyc.PlayerRace);
								if (lyc.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							GlobalGameOver (winners);
							return;

						}
						if (LycansDead ==1) {

							//"Claudios (Human) and the Vamps WIN"
							List <UserPlayer> vampz = new List<UserPlayer>();
							vampz = getPlayersWithRace ("Vamp");
							foreach (UserPlayer vmp in vampz) {
								Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
								if (vmp.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							GlobalGameOver (winners);
							return;
						}

					}
					if ((NumberOfPlayers + deadPlayers.Count) == 5) {  
						UserPlayer claudios = getPlayerWithCharName("Klaudios");
						Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

						winners.Add (claudiosWinner);

						if(VampsDead == 1 && LycansDead ==1 ){
							//means he killed one and there is still one alive

							//"Claudios (Human) WIN"
							GlobalGameOver (winners);
							return;
						}
						if(VampsDead == 2 ){

							//"Claudios (Human) and the Lycans WIN"
							List <UserPlayer> lycans = new List<UserPlayer>();
							lycans = getPlayersWithRace ("Lycan");
							foreach (UserPlayer lyc in lycans) {
								Winner temp = new Winner (lyc.PlName, lyc.PlayerCharacter, lyc.PlayerColor, lyc.PlayerRace);
								if (lyc.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							GlobalGameOver (winners);
							return;

						}
						if (LycansDead == 2) {
							
							//"Claudios (Human) and the Vamps WIN"
							List <UserPlayer> vampz = new List<UserPlayer>();
							vampz = getPlayersWithRace ("Vamp");
							foreach (UserPlayer vmp in vampz) {
								Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
								if (vmp.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							GlobalGameOver (winners);
							return;
						}

					}

				}
			}

		}





		//check for stantar Lycan vs Vamp cases

		if ((NumberOfPlayers + deadPlayers.Count) == 2) {
			//there are only vampires and lycans, check who is dead and who is alive, if all dead then its draw (probably never gonna happen)

			if(deadPlayers.Count>0){
				
//				if (deadPlayers.Count != NumberOfPlayers) {
//					//not draw, check if all opossites are dead
					if(VampsDead > 0 ){
						if (VampsDead == 1) {

							//"Lycans WIN"
							List <UserPlayer> lycans = new List<UserPlayer>();
							lycans = getPlayersWithRace ("Lycan");
							foreach (UserPlayer lyc in lycans) {
								Winner temp = new Winner (lyc.PlName, lyc.PlayerCharacter, lyc.PlayerColor, lyc.PlayerRace);
								if (lyc.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							GlobalGameOver (winners);
						} 
						else if(VampsDead>1) {
							Debug.LogWarning ("Number of players is 2, and we have more that 1 dead vampires!!");
						}
					}
					if (LycansDead > 0) {
						if (LycansDead == 1) {

							//"Vamps WIN"
							List <UserPlayer> vampz = new List<UserPlayer>();
							vampz = getPlayersWithRace ("Vamp");
							foreach (UserPlayer vmp in vampz) {
								Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
								if (vmp.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							GlobalGameOver (winners);
						} 
						else if(LycansDead>1) {
							Debug.LogWarning ("Number of players is 2, and we have more that 1 dead lycans!!");
						}
					}

//				} 
//
//				else {
//					//draw!! --impossible (almost)!!
//					//TODO: DRAW METHOD AND FINISH GAME
//
//					Debug.Log ("DRAW, impliment draw method");
//				}


			}

				
		}

		if ((NumberOfPlayers + deadPlayers.Count) == 3) {

			if(deadPlayers.Count>0){
				if (deadPlayers.Count != NumberOfPlayers) {
					//not draw, check if all opossites are dead


					if(VampsDead > 0 ){
						if (VampsDead == 1) {
							
							List <UserPlayer> lycans = new List<UserPlayer>();
							lycans = getPlayersWithRace ("Lycan");
							foreach (UserPlayer lyc in lycans) {
								Winner temp = new Winner (lyc.PlName, lyc.PlayerCharacter, lyc.PlayerColor, lyc.PlayerRace);
								if (lyc.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							if (HumansAlive == 1) {
								foreach (UserPlayer pl in players) {
									if (pl.PlayerCharacter == "Anta") {


										UserPlayer anta = getPlayerWithCharName("Anta");
										Winner antaWinner = new Winner (anta.PlName, anta.PlayerCharacter, anta.PlayerColor, anta.PlayerRace);

										winners.Add (antaWinner);

										//"Lycans and Anta (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Raphael") {

										UserPlayer raphael = getPlayerWithCharName("Raphael");
										Winner raphaelWinner = new Winner (raphael.PlName, raphael.PlayerCharacter, raphael.PlayerColor, raphael.PlayerRace);

										winners.Add (raphaelWinner);

										//"Lycans and Raphael (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Klaudios" && pl.destinationText == "10"){ 

										UserPlayer claudios = getPlayerWithCharName("Klaudios");
										Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

										winners.Add (claudiosWinner);
										
										//"Lycans and Claudios (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Xloey") {
										UserPlayer chloe = getPlayerWithCharName("Xloey");
										Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

										winners.Add (chloeWinner);

										//"Lycans and Chloe (Human) WIN"
										GlobalGameOver (winners);
									}
								}

							}
							else if (HumansAlive == 0) {
								//humans are all dead so only Lycans remain

								//"Lycans WIN" already stored from before 
								GlobalGameOver (winners);
							}
							else if (HumansAlive > 1) {
								Debug.Log ("HumansAlive should be 1 or 0!! please debug");
							}

						} 
						else if(VampsDead>1) {
							Debug.LogWarning ("Number of players is 3, and we have more that 1 dead vampires!!");
						}
					}


					if (LycansDead > 0) {
						if (LycansDead == 1) {
							List <UserPlayer> vampz = new List<UserPlayer>();
							vampz = getPlayersWithRace ("Vamp");
							foreach (UserPlayer vmp in vampz) {
								Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
								if (vmp.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							if (HumansAlive == 1) {
								foreach (UserPlayer pl in players) {
									if (pl.PlayerCharacter == "Anta") {
										UserPlayer anta = getPlayerWithCharName("Anta");
										Winner antaWinner = new Winner (anta.PlName, anta.PlayerCharacter, anta.PlayerColor, anta.PlayerRace);

										winners.Add (antaWinner);

										//"Vamp and Anta (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Klaudios" && pl.destinationText == "10"){
										UserPlayer claudios = getPlayerWithCharName("Klaudios");
										Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

										winners.Add (claudiosWinner);

										//"Vamp and Claudios (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Xloey") {
										UserPlayer chloe = getPlayerWithCharName("Xloey");
										Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

										winners.Add (chloeWinner);

										//"Vamp and Chloe (Human) WIN"
										GlobalGameOver (winners);
									}
								}
							}
							else if (HumansAlive == 0) {
								//humans are all dead so only Vamps remain

								//"Vamps WIN"

								GlobalGameOver (winners);
							}
							else if (HumansAlive > 1) {
								Debug.Log ("HumansAlive should be 1 or 0!! please debug");
							}
						} 
						else if(LycansDead>1) {
							Debug.LogWarning ("Number of players is 3, and we have more that 1 dead lycans!!");
						}
					}
				} 

				else {
					//draw!! --impossible (almost)!!
					//TODO: DRAW METHOD AND FINISH GAME

					Debug.Log ("Lycan-Vamp DRAW, impliment draw method");
				}


			}
		
		}
		if ((NumberOfPlayers + deadPlayers.Count) == 4) {

			if (deadPlayers.Count != (NumberOfPlayers + deadPlayers.Count)) {
				//not draw, check if all opossites are dead
				if(VampsDead > 0 ){
					if (VampsDead == 2) {

						//"Lycans WIN"
						List <UserPlayer> lycans = new List<UserPlayer>();
						lycans = getPlayersWithRace ("Lycan");
						foreach (UserPlayer lyc in lycans) {
							Winner temp = new Winner (lyc.PlName, lyc.PlayerCharacter, lyc.PlayerColor, lyc.PlayerRace);
							if (lyc.killedBy != "") {
								temp.isDead = true;
							}
							winners.Add (temp);
						}
						GlobalGameOver (winners);
					}
					else if(VampsDead>2) {
						Debug.LogWarning ("Number of players is 4, and we have more that 2 dead vampires!!");
					}
				}
				if (LycansDead > 0) {
					if (LycansDead == 2) {

						//"Vamps WIN"
						List <UserPlayer> vampz = new List<UserPlayer>();
						vampz = getPlayersWithRace ("Vamp");
						foreach (UserPlayer vmp in vampz) {
							Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
							if (vmp.killedBy != "") {
								temp.isDead = true;
							}
							winners.Add (temp);
						}
						GlobalGameOver (winners);
					} 
					else if(LycansDead>2) {
						Debug.LogWarning ("Number of players is 4, and we have more that 2 dead lycans!!");
					}
				}

			} 

			else {
				//draw!! --impossible (almost)!!
				//TODO: DRAW METHOD AND FINISH GAME

				Debug.Log ("Lycan-Vamp DRAW, impliment draw method");
			}


		}
		if ((NumberOfPlayers + deadPlayers.Count) == 5) {
			if(deadPlayers.Count>0){
				if (deadPlayers.Count != (NumberOfPlayers + deadPlayers.Count)) {
					//not draw, check if all opossites are dead


					if(VampsDead > 0 ){
						if (VampsDead == 2) {
							List <UserPlayer> lycans = new List<UserPlayer>();
							lycans = getPlayersWithRace ("Lycan");
							foreach (UserPlayer lyc in lycans) {
								Winner temp = new Winner (lyc.PlName, lyc.PlayerCharacter, lyc.PlayerColor, lyc.PlayerRace);
								if (lyc.killedBy != "") {
									temp.isDead = true;
								}
								winners.Add (temp);
							}

							if (HumansAlive == 1) {
								foreach (UserPlayer pl in players) {


									if (pl.PlayerCharacter == "Anta") {


										UserPlayer anta = getPlayerWithCharName("Anta");
										Winner antaWinner = new Winner (anta.PlName, anta.PlayerCharacter, anta.PlayerColor, anta.PlayerRace);

										winners.Add (antaWinner);

										//"Lycans and Anta (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Raphael") {

										UserPlayer raphael = getPlayerWithCharName("Raphael");
										Winner raphaelWinner = new Winner (raphael.PlName, raphael.PlayerCharacter, raphael.PlayerColor, raphael.PlayerRace);

										winners.Add (raphaelWinner);

										//"Lycans and Raphael (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Klaudios" && pl.destinationText == "10"){ 

										UserPlayer claudios = getPlayerWithCharName("Klaudios");
										Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

										winners.Add (claudiosWinner);

										//"Lycans and Claudios (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Xloey") {
										UserPlayer chloe = getPlayerWithCharName("Xloey");
										Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

										winners.Add (chloeWinner);

										//"Lycans and Chloe (Human) WIN"
										GlobalGameOver (winners);
									}



								}
							}
							else if (HumansAlive == 0) {
								//humans are all dead so only Lycans remain

								//"Lycans WIN"

								GlobalGameOver (winners);
							}
							else if (HumansAlive > 1) {
								Debug.Log ("HumansAlive should be 1 or 0!! please debug");
							}

						} 
						else if(VampsDead>2) {
							Debug.LogWarning ("Number of players is 5, and we have more that 2 dead vampires!!");
						}
					}


					if (LycansDead > 0) {
						if (LycansDead == 2) {
							if (HumansAlive == 1) {
								foreach (UserPlayer pl in players) {




									if (pl.PlayerCharacter == "Anta") {
										UserPlayer anta = getPlayerWithCharName("Anta");
										Winner antaWinner = new Winner (anta.PlName, anta.PlayerCharacter, anta.PlayerColor, anta.PlayerRace);

										winners.Add (antaWinner);

										//"Vamp and Anta (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Klaudios" && pl.destinationText == "10"){
										UserPlayer claudios = getPlayerWithCharName("Klaudios");
										Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

										winners.Add (claudiosWinner);

										//"Vamp and Claudios (Human) WIN"
										GlobalGameOver (winners);
									}
									if (pl.PlayerCharacter == "Xloey") {
										UserPlayer chloe = getPlayerWithCharName("Xloey");
										Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

										winners.Add (chloeWinner);

										//"Vamp and Chloe (Human) WIN"
										GlobalGameOver (winners);
									}



								}
							}
							else if (HumansAlive == 0) {
								//humans are all dead so only Vamps remain

								//"Vamps WIN"
								List <UserPlayer> vampz = new List<UserPlayer>();
								vampz = getPlayersWithRace ("Vamp");
								foreach (UserPlayer vmp in vampz) {
									Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
									if (vmp.killedBy != "") {
										temp.isDead = true;
									}
									winners.Add (temp);
								}
								GlobalGameOver (winners);
							}
							else if (HumansAlive > 1) {
								Debug.Log ("HumansAlive should be 1 or 0!! please debug");
							}
						} 
						else if(LycansDead>1) {
							Debug.LogWarning ("Number of players is 3, and we have more that 1 dead lycans!!");
						}
					}
				} 

				else {
					//draw!! --impossible (almost)!!
					//TODO: DRAW METHOD AND FINISH GAME

					Debug.Log ("Lycan-Vamp DRAW, impliment draw method");
				}


			}

		}

		if (winners.Count > 0 && !GameOverPanel.activeSelf) {
			//there are winners but for some reason there is no game over
			GlobalGameOver (winners);

		}
	}

	public UserPlayer getPlayerWithCharName (string charName){
		UserPlayer result = new UserPlayer ();

		foreach (UserPlayer pl in players) {
			if (pl.PlayerCharacter == charName) {
				result = pl;
				return result;
			}
		}

		foreach (UserPlayer dpl in deadPlayers) {
			if (dpl.PlayerCharacter == charName) {
				result = dpl;
				return result;
			}
		}

		return null;
	}

	public List<UserPlayer> getPlayersWithRace (string race){
		List <UserPlayer> result = new List<UserPlayer>();

		foreach (UserPlayer pl in players) {
			if (pl.PlayerRace == race) {
				result.Add(pl);
			}
		}

		foreach (UserPlayer dpl in deadPlayers) {
			if (dpl.PlayerRace == race) {
				result.Add(dpl);

			}
		}

		return result;

	}


	public void GlobalGameOver(List<Winner> winners){
		Debug.Log ("GLOBAL GAME OVER RUN");
		GameOver = true;

		DisableAllButtons ();

		GameOverPanel.SetActive (true);

		if (winners.Count == 1) {
			GameOverPanel.transform.Find("WinText").GetComponent<Text>().text = "Winner:";
		} 

		foreach (Winner w in winners) {
			GameObject WinnerEntry = Instantiate(EndGamePlayerEntry) as GameObject;
			WinnerEntry.transform.SetParent (GameOverPanel.transform.Find ("Contain").transform);

			string[] splitArray = w.PlName.Split(new char[]{'_'});

			string PlayerName = splitArray[0];
			string PlayerID = splitArray[1];

			WinnerEntry.transform.Find ("PlayerNamePanel").Find ("CharacterName").GetComponent<Text> ().text = w.PlayerCharacter;
			WinnerEntry.transform.Find ("Text").GetComponent<Text> ().text = PlayerName;

			if (w.PlayerRace == "Human") {
				WinnerEntry.transform.Find ("PlayerNamePanel").GetComponent<Image> ().color = Color.yellow;
			} 
			else if (w.PlayerRace == "Lycan") {
				WinnerEntry.transform.Find ("PlayerNamePanel").GetComponent<Image> ().color = Color.blue;
			} 
			else if (w.PlayerRace == "Vamp") {
				WinnerEntry.transform.Find  ("PlayerNamePanel").GetComponent<Image> ().color = Color.red;
			}
				
			Color c = WinnerEntry.transform.Find  ("PlayerNamePanel").GetComponent<Image> ().color;
			c.a = 100f;
			WinnerEntry.transform.Find  ("PlayerNamePanel").GetComponent<Image> ().color = c;

			FB.API (Util.GetPictureURL(PlayerID, 100, 100), HttpMethod.GET, delegate(IGraphResult pictureResult) {
				if(pictureResult.Error != null) { // in case there was an error
					Debug.Log (pictureResult.Error);
				}
				else { //we got the image
					Image PlayerAvatar = WinnerEntry.transform.Find  ("Image").GetComponent<Image>(); 
					PlayerAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect (0, 0, 100, 100), new Vector2 (0, 0));
				}
			});	

		}









	}

	public IEnumerator HandlePlayerNameAndColor(float duration, string n)		
	{
		
		yield return new WaitForSeconds(duration);
		
		//code
		GetComponent<PhotonView>().RPC("HandlePlayerNameAndColor_RPC", PhotonTargets.AllBuffered, n);		


		yield break;
		
		
	}
	
	[PunRPC]
	void HandlePlayerNameAndColor_RPC(string name){

		Debug.Log ("start of method run for: "+name );

		GameObject[] playerGOs = GameObject.FindGameObjectsWithTag ("PioniPlayer");

		for (int i = 0; i<playerGOs.Length; i++) {

			string goName = playerGOs[i].GetComponent<UserPlayer>().PlName;
			Debug.Log ("go Name: "+goName);
			if(name==goName){
				Debug.Log ("in frist if loop");
				foreach(PhotonPlayer pl in PhotonNetwork.playerList){
					ExitGames.Client.Photon.Hashtable h = pl.customProperties;
					Debug.Log(h["PlayerName"].ToString());
					if(name == h["PlayerName"].ToString()){
						playerGOs[i].name = name;
						playerGOs[i].GetComponent<UserPlayer>().PlayerColor = h["PlayerColor"].ToString();
						playerGOs[i].GetComponent<UserPlayer>().PlName = h["PlayerName"].ToString();

						//send character stuff
						string[] splitArray =  h["CharacterSpriteName"].ToString().Split(new char[]{'_'}); 
						playerGOs[i].GetComponent<UserPlayer>().PlayerRace = splitArray[0]; 
						playerGOs[i].GetComponent<UserPlayer>().PlayerCharacter = splitArray[1]; 
						playerGOs[i].GetComponent<UserPlayer>().DiesAt = int.Parse(splitArray[2]);


						playerGOs[i].GetComponent<UserPlayer>().CorrectColor();
						players.Add(playerGOs[i].GetComponent<UserPlayer>());


					}
				}

			}
		}

		GameObject[] hpGOs = GameObject.FindGameObjectsWithTag ("HPPlayer");

		for (int i = 0; i<hpGOs.Length; i++) {
			
			string hpName = hpGOs[i].GetComponent<HPPlayer>().HPName;
			 
			if(name==hpName){
				 
				foreach(PhotonPlayer pl in PhotonNetwork.playerList){
					ExitGames.Client.Photon.Hashtable h = pl.customProperties;

					if(name == h["PlayerName"].ToString()){
						hpGOs[i].name = name + '#' + "HP";
						hpGOs[i].GetComponent<HPPlayer>().HPColor = h["PlayerColor"].ToString();
						hpGOs[i].GetComponent<HPPlayer>().HPName = h["PlayerName"].ToString();
						hpGOs[i].GetComponent<HPPlayer>().CorrectColor();

						HPplayers.Add(hpGOs[i].GetComponent<HPPlayer>());

					}
				}
				
			}
		}




		foreach (Transform t in PlayerScrollContain.transform) {
			string plName = t.transform.Find("PlayerName").GetComponent<Text>().text;
			string plID = t.transform.Find("PlayerID").GetComponent<Text>().text;


			if(name == plName + '_' + plID){
			
				foreach(PhotonPlayer pl in PhotonNetwork.playerList){


					ExitGames.Client.Photon.Hashtable h = pl.customProperties;
					h["PlayerColor"].ToString();

					if(name == h["PlayerName"].ToString()){
						t.transform.Find("ColorCircleImage").GetComponent<Image>().color = GetColorFromString(h["PlayerColor"].ToString());
					}


				} 
			}			
		}


//		UserPlayer playerUP;
//
//		PhotonPlayer[] pls = PhotonNetwork.playerList;
//		GameObject[] playerGOs2 = GameObject.FindGameObjectsWithTag ("PioniPlayer");
//
//		for (int i=0; i<playerGOs.Length; i++) {
//
//			ExitGames.Client.Photon.Hashtable ht = pls[i].customProperties;
//			playerUP = playerGOs[i].GetComponent<UserPlayer>();
//			playerUP.gameObject.name = ht["PlayerName"].ToString();
//			playerUP.PlayerColor = ht["PlayerColor"].ToString();
//			playerUP.PlName = ht["PlayerName"].ToString();
//			if(playerUP.PlayerCharacter == ""){
//				playerUP.PlayerCharacter = ht["CharacterSpriteName"].ToString ();
//			}
//			playerUP.CorrectColor();
//			players.Add (playerUP);
//
//
//		}

	}

	void HandleCharacters(){
		Debug.Log ("Num of players: " + NumberOfPlayers);

		if (NumberOfPlayers == 2) {

			//select a lycan
			int indexL = UnityEngine.Random.Range(0, LycanSprites.Count);
			Debug.Log("indexl: " + indexL);
			string LycanSelected = LycanSprites[indexL].name;
			LycanSprites.RemoveAt(indexL);
	
			//select a vamp
			int indexV = UnityEngine.Random.Range(0, VampSprites.Count);
			string VampSelected = VampSprites[indexV].name;
			VampSprites.RemoveAt(indexV);

			
			List<string> CharRoster = new List<string>();


			//testing, delete that on a build:

//			foreach (PhotonPlayer pl in PhotonNetwork.playerList){
//				if (pl.name == PhotonNetwork.masterClient.name) {
//					//give ME this champ:
//					ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
//					customProperties.Add ("CharacterSpriteName", "Lycan_Elena_10");
//					pl.SetCustomProperties (customProperties);
//
//					foreach (Player pl2 in players) {
//						if (pl.name == pl2.PlName) {
//							pl2.PlayerCharacter = "Lycan_Elena_10";
//						}
//					}
//				
//				} else {
//					ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
//					customProperties.Add ("CharacterSpriteName", "Vamp_Malburca_14");
//					pl.SetCustomProperties (customProperties);
//
//					foreach (Player pl2 in players) {
//						if (pl.name == pl2.PlName) {
//							pl2.PlayerCharacter = "Vamp_Malburca_14";
//						}
//					}
//					
//				}
//
//			}
//
						

			//TODO: Disabled for testing reasons:

			CharRoster.Add (LycanSelected);
			CharRoster.Add (VampSelected);


			foreach (PhotonPlayer pl in PhotonNetwork.playerList){
				int indexR = UnityEngine.Random.Range(0, CharRoster.Count);
				
				ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
				customProperties.Add  ("CharacterSpriteName", CharRoster[indexR]);
				pl.SetCustomProperties (customProperties);
				
				
				foreach(Player pl2 in players){
					if(pl.name == pl2.PlName){
						pl2.PlayerCharacter = CharRoster[indexR];
					}
				}
				CharRoster.RemoveAt (indexR);
			}

		}

		if (NumberOfPlayers == 3) {
			
			//select a human
			
			int indexH = UnityEngine.Random.Range(0, HumanSprites.Count);
			string HumanSelected = HumanSprites[indexH].name;
			HumanSprites.RemoveAt(indexH);
			
			//select a lycan
			int indexL = UnityEngine.Random.Range(0, LycanSprites.Count);
			string LycanSelected = LycanSprites[indexL].name;
			LycanSprites.RemoveAt(indexL);
			
			//select a vamp
			int indexV = UnityEngine.Random.Range(0, VampSprites.Count);
			string VampSelected = VampSprites[indexV].name;
			VampSprites.RemoveAt(indexV);
			
			
			List<string> CharRoster = new List<string>();
			CharRoster.Add (HumanSelected);
			CharRoster.Add (LycanSelected);
			CharRoster.Add (VampSelected);


			//this is just testing, DELETE after:
//			foreach (PhotonPlayer pl in PhotonNetwork.playerList){
//				if (pl.name == PhotonNetwork.masterClient.name) {
//					//give ME this champ:
//					ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
//					customProperties.Add ("CharacterSpriteName", "Vamp_Mentour_14");
//					pl.SetCustomProperties (customProperties);
//
//					foreach (Player pl2 in players) {
//						if (pl.name == pl2.PlName) {
//							pl2.PlayerCharacter = "Vamp_Mentour_14";
//						}
//					}
//
//				} else {
//					ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
//					customProperties.Add ("CharacterSpriteName", "Human_Klerry_10");
//					pl.SetCustomProperties (customProperties);
//
//					foreach (Player pl2 in players) {
//						if (pl.name == pl2.PlName) {
//							pl2.PlayerCharacter = "Human_Klerry_10";
//						}
//					}
//
//				}
//
//			}
			
			foreach (PhotonPlayer pl in PhotonNetwork.playerList){
				int indexR = UnityEngine.Random.Range(0, CharRoster.Count);
				
				ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
				customProperties.Add  ("CharacterSpriteName", CharRoster[indexR]);
				pl.SetCustomProperties (customProperties);
				
				
				foreach(Player pl2 in players){
					if(pl.name == pl2.PlName){
						pl2.PlayerCharacter = CharRoster[indexR];
					}
				}
				CharRoster.RemoveAt (indexR);
			}
			
		}
		if (NumberOfPlayers == 4) {
			

			
			//select 2 lycans
			int indexL = UnityEngine.Random.Range(0, LycanSprites.Count);
			string LycanSelected = LycanSprites[indexL].name;
			LycanSprites.RemoveAt(indexL);
			
			indexL = UnityEngine.Random.Range(0, LycanSprites.Count);
			string LycanSelected2 = LycanSprites[indexL].name;
			LycanSprites.RemoveAt(indexL);
			
			//select 2 vamps
			int indexV = UnityEngine.Random.Range(0, VampSprites.Count);
			string VampSelected = VampSprites[indexV].name;
			VampSprites.RemoveAt(indexV);
			
			indexV = UnityEngine.Random.Range(0, VampSprites.Count);
			string VampSelected2 = VampSprites[indexV].name;
			LycanSprites.RemoveAt(indexV);
			
			List<string> CharRoster = new List<string>();

			CharRoster.Add (LycanSelected);
			CharRoster.Add (LycanSelected2);
			CharRoster.Add (VampSelected);
			CharRoster.Add (VampSelected2);
			
			foreach (PhotonPlayer pl in PhotonNetwork.playerList){
				int indexR = UnityEngine.Random.Range(0, CharRoster.Count);
				
				ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
				customProperties.Add  ("CharacterSpriteName", CharRoster[indexR]);
				pl.SetCustomProperties (customProperties);
				
				
				foreach(Player pl2 in players){
					if(pl.name == pl2.PlName){
						pl2.PlayerCharacter = CharRoster[indexR];
					}
				}
				CharRoster.RemoveAt (indexR);
			}

			
		}

		if (NumberOfPlayers == 5) {

			//select a human
		
			int indexH = UnityEngine.Random.Range(0, HumanSprites.Count);
			string HumanSelected = HumanSprites[indexH].name;
			HumanSprites.RemoveAt(indexH);

			//select 2 lycans
			int indexL = UnityEngine.Random.Range(0, LycanSprites.Count);
			string LycanSelected = LycanSprites[indexL].name;
			LycanSprites.RemoveAt(indexL);

			indexL = UnityEngine.Random.Range(0, LycanSprites.Count);
			string LycanSelected2 = LycanSprites[indexL].name;
			LycanSprites.RemoveAt(indexL);

			//select 2 vamps
			int indexV = UnityEngine.Random.Range(0, VampSprites.Count);
			string VampSelected = VampSprites[indexV].name;
			VampSprites.RemoveAt(indexV);
			
			indexV = UnityEngine.Random.Range(0, VampSprites.Count);
			string VampSelected2 = VampSprites[indexV].name;
			VampSprites.RemoveAt(indexV);

			List<string> CharRoster = new List<string>();
			CharRoster.Add (HumanSelected);
			CharRoster.Add (LycanSelected);
			CharRoster.Add (LycanSelected2);
			CharRoster.Add (VampSelected);
			CharRoster.Add (VampSelected2);

			foreach (PhotonPlayer pl in PhotonNetwork.playerList){
				int indexR = UnityEngine.Random.Range(0, CharRoster.Count);

				ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
				customProperties.Add  ("CharacterSpriteName", CharRoster[indexR]);
				pl.SetCustomProperties (customProperties);


				foreach(Player pl2 in players){
					if(pl.name == pl2.PlName){
						pl2.PlayerCharacter = CharRoster[indexR];
					}
				}
				CharRoster.RemoveAt (indexR);
			}

		}
	}


	public IEnumerator GetMyPlayer(float duration)		
	{
		
		yield return new WaitForSeconds(duration);
		
		//code
		ExitGames.Client.Photon.Hashtable plInfo = PhotonNetwork.player.customProperties;

		string myChar = plInfo ["CharacterSpriteName"].ToString ();
		foreach (Sprite spr in CharSprites) {
			if(spr.name == 	myChar){
				
				MyCharCardGO.GetComponent<CardSpriteHandling>().BackSprite = spr;  //FIXME: change names here, this is the frontsprite now not the back
				//MyCharCardGO.GetComponent<CardSpriteHandling>().Flip();
				MyCharCardGO.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
			}	
		}


		//get the opponents chars

		foreach (PhotonPlayer opl in PhotonNetwork.playerList) {
			ExitGames.Client.Photon.Hashtable otherplInfo = opl.customProperties;

			string thisplayersChar = otherplInfo ["CharacterSpriteName"].ToString ();
			foreach (Sprite spr in CharSprites) {
				if (spr.name == thisplayersChar) {

					//find playerentry in playerlist
					foreach (Transform t in PlayerScrollContain.transform) {
						string plEntryName = t.transform.Find("PlayerName").GetComponent<Text>().text;
						string plEntryID = t.transform.Find("PlayerID").GetComponent<Text>().text;

						if(opl.name == plEntryName + '_' + plEntryID){


							t.transform.Find("PlayerCharCard").GetComponent<CardSpriteHandling>().BackSprite = spr;  //FIXME: change names here, this is the frontsprite now not the back
							//t.transform.FindChild("PlayerCharCard").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

						}
					}

				}

			}	

		}



 
		
		yield break;
		
		
	}

	public IEnumerator RemoveLoadingScreenIn(float duration)		
	{
		
		yield return new WaitForSeconds(duration);
		
		//code

		LoadingPanel.SetActive (false);
		
		yield break;
		
		
	}

	public IEnumerator PlayerGameOver(Player plr, string damagerNameAndID)		
	{

		yield return new WaitForSeconds(2f);

		//code
		plr.killedBy = damagerNameAndID;

		deadPlayers.Add (plr);
		Player deadplayer = plr;


		foreach (Player p in players) {

			//a player died so increase the text for the rounds to mentour's ability
			if (p.PlName == PhotonNetwork.player.name) {
				if (p.PlayerCharacter == "Mentour") {
					ActivateAbilityButton.GetComponentInChildren<Text> ().text = "Activate, Extra Rounds: "+deadPlayers.Count.ToString();
					if (deadPlayers.Count == 1 && p.PlName == players[currentPlayerIndex].PlName && !p.AbilityDisabled && p.AbilityActivated) {
						// the button is deactivated so activate it if this is mentour'r turn
						ActivateAbilityButton.interactable = true;
					}
				}
			}

			//klaudios ability:
			if (plr.killedBy == p.PlName) {
				
				if (p.PlayerCharacter == "Klaudios" && plr.DiesAt <= 12) {
					p.isRevealed = true;
					p.AbilityActivated = true;
					p.AbilityDisabled = true;

					if (PhotonNetwork.player.name == p.name) {
						RevealCharacterButton.interactable = false;
					}
					else {
						string[] splitArray = p.PlName.Split(new char[]{'_'});
						string playername = splitArray [0];
						string playerID = splitArray [1];

						foreach(Transform t in PlayerScrollContain.transform){

							if (t.Find ("PlayerName").GetComponent<Text> ().text == playername && t.Find ("PlayerID").GetComponent<Text> ().text == playerID) {
								t.transform.Find("PlayerCharCard").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
								//TODO itween instead of regular rotation change


							}
							else {
								Debug.Log ("ERROR: could not find the player in scroll contain, playername is: " + t.Find ("PlayerName").GetComponent<Text> ().text + " player ID is: " + t.Find ("PlayerID").GetComponent<Text> ().text);
							}
						}
					}
				} 
			}

			//raphaelability
			if(p.PlayerCharacter == "Raphael"){

				p.isRevealed = true;
				p.AbilityActivated = true;
				p.AbilityDisabled = true;

				if (PhotonNetwork.player.name == p.name) {
					RevealCharacterButton.interactable = false;
				}
				else {
					string[] splitArray = p.PlName.Split(new char[]{'_'});
					string playername = splitArray [0];
					string playerID = splitArray [1];

					foreach(Transform t in PlayerScrollContain.transform){

						if (t.Find ("PlayerName").GetComponent<Text> ().text == playername && t.Find ("PlayerID").GetComponent<Text> ().text == playerID) {
							t.transform.Find("PlayerCharCard").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
							//TODO itween instead of regular rotation change


						}
						else {
							Debug.Log ("ERROR: could not find the player in scroll contain");
						}
					}
				}
			}
		}

		Debug.Log ("trying to run player game over for player: " + plr.PlName);

		foreach (Transform t in PlayerScrollContain.transform) {
			string toFind = t.transform.Find ("PlayerName").GetComponent<Text> ().text + "_" + t.transform.Find ("PlayerID").GetComponent<Text> ().text;
			Debug.Log ("to find: " + toFind + " deadplayer: " + plr.name);
			if(toFind == plr.name){

				t.transform.Find("DmgPointsText").GetComponent<Text> ().enabled = false;
				t.transform.Find("dmgintrotxt").GetComponent<Text> ().enabled = false;
				t.transform.Find("PlayerDeadText").GetComponent<Text> ().enabled = true;
			}

		}

		GameObject [] pionia = GameObject.FindGameObjectsWithTag ("PioniPlayer");
		foreach (GameObject p in pionia) {
			if (p.name == plr.PlName) {

				p.name = "DEAD " + p.name;

				Destroy (p.GetComponent<MeshRenderer>());
				Destroy (p.GetComponent<MeshFilter>());
				Destroy (p.GetComponent<CapsuleCollider>());
				p.transform.position = new Vector3 (0, -1, 0);
			}
		}


		foreach (HPPlayer hpp in HPplayers) {
			Debug.Log("hpname : " + hpp.HPName + ", hpcolor: " + hpp.HPColor + " // playername : " + plr.PlName + ", plcolor: " + plr.PlayerColor);
			if (hpp.HPName == plr.PlName && hpp.HPColor == plr.PlayerColor) {
				
				HPplayers.Remove (hpp);
				Destroy (hpp.transform.gameObject);
				break;
			}
		}








		bool diedinhisturn = false;
		if (players [currentPlayerIndex].PlName == deadplayer.PlName) {
			diedinhisturn = true;
		}

		players.Remove (plr);


		NumberOfPlayers--;

		CheckIfGlobalGameOver ();

		if (PhotonNetwork.player.name == deadplayer.PlName && diedinhisturn) {
			EndTurnClicked ();
		}

		//EndTurnClicked ();

		yield break;


	}
		




	void generateCards(){

		//GENERATING GREEN CARDS
		GameObject firstgreencard;
		firstgreencard = Instantiate(greenPrefab) as GameObject;
		firstgreencard.transform.SetParent (GameObject.Find ("GreenCardsHolder").transform, false);

		firstgreencard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (firstgreencard.GetComponent<RectTransform>().anchoredPosition.x, firstgreencard.GetComponent<RectTransform>().anchoredPosition.y, 0);


		firstgreencard.GetComponent<Button>().onClick.AddListener (() => firstgreencard.GetComponent<GreenCardScript>().CardClicked());
		firstgreencard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		firstgreencard.GetComponent<GreenCardScript> ().GreenCardID = GreenSprites [0].name;
		firstgreencard.GetComponent<CardSpriteHandling> ().FrontSprite = GreenSprites [0];
		firstgreencard.GetComponent<CardSpriteHandling> ().Flip();
		GreenCards.Add(firstgreencard);

		GameObject GCard;
		for(int i=1; i<GreenSprites.Count; i++){
			float temp = GreenCards[i-1].transform.position.z +0.045f;

			GCard = Instantiate(greenPrefab) as GameObject;
			GCard.transform.SetParent (GameObject.Find ("GreenCardsHolder").transform, false);

			GCard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (GCard.GetComponent<RectTransform>().anchoredPosition.x, GCard.GetComponent<RectTransform>().anchoredPosition.y, temp);


			//GCard.GetComponent<Button>().onClick.AddListener(() => GCard.GetComponent<GreenCardScript>().CardClicked());
			GCard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

			GCard.GetComponent<GreenCardScript> ().GreenCardID = GreenSprites [i].name;
			GCard.GetComponent<CardSpriteHandling> ().FrontSprite = GreenSprites [i];
			GCard.GetComponent<CardSpriteHandling> ().Flip();
			GreenCards.Add(GCard);
		}

		//GENERATING RED CARDS
		GameObject firstredcard;
		firstredcard = Instantiate(redPrefab) as GameObject;
		firstredcard.transform.SetParent (GameObject.Find ("RedCardsHolder").transform, false);
		
		firstredcard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (firstredcard.GetComponent<RectTransform>().anchoredPosition.x, firstredcard.GetComponent<RectTransform>().anchoredPosition.y, 0);
		
		
		firstredcard.GetComponent<Button>().onClick.AddListener (() => firstredcard.GetComponent<RedCardScript>().CardClicked());
		firstredcard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		string[] fredSplitArray =  RedSprites [0].name.Split(new char[]{'#'}); 
		firstredcard.GetComponent<RedCardScript> ().RedCardID = fredSplitArray[0];
		firstredcard.GetComponent<RedCardScript> ().type = fredSplitArray[1];

		firstredcard.GetComponent<CardSpriteHandling> ().FrontSprite = RedSprites [0];
		firstredcard.GetComponent<CardSpriteHandling> ().Flip();
		RedCards.Add(firstredcard);
		
		GameObject RCard;
		for(int i=1; i<RedSprites.Count; i++){
			float temp = RedCards[i-1].transform.position.z +0.045f;
			
			RCard = Instantiate(redPrefab) as GameObject;
			RCard.transform.SetParent (GameObject.Find ("RedCardsHolder").transform, false);
			
			RCard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (RCard.GetComponent<RectTransform>().anchoredPosition.x, RCard.GetComponent<RectTransform>().anchoredPosition.y, temp);

			
			//RCard.GetComponent<Button>().onClick.AddListener(() => RCard.GetComponent<RedCardScript>().CardClicked());
			RCard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

			string[] redSplitArray =  RedSprites [i].name.Split(new char[]{'#'}); 
			RCard.GetComponent<RedCardScript> ().RedCardID = redSplitArray [0];
			RCard.GetComponent<RedCardScript> ().type = redSplitArray [1];

			RCard.GetComponent<CardSpriteHandling> ().FrontSprite = RedSprites [i];
			RCard.GetComponent<CardSpriteHandling> ().Flip();
			RedCards.Add(RCard);
		}


		//GENERATING BLUE CARDS
		GameObject firstbluecard;
		firstbluecard = Instantiate(bluePrefab) as GameObject;
		firstbluecard.transform.SetParent (GameObject.Find ("BlueCardsHolder").transform, false);
		
		firstbluecard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (firstbluecard.GetComponent<RectTransform>().anchoredPosition.x, firstbluecard.GetComponent<RectTransform>().anchoredPosition.y, 0);
		
		
		firstbluecard.GetComponent<Button>().onClick.AddListener (() => firstbluecard.GetComponent<BlueCardScript>().CardClicked());
		firstbluecard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

		string[] fblueSplitArray =  BlueSprites [0].name.Split(new char[]{'#'}); 
		firstbluecard.GetComponent<BlueCardScript> ().BlueCardID = fblueSplitArray[0];
		firstbluecard.GetComponent<BlueCardScript> ().type = fblueSplitArray[1];

		firstbluecard.GetComponent<CardSpriteHandling> ().FrontSprite = BlueSprites [0];
		firstbluecard.GetComponent<CardSpriteHandling> ().Flip();
		BlueCards.Add(firstbluecard);
		
		GameObject BCard;
		for(int i=1; i<BlueSprites.Count; i++){
			float temp = BlueCards[i-1].transform.position.z +0.045f;
			
			BCard = Instantiate(bluePrefab) as GameObject;
			BCard.transform.SetParent (GameObject.Find ("BlueCardsHolder").transform, false);
			
			BCard.GetComponent<RectTransform>().anchoredPosition3D = new Vector3 (BCard.GetComponent<RectTransform>().anchoredPosition.x, BCard.GetComponent<RectTransform>().anchoredPosition.y, temp);
			
			
			//BCard.GetComponent<Button>().onClick.AddListener(() => BCard.GetComponent<BlueCardScript>().CardClicked());
			BCard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));


			string[] blueSplitArray =  BlueSprites [i].name.Split(new char[]{'#'}); 
			BCard.GetComponent<BlueCardScript> ().BlueCardID = blueSplitArray[0];
			BCard.GetComponent<BlueCardScript> ().type = blueSplitArray[1];

			BCard.GetComponent<CardSpriteHandling> ().FrontSprite = BlueSprites [i];
			BCard.GetComponent<CardSpriteHandling> ().Flip();
			BlueCards.Add(BCard);


		}

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


	[PunRPC]
	void UnsetPiksidaFlag_RPC(){
		StartCoroutine (UnsetPiksida (2f));
	}

	public IEnumerator UnsetPiksida(float duration)		
	{

		yield return new WaitForSeconds(duration);



		if(players [currentPlayerIndex].PlName == PhotonNetwork.player.name){

			EndTurnButtonGO.GetComponent<Button> ().interactable = true;

			if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_7")) {
				//this means he has valistra
				if(GetPlayersInOppositeAreas(players[currentPlayerIndex].destinationText)>0 && (players[currentPlayerIndex].destinationText != "none") && players [currentPlayerIndex].PlName == PhotonNetwork.player.name){ 
					if (!AttackStarted) {
						AttackButtonGO.GetComponent<Button> ().interactable = true;

					}
				}

			}
			else {
				//he does not have valistra

				if(GetPlayersInArea(players[currentPlayerIndex].destinationText)>1 && (players[currentPlayerIndex].destinationText != "none") && players [currentPlayerIndex].PlName == PhotonNetwork.player.name){ 
					if (!AttackStarted) {
						AttackButtonGO.GetComponent<Button> ().interactable = true;
					}
				}
			}


		}



		moveFromPiksida = false;
		waitfordicedelete = true;  //for some reason it needs that #chaos


		yield break;


	}


	[PunRPC]
	void DestroyDice_RPC(){
		StartCoroutine (DestroyDice (2f));
	}

	public IEnumerator DestroyDice(float duration)		
	{


		yield return new WaitForSeconds(duration);

		DestroyDiceWithNoDelay ();

		yield break;


	}

	[PunRPC]
	void DestroyDiceWithNoDealy_RPC(){
		DestroyDiceWithNoDelay ();
	}

	void DestroyDiceWithNoDelay(){
		
		GameObject fs = GameObject.Find ("FourSidedDie(Clone)");
		GameObject ss = GameObject.Find ("SixSidedDie(Clone)");

		if (ss == null) {
			Destroy (fs);
			if (rollForGregoryAbility) {
				rollForGregoryAbility = false;
			}
		}
		else if (fs == null) {
			Destroy (ss);
			if (rollForEnedra) {
				rollForEnedra = false;
			}
			if (RollForTherapiaApoMakria) {
				RollForTherapiaApoMakria = false;
			}
			if (rollForGregoryAbility) {
				rollForGregoryAbility = false;
			}
		}
		else {
			Destroy (ss);
			Destroy (fs);
		}
		if (RollDiceForAttack) {
			RollDiceForAttack = false;
		}
		else {
			//we just destroyed dice for moving
			// telos tou girou

			if (players [currentPlayerIndex].PlName == PhotonNetwork.player.name) {
				if (players [currentPlayerIndex].PlayerCharacter == "Gandolf" && players [currentPlayerIndex].isRevealed && !players [currentPlayerIndex].AbilityActivated && !players [currentPlayerIndex].AbilityDisabled && !AbilityActivatedOnce) {
					ActivateAbilityButton.interactable = true;

				}
				else {
					ActivateAbilityButton.interactable = false;
				}
			}

		}



		if(players [currentPlayerIndex].PlName == PhotonNetwork.player.name){

			if (!WaitingForMalburca) {
				EndTurnButtonGO.GetComponent<Button> ().interactable = true;
			}

			if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_7")) {
				//this means he has valistra
				if(GetPlayersInOppositeAreas(players[currentPlayerIndex].destinationText)>0 && (players[currentPlayerIndex].destinationText != "none") && players [currentPlayerIndex].PlName == PhotonNetwork.player.name){ 
					if (!AttackStarted) {
						AttackButtonGO.GetComponent<Button> ().interactable = true;

					}
				}

			}
			else {
				//he does not have valistra

				if(GetPlayersInArea(players[currentPlayerIndex].destinationText)>1 && (players[currentPlayerIndex].destinationText != "none") && players [currentPlayerIndex].PlName == PhotonNetwork.player.name){ 
					if (!AttackStarted) {
						AttackButtonGO.GetComponent<Button> ().interactable = true;
					}
				}
			}


		}



		waitfordicedelete = true;

	}




	public void PlayAgainClicked(){
		PhotonNetwork.LeaveRoom ();

		SceneManager.LoadScene ("_start");
	}
}
