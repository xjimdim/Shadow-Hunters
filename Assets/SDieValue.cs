using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SDieValue : MonoBehaviour {

	public int currentValue = 0;
	public bool dmgDone = false;
	float timeLeft = 10f;
	// Update is called once per frame

	void Update(){

		//if for any case the dice get stuck: 
		timeLeft -= Time.deltaTime;
		if(timeLeft < 0)
		{
			GameManager.instance.GetComponent<PhotonView> ().RPC ("DestroyDiceWithNoDealy_RPC", PhotonTargets.AllBufferedViaServer, null);
		}
	}
	void  FixedUpdate(){

		if (!GameManager.instance.RollDiceForAttack) {

			if (GameManager.instance.RollForEkriksi) {
				
				//we rolled for ekriksi
				GameObject dieGameObject1 = GameObject.Find("FourSidedDie(Clone)");

				if(dieGameObject1.GetComponent<Rigidbody>().IsSleeping() && GetComponent<Rigidbody>().IsSleeping()){

					FSDieValueNew dieValueComponent1 = dieGameObject1.GetComponent<FSDieValueNew>();

					int fscv = dieValueComponent1.currentValue;

					int total = fscv + currentValue;

					GameManager.instance.DamageAllPlayersAtArea (total.ToString(), 3);  //3 damage points giati etsi leei h ekriksi



				}

			}

			else{

				if (GameManager.instance.rollForEnedra) {

					if (GetComponent<Rigidbody> ().IsSleeping ()) {

						GameManager.instance.EnedraFinalResult (currentValue);
					}
				}
				else if (GameManager.instance.rollForGregoryAbility) {

					if (GetComponent<Rigidbody> ().IsSleeping ()) {

						GameManager.instance.GregoryAbilityFinalResult (currentValue);
					}
				}
				else if(GameManager.instance.RollForTherapiaApoMakria){


					if(GetComponent<Rigidbody>().IsSleeping()){


						int total = currentValue;

						GameManager.instance.DoTherapiaApoMakria (total);  


					}
				}
				else {
					
					// we just rolled to move
					GameObject dieGameObject1 = GameObject.Find("FourSidedDie(Clone)");
					if (dieGameObject1 == null) {
						return;
					}
					if(dieGameObject1.GetComponent<Rigidbody>().IsSleeping() && GetComponent<Rigidbody>().IsSleeping()){

						FSDieValueNew dieValueComponent1 = dieGameObject1.GetComponent<FSDieValueNew>();

						int fscv = dieValueComponent1.currentValue;

						int total = fscv + currentValue;

						Text diceText = GameObject.Find("DiceText").GetComponent<Text>();
						diceText.text = total.ToString(); 

						GameManager.instance.NewMakeMove();


					}
				
				
				}

			}


					
		}
		else{
			if(!dmgDone){
				Text toDoDmgText = null;
				if (GameManager.instance.rollForMalburcaAttack) {
					toDoDmgText = GameObject.Find("MalburcaAttackText").GetComponent<Text>();
				}
				else {
					toDoDmgText = GameObject.Find("DmgToDoText").GetComponent<Text>();
				}


				
				if(toDoDmgText!=null){
					GameObject dieGameObject1 = GameObject.Find("FourSidedDie(Clone)");

					List <string> playereksoplismoi = GameManager.instance.players [GameManager.instance.currentPlayerIndex].eksoplismoi;

					if (!playereksoplismoi.Contains ("Reds_4") || !(GameManager.instance.players[GameManager.instance.currentPlayerIndex].PlayerCharacter == "Valkyria" && GameManager.instance.players[GameManager.instance.currentPlayerIndex].AbilityActivated)) {  // reds 4 simenei pirsos = rixnei mono tetreplevro gia attack. just check if its false

						if(dieGameObject1.GetComponent<Rigidbody>().IsSleeping() && GetComponent<Rigidbody>().IsSleeping()){
							int fscv = dieGameObject1.GetComponent<FSDieValueNew>().currentValue;

							int total = Mathf.Abs (fscv - currentValue);

							if (playereksoplismoi.Contains ("Reds_5")) {
								int counter = 0;
								foreach (string s in playereksoplismoi) {
									if (s == "Reds_5") {
										counter++;
									}
								}


								Text sidereniagrothiatext = GameObject.Find ("IronFistText").GetComponent<Text> ();
								sidereniagrothiatext.enabled = true;
								sidereniagrothiatext.text = "Siderenia grothia bonus: +" + counter.ToString ();


								total = total + counter;
								toDoDmgText.text = total.ToString ();
							}
							if (playereksoplismoi.Contains ("Blues_6")) {
								//exei logxi
								if (GameManager.instance.players [GameManager.instance.currentPlayerIndex].PlayerRace == "Lycan" && GameManager.instance.players [GameManager.instance.currentPlayerIndex].isRevealed) {
									//einai kai likanthropos revealed 

									Text sidereniagrothiatext = GameObject.Find ("IronFistText").GetComponent<Text> ();
									sidereniagrothiatext.enabled = true;
									sidereniagrothiatext.text = "Logxi bonus: +2";
									total = total + 2;
									toDoDmgText.text = total.ToString ();
								}
								else {

									toDoDmgText.text = total.ToString ();
								}
							
							
							}

							if (playereksoplismoi.Contains ("Blues_9")) {

								Text prostateftikosmandiastext = GameObject.Find ("MandiasText").GetComponent<Text> ();
								prostateftikosmandiastext.enabled = false;
								prostateftikosmandiastext.text = "Prostateftikos Mandias nerf: -1";


								total = total - 1;
								toDoDmgText.text = total.ToString ();
							}

							if (!playereksoplismoi.Contains ("Reds_5") && !playereksoplismoi.Contains ("Blues_6")) {
								//if players has no equip cards that can change the dmg
							
								toDoDmgText.text = total.ToString(); 
							}


							GameManager.instance.DamagePointsCreated = true;	
							if (GameManager.instance.rollForMalburcaAttack) {
								GameManager.instance.MalburcaDoDamage (toDoDmgText.text);
							}
							GameManager.instance.NewMakeMove();

							dmgDone=true;
						}
					
					}
				}
			}



		}


	}
}
