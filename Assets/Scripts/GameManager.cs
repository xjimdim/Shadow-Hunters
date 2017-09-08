using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
		
		// README: In order to fully understand the rules of the game, please check out the instructions here: https://dimitriskalogirou.com/shadowhunters/Instructions.html

		// this is the instance of this particular script that can be used by other scripts to easily call methods of this specificGameManager
		public static GameManager instance;



		public Text serverStatusTextInGame;
		public int numberOfPlayers;

		// Prefabs
		public GameObject playerPrefab;
		public GameObject SSD;
		public GameObject FSD;
		public GameObject equipCardPrefab;

		// Button References
		public GameObject attackButtonGO;
		public Button StoneCircleButton;
		public Button revealCharacterButton;
		public Button activateAbilityButton;
		public Button rollToAttackButton;

		// Spots on the board for the Life path and the regular pawns
		public GameObject[] HPSpots;
		public Transform[] spawnSpots;

		// Attack panel references
		public GameObject attackPanelHead;
		public GameObject attackPanel;
		public Transform attackScrollContain;
		public GameObject attackPlayerEntry;
		public GameObject attackPlayerEntryEkriksi;
		public Text CDMTSPText;

		// Dead Player panel reference
		public GameObject deadPlayerPanel;

		// Magic Forest Panel references
		public GameObject magicPanelHead;
		public GameObject magicPanel;
		public Transform magicForestScrollContain;
		public GameObject magicForestPlayerEntry;
		public Text magicForestSelectText;

		// Steal card panel references
		public GameObject stealCardHead;
		public Transform stealCardScrollContain;
		public GameObject stealCardPlayerEntry;
		public GameObject selectCardToStealPanel;
		public Transform availableEqScrollContain;
		public GameObject equipCardToStealPrefab;

		// Trap panel references
		public GameObject trapHead;
		public Transform trapScrollContain;
		public GameObject trapPlayerEntry;
		public GameObject selectCardToGivePanel;
		public Transform availableEqToGiveScrollContain;
		public GameObject equipCardToGivePrefab;

		// Bat panel references
		public GameObject batHead;
		public Transform batScrollContain;
		public GameObject batPlayerEntry;
		public Text batHealText;

		// Bat panel references
		public GameObject bloodSpiderHead;
		public Transform bloodSpiderScrollContain;
		public GameObject bloodSpiderPlayerEntry;

		// Ambush panel references
		public GameObject ambushHead;
		public Transform ambushScrollContain;
		public GameObject ambushPlayerEntry;

		// Gregory ability panel references
		public GameObject gregoryAbilityHead;
		public Transform gregoryAbilityScrollContain;
		public GameObject gregoryAbilityPlayerEntry;

		// Treatment from afar panel references
		public GameObject treatmentFromAfarHead;
		public Transform treatmentFromAfarScrollContain;
		public GameObject treatmentFromAfarPlayerEntry;

		// Blood Moon panel references
		public GameObject bloodMoonHead;
		public Transform bloodMoonScrollContain;
		public GameObject bloodMoonPlayerEntry;

		// Elena ability mini-panel reference + warning text (that applies only to elena players)
		public GameObject elenaAbilityHead;
		public GameObject warningText;

		// Malbuca panel references + flags
		public GameObject malburcaHead;
		public String malburcaPlayerToAttackBack = "none";
		public bool waitingForMalburca = false;

		// Gameover panel references
		public GameObject gameOverPanel;
		public GameObject endGamePlayerEntry;

		// Player List panel references
		public GameObject playerScrollContain;
		public GameObject playerEntryPrefab;
		string[] playerColor;

		// reference to the loading screen (start of the game)
		public GameObject loadingPanel;

		// references for the compass panel functionality
		public GameObject compassHead;
		public string compassArea = "none";

		//  used for the DisableAllButtons() and EnableAllButtons() fuctions
		public bool[] buttonState = new bool[10];


		// references to players for specific abilities - cards
		public string ambushTargetedPlayer = "none";
		public string gregoryAbilityTargetedPlayer = "none";
		public string nameOfPLayerToHealFromAfar = "none";

		//  the extra rounds text (usually for when someone draws the Time Jump card)
		public Text extraRoundsText;



		//  FLAGS
		public bool firstMoveDone = false;
		public bool bothSleeping = false;
		public bool bothCreated = false;
		public bool bothDeleted = true;
		public bool waitForDiceDelete = true;
		public bool waitForPlayerToRoll = true;
		public bool destinationTextSet = false;

		public bool gameOver = false;

		public bool cardsActivated = false;

		public bool moveFromCompass = false;


		public bool isBeginningOfRound = false;

		public bool abilityActivatedOnce = false;

		public bool rollForMalburcaAttack = false;


		public bool rollForAmbush = false;
		public bool ambushDone = false;


		public bool rollForGregoryAbility = false;
		public bool gregoryAbilityDone = false;

		public bool rollForExplosion = false;

		public bool rollForTreatmentFromAfar = false;


		// attack flags
		public bool rollDiceForAttack = false;
		public bool damagePointsCreated = false;
		public bool attackStarted = false;
		public bool attackPanelCleared = true;

		public bool greenClicked = false;
		public int activeGreenCardID = -1;
		public GameObject playerTileSelectedGO;
		public bool playerTileClicked = false;

		public bool redClicked = false;
		public int activeRedCardID = -1;

		public bool blueClicked = false;
		public int activeBlueCardID = -1;


		// Magic Forest Stuff
		public int magicForestWhatToDo = -1;
		//  0 = dmg, 1=heal
		public Button magicForestDamageButton;
		public Button magicForestHealButton;


		// FOR CARDS GENERATOR
		// GREEN CARDS
		public GameObject greenPrefab;
		public GameObject greenCardButton;
		public List <Sprite> greenSprites = new List<Sprite> ();
		List <GameObject> greenCards = new List<GameObject> ();

		// RED CARDS
		public GameObject redPrefab;
		public GameObject redCardButton;
		public List <Sprite> redSprites = new List<Sprite> ();
		List <GameObject> redCards = new List<GameObject> ();

		// BLUE CARDS
		public GameObject bluePrefab;
		public GameObject blueCardButton;
		public List <Sprite> blueSprites = new List<Sprite> ();
		List <GameObject> blueCards = new List<GameObject> ();

		// CHARACTER CARDS
		public GameObject charCardPrefab;
		public List <Sprite> charSprites = new List<Sprite> ();
		public GameObject myCharCardGO;

		// CHARACTER SPRITE LISTS
		List <Sprite> humanSprites = new List<Sprite> ();
		List <Sprite> lycanSprites = new List<Sprite> ();
		List <Sprite> vampSprites = new List<Sprite> ();

		// Lists to keep track of players and their states
		public List <Player> players = new List<Player> ();
		public List <HPPlayer> HPplayers = new List<HPPlayer> ();
		public List <Player> deadPlayers = new List<Player> ();

		//  index of the currentplayer (every player has to have the same value for this variable)
		public int currentPlayerIndex = 0;


		// variables that help moving the player pawns to the right destination
		public string myDestinationText = "none";
		public Tile place = null;
		public Tile fromPlace = null;

		//  the chat text :)
		public Text chatText;


		//  private vars
		bool playersSorted = false;
		GameObject placeObj2 = null;
		GameObject currPlayerTextGO;
		GameObject endTurnButtonGO;
		GameObject magicForestButtonGO;
		GameObject rollButtonGO;





		void Awake ()
		{
				instance = this;


				playerColor = new string[] { "gray", "blue", "yellow", "red", "black" };
				numberOfPlayers = NetworkManager.instance.playerNames.Count;

				HPSpots = new GameObject[] {
						GameObject.Find ("Player1HPSpots"),
						GameObject.Find ("Player2HPSpots"),
						GameObject.Find ("Player3HPSpots"),
						GameObject.Find ("Player4HPSpots"),
						GameObject.Find ("Player5HPSpots")
				};

				for (int i = 1; i < charSprites.Count; i++) {
						if (i >= 1 && i <= 5) {
								humanSprites.Add (charSprites [i]);
						} else if (i >= 6 && i <= 11) {
								lycanSprites.Add (charSprites [i]);

						} else if (i >= 12 && i <= 16) {
								vampSprites.Add (charSprites [i]);
						}
			
				}
				if (PhotonNetwork.isMasterClient) {
						HandleCharacters ();		
				}
				generatePlayers ();


				currPlayerTextGO = GameObject.Find ("CurrentPlayerText");


				endTurnButtonGO = GameObject.Find ("EndTurnButton");
				attackButtonGO = GameObject.Find ("AttackButton");
				magicForestButtonGO = GameObject.Find ("MagicForestButton");
				rollButtonGO = GameObject.Find ("RollButton");

				blueCardButton.GetComponent<Button> ().onClick.AddListener (() => blueCardButton.GetComponent<RGBCardHolder> ().CardClicked ());
				redCardButton.GetComponent<Button> ().onClick.AddListener (() => redCardButton.GetComponent<RGBCardHolder> ().CardClicked ());
				greenCardButton.GetComponent<Button> ().onClick.AddListener (() => greenCardButton.GetComponent<RGBCardHolder> ().CardClicked ());

				DisableBlueCards (true);
				DisableRedCards (true);
				DisableGreenCards (true);

				StartCoroutine (GetMyPlayer (3));


		}

		void FixedUpdate ()
		{
				if (players.Count == numberOfPlayers && !gameOver) {
						if (players [currentPlayerIndex].GetComponent<UserPlayer> () != null) { // if == null then probably the player is dead

								players [currentPlayerIndex].TurnUpdate ();	
						}
				
				}

				GameObject fs = GameObject.Find ("FourSidedDie(Clone)");
				GameObject ss = GameObject.Find ("SixSidedDie(Clone)");
		
		
				// checking dice status and updating flags
				if (fs != null && ss != null) {
						bothCreated = true;
						if (fs.GetComponent<Rigidbody> ().IsSleeping () && ss.GetComponent<Rigidbody> ().IsSleeping () && waitForDiceDelete) {
				
								bothSleeping = true;
				
						} else {
								bothSleeping = false;
						}
				} else {
						bothCreated = false;
				}

				if (moveFromCompass) {
						NewMakeMove ();
				}

		}
	
		// Update is called once per frame
		void Update ()
		{
				serverStatusTextInGame.text = "Status: " + PhotonNetwork.connectionStateDetailed.ToString ();
				if (players.Count == numberOfPlayers && !gameOver) {

						if (!playersSorted) {
								if (PhotonNetwork.isMasterClient) {
										GetComponent<PhotonView> ().RPC ("SortPlayerList_rpc", PhotonTargets.AllBufferedViaServer, null);
								}


								if (players [currentPlayerIndex].PlName != PhotonNetwork.player.name) {

										rollButtonGO.GetComponent<Button> ().interactable = false; 

										attackButtonGO.GetComponent<Button> ().interactable = false; 
					

										endTurnButtonGO.GetComponent<Button> ().interactable = false; 
										revealCharacterButton.interactable = false;
								}

						}
						ChangeCurrentPlayerText ();
			 
				}


		}

		[PunRPC]
		void SortPlayerList_rpc ()
		{

				List<string> tempPls = new List<string> ();

				for (int i = 0; i < players.Count; i++) {
						tempPls.Add (players [i].PlName);
				}

				tempPls.Sort ();

				for (int i = 0; i < tempPls.Count; i++) {
						for (int j = 0; j < players.Count; j++) {
								if (tempPls [i] == players [j].PlName) {

										players.Insert (i, players [j]);
										HPplayers.Insert (i, HPplayers [j]);
										if (i <= j) {
												players.RemoveAt (j + 1);
												HPplayers.RemoveAt (j + 1);
										} else {
												players.RemoveAt (j);
												HPplayers.RemoveAt (j);
										}


								}
						}
				}



				ExitGames.Client.Photon.Hashtable rht = PhotonNetwork.room.customProperties;
				string MasterName = rht ["MasterRightNow"].ToString ();

				for (int x = 0; x < players.Count; x++) {
						if (players [x].PlName == MasterName) {

								players.Insert (0, players [x]);
								players.RemoveAt (x + 1);



								HPplayers.Insert (0, HPplayers [x]);
								HPplayers.RemoveAt (x + 1);
						}				
				}


				playersSorted = true;

		}

		public void EndTurnClicked ()
		{

				// handling the delay "glitch"
				// we have to call nextturn locally and then to all the other players


				rollButtonGO.GetComponent<Button> ().interactable = false; 

				attackButtonGO.GetComponent<Button> ().interactable = false; 
				attackStarted = false;
				ClearAttackPanel (); //se periptosi pou patisame apla x kai den kaname clear

				magicForestButtonGO.GetComponent<Button> ().interactable = false;
				StoneCircleButton.interactable = false;
				endTurnButtonGO.GetComponent<Button> ().interactable = false; 
				revealCharacterButton.interactable = false;
				activateAbilityButton.interactable = false;

				DisableGreenCards (true);
				DisableRedCards (true);
				DisableBlueCards (true);

				cardsActivated = false;

				if (rollDiceForAttack) {
						rollDiceForAttack = false;
				}
				if (ambushDone) {
						ambushDone = false;
				}

				if (players [currentPlayerIndex].extraRounds > 0) {
						extraRoundsText.enabled = true;
				} else {
						extraRoundsText.enabled = false;
				}

				warningText.SetActive (false); //in case it is active

				isBeginningOfRound = false;

				GetComponent<PhotonView> ().RPC ("nextTurn_RPC", PhotonTargets.AllBufferedViaServer, null);
		}


		[PunRPC]
		public void nextTurn_RPC ()
		{

				destinationTextSet = false;
				if (players [currentPlayerIndex].extraRounds == 0) {
						if (currentPlayerIndex + 1 < players.Count) {
								currentPlayerIndex++;
						} else {
								currentPlayerIndex = 0;
						}
				} else {
						players [currentPlayerIndex].extraRounds--;

				}


				if (players [currentPlayerIndex].PlName == PhotonNetwork.player.name) {

						// only the current player will run this code:


						if (players [currentPlayerIndex].eksoplismoi.Contains ("Blues_15")) {
								string[] splitArray = players [currentPlayerIndex].PlName.Split (new char[]{ '_' });

								GetComponent<PhotonView> ().RPC ("RemoveEquipCardFromPlayer_RPC", PhotonTargets.AllBuffered, "Blues", "Blues_15", splitArray [0], splitArray [1]);

						}

						Player currPl = players [currentPlayerIndex];

						if (currPl.PlayerCharacter == "Gandolf" && currPl.AbilityActivated && !currPl.AbilityDisabled) {
								// that means we are gandolf, and we activated the ability last round, so its time to disable it
								currPl.AbilityDisabled = true;
								GetComponent<PhotonView> ().RPC ("DisableAbilityOfPlayer", PhotonTargets.AllBufferedViaServer, players [currentPlayerIndex].PlName, players [currentPlayerIndex].PlName);
						}


						rollButtonGO.GetComponent<Button> ().interactable = true; 
						endTurnButtonGO.GetComponent<Button> ().interactable = true;
						isBeginningOfRound = true;
						if (players [currentPlayerIndex].isRevealed) {
								string ch = players [currentPlayerIndex].PlayerCharacter;


								if (ch == "Gregor" || ch == "Frederic" || ch == "Flora" || ch == "Etta" || ch == "Mentour") {
										// mia fora sto paixnidi sto ksekinima
										if (!abilityActivatedOnce && !players [currentPlayerIndex].AbilityDisabled) {
												activateAbilityButton.interactable = true;
										} else {
												activateAbilityButton.interactable = false;
										}

								}
								if (ch == "Elena" || ch == "Xloey") {
										if (!players [currentPlayerIndex].AbilityDisabled) {
												activateAbilityButton.interactable = true;
										}
								}
								if (ch == "Ouriel") {
					
										if (!players [currentPlayerIndex].AbilityDisabled && GetPlayersInArea ("8") > 0) {
												activateAbilityButton.interactable = true;
										}
								}

								if (ch == "Volco" || ch == "Anta") {
					
										if (!players [currentPlayerIndex].AbilityDisabled && !players [currentPlayerIndex].AbilityActivated) {
												activateAbilityButton.interactable = true;
										} else {
												activateAbilityButton.interactable = false;
										}
								}

								// Klerry has passive ability so donothing about her

						}

						if (players [currentPlayerIndex].isRevealed == false) {

								revealCharacterButton.interactable = true;
						}

				}
		}

		public void addExtraRoundToCurrentPlayer ()
		{
				GetComponent<PhotonView> ().RPC ("addExtraRoundToCurrentPlayer_RPC", PhotonTargets.AllBufferedViaServer, null);
		}

		[PunRPC]
		public void addExtraRoundToCurrentPlayer_RPC ()
		{
				players [currentPlayerIndex].extraRounds++;
		}

		public void addExtraRoundsToCurrentPlayer (int numRounds)
		{
				GetComponent<PhotonView> ().RPC ("addExtraRoundsToCurrentPlayer_RPC", PhotonTargets.AllBufferedViaServer, numRounds);
		}

		[PunRPC]
		public void addExtraRoundsToCurrentPlayer_RPC (int rounds)
		{
				players [currentPlayerIndex].extraRounds += rounds;
		}

		public void ChangeCurrentPlayerText ()
		{

				if (players [currentPlayerIndex].PlName == PhotonNetwork.player.name && !firstMoveDone) {

						rollButtonGO.GetComponent<Button> ().interactable = true; 
						endTurnButtonGO.GetComponent<Button> ().interactable = true;
						isBeginningOfRound = true;
						if (players [currentPlayerIndex].isRevealed == false) {

								revealCharacterButton.interactable = true;
						}

						GetComponent<PhotonView> ().RPC ("SetFirstMoveDoneFlag_RPC", PhotonTargets.AllBuffered, null);
				}

				string[] splitArray = players [currentPlayerIndex].PlName.Split (new char[]{ '_' }); //Here we're passing the splitted string to array by that char
		
				string CurrentPlayerName = splitArray [0]; //Here we assign the first part to the name

				currPlayerTextGO.GetComponent<Text> ().text = CurrentPlayerName + " Plays"; 
		}

		[PunRPC]
		void SetFirstMoveDoneFlag_RPC ()
		{
				firstMoveDone = true;
				loadingPanel.SetActive (false);
		}

		public void DoApokalipsi ()
		{

				if (players [currentPlayerIndex].PlayerRace == "Lycan" || players [currentPlayerIndex].PlayerRace == "Vamp") {
						if (players [currentPlayerIndex].isRevealed == false) {
								RevealButtonClicked ();
						}
				}
		}

		public void RollButtonClicked ()
		{
				
				isBeginningOfRound = false;

				// no longer the beggining of the round TODO: depends on the player - not for all:
				if (activateAbilityButton.interactable) {
						string ch = players [currentPlayerIndex].PlayerCharacter;
						if (ch == "Gregor" || ch == "Frederic" || ch == "Flora" || ch == "Xloey" || ch == "Etta" || ch == "Ouriel") {
								activateAbilityButton.interactable = false;	
						}

				}

				rollButtonGO.GetComponent<Button> ().interactable = false; 
				endTurnButtonGO.GetComponent<Button> ().interactable = false;

				if (players [currentPlayerIndex].eksoplismoi.Contains ("Blues_8")) {
						compassHead.SetActive (true);

				} else {

						RollDice ();
				}

		}


		public void ClicekdFromCompassPanel (string areatogo)
		{

				moveFromCompass = true;
				compassArea = areatogo;
				compassHead.SetActive (false);
		}

		public void RevealButtonClicked ()
		{

				players [currentPlayerIndex].isRevealed = true;
				Player pl = players [currentPlayerIndex];
				if (isBeginningOfRound && !pl.AbilityDisabled && pl.PlayerCharacter != "Klaudios" && pl.PlayerCharacter != "Raphael" && pl.PlayerCharacter != "Klerry" && pl.PlayerCharacter != "Valkyria" && pl.PlayerCharacter != "Gandolf") {
						if (pl.PlayerCharacter == "Ouriel") {
								if (GetPlayersInArea ("8") > 0) {
					
										activateAbilityButton.interactable = true;
								} else {
										activateAbilityButton.interactable = false;
								}
						} else {
								activateAbilityButton.interactable = true;
						}

				}

				// activate passive abilities:
				if (pl.PlayerCharacter == "Klerry" || pl.PlayerCharacter == "Valkyria" || pl.PlayerCharacter == "Mentour" || pl.PlayerCharacter == "Malburca") {
						ActivateAbilityClicked ();
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Xloey") {
						activateAbilityButton.GetComponentInChildren<Text> ().text = "Heal 1pt";
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Mentour") {
						activateAbilityButton.GetComponentInChildren<Text> ().text = "Activate, Extra Rounds: " + deadPlayers.Count.ToString ();
				}

				GetComponent<PhotonView> ().RPC ("RevealCharacter_RPC", PhotonTargets.OthersBuffered, PhotonNetwork.player.name);

				revealCharacterButton.interactable = false;


		}

		[PunRPC]
		public void RevealCharacter_RPC (string playernameandID)
		{
		
				players [currentPlayerIndex].isRevealed = true;

				string[] splitArray = playernameandID.Split (new char[]{ '_' });
				string playername = splitArray [0];
				string playerID = splitArray [1];

				foreach (Transform t in playerScrollContain.transform) {
			
						if (t.Find ("PlayerName").GetComponent<Text> ().text == playername && t.Find ("PlayerID").GetComponent<Text> ().text == playerID) {
								t.transform.Find ("PlayerCharCard").transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								// TODO itween instead of regular rotation change


						} else {
								Debug.Log ("ERROR: could not find the player in scroll contain, playername is: " + t.Find ("PlayerName").GetComponent<Text> ().text + " player ID is: " + t.Find ("PlayerID").GetComponent<Text> ().text);
						}
				}

		}

		public void ActivateAbilityClicked ()
		{
				Debug.Log ("Activating ability...");

				abilityActivatedOnce = true;
				GetComponent<PhotonView> ().RPC ("AbilityActivatedForPlayer", PhotonTargets.AllBuffered, players [currentPlayerIndex].PlName);


				if (players [currentPlayerIndex].PlayerCharacter == "Gregor" || players [currentPlayerIndex].PlayerCharacter == "Frederic" || players [currentPlayerIndex].PlayerCharacter == "Etta") {
						// once again gregory panel is a general panel, not just for gregory
						ShowGregoryAbilityPanel ();
						activateAbilityButton.interactable = false;
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Flora") {
						ShowBloodMoonPanel ();
						activateAbilityButton.interactable = false;
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Elena") {
						activateAbilityButton.GetComponentInChildren<Text> ().text = "Move left or right";
						activateAbilityButton.onClick.AddListener (() => this.ElenaAbility ());
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Volco") {
						activateAbilityButton.GetComponentInChildren<Text> ().text = "Ability Activated";
						activateAbilityButton.interactable = false;
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Anta") {
						DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);
						activateAbilityButton.GetComponentInChildren<Text> ().text = "Ability Done";
						activateAbilityButton.interactable = false;
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Klerry" || players [currentPlayerIndex].PlayerCharacter == "Valkyria" || players [currentPlayerIndex].PlayerCharacter == "Malburca") {
						activateAbilityButton.GetComponentInChildren<Text> ().text = "Ability Active";
						activateAbilityButton.interactable = false;
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Xloey") {
			
						DoHealTo (1, players [currentPlayerIndex].PlName);
						activateAbilityButton.interactable = false;
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Ouriel") {
						// check for sure:
						if (GetPlayersInArea ("8") > 0) {

								ShowGregoryAbilityPanel (); //reminder: this is a general select player panel not just for gregor
						}

						activateAbilityButton.interactable = false;
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Mentour") {


						addExtraRoundsToCurrentPlayer (deadPlayers.Count);

						activateAbilityButton.interactable = false;
				}

				if (players [currentPlayerIndex].PlayerCharacter == "Gandolf") {
						// end the round:
						attackButtonGO.GetComponent<Button> ().interactable = false;
						magicForestButtonGO.GetComponent<Button> ().interactable = false;
						StoneCircleButton.interactable = false;


						activateAbilityButton.interactable = false;
				}


		}

		[PunRPC]
		public void AbilityActivatedForPlayer (string pName)
		{
				foreach (Player pl in players) {
						if (pl.PlName == pName) {
								pl.AbilityActivated = true;
						}
				}
		}

		public void ElenaAbility ()
		{
				Debug.Log ("ElenaAbility");
				if (players [currentPlayerIndex].destinationText == "none") {
						warningText.SetActive (true);
						warningText.GetComponent<Text> ().text = "On your first move you can ONLY roll the dice";
				} else {
						elenaAbilityHead.SetActive (true);
						rollButtonGO.GetComponent<Button> ().interactable = false;
						endTurnButtonGO.GetComponent<Button> ().interactable = false;
				}


				activateAbilityButton.interactable = false;
		}

		public void ElenaMoveLeft ()
		{
				moveFromCompass = true;
				compassArea = GetLeftArea (players [currentPlayerIndex].destinationText);
				Debug.Log ("Elenamove LEFT compass area: " + compassArea);
				elenaAbilityHead.SetActive (false);
		}

		public void ElenaMoveRight ()
		{
				moveFromCompass = true;
				compassArea = GetRightArea (players [currentPlayerIndex].destinationText);
				Debug.Log ("Elenamove Right compass area: " + compassArea);
				elenaAbilityHead.SetActive (false);
		}


		public string GetLeftArea (string currentArea)
		{
				string leftarea = "none";

				if (currentArea == "23") {
						leftarea = "10";
				} else if (currentArea == "45") {
						leftarea = "10";
				} else if (currentArea == "6") {
						leftarea = "45";
				} else if (currentArea == "7") {
						leftarea = "23";
				} else if (currentArea == "8") {
						leftarea = "23";
				} else if (currentArea == "9") {
						leftarea = "8";
				} else if (currentArea == "10") {
						leftarea = "23";
				}

				return leftarea;
		}

		public string GetRightArea (string currentArea)
		{
				string rightarea = "none";

				if (currentArea == "23") {
						rightarea = "8";
				} else if (currentArea == "45") {
						rightarea = "6";
				} else if (currentArea == "6") {
						rightarea = "9";
				} else if (currentArea == "7") {
						rightarea = "45";
				} else if (currentArea == "8") {
						rightarea = "9";
				} else if (currentArea == "9") {
						rightarea = "6";
				} else if (currentArea == "10") {
						rightarea = "45";
				}

				return rightarea;
		}

		public void MalburcaAttackBackClicked ()
		{
		
				GetComponent<PhotonView> ().RPC ("MalburcaResult", PhotonTargets.AllBuffered, true, PhotonNetwork.player.name, malburcaPlayerToAttackBack);
		}

		public void MalubrcaDoNothingClicked ()
		{
		
				GetComponent<PhotonView> ().RPC ("MalburcaResult", PhotonTargets.AllBuffered, false, PhotonNetwork.player.name, "DoNothing");
		}

		[PunRPC]
		public void MalburcaResult (bool attackBack, string malburcaPlayerName, string attackBackPlName)
		{
				if (attackBack) {

						foreach (Player p in players) {
								if (p.PlName == PhotonNetwork.player.name) {
										if (p.PlName == malburcaPlayerName) {
												malburcaHead.SetActive (false);
												rollDiceForAttack = true;
												rollForMalburcaAttack = true;
												RollDice ();
						
										} else if (p.PlName == attackBackPlName) {
												// the damager 
												warningText.SetActive (false);
												waitingForMalburca = false;
												EnableButtons ();


										} else {
												// everyone else
												warningText.SetActive (false);
												waitingForMalburca = false;
										}

								}
						}

				} else {

						foreach (Player p in players) {
								if (p.PlName == PhotonNetwork.player.name) {
										if (p.PlName == malburcaPlayerName) {
												// do nothing
												malburcaHead.SetActive (false);
										} else if (p.PlName == attackBackPlName) {
												// the damager 
												warningText.SetActive (false);
												EnableButtons ();

										} else {
												// everyone else
												warningText.SetActive (false);
										}

								}
						}
		
				}
		
		}

		public void MalburcaDoDamage (string points)
		{
				GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
				int integerPoints = int.Parse (points);
				if (damagePointsCreated) {
						DoDamageTo (integerPoints, malburcaPlayerToAttackBack);
				}

				rollForMalburcaAttack = false;
		}


		public void NewMakeMove ()
		{

				if (moveFromCompass && !destinationTextSet && !rollDiceForAttack) {  // this means we move from compass
						Debug.Log ("compass move");
						string cloc = players [currentPlayerIndex].currentLocation;

						if (compassArea == "none") {
								Debug.LogWarning ("compass area is set to none");
						}

						if (!cardsActivated) {
								DisableCardsForArea (compassArea);
								cardsActivated = true;
						}

						if (compassArea == "9") {
								magicForestButtonGO.GetComponent<Button> ().interactable = true; 
						}

						if (compassArea == "10") {
								if (GameManager.instance.GetEquipmentNumberOfOtherPlayers () > 0) {
										StoneCircleButton.interactable = true;
								}

						}


						GameObject placeobj = GameObject.Find (compassArea);

						if (cloc != "none") {  // if this is not the first move of the player find FROM location
								placeObj2 = GameObject.Find (cloc);
								Debug.Log ("placeobj2 name: " + placeObj2.transform.name);
						} else {
								// Debug.Log ("cloc = none");
						}
						if (placeobj != null) {

								players [currentPlayerIndex].GetComponent<UserPlayer> ().SetPlace (placeobj.GetComponent<Tile> (), compassArea);

								if (placeObj2 != null) {
										players [currentPlayerIndex].GetComponent<UserPlayer> ().SetFromPlace (placeObj2.GetComponent<Tile> ());		
								}

						}


						GetComponent<PhotonView> ().RPC ("UnsetCompassFlag_RPC", PhotonTargets.AllBufferedViaServer, null);

				}


				if (bothSleeping && waitForDiceDelete && !destinationTextSet && !rollDiceForAttack && !moveFromCompass) {  // this means we rolled the dice to move
						Debug.Log ("normal move");


						// evresi topothesias
						string dtext = GameObject.Find ("DiceText").GetComponent<Text> ().text;

			
						if (dtext == "4" || dtext == "5")
								dtext = "45";
						if (dtext == "2" || dtext == "3")
								dtext = "23";
						if (dtext == "7" || dtext == "1") {

								if (!compassHead.activeSelf) {
										compassHead.SetActive (true);
										GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);

								}
								return;

						}


						if (!cardsActivated) {
								DisableCardsForArea (dtext);
								cardsActivated = true;
						}

						if (dtext == "9") {
								magicForestButtonGO.GetComponent<Button> ().interactable = true; 
						}

						if (dtext == "10") {
								if (GameManager.instance.GetEquipmentNumberOfOtherPlayers () > 0) {
										StoneCircleButton.interactable = true;
								}

						}

			
						GameObject placeobj = GameObject.Find (dtext);
			
						string cloc = players [currentPlayerIndex].currentLocation;
			
						if (cloc != "none") {  // if this is not the first move of the player find FROM location
								placeObj2 = GameObject.Find (cloc);
								Debug.Log ("placeobj2 name: " + placeObj2.transform.name);
						} else {
								// Debug.Log ("cloc = none");
						}
						if (placeobj != null) {
				
								players [currentPlayerIndex].GetComponent<UserPlayer> ().SetPlace (placeobj.GetComponent<Tile> (), dtext);
				
								if (placeObj2 != null) {
										players [currentPlayerIndex].GetComponent<UserPlayer> ().SetFromPlace (placeObj2.GetComponent<Tile> ());		
								}
				
						}
			

						GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
			
			
			
			
				} else if ((bothSleeping || ((players [currentPlayerIndex].eksoplismoi.Contains ("Reds_4") || (players [currentPlayerIndex].PlayerCharacter == "Valkyria" && players [currentPlayerIndex].AbilityActivated))
				           && GameObject.Find ("FourSidedDie(Clone)").GetComponent<Rigidbody> ().IsSleeping ())) && waitForDiceDelete && rollDiceForAttack && !moveFromCompass) {   // << thats a really complicated contition :P 



						GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
			
			
						if (damagePointsCreated) {
								CDMTSPText.text = "Points Created, please select a player";
								CDMTSPText.color = Color.blue;

								if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_9")) {

										foreach (Transform t in attackScrollContain.transform) {
												t.GetComponent<Button> ().interactable = true;
												t.GetComponent<ClickedToAttack> ().isEkriksi = true;
										}


								} else {
					
										foreach (Transform t in attackScrollContain.transform) {
												t.GetComponent<Button> ().interactable = true;
										}

								}



						} else {
								CDMTSPText.text = "Create Damage Points to select a player";
								CDMTSPText.color = Color.red;
						}
				}

		}


		public void moveCurrentPlayer (Tile destTile, string dt)
		{ //called onUpdate


				if (!players [currentPlayerIndex].moveStarted) {

						players [currentPlayerIndex].currentLocation = players [currentPlayerIndex].destinationText; 
						if (place.transform.name != fromPlace.transform.name) {
								players [currentPlayerIndex].moveDestination = destTile.getEmptyPlace () + 0.5f * Vector3.up;
						}


						// the from destination;
						players [currentPlayerIndex].destinationText = dt;
				}

				//  for the deactivation of the cards when someone is in the destination tile 
				myDestinationText = dt;

		}



		public void DisableCardsForArea (string area)
		{
				if (area == "23") {
						DisableBlueCards (true);
						DisableRedCards (true);
						DisableGreenCards (false);
				}
				if (area == "8") {
						DisableGreenCards (true);
						DisableBlueCards (true);
						DisableRedCards (false);
				}
				if (area == "6") {
						DisableGreenCards (true);
						DisableRedCards (true);
						DisableBlueCards (false);
				}
				if (area == "10" || area == "9") {
						DisableGreenCards (true);
						DisableRedCards (true);
						DisableBlueCards (true);
				}
				if (area == "45") {
						DisableBlueCards (false);
						DisableRedCards (false);
						DisableGreenCards (false);
				}


		}

		public void DisableGreenCards (bool ac)
		{

				if (ac) {
			
						greenCardButton.GetComponent<Image> ().color = Color.gray;
						greenCardButton.GetComponent<Button> ().interactable = false;
				} else {
			
						greenCardButton.GetComponent<Image> ().color = Color.white;
						greenCardButton.GetComponent<Button> ().interactable = true;
				}

		}

		public void DisableRedCards (bool ac)
		{

				if (ac) {
			
						redCardButton.GetComponent<Image> ().color = Color.gray;
						redCardButton.GetComponent<Button> ().interactable = false;
				} else {
			
						redCardButton.GetComponent<Image> ().color = Color.white;
						redCardButton.GetComponent<Button> ().interactable = true;
				}


		}

		public void DisableBlueCards (bool ac)
		{

				if (ac) {

						blueCardButton.GetComponent<Image> ().color = Color.gray;
						blueCardButton.GetComponent<Button> ().interactable = false;
				} else {

						blueCardButton.GetComponent<Image> ().color = Color.white;
						blueCardButton.GetComponent<Button> ().interactable = true;
				}


		}

		public void SkoteiniTeletiHealClicked ()
		{
				// be sure curr pl is vamp
				Debug.Log ("Skoteini Teleti Heal Clicked");
				if (players [currentPlayerIndex].PlayerRace == "Vamp") {

						if (players [currentPlayerIndex].isRevealed) {
								// then just heal
								DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);
						} else {
								// reveal and then heal
								Debug.Log ("not revealed trying to reveal and heal");
								RevealButtonClicked ();
								DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);

						}

				}

		}

		public void TherapeiaHealClicked ()
		{
				// be sure curr pl is lycan
				Debug.Log ("Therapeia Heal Clicked");
				if (players [currentPlayerIndex].PlayerRace == "Lycan") {

						if (players [currentPlayerIndex].isRevealed) {
								// then just heal
								DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);
						} else {
								// reveal and then heal
								Debug.Log ("not revealed trying to reveal and heal");
								RevealButtonClicked ();
								DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);

						}
				}

		}

		public void EnergiaClicked ()
		{

				if (players [currentPlayerIndex].isRevealed) {
						// then just heal
						DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);
				} else {
						// reveal and then heal
						Debug.Log ("not revealed trying to reveal and heal");
						RevealButtonClicked ();
						DoHealTo (players [currentPlayerIndex].DamagePoints, players [currentPlayerIndex].PlName);

				}

		}

		public void EvlogiaClicked ()
		{
				DoHealTo (2, players [currentPlayerIndex].PlName);
		}

		public Sprite GetRandomCard (string type)
		{
				Sprite result = null;

				if (type == "green") {
						result = greenSprites [UnityEngine.Random.Range (0, greenSprites.Count)];
				} else if (type == "red") {
						result = redSprites [UnityEngine.Random.Range (0, redSprites.Count)];
				} else if (type == "blue") {
						// result = BlueSprites [16];
						result = blueSprites [UnityEngine.Random.Range (0, blueSprites.Count)];
				}

				return result;

		}


		public Sprite GetCardFromSpriteName (string type, string name)
		{
				Sprite result = null;

				if (type == "Greens") {
			
						foreach (Sprite s in greenSprites) {
								if (s.name == name) {
										result = s;
								}
						}
	
				} else if (type == "Reds") {

						foreach (Sprite s in redSprites) {
								if (s.name == name) {
										result = s;
								}
						}

				} else if (type == "Blues") {

						foreach (Sprite s in blueSprites) {
								if (s.name == name) {
										result = s;
								}
						}

				}



				if (result == null) {
			
						Debug.LogWarning ("Could not find sprite of type " + type + " with name: " + name);

				}

				return result;

		}


		void HandleDmgText ()
		{
				foreach (Transform t in playerScrollContain.transform) {
						string plName = t.transform.Find ("PlayerName").GetComponent<Text> ().text;
						string plID = t.transform.Find ("PlayerID").GetComponent<Text> ().text;
						foreach (Player pl in players) {
								if (pl.PlName == plName + '_' + plID) {
										t.transform.Find ("DmgPointsText").GetComponent<Text> ().text = pl.DamagePoints.ToString ();
								}
						}
				}

				if (attackStarted) {
						foreach (Transform ta in attackScrollContain.transform) {
								if (ta.name != "EkriksiButton") {
										string plName = ta.transform.Find ("PlayerName").GetComponent<Text> ().text;
										string plID = ta.transform.Find ("PlayerID").GetComponent<Text> ().text;
										foreach (Player pl in players) {
												if (pl.PlName == plName + '_' + plID) {
														ta.transform.Find ("DmgPointsText").GetComponent<Text> ().text = pl.DamagePoints.ToString ();
												}
										}
								}

						}	
				}
		}

		public void ClearAttackPanel ()
		{
				attackButtonGO.GetComponent<Button> ().interactable = false;
				GameObject dmgtdtext = GameObject.Find ("DmgToDoText");
				if (dmgtdtext != null) {
						dmgtdtext.GetComponent<Text> ().text = "0";	
				}

			
				CDMTSPText.text = "Create Damage Points to select player";
				CDMTSPText.color = Color.red;

				foreach (Transform t in attackScrollContain) {
						Destroy (t.gameObject);		
				}

				GameObject ARB = GameObject.Find ("AttackRollButton");

				if (ARB != null) {
						ARB.GetComponent<Button> ().interactable = true;		
				}

				if (GameObject.Find ("IronFistText") != null) {
						Text ironFistTxt = GameObject.Find ("IronFistText").GetComponent<Text> ();
						ironFistTxt.enabled = false;
				}

				if (GameObject.Find ("ProtectiveCloakText") != null) {
						Text protectiveCloakTxt = GameObject.Find ("ProtectiveCloakText").GetComponent<Text> ();
						protectiveCloakTxt.enabled = false;	
				}

				if (GameObject.Find ("FireSpellTextAc") != null) {
						Text fireSpellActiveTxt = GameObject.Find ("FireSpellTextAc").GetComponent<Text> ();
						fireSpellActiveTxt.enabled = false;
				}

				rollToAttackButton.interactable = true;
				attackPanelCleared = true;


				CloseAttackPanel ();

		}

		public void AttackButtonClicked ()
		{

				attackButtonGO.GetComponent<Button> ().interactable = false;
				attackPanelHead.SetActive (true);
				attackPanelCleared = false;

				foreach (Transform child in attackScrollContain) {
						if (child.gameObject.GetComponent<Button> ().interactable && !damagePointsCreated) {
								child.gameObject.GetComponent<Button> ().interactable = false;
						} else if (!child.gameObject.GetComponent<Button> ().interactable && damagePointsCreated) {
								child.gameObject.GetComponent<Button> ().interactable = true;
						}
				}	

				attackStarted = true;

				if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_9")) {
						// TODO a visual to let the user know about firespell
						Text fireSpellActiveTxt = GameObject.Find ("FireSpellTextAc").GetComponent<Text> ();
						fireSpellActiveTxt.enabled = true;
				}

				foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {
						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;
			
						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {


										if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_7")) {

												if (localp.destinationText != myDestinationText || localp.destinationText != GetNeighboor (myDestinationText)) {
														GameObject PlayerEntry = Instantiate (attackPlayerEntry) as GameObject;
														PlayerEntry.transform.SetParent (attackScrollContain.transform);

														string[] splitArray = localp.PlName.Split (new char[]{ '_' });

														string AttackPlayerName = splitArray [0];
														string AttackPlayerID = splitArray [1];


														PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = AttackPlayerName;
														PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = AttackPlayerID;
														PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

														if (localp.eksoplismoi.Contains ("Blues_15") || (localp.PlayerCharacter == "Gandolf" && localp.AbilityActivated && !localp.AbilityDisabled)) {  
																PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
														}

														PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedToAttack> ().PlayerClickedToAttack ());
														PlayerEntry.GetComponent<Button> ().interactable = false;

														foreach (Transform t in playerScrollContain.transform) {
																if (t.Find ("PlayerName").GetComponent<Text> ().text == AttackPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == AttackPlayerID) {
																		Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
																		PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

																		PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
																} 
														}

														if (localp.eksoplismoi.Contains ("Blues_9") && !localp.eksoplismoi.Contains ("Blues_15")) {  //TODO: seperate those too, someone may have both
																PlayerEntry.transform.Find ("MandiasPlayerText").GetComponent<Text> ().enabled = true;
														}
												}

										} else {
						
												if (localp.destinationText == myDestinationText || localp.destinationText == GetNeighboor (myDestinationText)) {
														GameObject PlayerEntry = Instantiate (attackPlayerEntry) as GameObject;
														PlayerEntry.transform.SetParent (attackScrollContain.transform);

														string[] splitArray = localp.PlName.Split (new char[]{ '_' });

														string AttackPlayerName = splitArray [0];
														string AttackPlayerID = splitArray [1];


														PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = AttackPlayerName;
														PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = AttackPlayerID;
														PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

														if (localp.eksoplismoi.Contains ("Blues_15") || (localp.PlayerCharacter == "Gandolf" && localp.AbilityActivated && !localp.AbilityDisabled)) {  
																PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
														}

														PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedToAttack> ().PlayerClickedToAttack ());
														PlayerEntry.GetComponent<Button> ().interactable = false;

														foreach (Transform t in playerScrollContain.transform) {
																if (t.Find ("PlayerName").GetComponent<Text> ().text == AttackPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == AttackPlayerID) {
																		Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
																		PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

																		PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
																} 
														}

														if (localp.eksoplismoi.Contains ("Blues_9") && !localp.eksoplismoi.Contains ("Blues_15")) {  //TODO: seperate those too, someone may have both
																PlayerEntry.transform.Find ("MandiasPlayerText").GetComponent<Text> ().enabled = true;
														}
												}


					
										}

								}	
						}
				}




		}

		public string GetCharactersFirstLetter (string fullcharname)
		{
				string first = "none";

				first = fullcharname.Substring (0, 1);
				return first;
		}

		public void DoTrap ()
		{

				string[] splitArray = players [currentPlayerIndex].PlName.Split (new char[]{ '_' });

				string MyName = splitArray [0];
				string MyID = splitArray [1];

				if (GetEquipmentNumberForPlayer (MyName, MyID) > 0) {
						trapHead.SetActive (true);

						foreach (Transform pl in playerScrollContain.transform) {

								string trapPlayerName = pl.Find ("PlayerName").GetComponent<Text> ().text;
								string trapPlayerID = pl.Find ("PlayerID").GetComponent<Text> ().text;	


								// this player has at least one equip card so instantiate him in the list and add copy all his eq cards
								GameObject PlayerEntry = Instantiate (trapPlayerEntry) as GameObject;
								PlayerEntry.transform.SetParent (trapScrollContain);


								PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = trapPlayerName;
								PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = trapPlayerID;
								PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = pl.Find ("ColorCircleImage").GetComponent<Image> ().color;

								PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromPagidaPanel> ().PlayerClickedFromPagidaPanel ());



								Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
								PlayerAvatar.sprite = pl.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

								if (GetEquipmentNumberForPlayer (trapPlayerName, trapPlayerID) > 0) {
					

										// we know the player has one or more eq cards so lets find them and copy them to the new player entry
										foreach (Transform t in pl.Find("PlayerEqTamplo").transform) {
												GameObject eqinplEntry = Instantiate (t.gameObject);
												eqinplEntry.transform.SetParent (PlayerEntry.transform.Find ("PlayerEqTamplo").transform);
												eqinplEntry.GetComponent<ZoomOnHover> ().enabled = false;

										}

								}

						}	
				} else {

						DoDamageTo (1, MyName + "_" + MyID);
				}


		}

		public void ShowSelectCardToGiveToPlayer (string PlNameToGive, string PlID)
		{
				selectCardToGivePanel.SetActive (true);
				selectCardToGivePanel.transform.Find ("InstructionsInGivePanel").GetComponent<Text> ().text = "Please select a card to give to " + PlNameToGive + ":";

				foreach (Transform t in GameObject.Find ("HandDownPanel").transform.Find("EquipmentTamplo").transform) {
			
						GameObject eqinSelect = Instantiate (equipCardToGivePrefab);

						eqinSelect.transform.SetParent (availableEqToGiveScrollContain);

						eqinSelect.GetComponent<Image> ().sprite = t.gameObject.GetComponent<Image> ().sprite;


						eqinSelect.GetComponent<GiveThisCard> ().DestinationPlayerName = PlNameToGive;
						eqinSelect.GetComponent<GiveThisCard> ().DestinationPlayerID = PlID;

				}


		}

		public void ShowSelectCardToGiveToPlayerFromGreen (string PlNameToGive, string PlID)
		{
		
				selectCardToGivePanel.SetActive (true);
				selectCardToGivePanel.transform.Find ("InstructionsInGivePanel").GetComponent<Text> ().text = "Please select a card to give to " + PlNameToGive + ":";

				foreach (Transform t in GameObject.Find ("HandDownPanel").transform.Find("EquipmentTamplo").transform) {

						GameObject eqinSelect = Instantiate (equipCardToGivePrefab);

						eqinSelect.transform.SetParent (availableEqToGiveScrollContain);

						eqinSelect.GetComponent<Image> ().sprite = t.gameObject.GetComponent<Image> ().sprite;


						eqinSelect.GetComponent<GiveThisCard> ().DestinationPlayerName = PlNameToGive;
						eqinSelect.GetComponent<GiveThisCard> ().DestinationPlayerID = PlID;
						eqinSelect.GetComponent<GiveThisCard> ().isForGreenCard = true;

				}


		}

		public void ClearCardToGiveFromGreen ()
		{


				foreach (Transform t in availableEqToGiveScrollContain) {
						Destroy (t.gameObject);		
				}

				selectCardToGivePanel.SetActive (false);
		}


		public void ClearTrapPanel ()
		{


				foreach (Transform t in availableEqToGiveScrollContain) {
						Destroy (t.gameObject);		
				}


				foreach (Transform t2 in trapScrollContain) {
						Destroy (t2.gameObject);		
				}

				selectCardToGivePanel.SetActive (false);

				trapHead.SetActive (false);

		}

		public void StoneCircleClicked ()
		{
				stealCardHead.SetActive (true);
				StoneCircleButton.interactable = false;


				foreach (Transform pl in playerScrollContain.transform) {


						string ascPlayerName = pl.Find ("PlayerName").GetComponent<Text> ().text;
						string ascPlayerID = pl.Find ("PlayerID").GetComponent<Text> ().text;	

						if (GetEquipmentNumberForPlayer (ascPlayerName, ascPlayerID) > 0) {
								// this player has at least one equip card so instantiate him in the list and add copy all his eq cards
								GameObject PlayerEntry = Instantiate (stealCardPlayerEntry) as GameObject;
								PlayerEntry.transform.SetParent (stealCardScrollContain);

								PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = ascPlayerName;
								PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = ascPlayerID;
								PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = pl.Find ("ColorCircleImage").GetComponent<Image> ().color;

								PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromStealCard> ().PlayerClickedFromStealCardPanel ());

								Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
								PlayerAvatar.sprite = pl.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

								// we know the player has more than one eq cards so lets find them and copy them to the new player entry
								foreach (Transform t in pl.Find("PlayerEqTamplo").transform) {
										GameObject eqinplEntry = Instantiate (t.gameObject);
										eqinplEntry.transform.SetParent (PlayerEntry.transform.Find ("PlayerEqTamplo").transform);
										eqinplEntry.GetComponent<ZoomOnHover> ().enabled = false;
								}

						}


				}

		}

		public void ShowSelectCardToStealFromPlayer (string PlName, string PlID)
		{
				selectCardToStealPanel.SetActive (true);
				selectCardToStealPanel.transform.Find ("InstructionsInStealPanel").GetComponent<Text> ().text = "Please select a card to steal from " + PlName + ":";

				foreach (Transform pl in playerScrollContain.transform) {
						string ascPlayerName = pl.Find ("PlayerName").GetComponent<Text> ().text;
						string ascPlayerID = pl.Find ("PlayerID").GetComponent<Text> ().text;	

						if (ascPlayerName == PlName && ascPlayerID == PlID) {

								foreach (Transform t in pl.Find("PlayerEqTamplo").transform) {
										GameObject eqinSelect = Instantiate (equipCardToStealPrefab);

										eqinSelect.transform.SetParent (availableEqScrollContain);

										eqinSelect.GetComponent<Image> ().sprite = t.gameObject.GetComponent<Image> ().sprite;
										eqinSelect.GetComponent<StealThisCard> ().OwnerName = PlName;
										eqinSelect.GetComponent<StealThisCard> ().OwnerID = PlID;
								}
			
						}
				}
		}

		public void ClearStealCardPanel ()
		{


				foreach (Transform t in availableEqScrollContain) {
						Destroy (t.gameObject);		
				}



				foreach (Transform t2 in stealCardScrollContain) {
						Destroy (t2.gameObject);		
				}

				selectCardToStealPanel.SetActive (false);

				stealCardHead.SetActive (false);


		}


		public void AddEquipCardToPlayer (string cardType, string cardID, bool fromCardButton)
		{
				Debug.Log ("AddEquipCardToPlayer run for cardid " + cardID);

				// check if we need to stack?
				foreach (Transform t in GameObject.Find ("HandDownPanel").transform.Find("EquipmentTamplo").transform) {
						if (t.GetComponent<Image> ().sprite.name == cardID + "#eksoplismos") {

								// stack it
								if (t.GetComponent<Image> ().sprite.name == "Reds_5#eksoplismos") {
										// stack only siderenia grothia, ola ta alla apla min kaneis tpt  ...for now
										t.Find ("EqStar").gameObject.SetActive (true);
										Text stacktxt = t.Find ("EqStar").Find ("Stacknumber").GetComponent<Text> ();
										int stacknumber = int.Parse (t.Find ("EqStar").Find ("Stacknumber").GetComponent<Text> ().text);
										stacknumber++;
										stacktxt.text = stacknumber.ToString ();
								}

								return;
						}

				}

				if (cardID == "Blues_8") {

						rollButtonGO.transform.Find ("Text").GetComponent<Text> ().text = "Make a move";

				}

				GameObject eqCard = Instantiate (equipCardPrefab) as GameObject;

				eqCard.transform.SetParent (GameObject.Find ("HandDownPanel").transform.Find ("EquipmentTamplo").transform);

				eqCard.GetComponent<Image> ().sprite = GetCardFromSpriteName (cardType, cardID + "#eksoplismos");


				eqCard.GetComponent<ZoomOnHover> ().isInMyEquipTamplo = true;


				if (fromCardButton) {
						// this method was called from the called buttons and not from steal or give card
						// check for klerry win
						Debug.Log ("Checking for Klerry");
						foreach (Player pl in players) {
								if (pl.PlayerCharacter == "Klerry" && pl.eksoplismoi.Count >= 4) {
										Debug.Log ("Klerry Won");
										Winner klerrywinner = new Winner (pl.PlName, pl.PlayerCharacter, pl.PlayerColor, pl.PlayerRace);
										List <Winner> winners = new List<Winner> ();
										winners.Add (klerrywinner);
										GlobalGameOver (winners);

								}
						}
				}


		}

		[PunRPC]
		public void AddEquipCardToPlayerNetwork_RPC (string cType, string cID, string pName, string pID)
		{
				//  this method is called (for now) from the StealThisCard script

				AddEquipCardToPlayerNetwork (cType, cID, pName, pID);

		}

		public void AddEquipCardToPlayerNetwork (string cardType, string cardID, string playername, string playerID)
		{
				Debug.Log ("AddEquipCardToPlayerNetwork run for cardid " + cardID);
				Debug.Log ("photon name: " + PhotonNetwork.player.name + "method name: " + playername + "_" + playerID);

				if (PhotonNetwork.player.name == playername + "_" + playerID) {
						// add it to my local player
						// this will run from steal or give card
						Debug.Log ("steal card add to my local player");
						AddEquipCardToPlayer (cardType, cardID, false);


				} else {
						// add it on another (network) player 
						GameObject eqCard = Instantiate (equipCardPrefab) as GameObject;
						eqCard.GetComponent<LayoutElement> ().preferredWidth = 11.7f;
						eqCard.GetComponent<LayoutElement> ().preferredHeight = 18.4f;
						eqCard.GetComponent<RectTransform> ().sizeDelta = new Vector2 (11.7f, 18.4f);

						foreach (Transform t1 in playerScrollContain.transform) {
								if (t1.Find ("PlayerName").GetComponent<Text> ().text == playername && t1.Find ("PlayerID").GetComponent<Text> ().text == playerID) {


										// check for stack
										foreach (Transform t2 in t1.Find ("PlayerEqTamplo").transform) {
												if (t2.GetComponent<Image> ().sprite.name == cardID + "#eksoplismos") {


														if (t2.GetComponent<Image> ().sprite.name == "Reds_5#eksoplismos") {
																// add it either way 
																eqCard.transform.SetParent (t1.Find ("PlayerEqTamplo").transform);
																eqCard.GetComponent<Image> ().sprite = GetCardFromSpriteName (cardType, cardID + "#eksoplismos");
																eqCard.GetComponent<ZoomOnHover> ().isInPlayerScrollc = true;

																foreach (Player p in players) {
																		if (p.PlName == playername + "_" + playerID) {

																				if (p.eksoplismoi.Contains (cardID) == false || (p.eksoplismoi.Contains (cardID) && cardID == "Reds_5")) {
																						p.eksoplismoi.Add (cardID);
																				}
																		}
																}
														}

														// if not just delete it like nothing happened
														Destroy (eqCard);
														return;


												}

										}

										// if it doesnt exist then add it 
										eqCard.transform.SetParent (t1.Find ("PlayerEqTamplo").transform);
								} else {
										Debug.Log ("ERROR: could not find the player in scroll contain, playername is: " + t1.Find ("PlayerName").GetComponent<Text> ().text + " player ID is: " + t1.Find ("PlayerID").GetComponent<Text> ().text);
								}
						}

						// check if klerry just won all clients will check that
						Debug.Log ("Check Klerry");
						foreach (Player pl in players) {
								if (pl.PlayerCharacter == "Klerry" && pl.eksoplismoi.Count >= 4) {
										Debug.Log ("Klerry Won");
										Winner klerrywinner = new Winner (pl.PlName, pl.PlayerCharacter, pl.PlayerColor, pl.PlayerRace);
										List <Winner> winners = new List<Winner> ();
										winners.Add (klerrywinner);
										GlobalGameOver (winners);

								}
						}


						eqCard.GetComponent<Image> ().sprite = GetCardFromSpriteName (cardType, cardID + "#eksoplismos");
						eqCard.GetComponent<ZoomOnHover> ().isInPlayerScrollc = true;

		
				}

				foreach (Player p in players) {
						if (p.PlName == playername + "_" + playerID) {

								if (p.eksoplismoi.Contains (cardID) == false || (p.eksoplismoi.Contains (cardID) && cardID == "Reds_5")) {
										p.eksoplismoi.Add (cardID);
								}
						}
				}

		}


		[PunRPC]
		public void RemoveEquipCardFromPlayer_RPC (string cType, string cID, string pName, string pID)
		{
				//  this method is called (for now) from the StealThisCard script

				RemoveEquipCardFromPlayer (cType, cID, pName, pID);

		}

		public void RemoveEquipCardFromPlayer (string cardType, string cardID, string playername, string playerID)
		{
				Debug.Log ("RemoveEquipCardFromPlayer run for cardid " + cardID);

				if (PhotonNetwork.player.name == playername + "_" + playerID) {
						// this means the card will be removed from the eqtamplo not from the player scroll contain



						foreach (Transform t in GameObject.Find ("HandDownPanel").transform.Find("EquipmentTamplo").transform) {
								if (t.GetComponent<Image> ().sprite.name == cardID + "#eksoplismos") {

										// remove one stack if stacked
										if (t.GetComponent<Image> ().sprite.name == "Reds_5#eksoplismos") {
												if (t.Find ("EqStar").gameObject.activeSelf) {
														//  this means we have reds5 and also its stacked with at least 2 cards

														Text stacktxt = t.Find ("EqStar").Find ("Stacknumber").GetComponent<Text> ();
														int stacknumber = int.Parse (t.Find ("EqStar").Find ("Stacknumber").GetComponent<Text> ().text);
														if (stacknumber <= 2) {
																// just disable stack star and set stack number to 1
																stacknumber = 1;
																t.Find ("EqStar").gameObject.SetActive (false);

														} else {
																stacknumber--;
														}

														stacktxt.text = stacknumber.ToString ();
														foreach (Player p in players) {
																if (p.PlName == playername + "_" + playerID) {

																		if (p.eksoplismoi.Contains (cardID) == true) {
																				p.eksoplismoi.Remove (cardID);
																		}
																}
														}

														return;
												}


										}

										if (cardID == "Blues_8") {

												rollButtonGO.transform.Find ("Text").GetComponent<Text> ().text = "Roll";

										}

										foreach (Player p in players) {
												if (p.PlName == playername + "_" + playerID) {

														if (p.eksoplismoi.Contains (cardID) == true) {
																p.eksoplismoi.Remove (cardID);
														}
												}
										}
										Destroy (t.gameObject);
										return;


								}

						}
				} else {
						// this means the card will be removed from the player scroll contain

						foreach (Transform t in playerScrollContain.transform) {
								if (t.Find ("PlayerName").GetComponent<Text> ().text == playername && t.Find ("PlayerID").GetComponent<Text> ().text == playerID) {

										foreach (Transform t2 in t.Find ("PlayerEqTamplo").transform) {
												if (t2.GetComponent<Image> ().sprite.name == cardID + "#eksoplismos") {
														// no stacks here yet so just destroy TODO: stacks maybe? we will see at the debugging stage

														Destroy (t2.gameObject);
														foreach (Player p in players) {
																if (p.PlName == playername + "_" + playerID) {

																		if (p.eksoplismoi.Contains (cardID) == true) {
																				p.eksoplismoi.Remove (cardID);
																		}
																}
														}
														return;
												}


										}
								} else {
										Debug.Log ("ERROR: could not find the player in scroll contain, playername is: " + t.Find ("PlayerName").GetComponent<Text> ().text + " player ID is: " + t.Find ("PlayerID").GetComponent<Text> ().text);
								}
						}


		
				}





		}



		public int GetEquipmentNumberOfOtherPlayers ()
		{
				int counter = 0;
				foreach (Transform t in playerScrollContain.transform) {
						foreach (Transform t2 in t.Find("PlayerEqTamplo").transform) {
								counter++;
						}
				}

				Debug.Log ("GetEquipmentNumberOfOtherPlayers returning: " + counter);
				return counter;

		}

		public int GetEquipmentNumberForPlayer (string PlayerName, string PlayerID)
		{
				int counter = 0;

				if (players [currentPlayerIndex].PlName == PlayerName + "_" + PlayerID) {
						// search my player
						Debug.Log ("search my player");
						foreach (Transform t in GameObject.Find ("HandDownPanel").transform.Find("EquipmentTamplo").transform) {
								counter++;
						}

						Debug.Log ("GetEquipmentNumberForPlayer returning: " + counter + " for player " + PlayerName);
						return counter;
				}

				// the player wasn't local

				// search the net players
				foreach (Transform t in playerScrollContain.transform) {
						string tPlName = t.Find ("PlayerName").GetComponent<Text> ().text;
						string tPlID = t.Find ("PlayerID").GetComponent<Text> ().text;

						if (tPlName == PlayerName && tPlID == PlayerID) {
								foreach (Transform t2 in t.Find("PlayerEqTamplo").transform) {
										counter++;
								}
						}
				}


				Debug.Log ("GetEquipmentNumberForPlayer returning: " + counter + " for player " + PlayerName);
				return counter;

		}

		public int GetEquipmentNumberForPlayerGreen (string PlayerName, string PlayerID)
		{
				int counter = 0;

				if (PhotonNetwork.player.name == PlayerName + "_" + PlayerID) {
						// search my player
						Debug.Log ("search my player");
						foreach (Transform t in GameObject.Find ("HandDownPanel").transform.Find("EquipmentTamplo").transform) {
								counter++;
						}

						Debug.Log ("GetEquipmentNumberForPlayer returning: " + counter + " for player " + PlayerName);
						return counter;
				}

				// the player wasn't local

				// search the net players
				foreach (Transform t in playerScrollContain.transform) {
						string tPlName = t.Find ("PlayerName").GetComponent<Text> ().text;
						string tPlID = t.Find ("PlayerID").GetComponent<Text> ().text;

						if (tPlName == PlayerName && tPlID == PlayerID) {
								foreach (Transform t2 in t.Find("PlayerEqTamplo").transform) {
										counter++;
								}
						}
				}



				Debug.Log ("GetEquipmentNumberForPlayer returning: " + counter + " for player " + PlayerName);
				return counter;

		}

		public void MagicForestDamageClicked ()
		{
		
				magicForestWhatToDo = 0;
				magicForestDamageButton.interactable = false;
				magicForestHealButton.interactable = false;

				magicForestSelectText.text = "Select Player To Damage 2pts";
				foreach (Transform t in magicForestScrollContain.transform) {
						t.GetComponent<Button> ().interactable = true;
				}

		}

		public void MagicForestHealClicked ()
		{
		
				magicForestWhatToDo = 1;
				magicForestDamageButton.interactable = false;
				magicForestHealButton.interactable = false;
				magicForestSelectText.text = "Select Player To Heal 1pt";
				foreach (Transform t in magicForestScrollContain.transform) {
						t.GetComponent<Button> ().interactable = true;
				}
		}


		public void MagicForestButtonClicked ()
		{
				// do 1 damage to a random other player
				magicForestButtonGO.GetComponent<Button> ().interactable = false;
				magicPanelHead.SetActive (true);


				foreach (Transform child in magicForestScrollContain) {
						if (child.gameObject.GetComponent<Button> ().interactable && !damagePointsCreated) {
								child.gameObject.GetComponent<Button> ().interactable = false;
						} else if (!child.gameObject.GetComponent<Button> ().interactable && damagePointsCreated) {
								child.gameObject.GetComponent<Button> ().interactable = true;
						}
				}	


				foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
						// we also want our player thats why we choose playerList and not otherplayers

						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {
					
										GameObject PlayerEntry = Instantiate (magicForestPlayerEntry) as GameObject;
										PlayerEntry.transform.SetParent (magicForestScrollContain.transform);

										string[] splitArray = localp.PlName.Split (new char[]{ '_' });

										string MagicPlayerName = splitArray [0];
										string MagickPlayerID = splitArray [1];


										PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = MagicPlayerName;
										PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = MagickPlayerID;
										PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

										if (localp.eksoplismoi.Contains ("Blues_16")) {  //he is protected from magic forest
												PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
										}

										PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromMagicForest> ().PlayerClickedFromMagicForest ());
										PlayerEntry.GetComponent<Button> ().interactable = false;

										foreach (Transform t in playerScrollContain.transform) {
												if (t.Find ("PlayerName").GetComponent<Text> ().text == MagicPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == MagickPlayerID) {
														Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
														PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

														PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
												} 
										}

										if (players [currentPlayerIndex].PlName == MagicPlayerName + "_" + MagickPlayerID) {
												Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
												PlayerAvatar.sprite = NetworkManager.instance.myProfilePic;

												PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = players [currentPlayerIndex].DamagePoints.ToString ();
										}
								}	
						}
				}



		}

		public void CloseMagicForestPanel ()
		{
				magicPanelHead.SetActive (false);

		}

		public void ClearMagicForestPanel ()
		{
				magicForestButtonGO.GetComponent<Button> ().interactable = false;

				magicForestSelectText.text = "Available Players:";

				foreach (Transform t in magicForestScrollContain) {
						Destroy (t.gameObject);		
				}


				magicForestDamageButton.GetComponent<Button> ().interactable = true;		
				magicForestHealButton.GetComponent<Button> ().interactable = true;	


				CloseMagicForestPanel ();

		}

		public void ShowBatPanel ()
		{
		

				batHead.SetActive (true);
				Button batHealButton = batHead.transform.Find ("BatPanel").Find ("BatHealButton").GetComponent<Button> ();

				if (players [currentPlayerIndex].DamagePoints > 0) {
						batHealText.text = "You can heal yourself:";
						batHealButton.interactable = true;
				} else {
						batHealText.text = "Your health is full!";
						batHealButton.interactable = false;
				}


				foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {
										GameObject PlayerEntry = Instantiate (batPlayerEntry) as GameObject;
										PlayerEntry.transform.SetParent (batScrollContain.transform);

										string[] splitArray = localp.PlName.Split (new char[]{ '_' });

										string batPlayerName = splitArray [0];
										string batPlayerID = splitArray [1];


										PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = batPlayerName;
										PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = batPlayerID;
										PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

										if (localp.eksoplismoi.Contains ("Blues_13")) {  
												PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
										}

										PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromNyxterida> ().PlayerClickedFromNyxterida ());
										PlayerEntry.GetComponent<Button> ().interactable = true;

										// handle images:
										foreach (Transform t in playerScrollContain.transform) {
												if (t.Find ("PlayerName").GetComponent<Text> ().text == batPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == batPlayerID) {
														Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
														PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

														PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
												} 
										}
								}	
						}
				}

				// check if they are all protected:
				foreach (Transform t in batScrollContain) {
						if (!t.Find ("ProtectedText").GetComponent<Text> ().enabled) {
								// if at least one of them is not protected then just return
								return;
						}
				}

				// if we reach this point everyone is protected so enable the x button
				batHead.transform.Find ("BatPanel").Find ("CloseBatPanelWindowImg").GetComponent<Button> ().enabled = true;
				batHead.transform.Find ("BatPanel").Find ("CloseBatPanelWindowImg").GetComponent<Image> ().enabled = true;

		}

		public void CloseBatClicked ()
		{
				ClearBatPanel ();
		}

		public void BatHeal ()
		{
				DoHealTo (1, players [currentPlayerIndex].PlName);

				batHead.transform.Find ("BatPanel").Find ("BatHealButton").GetComponent<Button> ().interactable = false;
		}


		public void ClearBatPanel ()
		{
		




				foreach (Transform t in batScrollContain) {
						Destroy (t.gameObject);		
				}


				batHead.SetActive (false);

		}

		public void ShowTreatmentFromAfarPanel ()
		{

				treatmentFromAfarHead.SetActive (true);




				foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {
										GameObject PlayerEntry = Instantiate (treatmentFromAfarPlayerEntry) as GameObject;
										PlayerEntry.transform.SetParent (treatmentFromAfarScrollContain);

										string[] splitArray = localp.PlName.Split (new char[]{ '_' });

										string AmbushPlayerName = splitArray [0];
										string AmbushPlayerID = splitArray [1];


										PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = AmbushPlayerName;
										PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = AmbushPlayerID;
										PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

										PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromTheApMa> ().PlayerClickedFromTherapiaApoMakria ());
										PlayerEntry.GetComponent<Button> ().interactable = true;

										// handle images:
										foreach (Transform t in playerScrollContain.transform) {
												if (t.Find ("PlayerName").GetComponent<Text> ().text == AmbushPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == AmbushPlayerID) {
														Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
														PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

														PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
												} 
										}
								}	
						}
				}

		}

		public void RollForTreatmentFromAfar (string nameToHeal)
		{
				rollForTreatmentFromAfar = true;
				nameOfPLayerToHealFromAfar = nameToHeal;

				// roll only 6plevro
				Vector3 position2 = new Vector3 (0f, 10.5f, 0.42f);
				Vector3 rotation2 = new Vector3 (UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f));
				GameObject ssided = ((GameObject)PhotonNetwork.Instantiate ("SixSidedDie", position2, Quaternion.Euler (rotation2), 0));
				ssided.GetComponent<Rigidbody> ().AddTorque (UnityEngine.Random.Range (150f, 300f), UnityEngine.Random.Range (10f, 32f), UnityEngine.Random.Range (160f, 280f));

		}

		public void DoTreatmentFromAfar (int pointsAfterRoll)
		{
				GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);
				DoHealTo (pointsAfterRoll, nameOfPLayerToHealFromAfar);

				rollForTreatmentFromAfar = false;


		
		}

		public void ClearTreatmentFromAfarPanel ()
		{





				foreach (Transform t in treatmentFromAfarScrollContain) {
						Destroy (t.gameObject);		
				}


				treatmentFromAfarHead.SetActive (false);

		}

		public void ShowBloodSpiderPanel ()
		{


				bloodSpiderHead.SetActive (true);


				if (players [currentPlayerIndex].eksoplismoi.Contains ("Blues_13")) { 
						Text YouWillGetDamagedTxt = bloodSpiderHead.transform.Find ("BloodSpiderPanel").Find ("YouWillGetDamagedTxt").GetComponent<Text> ();

						YouWillGetDamagedTxt.text = "You are protected with Amulet";
						YouWillGetDamagedTxt.color = Color.blue;
				}

				foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {
										GameObject PlayerEntry = Instantiate (bloodSpiderPlayerEntry) as GameObject;
										PlayerEntry.transform.SetParent (bloodSpiderScrollContain.transform);

										string[] splitArray = localp.PlName.Split (new char[]{ '_' });

										string BloodSpiderPlayerName = splitArray [0];
										string BloodSpiderPlayerID = splitArray [1];


										PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = BloodSpiderPlayerName;
										PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = BloodSpiderPlayerID;
										PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

										if (localp.eksoplismoi.Contains ("Blues_13")) {  
												PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
										}


										PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromMatomeniAraxni> ().PlayerClickedFromMatomeniAraxni ());
										PlayerEntry.GetComponent<Button> ().interactable = true;

										// handle images:
										foreach (Transform t in playerScrollContain.transform) {
												if (t.Find ("PlayerName").GetComponent<Text> ().text == BloodSpiderPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == BloodSpiderPlayerID) {
														Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
														PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

														PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
												} 
										}
								}	
						}
				}

				// check if they are all protected:
				foreach (Transform t in bloodSpiderScrollContain) {
						if (!t.Find ("ProtectedText").GetComponent<Text> ().enabled) {
								// if at least one of them is not protected then just return
								return;
						}
				}

				// if we reach this point everyone is protected so enable the x button
				bloodSpiderHead.transform.Find ("BloodSpiderPanel").Find ("BloodSpiderPanelWindowImg").GetComponent<Button> ().enabled = true;
				bloodSpiderHead.transform.Find ("BloodSpiderPanel").Find ("BloodSpiderPanelWindowImg").GetComponent<Image> ().enabled = true;

		}

		public void CloseBloodSpiderClicked ()
		{
				if (!players [currentPlayerIndex].eksoplismoi.Contains ("Blues_13")) { //he is NOT protected from magic forest but everyone else was so he can only click x now
						DoDamageTo (2, players [currentPlayerIndex].PlName);
				}  


				ClearBloodSpiderPanel ();
		}



		public void ClearBloodSpiderPanel ()
		{





				foreach (Transform t in bloodSpiderScrollContain) {
						Destroy (t.gameObject);		
				}


				bloodSpiderHead.SetActive (false);

		}

		public void ShowAmbushPanel ()
		{


				ambushHead.SetActive (true);


				foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {
										GameObject PlayerEntry = Instantiate (ambushPlayerEntry) as GameObject;
										PlayerEntry.transform.SetParent (ambushScrollContain);

										string[] splitArray = localp.PlName.Split (new char[]{ '_' });

										string AmbushPlayerName = splitArray [0];
										string AmbushPlayerID = splitArray [1];


										PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = AmbushPlayerName;
										PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = AmbushPlayerID;
										PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

										PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromEnedra> ().PlayerClickedFromEnedra ());
										PlayerEntry.GetComponent<Button> ().interactable = true;

										// handle images:
										foreach (Transform t in playerScrollContain.transform) {
												if (t.Find ("PlayerName").GetComponent<Text> ().text == AmbushPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == AmbushPlayerID) {
														Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
														PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

														PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
												} 
										}
								}	
						}
				}



		}

		public void AmbushPlayer (string plName)
		{

				ambushTargetedPlayer = plName;

				rollForAmbush = true;

				// roll only eksaplevro

				Vector3 position2 = new Vector3 (0f, 10.5f, 0.42f);
				Vector3 rotation2 = new Vector3 (UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f));
				GameObject ssided = ((GameObject)PhotonNetwork.Instantiate ("SixSidedDie", position2, Quaternion.Euler (rotation2), 0));
				ssided.GetComponent<Rigidbody> ().AddTorque (UnityEngine.Random.Range (150f, 300f), UnityEngine.Random.Range (10f, 32f), UnityEngine.Random.Range (160f, 280f));


		}

		public void AmbushFinalResult (int result)
		{
				if (ambushDone == false) {
			

						GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);

						if (result >= 1 && result <= 4) {
								DoDamageTo (3, ambushTargetedPlayer);
						} else if (result > 4 && result <= 6) {
								DoDamageTo (3, players [currentPlayerIndex].PlName);
						}

						ambushDone = true;
				}

		}

		public void ClearAmbushPanel ()
		{

				foreach (Transform t in ambushScrollContain) {
						Destroy (t.gameObject);		
				}

				ambushHead.SetActive (false);

		}

		public void ShowGregoryAbilityPanel ()
		{
				// this panel is not only for gregory TODO: Rename to general select player panel

				gregoryAbilityHead.SetActive (true);

				if (players [currentPlayerIndex].PlayerCharacter == "Ouriel") {
						//  we nneed to display only players in cemetery

						foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

								ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

								foreach (Player localp in players) {
										if (ht ["PlayerName"].ToString () == localp.PlName) {

												if (localp.destinationText == "8") {

														GameObject PlayerEntry = Instantiate (gregoryAbilityPlayerEntry) as GameObject;
														PlayerEntry.transform.SetParent (gregoryAbilityScrollContain);

														string[] splitArray = localp.PlName.Split (new char[]{ '_' });

														string GregoryAbilityPlayerName = splitArray [0];
														string GregoryAbilityPlayerID = splitArray [1];


														PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = GregoryAbilityPlayerName;
														PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = GregoryAbilityPlayerID;
														PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

														PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromGregorAbility> ().PlayerClickedFromGregorAbility ());
														PlayerEntry.GetComponent<Button> ().interactable = true;

														// handle images:
														foreach (Transform t in playerScrollContain.transform) {
																if (t.Find ("PlayerName").GetComponent<Text> ().text == GregoryAbilityPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == GregoryAbilityPlayerID) {
																		Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
																		PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

																		PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
																} 
														}

												}


										}	
								}
						}
				} else {
						// its for the abilities of the other characters

						foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

								ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

								foreach (Player localp in players) {
										if (ht ["PlayerName"].ToString () == localp.PlName) {
												GameObject PlayerEntry = Instantiate (gregoryAbilityPlayerEntry) as GameObject;
												PlayerEntry.transform.SetParent (gregoryAbilityScrollContain);

												string[] splitArray = localp.PlName.Split (new char[]{ '_' });

												string GregoryAbilityPlayerName = splitArray [0];
												string GregoryAbilityPlayerID = splitArray [1];


												PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = GregoryAbilityPlayerName;
												PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = GregoryAbilityPlayerID;
												PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

												PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromGregorAbility> ().PlayerClickedFromGregorAbility ());
												PlayerEntry.GetComponent<Button> ().interactable = true;

												// handle images:
												foreach (Transform t in playerScrollContain.transform) {
														if (t.Find ("PlayerName").GetComponent<Text> ().text == GregoryAbilityPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == GregoryAbilityPlayerID) {
																Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
																PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

																PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
														} 
												}
										}	
								}
						}
				}



		}

		public void GregoryAbilityPlayer (string plName)
		{

				if (players [currentPlayerIndex].PlayerCharacter == "Etta") {
						GetComponent<PhotonView> ().RPC ("DisableAbilityOfPlayer", PhotonTargets.AllBufferedViaServer, plName, players [currentPlayerIndex].PlName);
				} else if (players [currentPlayerIndex].PlayerCharacter == "Ouriel") {
						DoDamageTo (3, plName);
				} else {
						// its from gregor


						gregoryAbilityTargetedPlayer = plName;

						rollForGregoryAbility = true;

						// roll only tetraplevro

						if (players [currentPlayerIndex].PlayerCharacter == "Gregor") {
								Vector3 position = new Vector3 (-3.24f, 10.5f, 0.42f);
								Vector3 rotation = new Vector3 (UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f));

								GameObject fsided = ((GameObject)PhotonNetwork.Instantiate ("FourSidedDie", position, Quaternion.Euler (rotation), 0));
								fsided.GetComponent<Rigidbody> ().AddTorque (UnityEngine.Random.Range (150f, 300f), UnityEngine.Random.Range (10f, 32f), UnityEngine.Random.Range (160f, 280f));
						} else if (players [currentPlayerIndex].PlayerCharacter == "Frederic") {
								Vector3 position2 = new Vector3 (0f, 10.5f, 0.42f);
								Vector3 rotation2 = new Vector3 (UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f));
								GameObject ssided = ((GameObject)PhotonNetwork.Instantiate ("SixSidedDie", position2, Quaternion.Euler (rotation2), 0));
								ssided.GetComponent<Rigidbody> ().AddTorque (UnityEngine.Random.Range (150f, 300f), UnityEngine.Random.Range (10f, 32f), UnityEngine.Random.Range (160f, 280f));
						}
		
				}

		}

		[PunRPC]
		public void DisableAbilityOfPlayer (string PlayerToDisable, string fromPlayer)
		{

				string[] splitArray = fromPlayer.Split (new char[]{ '_' });


				foreach (Player pl in players) {
						if (pl.PlName == PlayerToDisable) {
								pl.AbilityDisabled = true;
								if (pl.PlName == PhotonNetwork.player.name) {
										// if our ability just got deactivated then disable the button and edit text
										if (PlayerToDisable == fromPlayer) {
												// that means we are Gandolf
												activateAbilityButton.GetComponentInChildren<Text> ().text = "Ability Done";
										} else {
												// someone disabled my ability
												activateAbilityButton.GetComponentInChildren<Text> ().text = "Ability Disabled by " + splitArray [0];
										}

										if (activateAbilityButton.interactable) {
												activateAbilityButton.interactable = false;
										}
								}

						}

				}

				foreach (Player pl2 in players) {
						if (pl2.PlName == PhotonNetwork.player.name) {
				
						}
				}



		}


		public void GregoryAbilityFinalResult (int result)
		{
				if (gregoryAbilityDone == false) {


						GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);

						DoDamageTo (result, gregoryAbilityTargetedPlayer);
		

						gregoryAbilityDone = true;
				}

		}

		public void ClearGregoryAbilityPanel ()
		{

				foreach (Transform t in gregoryAbilityScrollContain) {
						Destroy (t.gameObject);		
				}


				gregoryAbilityHead.SetActive (false);

		}


		public void ShowBloodMoonPanel ()
		{
		
				bloodMoonHead.SetActive (true);

				foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
						// we also want our player thats why we choose playerList and not otherplayers

						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {

										GameObject PlayerEntry = Instantiate (bloodMoonPlayerEntry) as GameObject;
										PlayerEntry.transform.SetParent (bloodMoonScrollContain.transform);

										string[] splitArray = localp.PlName.Split (new char[]{ '_' });

										string BloodMoonPlayerName = splitArray [0];
										string BloodMoonPlayerID = splitArray [1];


										PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = BloodMoonPlayerName;
										PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = BloodMoonPlayerID;
										PlayerEntry.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (localp.PlayerColor);

										if (localp.eksoplismoi.Contains ("Blues_3")) {  //he is protected from magic forest
												PlayerEntry.transform.Find ("ProtectedText").GetComponent<Text> ().enabled = true;
										}

										PlayerEntry.GetComponent<Button> ().onClick.AddListener (() => PlayerEntry.GetComponent<ClickedFromMatomenoFegari> ().PlayerClickedFromMatomenoFegari ());
										PlayerEntry.GetComponent<Button> ().interactable = true;

										foreach (Transform t in playerScrollContain.transform) {
												if (t.Find ("PlayerName").GetComponent<Text> ().text == BloodMoonPlayerName && t.Find ("PlayerID").GetComponent<Text> ().text == BloodMoonPlayerID) {
														Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
														PlayerAvatar.sprite = t.transform.Find ("PlayerAvatar").GetComponent<Image> ().sprite; 

														PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = t.Find ("DmgPointsText").GetComponent<Text> ().text;
												} 
										}

										if (players [currentPlayerIndex].PlName == BloodMoonPlayerName + "_" + BloodMoonPlayerID) {
												Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
												PlayerAvatar.sprite = NetworkManager.instance.myProfilePic;

												PlayerEntry.transform.Find ("DmgPointsText").GetComponent<Text> ().text = players [currentPlayerIndex].DamagePoints.ToString ();
										}
								}	
						}
				}


		}

		public void ClearBloodMoonPanel ()
		{

				foreach (Transform t in bloodMoonScrollContain) {
						Destroy (t.gameObject);		
				}

				bloodMoonHead.SetActive (false);

		}


		public void DoIeriOrgi ()
		{

				foreach (PhotonPlayer pl in PhotonNetwork.otherPlayers) {

						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {
					
										DoDamageTo (2, localp.PlName);
				
								}
			
						}
				}
	
		}

		public void DoEkriksi ()
		{
				rollForExplosion = true;
				RollDice ();
		}

		public void HandleGreenCardResult (string rName)
		{
		
				GetComponent<PhotonView> ().RPC ("HandleGreenCardResult_RPC", PhotonTargets.AllBufferedViaServer, rName);
				GameObject.FindObjectOfType<GreenCardScript> ().ClearGreenCardPanel ();
		}

		[PunRPC]
		public void HandleGreenCardResult_RPC (string receivName)
		{
				GameObject.FindObjectOfType<GreenCardScript> ().GreenCardResult (receivName);
		}


		public void DamageAllPlayersAtArea (string area, int dmgpoints)
		{
				GetComponent<PhotonView> ().RPC ("DestroyDice_RPC", PhotonTargets.AllBufferedViaServer, null);

				if (area == "4" || area == "5")
						area = "45";
				if (area == "2" || area == "3")
						area = "23";
				if (area == "7") {  //Do nothing if 7
						rollForExplosion = false;
						return;
				}

				foreach (Player pl in players) {
						if (pl.destinationText == area) {
								if (!pl.eksoplismoi.Contains ("Blues_13")) {
										DoDamageTo (dmgpoints, pl.PlName);					
								} else {
										// do nothing
										// TODO: add here some fancy way to let the user now that player is protected
								}
					

						}
				}

				rollForExplosion = false;

		}



		public Color GetColorFromString (string str)
		{
				if (str == "gray") {
						return Color.gray;
				} else if (str == "blue") {
						return Color.blue;
				} else if (str == "yellow") {
						return Color.yellow;
				} else if (str == "red") {
						return Color.red;
				} else if (str == "black") {
						return Color.black;
				}

				return Color.white; // white is the error color probably :P
		}

		public void CloseAttackPanel ()
		{
				attackPanelHead.SetActive (false);
				if (!attackPanelCleared) {
						ClearAttackPanel ();
				}
		}



		public void RollToAttackClicked ()
		{
				rollDiceForAttack = true;
				rollToAttackButton.interactable = false;
				endTurnButtonGO.GetComponent<Button> ().interactable = false;
				RollDice ();
		}

		public void DisableAllButtons ()
		{
				// first we have to log the button state
				// the order goes the same as they are disabled bellow
				buttonState [0] = rollButtonGO.GetComponent<Button> ().interactable;
				buttonState [1] = attackButtonGO.GetComponent<Button> ().interactable;
				buttonState [2] = endTurnButtonGO.GetComponent<Button> ().interactable;
				buttonState [3] = magicForestButtonGO.GetComponent<Button> ().interactable;
				buttonState [4] = StoneCircleButton.interactable;
				buttonState [5] = revealCharacterButton.interactable;
				buttonState [6] = activateAbilityButton.interactable;
				buttonState [7] = greenCardButton.GetComponent<Button> ().interactable;
				buttonState [8] = redCardButton.GetComponent<Button> ().interactable;
				buttonState [9] = blueCardButton.GetComponent<Button> ().interactable;


				rollButtonGO.GetComponent<Button> ().interactable = false; 
				attackButtonGO.GetComponent<Button> ().interactable = false; 
				endTurnButtonGO.GetComponent<Button> ().interactable = false;

				magicForestButtonGO.GetComponent<Button> ().interactable = false;
				StoneCircleButton.interactable = false;

				revealCharacterButton.interactable = false;
				activateAbilityButton.interactable = false;

				DisableGreenCards (true);
				DisableRedCards (true);
				DisableBlueCards (true);
		}

		public void EnableButtons ()
		{
				rollButtonGO.GetComponent<Button> ().interactable = buttonState [0]; 
				attackButtonGO.GetComponent<Button> ().interactable = buttonState [1]; 
				endTurnButtonGO.GetComponent<Button> ().interactable = buttonState [2];

				magicForestButtonGO.GetComponent<Button> ().interactable = buttonState [3];
				StoneCircleButton.interactable = buttonState [4];

				revealCharacterButton.interactable = buttonState [5];
				activateAbilityButton.interactable = buttonState [6];

				DisableGreenCards (buttonState [7]);
				DisableRedCards (buttonState [8]);
				DisableBlueCards (buttonState [9]);
		}

		public void SetDamageTo (int DmgToSet, string n)
		{
				GetComponent<PhotonView> ().RPC ("SetDamageTo_RPC", PhotonTargets.AllBuffered, DmgToSet.ToString () + '#' + n, PhotonNetwork.player.name);
		}

		[PunRPC]
		public void SetDamageTo_RPC (string PointsAndName, string nameOfDamager)
		{

				string[] splitArray = PointsAndName.Split (new char[]{ '#' });

				int PointsToDmg = int.Parse (splitArray [0]);
				string NameOfPlayerToDamage = splitArray [1];
				Debug.Log ("Trying to damage: " + NameOfPlayerToDamage);
				foreach (Player pl in players) {
						if (pl.PlName == NameOfPlayerToDamage) {
								pl.DamagePoints = PointsToDmg;
						}
						HandleDmgText ();
				}

				HandleDmgPositionForPlayer (NameOfPlayerToDamage, nameOfDamager);

		}


		public void DoDamageTo (int DmgPoints, string n)
		{
				if (DmgPoints > 0) {
						GetComponent<PhotonView> ().RPC ("DoDamageTo_RPC", PhotonTargets.AllBuffered, DmgPoints.ToString () + '#' + n, PhotonNetwork.player.name);	
				}
		}


		[PunRPC]
		public void DoDamageTo_RPC (string PointsAndName, string nameOfDamager)
		{

				string[] splitArray = PointsAndName.Split (new char[]{ '#' });

				int PointsOfDmg = int.Parse (splitArray [0]);
				string NameOfPlayerToDamage = splitArray [1];
				Debug.Log ("Trying to damage: " + NameOfPlayerToDamage);
				foreach (Player pl in players) {
						if (pl.PlName == NameOfPlayerToDamage) {
								pl.DamagePoints += PointsOfDmg;
						}
						HandleDmgText ();
				}

				HandleDmgPositionForPlayer (NameOfPlayerToDamage, nameOfDamager);

		}

		public void DoDamageFromAttackTo (int DmgPoints, string n)
		{
				if (DmgPoints > 0) {
						GetComponent<PhotonView> ().RPC ("DoDamageFromAttackTo_RPC", PhotonTargets.AllBuffered, DmgPoints.ToString () + '#' + n, PhotonNetwork.player.name);
				}

		}

		[PunRPC]
		public void DoDamageFromAttackTo_RPC (string PointsAndName, string nameOfDamager)
		{
		
				string[] splitArray = PointsAndName.Split (new char[]{ '#' });


				int PointsOfDmg = int.Parse (splitArray [0]);
				string NameOfPlayerToDamage = splitArray [1];

				foreach (Player p in players) {
						if (p.PlName == PhotonNetwork.player.name) {
								if (p.PlName == NameOfPlayerToDamage) {
										if (p.PlayerCharacter == "Malburca" && p.AbilityActivated && !p.AbilityDisabled) {
												malburcaHead.SetActive (true);
												malburcaPlayerToAttackBack = nameOfDamager;
										}
								} else if (p.PlName == nameOfDamager) {
										// the damager 
										bool attackingactivemalburca = false;
										// check if malburca is the player to damage and if she's activated
										foreach (Player p2 in players) {
												if (p2.PlName == NameOfPlayerToDamage) {
														if (p2.PlayerCharacter == "Malburca" && p2.AbilityActivated && !p2.AbilityDisabled) {
																attackingactivemalburca = true;

														}
												}

										}
										if (attackingactivemalburca) {

												warningText.SetActive (true);
												warningText.GetComponent<Text> ().text = "Waiting for Malburca to attack back...";
												waitingForMalburca = true;
												DisableAllButtons ();
										}

								} else {
										// everyone else
										// check if malburca is the player to damage and if she's activated
										bool attackingactivemalburca = false;
										// check if malburca is the player to damage and if she's activated
										foreach (Player p2 in players) {
												if (p2.PlName == NameOfPlayerToDamage) {
														if (p2.PlayerCharacter == "Malburca" && p2.AbilityActivated && !p2.AbilityDisabled) {
																attackingactivemalburca = true;
														}
												}

										}
										if (attackingactivemalburca) {

												warningText.SetActive (true);
												warningText.GetComponent<Text> ().text = "Waiting for Malburca to attack back...";
												waitingForMalburca = true;
										}

								}
						}



				}

				Debug.Log ("Trying to damage: " + NameOfPlayerToDamage);
				foreach (Player pl in players) {
						if (pl.PlName == NameOfPlayerToDamage) {
								pl.DamagePoints += PointsOfDmg;
						}
						HandleDmgText ();
				}

				HandleDmgPositionForPlayer (NameOfPlayerToDamage, nameOfDamager);

		}


		public void DoHealTo (int HealPoints, string n)
		{
				GetComponent<PhotonView> ().RPC ("HealPlayer_RPC", PhotonTargets.AllBuffered, HealPoints.ToString () + '#' + n);
		}

		[PunRPC]
		public void HealPlayer_RPC (string PointsAndName)
		{

				string[] splitArray = PointsAndName.Split (new char[]{ '#' });
		
				int PointsToHeal = int.Parse (splitArray [0]);
				string NameOfPlayerToHeal = splitArray [1];
				Debug.Log ("Trying to heal: " + NameOfPlayerToHeal);
				foreach (Player pl in players) {
						if (pl.PlName == NameOfPlayerToHeal) {
								pl.DamagePoints -= PointsToHeal;

								if (pl.DamagePoints < 0) {
										pl.DamagePoints = 0;
								}
						}
						HandleDmgText ();
				}

				HandleDmgPositionForPlayer (NameOfPlayerToHeal);

		}

		public int GetPlayersInArea (string area)
		{
				int PlayersInThatArea = 0;


				foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {
										if (localp.destinationText == area) {
												PlayersInThatArea++;
										}
										if (GetNeighboor (area) != "none") {
												if (localp.destinationText == GetNeighboor (area)) {
														PlayersInThatArea++;
												}
										} else {
												Debug.Log ("Can't find neighboor, wtf?");
										}


								}
						}


				}

				return PlayersInThatArea;
		}

		public int GetPlayersInOppositeAreas (string area)
		{
				// this method is for valistra 

				int PlayersInThoseAreas = 0;


				foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
						ExitGames.Client.Photon.Hashtable ht = pl.customProperties;

						foreach (Player localp in players) {
								if (ht ["PlayerName"].ToString () == localp.PlName) {
										if (localp.destinationText != area && localp.destinationText != "none" && localp.destinationText != GetNeighboor (area)) {
												PlayersInThoseAreas++;
												Debug.Log ("GetPlayersInOppositeAreas: run on first if +1");
										}
								}
						}


				}
				Debug.Log ("GetPlayersInOppositeAreas: total: " + PlayersInThoseAreas.ToString ());
				return PlayersInThoseAreas;
		}

		public string GetNeighboor (string area)
		{
				string neighboor = "none";

				if (area == "23") {
						neighboor = "8";
				} else if (area == "45") {
						neighboor = "10";
				} else if (area == "6") {
						neighboor = "9";
				} else if (area == "7") {
						neighboor = "45";
				} else if (area == "8") {
						neighboor = "23";
				} else if (area == "9") {
						neighboor = "6";
				} else if (area == "10") {
						neighboor = "45";
				}

				return neighboor;
		}


		public void RollDice ()
		{


				Vector3 position = new Vector3 (-3.24f, 10.5f, 0.42f);
				Vector3 rotation = new Vector3 (UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f));

				GameObject fsided = ((GameObject)PhotonNetwork.Instantiate ("FourSidedDie", position, Quaternion.Euler (rotation), 0));
				fsided.GetComponent<Rigidbody> ().AddTorque (UnityEngine.Random.Range (150f, 280f), UnityEngine.Random.Range (10f, 32f), UnityEngine.Random.Range (160f, 290f));


				if (rollDiceForAttack && (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_4") || (players [currentPlayerIndex].PlayerCharacter == "Valkyria" && players [currentPlayerIndex].AbilityActivated))) {

						return;

				}

		
				Vector3 position2 = new Vector3 (0f, 10.5f, 0.42f);
				Vector3 rotation2 = new Vector3 (UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f), UnityEngine.Random.Range (0f, 360f));
				GameObject ssided = ((GameObject)PhotonNetwork.Instantiate ("SixSidedDie", position2, Quaternion.Euler (rotation2), 0));
				ssided.GetComponent<Rigidbody> ().AddTorque (UnityEngine.Random.Range (150f, 280f), UnityEngine.Random.Range (10f, 32f), UnityEngine.Random.Range (160f, 290f));
		
		}



	
		void generatePlayers ()
		{

				// generate players in the list
				foreach (string NameWithID in NetworkManager.instance.playerNames) {
						if (NameWithID != PhotonNetwork.player.name) {
								Debug.Log ("namewithid: " + NameWithID);
								string[] splitArray = NameWithID.Split (new char[]{ '_' }); //Here we're passing the splitted string to array by that char

								string name = splitArray [0]; //Here we assign the first part to the name

								string ID = splitArray [1]; //Here we assing the second part to the ID


								GameObject PlayerEntry;
								PlayerEntry = Instantiate (playerEntryPrefab) as GameObject;
								PlayerEntry.transform.SetParent (playerScrollContain.transform);

								PlayerEntry.transform.Find ("PlayerName").GetComponent<Text> ().text = name;
								PlayerEntry.transform.Find ("PlayerID").GetComponent<Text> ().text = ID;


								FB.API (Util.GetPictureURL (ID.ToString (), 50, 50), HttpMethod.GET, delegate(IGraphResult pictureResult) {
										if (pictureResult.Error != null) { // in case there was an error
												Debug.Log (pictureResult.Error);
										} else { //we got the image

												Image PlayerAvatar = PlayerEntry.transform.Find ("PlayerAvatar").GetComponent<Image> (); 
												PlayerAvatar.sprite = Sprite.Create (pictureResult.Texture, new Rect (0, 0, 50, 50), new Vector2 (0, 0));
										}
								});	
						}

				}






				// generate pionia kai hp pionia
				ExitGames.Client.Photon.Hashtable hashtable = PhotonNetwork.player.customProperties;

				List<string> tempNames = NetworkManager.instance.playerNames;
				for (int i = 0; i < NetworkManager.instance.playerNames.Count; i++) {
						if (hashtable ["PlayerName"].ToString () == tempNames [i]) {
								GameObject PGO = PhotonNetwork.Instantiate ("PlayerPrefab", new Vector3 (spawnSpots [i].position.x, 0.5f, 2.67f), Quaternion.Euler (new Vector3 (0, 0, 0)), 0);
								PGO.GetComponent<UserPlayer> ().PlName = hashtable ["PlayerName"].ToString ();
								ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
								customProperties.Add ("PlayerColor", playerColor [i]);
								PhotonNetwork.player.SetCustomProperties (customProperties);

								Vector3 HPPosition = HPSpots [i].transform.Find ("0hp").position;

								GameObject HPGO = PhotonNetwork.Instantiate ("HPPrefab", new Vector3 (HPPosition.x, HPPosition.y, HPPosition.z), Quaternion.Euler (new Vector3 (-90, -180, 0)), 0);


								HPGO.GetComponent<HPPlayer> ().HPName = hashtable ["PlayerName"].ToString ();
								HPGO.GetComponent<HPPlayer> ().moveDestination = HPPosition;

								PGO.GetComponent<UserPlayer> ().PlName = hashtable ["PlayerName"].ToString ();
						}
				}


		


				StartCoroutine (HandlePlayerNameAndColor (3, hashtable ["PlayerName"].ToString ()));


		}

		public void HandleDmgPositionForPlayer (string NameAndID, string nameOfDamagerAndID)
		{


				for (int i = 0; i < HPplayers.Count; i++) {
						if (HPplayers [i].HPName == NameAndID) {

								foreach (Player pl in players) {
										if (pl.PlName == HPplayers [i].HPName) {

												if (pl.DamagePoints >= pl.DiesAt) {
														HPplayers [i].moveDestination = HPSpots [i].transform.Find (pl.DiesAt.ToString () + "hp").position;	
														DisplayGameOverText (pl.PlName);
														StartCoroutine (PlayerGameOver (pl, nameOfDamagerAndID));




														foreach (Player damager in players) {
																if (damager.PlName == nameOfDamagerAndID) {
																		if (damager.eksoplismoi.Contains ("Blues_7") || (players [currentPlayerIndex].PlayerCharacter == "Klerry" && players [currentPlayerIndex].AbilityActivated)) {
																				// o damager exei sakidio opote kane steal oles tis kartes tou dead player

																				string[] Owner = pl.PlName.Split (new char[]{ '_' });
																				string OwnerName = Owner [0];
																				string OwnerID = Owner [1];

																				string[] Killer = nameOfDamagerAndID.Split (new char[]{ '_' });
																				string KillerName = Killer [0];
																				string KillerID = Killer [1];


																				foreach (String card in pl.eksoplismoi) {

																						string[] Card = card.Split (new char[]{ '_' });
																						string cardtype = Card [0];
																						string cardID = card;


																						if (PhotonNetwork.player.isMasterClient) {
																								// run this only once from the master
																								GetComponent<PhotonView> ().RPC ("RemoveEquipCardFromPlayer_RPC", PhotonTargets.AllBuffered, cardtype, cardID, OwnerName, OwnerID);

																								GetComponent<PhotonView> ().RPC ("AddEquipCardToPlayerNetwork_RPC", PhotonTargets.AllBuffered, cardtype, cardID, KillerName, KillerID);
																						}

																				}



																		}
																}
														}





												} else {
														HPplayers [i].moveDestination = HPSpots [i].transform.Find (pl.DamagePoints.ToString () + "hp").position;	
												}
					
										}
								}
						}
				}


		}

		public void HandleDmgPositionForPlayer (string NameAndID)
		{


				for (int i = 0; i < HPplayers.Count; i++) {
						if (HPplayers [i].HPName == NameAndID) {

								foreach (Player pl in players) {
										if (pl.PlName == HPplayers [i].HPName) {

												if (pl.DamagePoints >= pl.DiesAt) {
														HPplayers [i].moveDestination = HPSpots [i].transform.Find (pl.DiesAt.ToString () + "hp").position;	
														DisplayGameOverText (pl.PlName);
														StartCoroutine (PlayerGameOver (pl, null));

												} else {
														HPplayers [i].moveDestination = HPSpots [i].transform.Find (pl.DamagePoints.ToString () + "hp").position;	
												}

										}
								}
						}
				}


		}

		public void DisplayGameOverText (string name)
		{
		
				deadPlayerPanel.SetActive (true);
				string[] splitArray = name.Split (new char[]{ '_' }); 
				string onlyname = splitArray [0];

				if (name == PhotonNetwork.player.name) {

						deadPlayerPanel.transform.Find ("DeadText").GetComponent<Text> ().text = "You are DEAD!!!";
				} else {

						deadPlayerPanel.transform.Find ("DeadText").GetComponent<Text> ().text = onlyname + " is DEAD!!!";	
				}

				StartCoroutine (HideDeadPlayerPanel (2f));
		}

		public IEnumerator HideDeadPlayerPanel (float duration)
		{

				yield return new WaitForSeconds (duration);

				// code
				deadPlayerPanel.SetActive (false);	


				yield break;


		}

		public void CheckIfGlobalGameOver ()
		{
				Debug.Log ("check if global gameover");
				Debug.Log ("dead players count: " + deadPlayers.Count);
				List <Winner> winners = new List<Winner> ();

				int LycansAlive = 0;
				int LycansDead = 0;
				int VampsAlive = 0;
				int VampsDead = 0;
				int HumansAlive = 0;
				int HumansDead = 0;

				// check if chloe is the first to die
				if (deadPlayers.Count == 1) {
			
						if (deadPlayers [0].PlayerCharacter == "Xloey") {

								// "Chloe (Human) WINS"
								UserPlayer chloe = getPlayerWithCharName ("Xloey");
								Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

								winners.Add (chloeWinner);
								GlobalGameOver (winners);
								return;
						}
				}

				// check if raphael is the first to die
				if (deadPlayers.Count == 1) {
						if (deadPlayers [0].PlayerCharacter == "Raphael") {

								// "Raphael (Human) WINS"
								UserPlayer raphael = getPlayerWithCharName ("Raphael");
								Winner raphaelWinner = new Winner (raphael.PlName, raphael.PlayerCharacter, raphael.PlayerColor, raphael.PlayerRace);

								winners.Add (raphaelWinner);
								GlobalGameOver (winners);
								return;
						}
				}



				foreach (UserPlayer pl in players) {
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

				foreach (UserPlayer dpl in deadPlayers) {
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

				Debug.Log ("CheckIfGlobalGameOver game: Lycans Dead = " + LycansDead + ", Vamps Dead = " + VampsDead + ", Humans Dead = " + HumansDead + " NumOfPlayers: " + numberOfPlayers + " dead players: " + deadPlayers.Count.ToString ());



				// Check if Klerry just won and died at the same time
				foreach (UserPlayer deadpl in deadPlayers) {
						if (deadpl.PlayerCharacter == "Klerry" && deadpl.eksoplismoi.Count >= 4) {
								Winner klerrywinner = new Winner (deadpl.PlName, deadpl.PlayerCharacter, deadpl.PlayerColor, deadpl.PlayerRace);
								winners.Add (klerrywinner);

						}
				}


				bool ClaudiosKilled12less = false;
				// check if claudios killed someone with 12 or less 
				foreach (UserPlayer deadpl in deadPlayers) {
						if (HumansAlive == 1 && deadpl.killedBy == "Klaudios" && deadpl.DiesAt <= 12) {
								// check if claudios also gave win to other players
								ClaudiosKilled12less = true;
						}
				}


				// check if claudios killed someone with 13 or higher TODO: add exceptions checks and Debug.LogWarning later
				foreach (UserPlayer deadpl in deadPlayers) {
						if (ClaudiosKilled12less == false) {
								if (HumansAlive == 1 && deadpl.killedBy == "Klaudios" && deadpl.DiesAt >= 13) {
										// check if claudios also gave win to other players
										//  numberofplayers 2 and 4 means no humans therefore no claudios

										if ((numberOfPlayers + deadPlayers.Count) == 3) { 
												UserPlayer claudios = getPlayerWithCharName ("Klaudios");
												Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

												winners.Add (claudiosWinner);

												if (VampsDead == 1) {

														// "Claudios (Human) and the Lycans WIN"

														List <UserPlayer> lycans = new List<UserPlayer> ();
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
												if (LycansDead == 1) {

														// "Claudios (Human) and the Vamps WIN"
														List <UserPlayer> vampz = new List<UserPlayer> ();
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
										if ((numberOfPlayers + deadPlayers.Count) == 5) {  
												UserPlayer claudios = getPlayerWithCharName ("Klaudios");
												Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

												winners.Add (claudiosWinner);

												if (VampsDead == 1 && LycansDead == 1) {
														// means he killed one and there is still one alive

														// "Claudios (Human) WIN"
														GlobalGameOver (winners);
														return;
												}
												if (VampsDead == 2) {

														// "Claudios (Human) and the Lycans WIN"
														List <UserPlayer> lycans = new List<UserPlayer> ();
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
							
														// "Claudios (Human) and the Vamps WIN"
														List <UserPlayer> vampz = new List<UserPlayer> ();
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


				// check for stantar Lycan vs Vamp cases

				if ((numberOfPlayers + deadPlayers.Count) == 2) {
						// there are only vampires and lycans, check who is dead and who is alive, if all dead then its draw (probably never gonna happen)

						if (deadPlayers.Count > 0) {
				
								if (VampsDead > 0) {
										if (VampsDead == 1) {

												// "Lycans WIN"
												List <UserPlayer> lycans = new List<UserPlayer> ();
												lycans = getPlayersWithRace ("Lycan");
												foreach (UserPlayer lyc in lycans) {
														Winner temp = new Winner (lyc.PlName, lyc.PlayerCharacter, lyc.PlayerColor, lyc.PlayerRace);
														if (lyc.killedBy != "") {
																temp.isDead = true;
														}
														winners.Add (temp);
												}

												GlobalGameOver (winners);
										} else if (VampsDead > 1) {
												Debug.LogWarning ("Number of players is 2, and we have more that 1 dead vampires!!");
										}
								}
								if (LycansDead > 0) {
										if (LycansDead == 1) {

												// "Vamps WIN"
												List <UserPlayer> vampz = new List<UserPlayer> ();
												vampz = getPlayersWithRace ("Vamp");
												foreach (UserPlayer vmp in vampz) {
														Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
														if (vmp.killedBy != "") {
																temp.isDead = true;
														}
														winners.Add (temp);
												}

												GlobalGameOver (winners);
										} else if (LycansDead > 1) {
												Debug.LogWarning ("Number of players is 2, and we have more that 1 dead lycans!!");
										}
								}

						}

				
				}

				if ((numberOfPlayers + deadPlayers.Count) == 3) {

						if (deadPlayers.Count > 0) {
								if (deadPlayers.Count != numberOfPlayers) {
										// not draw, check if all opossites are dead


										if (VampsDead > 0) {
												if (VampsDead == 1) {
							
														List <UserPlayer> lycans = new List<UserPlayer> ();
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


																				UserPlayer anta = getPlayerWithCharName ("Anta");
																				Winner antaWinner = new Winner (anta.PlName, anta.PlayerCharacter, anta.PlayerColor, anta.PlayerRace);

																				winners.Add (antaWinner);

																				// "Lycans and Anta (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Raphael") {

																				UserPlayer raphael = getPlayerWithCharName ("Raphael");
																				Winner raphaelWinner = new Winner (raphael.PlName, raphael.PlayerCharacter, raphael.PlayerColor, raphael.PlayerRace);

																				winners.Add (raphaelWinner);

																				// "Lycans and Raphael (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Klaudios" && pl.destinationText == "10") { 

																				UserPlayer claudios = getPlayerWithCharName ("Klaudios");
																				Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

																				winners.Add (claudiosWinner);
										
																				// "Lycans and Claudios (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Xloey") {
																				UserPlayer chloe = getPlayerWithCharName ("Xloey");
																				Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

																				winners.Add (chloeWinner);

																				// "Lycans and Chloe (Human) WIN"
																				GlobalGameOver (winners);
																		}
																}

														} else if (HumansAlive == 0) {
																// humans are all dead so only Lycans remain

																// "Lycans WIN" already stored from before 
																GlobalGameOver (winners);
														} else if (HumansAlive > 1) {
																Debug.Log ("HumansAlive should be 1 or 0!! please debug");
														}

												} else if (VampsDead > 1) {
														Debug.LogWarning ("Number of players is 3, and we have more that 1 dead vampires!!");
												}
										}


										if (LycansDead > 0) {
												if (LycansDead == 1) {
														List <UserPlayer> vampz = new List<UserPlayer> ();
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
																				UserPlayer anta = getPlayerWithCharName ("Anta");
																				Winner antaWinner = new Winner (anta.PlName, anta.PlayerCharacter, anta.PlayerColor, anta.PlayerRace);

																				winners.Add (antaWinner);

																				// "Vamp and Anta (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Klaudios" && pl.destinationText == "10") {
																				UserPlayer claudios = getPlayerWithCharName ("Klaudios");
																				Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

																				winners.Add (claudiosWinner);

																				// "Vamp and Claudios (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Xloey") {
																				UserPlayer chloe = getPlayerWithCharName ("Xloey");
																				Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

																				winners.Add (chloeWinner);

																				// "Vamp and Chloe (Human) WIN"
																				GlobalGameOver (winners);
																		}
																}
														} else if (HumansAlive == 0) {
																// humans are all dead so only Vamps remain

																// "Vamps WIN"

																GlobalGameOver (winners);
														} else if (HumansAlive > 1) {
																Debug.Log ("HumansAlive should be 1 or 0!! please debug");
														}
												} else if (LycansDead > 1) {
														Debug.LogWarning ("Number of players is 3, and we have more that 1 dead lycans!!");
												}
										}
								} else {
										// draw!! --impossible (almost)!!
										// TODO: DRAW METHOD AND FINISH GAME

										Debug.Log ("Lycan-Vamp DRAW, impliment draw method");
								}


						}
		
				}
				if ((numberOfPlayers + deadPlayers.Count) == 4) {

						if (deadPlayers.Count != (numberOfPlayers + deadPlayers.Count)) {
								// not draw, check if all opossites are dead
								if (VampsDead > 0) {
										if (VampsDead == 2) {

												// "Lycans WIN"
												List <UserPlayer> lycans = new List<UserPlayer> ();
												lycans = getPlayersWithRace ("Lycan");
												foreach (UserPlayer lyc in lycans) {
														Winner temp = new Winner (lyc.PlName, lyc.PlayerCharacter, lyc.PlayerColor, lyc.PlayerRace);
														if (lyc.killedBy != "") {
																temp.isDead = true;
														}
														winners.Add (temp);
												}
												GlobalGameOver (winners);
										} else if (VampsDead > 2) {
												Debug.LogWarning ("Number of players is 4, and we have more that 2 dead vampires!!");
										}
								}
								if (LycansDead > 0) {
										if (LycansDead == 2) {

												// "Vamps WIN"
												List <UserPlayer> vampz = new List<UserPlayer> ();
												vampz = getPlayersWithRace ("Vamp");
												foreach (UserPlayer vmp in vampz) {
														Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
														if (vmp.killedBy != "") {
																temp.isDead = true;
														}
														winners.Add (temp);
												}
												GlobalGameOver (winners);
										} else if (LycansDead > 2) {
												Debug.LogWarning ("Number of players is 4, and we have more that 2 dead lycans!!");
										}
								}

						} else {
								// draw!! --impossible (almost)!!
								// TODO: DRAW METHOD AND FINISH GAME

								Debug.Log ("Lycan-Vamp DRAW, impliment draw method");
						}


				}
				if ((numberOfPlayers + deadPlayers.Count) == 5) {
						if (deadPlayers.Count > 0) {
								if (deadPlayers.Count != (numberOfPlayers + deadPlayers.Count)) {
										// not draw, check if all opossites are dead


										if (VampsDead > 0) {
												if (VampsDead == 2) {
														List <UserPlayer> lycans = new List<UserPlayer> ();
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


																				UserPlayer anta = getPlayerWithCharName ("Anta");
																				Winner antaWinner = new Winner (anta.PlName, anta.PlayerCharacter, anta.PlayerColor, anta.PlayerRace);

																				winners.Add (antaWinner);

																				// "Lycans and Anta (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Raphael") {

																				UserPlayer raphael = getPlayerWithCharName ("Raphael");
																				Winner raphaelWinner = new Winner (raphael.PlName, raphael.PlayerCharacter, raphael.PlayerColor, raphael.PlayerRace);

																				winners.Add (raphaelWinner);

																				// "Lycans and Raphael (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Klaudios" && pl.destinationText == "10") { 

																				UserPlayer claudios = getPlayerWithCharName ("Klaudios");
																				Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

																				winners.Add (claudiosWinner);

																				// "Lycans and Claudios (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Xloey") {
																				UserPlayer chloe = getPlayerWithCharName ("Xloey");
																				Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

																				winners.Add (chloeWinner);

																				// "Lycans and Chloe (Human) WIN"
																				GlobalGameOver (winners);
																		}



																}
														} else if (HumansAlive == 0) {
																// humans are all dead so only Lycans remain

																// "Lycans WIN"

																GlobalGameOver (winners);
														} else if (HumansAlive > 1) {
																Debug.Log ("HumansAlive should be 1 or 0!! please debug");
														}

												} else if (VampsDead > 2) {
														Debug.LogWarning ("Number of players is 5, and we have more that 2 dead vampires!!");
												}
										}


										if (LycansDead > 0) {
												if (LycansDead == 2) {
														if (HumansAlive == 1) {
																foreach (UserPlayer pl in players) {




																		if (pl.PlayerCharacter == "Anta") {
																				UserPlayer anta = getPlayerWithCharName ("Anta");
																				Winner antaWinner = new Winner (anta.PlName, anta.PlayerCharacter, anta.PlayerColor, anta.PlayerRace);

																				winners.Add (antaWinner);

																				// "Vamp and Anta (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Klaudios" && pl.destinationText == "10") {
																				UserPlayer claudios = getPlayerWithCharName ("Klaudios");
																				Winner claudiosWinner = new Winner (claudios.PlName, claudios.PlayerCharacter, claudios.PlayerColor, claudios.PlayerRace);

																				winners.Add (claudiosWinner);

																				// "Vamp and Claudios (Human) WIN"
																				GlobalGameOver (winners);
																		}
																		if (pl.PlayerCharacter == "Xloey") {
																				UserPlayer chloe = getPlayerWithCharName ("Xloey");
																				Winner chloeWinner = new Winner (chloe.PlName, chloe.PlayerCharacter, chloe.PlayerColor, chloe.PlayerRace);

																				winners.Add (chloeWinner);

																				// "Vamp and Chloe (Human) WIN"
																				GlobalGameOver (winners);
																		}



																}
														} else if (HumansAlive == 0) {
																// humans are all dead so only Vamps remain

																// "Vamps WIN"
																List <UserPlayer> vampz = new List<UserPlayer> ();
																vampz = getPlayersWithRace ("Vamp");
																foreach (UserPlayer vmp in vampz) {
																		Winner temp = new Winner (vmp.PlName, vmp.PlayerCharacter, vmp.PlayerColor, vmp.PlayerRace);
																		if (vmp.killedBy != "") {
																				temp.isDead = true;
																		}
																		winners.Add (temp);
																}
																GlobalGameOver (winners);
														} else if (HumansAlive > 1) {
																Debug.Log ("HumansAlive should be 1 or 0!! please debug");
														}
												} else if (LycansDead > 1) {
														Debug.LogWarning ("Number of players is 3, and we have more that 1 dead lycans!!");
												}
										}
								} else {
										// draw!! --impossible (almost)!!
										// TODO: DRAW METHOD AND FINISH GAME

										Debug.Log ("Lycan-Vamp DRAW, impliment draw method");
								}


						}

				}

				if (winners.Count > 0 && !gameOverPanel.activeSelf) {
						// there are winners but for some reason there is no game over
						GlobalGameOver (winners);

				}
		}

		public UserPlayer getPlayerWithCharName (string charName)
		{
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

		public List<UserPlayer> getPlayersWithRace (string race)
		{
				List <UserPlayer> result = new List<UserPlayer> ();

				foreach (UserPlayer pl in players) {
						if (pl.PlayerRace == race) {
								result.Add (pl);
						}
				}

				foreach (UserPlayer dpl in deadPlayers) {
						if (dpl.PlayerRace == race) {
								result.Add (dpl);

						}
				}

				return result;

		}


		public void GlobalGameOver (List<Winner> winners)
		{
				Debug.Log ("GLOBAL GAME OVER RUN");
				gameOver = true;

				DisableAllButtons ();

				gameOverPanel.SetActive (true);

				if (winners.Count == 1) {
						gameOverPanel.transform.Find ("WinText").GetComponent<Text> ().text = "Winner:";
				} 

				foreach (Winner w in winners) {
						GameObject WinnerEntry = Instantiate (endGamePlayerEntry) as GameObject;
						WinnerEntry.transform.SetParent (gameOverPanel.transform.Find ("Contain").transform);

						string[] splitArray = w.PlName.Split (new char[]{ '_' });

						string PlayerName = splitArray [0];
						string PlayerID = splitArray [1];

						WinnerEntry.transform.Find ("PlayerNamePanel").Find ("CharacterName").GetComponent<Text> ().text = w.PlayerCharacter;
						WinnerEntry.transform.Find ("Text").GetComponent<Text> ().text = PlayerName;

						if (w.PlayerRace == "Human") {
								WinnerEntry.transform.Find ("PlayerNamePanel").GetComponent<Image> ().color = Color.yellow;
						} else if (w.PlayerRace == "Lycan") {
								WinnerEntry.transform.Find ("PlayerNamePanel").GetComponent<Image> ().color = Color.blue;
						} else if (w.PlayerRace == "Vamp") {
								WinnerEntry.transform.Find ("PlayerNamePanel").GetComponent<Image> ().color = Color.red;
						}
				
						Color c = WinnerEntry.transform.Find ("PlayerNamePanel").GetComponent<Image> ().color;
						c.a = 100f;
						WinnerEntry.transform.Find ("PlayerNamePanel").GetComponent<Image> ().color = c;

						FB.API (Util.GetPictureURL (PlayerID, 100, 100), HttpMethod.GET, delegate(IGraphResult pictureResult) {
								if (pictureResult.Error != null) { // in case there was an error
										Debug.Log (pictureResult.Error);
								} else { //we got the image
										Image PlayerAvatar = WinnerEntry.transform.Find ("Image").GetComponent<Image> (); 
										PlayerAvatar.sprite = Sprite.Create (pictureResult.Texture, new Rect (0, 0, 100, 100), new Vector2 (0, 0));
								}
						});	

				}



		}

		public IEnumerator HandlePlayerNameAndColor (float duration, string n)
		{
		
				yield return new WaitForSeconds (duration);
		
				// code
				GetComponent<PhotonView> ().RPC ("HandlePlayerNameAndColor_RPC", PhotonTargets.AllBuffered, n);		


				yield break;
		
		
		}

		[PunRPC]
		void HandlePlayerNameAndColor_RPC (string name)
		{

				Debug.Log ("start of method run for: " + name);

				GameObject[] playerGOs = GameObject.FindGameObjectsWithTag ("PioniPlayer");

				for (int i = 0; i < playerGOs.Length; i++) {

						string goName = playerGOs [i].GetComponent<UserPlayer> ().PlName;
						Debug.Log ("go Name: " + goName);
						if (name == goName) {
								Debug.Log ("in frist if loop");
								foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
										ExitGames.Client.Photon.Hashtable h = pl.customProperties;
										Debug.Log (h ["PlayerName"].ToString ());
										if (name == h ["PlayerName"].ToString ()) {
												playerGOs [i].name = name;
												playerGOs [i].GetComponent<UserPlayer> ().PlayerColor = h ["PlayerColor"].ToString ();
												playerGOs [i].GetComponent<UserPlayer> ().PlName = h ["PlayerName"].ToString ();

												// send character stuff
												string[] splitArray = h ["CharacterSpriteName"].ToString ().Split (new char[]{ '_' }); 
												playerGOs [i].GetComponent<UserPlayer> ().PlayerRace = splitArray [0]; 
												playerGOs [i].GetComponent<UserPlayer> ().PlayerCharacter = splitArray [1]; 
												playerGOs [i].GetComponent<UserPlayer> ().DiesAt = int.Parse (splitArray [2]);


												playerGOs [i].GetComponent<UserPlayer> ().CorrectColor ();
												players.Add (playerGOs [i].GetComponent<UserPlayer> ());


										}
								}

						}
				}

				GameObject[] hpGOs = GameObject.FindGameObjectsWithTag ("HPPlayer");

				for (int i = 0; i < hpGOs.Length; i++) {
			
						string hpName = hpGOs [i].GetComponent<HPPlayer> ().HPName;
			 
						if (name == hpName) {
				 
								foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
										ExitGames.Client.Photon.Hashtable h = pl.customProperties;

										if (name == h ["PlayerName"].ToString ()) {
												hpGOs [i].name = name + '#' + "HP";
												hpGOs [i].GetComponent<HPPlayer> ().HPColor = h ["PlayerColor"].ToString ();
												hpGOs [i].GetComponent<HPPlayer> ().HPName = h ["PlayerName"].ToString ();
												hpGOs [i].GetComponent<HPPlayer> ().CorrectColor ();

												HPplayers.Add (hpGOs [i].GetComponent<HPPlayer> ());

										}
								}
				
						}
				}




				foreach (Transform t in playerScrollContain.transform) {
						string plName = t.transform.Find ("PlayerName").GetComponent<Text> ().text;
						string plID = t.transform.Find ("PlayerID").GetComponent<Text> ().text;


						if (name == plName + '_' + plID) {
			
								foreach (PhotonPlayer pl in PhotonNetwork.playerList) {


										ExitGames.Client.Photon.Hashtable h = pl.customProperties;
										h ["PlayerColor"].ToString ();

										if (name == h ["PlayerName"].ToString ()) {
												t.transform.Find ("ColorCircleImage").GetComponent<Image> ().color = GetColorFromString (h ["PlayerColor"].ToString ());
										}


								} 
						}			
				}

		}

		void HandleCharacters ()
		{
				Debug.Log ("Num of players: " + numberOfPlayers);

				if (numberOfPlayers == 2) {

						// select a lycan
						int indexL = UnityEngine.Random.Range (0, lycanSprites.Count);
						Debug.Log ("indexl: " + indexL);
						string LycanSelected = lycanSprites [indexL].name;
						lycanSprites.RemoveAt (indexL);
	
						// select a vamp
						int indexV = UnityEngine.Random.Range (0, vampSprites.Count);
						string VampSelected = vampSprites [indexV].name;
						vampSprites.RemoveAt (indexV);

			
						List<string> CharRoster = new List<string> ();



						CharRoster.Add (LycanSelected);
						CharRoster.Add (VampSelected);


						foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
								int indexR = UnityEngine.Random.Range (0, CharRoster.Count);
				
								ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
								customProperties.Add ("CharacterSpriteName", CharRoster [indexR]);
								pl.SetCustomProperties (customProperties);
				
				
								foreach (Player pl2 in players) {
										if (pl.name == pl2.PlName) {
												pl2.PlayerCharacter = CharRoster [indexR];
										}
								}
								CharRoster.RemoveAt (indexR);
						}

				}

				if (numberOfPlayers == 3) {
			
						// select a human
			
						int indexH = UnityEngine.Random.Range (0, humanSprites.Count);
						string HumanSelected = humanSprites [indexH].name;
						humanSprites.RemoveAt (indexH);
			
						// select a lycan
						int indexL = UnityEngine.Random.Range (0, lycanSprites.Count);
						string LycanSelected = lycanSprites [indexL].name;
						lycanSprites.RemoveAt (indexL);
			
						// select a vamp
						int indexV = UnityEngine.Random.Range (0, vampSprites.Count);
						string VampSelected = vampSprites [indexV].name;
						vampSprites.RemoveAt (indexV);
			
			
						List<string> CharRoster = new List<string> ();
						CharRoster.Add (HumanSelected);
						CharRoster.Add (LycanSelected);
						CharRoster.Add (VampSelected);


// 			this is just testing, DELETE after:

//			foreach (PhotonPlayer pl in PhotonNetwork.playerList){
//				if (pl.name == PhotonNetwork.masterClient.name) {
//					// give ME this champ:
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
			
						foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
								int indexR = UnityEngine.Random.Range (0, CharRoster.Count);
				
								ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
								customProperties.Add ("CharacterSpriteName", CharRoster [indexR]);
								pl.SetCustomProperties (customProperties);
				
				
								foreach (Player pl2 in players) {
										if (pl.name == pl2.PlName) {
												pl2.PlayerCharacter = CharRoster [indexR];
										}
								}
								CharRoster.RemoveAt (indexR);
						}
			
				}
				if (numberOfPlayers == 4) {
			

			
						// select 2 lycans
						int indexL = UnityEngine.Random.Range (0, lycanSprites.Count);
						string LycanSelected = lycanSprites [indexL].name;
						lycanSprites.RemoveAt (indexL);
			
						indexL = UnityEngine.Random.Range (0, lycanSprites.Count);
						string LycanSelected2 = lycanSprites [indexL].name;
						lycanSprites.RemoveAt (indexL);
			
						// select 2 vamps
						int indexV = UnityEngine.Random.Range (0, vampSprites.Count);
						string VampSelected = vampSprites [indexV].name;
						vampSprites.RemoveAt (indexV);
			
						indexV = UnityEngine.Random.Range (0, vampSprites.Count);
						string VampSelected2 = vampSprites [indexV].name;
						lycanSprites.RemoveAt (indexV);
			
						List<string> CharRoster = new List<string> ();

						CharRoster.Add (LycanSelected);
						CharRoster.Add (LycanSelected2);
						CharRoster.Add (VampSelected);
						CharRoster.Add (VampSelected2);
			
						foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
								int indexR = UnityEngine.Random.Range (0, CharRoster.Count);
				
								ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
								customProperties.Add ("CharacterSpriteName", CharRoster [indexR]);
								pl.SetCustomProperties (customProperties);
				
				
								foreach (Player pl2 in players) {
										if (pl.name == pl2.PlName) {
												pl2.PlayerCharacter = CharRoster [indexR];
										}
								}
								CharRoster.RemoveAt (indexR);
						}

			
				}

				if (numberOfPlayers == 5) {

						// select a human
		
						int indexH = UnityEngine.Random.Range (0, humanSprites.Count);
						string HumanSelected = humanSprites [indexH].name;
						humanSprites.RemoveAt (indexH);

						// select 2 lycans
						int indexL = UnityEngine.Random.Range (0, lycanSprites.Count);
						string LycanSelected = lycanSprites [indexL].name;
						lycanSprites.RemoveAt (indexL);

						indexL = UnityEngine.Random.Range (0, lycanSprites.Count);
						string LycanSelected2 = lycanSprites [indexL].name;
						lycanSprites.RemoveAt (indexL);

						// select 2 vamps
						int indexV = UnityEngine.Random.Range (0, vampSprites.Count);
						string VampSelected = vampSprites [indexV].name;
						vampSprites.RemoveAt (indexV);
			
						indexV = UnityEngine.Random.Range (0, vampSprites.Count);
						string VampSelected2 = vampSprites [indexV].name;
						vampSprites.RemoveAt (indexV);

						List<string> CharRoster = new List<string> ();
						CharRoster.Add (HumanSelected);
						CharRoster.Add (LycanSelected);
						CharRoster.Add (LycanSelected2);
						CharRoster.Add (VampSelected);
						CharRoster.Add (VampSelected2);

						foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
								int indexR = UnityEngine.Random.Range (0, CharRoster.Count);

								ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable ();
								customProperties.Add ("CharacterSpriteName", CharRoster [indexR]);
								pl.SetCustomProperties (customProperties);


								foreach (Player pl2 in players) {
										if (pl.name == pl2.PlName) {
												pl2.PlayerCharacter = CharRoster [indexR];
										}
								}
								CharRoster.RemoveAt (indexR);
						}

				}
		}


		public IEnumerator GetMyPlayer (float duration)
		{
		
				yield return new WaitForSeconds (duration);
		
				// code
				ExitGames.Client.Photon.Hashtable plInfo = PhotonNetwork.player.customProperties;

				string myChar = plInfo ["CharacterSpriteName"].ToString ();
				foreach (Sprite spr in charSprites) {
						if (spr.name == myChar) {
				
								myCharCardGO.GetComponent<CardSpriteHandling> ().BackSprite = spr;  //FIXME: change names here, this is the frontsprite now not the back
								// MyCharCardGO.GetComponent<CardSpriteHandling>().Flip();
								myCharCardGO.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
						}	
				}


				// get the opponents chars

				foreach (PhotonPlayer opl in PhotonNetwork.playerList) {
						ExitGames.Client.Photon.Hashtable otherplInfo = opl.customProperties;

						string thisplayersChar = otherplInfo ["CharacterSpriteName"].ToString ();
						foreach (Sprite spr in charSprites) {
								if (spr.name == thisplayersChar) {

										// find playerentry in playerlist
										foreach (Transform t in playerScrollContain.transform) {
												string plEntryName = t.transform.Find ("PlayerName").GetComponent<Text> ().text;
												string plEntryID = t.transform.Find ("PlayerID").GetComponent<Text> ().text;

												if (opl.name == plEntryName + '_' + plEntryID) {


														t.transform.Find ("PlayerCharCard").GetComponent<CardSpriteHandling> ().BackSprite = spr;  //FIXME: change names here, this is the frontsprite now not the back
														// t.transform.FindChild("PlayerCharCard").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

												}
										}

								}

						}	

				}



 
		
				yield break;
		
		
		}

		public IEnumerator RemoveLoadingScreenIn (float duration)
		{
		
				yield return new WaitForSeconds (duration);
		
				// code

				loadingPanel.SetActive (false);
		
				yield break;
		
		
		}

		public IEnumerator PlayerGameOver (Player plr, string damagerNameAndID)
		{

				yield return new WaitForSeconds (2f);

				// code
				plr.killedBy = damagerNameAndID;

				deadPlayers.Add (plr);
				Player deadplayer = plr;


				foreach (Player p in players) {

						// a player died so increase the text for the rounds to mentour's ability
						if (p.PlName == PhotonNetwork.player.name) {
								if (p.PlayerCharacter == "Mentour") {
										activateAbilityButton.GetComponentInChildren<Text> ().text = "Activate, Extra Rounds: " + deadPlayers.Count.ToString ();
										if (deadPlayers.Count == 1 && p.PlName == players [currentPlayerIndex].PlName && !p.AbilityDisabled && p.AbilityActivated) {
												//  the button is deactivated so activate it if this is mentour'r turn
												activateAbilityButton.interactable = true;
										}
								}
						}

						// klaudios ability:
						if (plr.killedBy == p.PlName) {
				
								if (p.PlayerCharacter == "Klaudios" && plr.DiesAt <= 12) {
										p.isRevealed = true;
										p.AbilityActivated = true;
										p.AbilityDisabled = true;

										if (PhotonNetwork.player.name == p.name) {
												revealCharacterButton.interactable = false;
										} else {
												string[] splitArray = p.PlName.Split (new char[]{ '_' });
												string playername = splitArray [0];
												string playerID = splitArray [1];

												foreach (Transform t in playerScrollContain.transform) {

														if (t.Find ("PlayerName").GetComponent<Text> ().text == playername && t.Find ("PlayerID").GetComponent<Text> ().text == playerID) {
																t.transform.Find ("PlayerCharCard").transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
																// TODO itween instead of regular rotation change


														} else {
																Debug.Log ("ERROR: could not find the player in scroll contain, playername is: " + t.Find ("PlayerName").GetComponent<Text> ().text + " player ID is: " + t.Find ("PlayerID").GetComponent<Text> ().text);
														}
												}
										}
								} 
						}

						// raphaelability
						if (p.PlayerCharacter == "Raphael") {

								p.isRevealed = true;
								p.AbilityActivated = true;
								p.AbilityDisabled = true;

								if (PhotonNetwork.player.name == p.name) {
										revealCharacterButton.interactable = false;
								} else {
										string[] splitArray = p.PlName.Split (new char[]{ '_' });
										string playername = splitArray [0];
										string playerID = splitArray [1];

										foreach (Transform t in playerScrollContain.transform) {

												if (t.Find ("PlayerName").GetComponent<Text> ().text == playername && t.Find ("PlayerID").GetComponent<Text> ().text == playerID) {
														t.transform.Find ("PlayerCharCard").transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
														// TODO itween instead of regular rotation change


												} else {
														Debug.Log ("ERROR: could not find the player in scroll contain");
												}
										}
								}
						}
				}

				Debug.Log ("trying to run player game over for player: " + plr.PlName);

				foreach (Transform t in playerScrollContain.transform) {
						string toFind = t.transform.Find ("PlayerName").GetComponent<Text> ().text + "_" + t.transform.Find ("PlayerID").GetComponent<Text> ().text;
						Debug.Log ("to find: " + toFind + " deadplayer: " + plr.name);
						if (toFind == plr.name) {

								t.transform.Find ("DmgPointsText").GetComponent<Text> ().enabled = false;
								t.transform.Find ("dmgintrotxt").GetComponent<Text> ().enabled = false;
								t.transform.Find ("PlayerDeadText").GetComponent<Text> ().enabled = true;
						}

				}

				GameObject[] pionia = GameObject.FindGameObjectsWithTag ("PioniPlayer");
				foreach (GameObject p in pionia) {
						if (p.name == plr.PlName) {

								p.name = "DEAD " + p.name;

								Destroy (p.GetComponent<MeshRenderer> ());
								Destroy (p.GetComponent<MeshFilter> ());
								Destroy (p.GetComponent<CapsuleCollider> ());
								p.transform.position = new Vector3 (0, -1, 0);
						}
				}


				foreach (HPPlayer hpp in HPplayers) {
						Debug.Log ("hpname : " + hpp.HPName + ", hpcolor: " + hpp.HPColor + " // playername : " + plr.PlName + ", plcolor: " + plr.PlayerColor);
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


				numberOfPlayers--;

				CheckIfGlobalGameOver ();

				if (PhotonNetwork.player.name == deadplayer.PlName && diedinhisturn) {
						EndTurnClicked ();
				}

				// EndTurnClicked ();

				yield break;


		}





		void generateCards ()
		{

				// GENERATING GREEN CARDS
				GameObject firstgreencard;
				firstgreencard = Instantiate (greenPrefab) as GameObject;
				firstgreencard.transform.SetParent (GameObject.Find ("GreenCardsHolder").transform, false);

				firstgreencard.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (firstgreencard.GetComponent<RectTransform> ().anchoredPosition.x, firstgreencard.GetComponent<RectTransform> ().anchoredPosition.y, 0);


				firstgreencard.GetComponent<Button> ().onClick.AddListener (() => firstgreencard.GetComponent<GreenCardScript> ().CardClicked ());
				firstgreencard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

				firstgreencard.GetComponent<GreenCardScript> ().GreenCardID = greenSprites [0].name;
				firstgreencard.GetComponent<CardSpriteHandling> ().FrontSprite = greenSprites [0];
				firstgreencard.GetComponent<CardSpriteHandling> ().Flip ();
				greenCards.Add (firstgreencard);

				GameObject GCard;
				for (int i = 1; i < greenSprites.Count; i++) {
						float temp = greenCards [i - 1].transform.position.z + 0.045f;

						GCard = Instantiate (greenPrefab) as GameObject;
						GCard.transform.SetParent (GameObject.Find ("GreenCardsHolder").transform, false);

						GCard.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (GCard.GetComponent<RectTransform> ().anchoredPosition.x, GCard.GetComponent<RectTransform> ().anchoredPosition.y, temp);


						// GCard.GetComponent<Button>().onClick.AddListener(() => GCard.GetComponent<GreenCardScript>().CardClicked());
						GCard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

						GCard.GetComponent<GreenCardScript> ().GreenCardID = greenSprites [i].name;
						GCard.GetComponent<CardSpriteHandling> ().FrontSprite = greenSprites [i];
						GCard.GetComponent<CardSpriteHandling> ().Flip ();
						greenCards.Add (GCard);
				}

				// GENERATING RED CARDS
				GameObject firstredcard;
				firstredcard = Instantiate (redPrefab) as GameObject;
				firstredcard.transform.SetParent (GameObject.Find ("RedCardsHolder").transform, false);
		
				firstredcard.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (firstredcard.GetComponent<RectTransform> ().anchoredPosition.x, firstredcard.GetComponent<RectTransform> ().anchoredPosition.y, 0);
		
		
				firstredcard.GetComponent<Button> ().onClick.AddListener (() => firstredcard.GetComponent<RedCardScript> ().CardClicked ());
				firstredcard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

				string[] fredSplitArray = redSprites [0].name.Split (new char[]{ '#' }); 
				firstredcard.GetComponent<RedCardScript> ().RedCardID = fredSplitArray [0];
				firstredcard.GetComponent<RedCardScript> ().type = fredSplitArray [1];

				firstredcard.GetComponent<CardSpriteHandling> ().FrontSprite = redSprites [0];
				firstredcard.GetComponent<CardSpriteHandling> ().Flip ();
				redCards.Add (firstredcard);
		
				GameObject RCard;
				for (int i = 1; i < redSprites.Count; i++) {
						float temp = redCards [i - 1].transform.position.z + 0.045f;
			
						RCard = Instantiate (redPrefab) as GameObject;
						RCard.transform.SetParent (GameObject.Find ("RedCardsHolder").transform, false);
			
						RCard.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (RCard.GetComponent<RectTransform> ().anchoredPosition.x, RCard.GetComponent<RectTransform> ().anchoredPosition.y, temp);

			
						// RCard.GetComponent<Button>().onClick.AddListener(() => RCard.GetComponent<RedCardScript>().CardClicked());
						RCard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

						string[] redSplitArray = redSprites [i].name.Split (new char[]{ '#' }); 
						RCard.GetComponent<RedCardScript> ().RedCardID = redSplitArray [0];
						RCard.GetComponent<RedCardScript> ().type = redSplitArray [1];

						RCard.GetComponent<CardSpriteHandling> ().FrontSprite = redSprites [i];
						RCard.GetComponent<CardSpriteHandling> ().Flip ();
						redCards.Add (RCard);
				}


				// GENERATING BLUE CARDS
				GameObject firstbluecard;
				firstbluecard = Instantiate (bluePrefab) as GameObject;
				firstbluecard.transform.SetParent (GameObject.Find ("BlueCardsHolder").transform, false);
		
				firstbluecard.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (firstbluecard.GetComponent<RectTransform> ().anchoredPosition.x, firstbluecard.GetComponent<RectTransform> ().anchoredPosition.y, 0);
		
		
				firstbluecard.GetComponent<Button> ().onClick.AddListener (() => firstbluecard.GetComponent<BlueCardScript> ().CardClicked ());
				firstbluecard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));

				string[] fblueSplitArray = blueSprites [0].name.Split (new char[]{ '#' }); 
				firstbluecard.GetComponent<BlueCardScript> ().BlueCardID = fblueSplitArray [0];
				firstbluecard.GetComponent<BlueCardScript> ().type = fblueSplitArray [1];

				firstbluecard.GetComponent<CardSpriteHandling> ().FrontSprite = blueSprites [0];
				firstbluecard.GetComponent<CardSpriteHandling> ().Flip ();
				blueCards.Add (firstbluecard);
		
				GameObject BCard;
				for (int i = 1; i < blueSprites.Count; i++) {
						float temp = blueCards [i - 1].transform.position.z + 0.045f;
			
						BCard = Instantiate (bluePrefab) as GameObject;
						BCard.transform.SetParent (GameObject.Find ("BlueCardsHolder").transform, false);
			
						BCard.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3 (BCard.GetComponent<RectTransform> ().anchoredPosition.x, BCard.GetComponent<RectTransform> ().anchoredPosition.y, temp);
			
			
						// BCard.GetComponent<Button>().onClick.AddListener(() => BCard.GetComponent<BlueCardScript>().CardClicked());
						BCard.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 90));


						string[] blueSplitArray = blueSprites [i].name.Split (new char[]{ '#' }); 
						BCard.GetComponent<BlueCardScript> ().BlueCardID = blueSplitArray [0];
						BCard.GetComponent<BlueCardScript> ().type = blueSplitArray [1];

						BCard.GetComponent<CardSpriteHandling> ().FrontSprite = blueSprites [i];
						BCard.GetComponent<CardSpriteHandling> ().Flip ();
						blueCards.Add (BCard);


				}

		}

		public void AddChatMessage (string m)
		{
				string[] splitArray = PhotonNetwork.player.name.Split (new char[]{ '_' }); 
		
				string ChatName = splitArray [0]; 
		
				GetComponent<PhotonView> ().RPC ("AddChatMessage_RPC", PhotonTargets.AllViaServer, "<b>" + ChatName + "</b>: " + m);
		}

		[PunRPC]
		void AddChatMessage_RPC (string m)
		{
		
				chatText.text = chatText.text + Environment.NewLine + m;
		
		}


		[PunRPC]
		void UnsetCompassFlag_RPC ()
		{
				StartCoroutine (UnsetCompass (2f));
		}

		public IEnumerator UnsetCompass (float duration)
		{

				yield return new WaitForSeconds (duration);



				if (players [currentPlayerIndex].PlName == PhotonNetwork.player.name) {

						endTurnButtonGO.GetComponent<Button> ().interactable = true;

						if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_7")) {
								// this means he has valistra
								if (GetPlayersInOppositeAreas (players [currentPlayerIndex].destinationText) > 0 && (players [currentPlayerIndex].destinationText != "none") && players [currentPlayerIndex].PlName == PhotonNetwork.player.name) { 
										if (!attackStarted) {
												attackButtonGO.GetComponent<Button> ().interactable = true;

										}
								}

						} else {
								// he does not have valistra

								if (GetPlayersInArea (players [currentPlayerIndex].destinationText) > 1 && (players [currentPlayerIndex].destinationText != "none") && players [currentPlayerIndex].PlName == PhotonNetwork.player.name) { 
										if (!attackStarted) {
												attackButtonGO.GetComponent<Button> ().interactable = true;
										}
								}
						}


				}



				moveFromCompass = false;
				waitForDiceDelete = true;  //for some reason it needs that #chaos


				yield break;


		}


		[PunRPC]
		void DestroyDice_RPC ()
		{
				StartCoroutine (DestroyDice (2f));
		}

		public IEnumerator DestroyDice (float duration)
		{


				yield return new WaitForSeconds (duration);

				DestroyDiceWithNoDelay ();

				yield break;


		}

		[PunRPC]
		void DestroyDiceWithNoDealy_RPC ()
		{
				DestroyDiceWithNoDelay ();
		}

		void DestroyDiceWithNoDelay ()
		{
		
				GameObject fs = GameObject.Find ("FourSidedDie(Clone)");
				GameObject ss = GameObject.Find ("SixSidedDie(Clone)");

				if (ss == null) {
						Destroy (fs);
						if (rollForGregoryAbility) {
								rollForGregoryAbility = false;
						}
				} else if (fs == null) {
						Destroy (ss);
						if (rollForAmbush) {
								rollForAmbush = false;
						}
						if (rollForTreatmentFromAfar) {
								rollForTreatmentFromAfar = false;
						}
						if (rollForGregoryAbility) {
								rollForGregoryAbility = false;
						}
				} else {
						Destroy (ss);
						Destroy (fs);
				}
				if (rollDiceForAttack) {
						rollDiceForAttack = false;
				} else {
						// we just destroyed dice for moving
						//  telos tou girou

						if (players [currentPlayerIndex].PlName == PhotonNetwork.player.name) {
								if (players [currentPlayerIndex].PlayerCharacter == "Gandolf" && players [currentPlayerIndex].isRevealed && !players [currentPlayerIndex].AbilityActivated && !players [currentPlayerIndex].AbilityDisabled && !abilityActivatedOnce) {
										activateAbilityButton.interactable = true;

								} else {
										activateAbilityButton.interactable = false;
								}
						}

				}



				if (players [currentPlayerIndex].PlName == PhotonNetwork.player.name) {

						if (!waitingForMalburca) {
								endTurnButtonGO.GetComponent<Button> ().interactable = true;
						}

						if (players [currentPlayerIndex].eksoplismoi.Contains ("Reds_7")) {
								// this means he has valistra
								if (GetPlayersInOppositeAreas (players [currentPlayerIndex].destinationText) > 0 && (players [currentPlayerIndex].destinationText != "none") && players [currentPlayerIndex].PlName == PhotonNetwork.player.name) { 
										if (!attackStarted) {
												attackButtonGO.GetComponent<Button> ().interactable = true;

										}
								}

						} else {
								// he does not have valistra

								if (GetPlayersInArea (players [currentPlayerIndex].destinationText) > 1 && (players [currentPlayerIndex].destinationText != "none") && players [currentPlayerIndex].PlName == PhotonNetwork.player.name) { 
										if (!attackStarted) {
												attackButtonGO.GetComponent<Button> ().interactable = true;
										}
								}
						}


				}



				waitForDiceDelete = true;

		}


		public void PlayAgainClicked ()
		{
				PhotonNetwork.LeaveRoom ();

				SceneManager.LoadScene ("_start");
		}
}
