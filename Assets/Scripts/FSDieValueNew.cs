using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FSDieValueNew : MonoBehaviour
{

		// placeholder script for the variable:
		public int currentValue = 0;
		public bool dmgDone = false;

		void  FixedUpdate ()
		{

				if (GameManager.instance.rollDiceForAttack) {

						if (!dmgDone) {
								Player currPl = GameManager.instance.players [GameManager.instance.currentPlayerIndex];
								if (currPl.eksoplismoi.Contains ("Reds_4") || (currPl.PlayerCharacter == "Valkyria" && currPl.AbilityActivated)) {
										Text toDoDmgText = null;
										if (GameManager.instance.rollForMalburcaAttack) {
												toDoDmgText = GameObject.Find ("MalburcaAttackText").GetComponent<Text> ();
										} else {
												toDoDmgText = GameObject.Find ("DmgToDoText").GetComponent<Text> ();
										}


										//here will be a conditional if the player has the ability to attack with only the fsd

										if (toDoDmgText != null) {

												List <string> playereksoplismoi = GameManager.instance.players [GameManager.instance.currentPlayerIndex].eksoplismoi;
												if (playereksoplismoi.Contains ("Reds_4") || (currPl.PlayerCharacter == "Valkyria" && currPl.AbilityActivated)) {  // reds 4 simenei pirsos = rixnei mono tetreplevro gia attack

														if (GetComponent<Rigidbody> ().IsSleeping ()) {
																int fscv = GetComponent<FSDieValueNew> ().currentValue;

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
																		fscv = fscv + counter;
																		toDoDmgText.text = fscv.ToString ();
																}
																if (playereksoplismoi.Contains ("Blues_6")) {
																		//exei logxi
																		if (currPl.PlayerRace == "Lycan" && currPl.isRevealed) {
																				//einai kai likanthropos revealed 

																				Text sidereniagrothiatext = GameObject.Find ("IronFistText").GetComponent<Text> ();
																				sidereniagrothiatext.enabled = true;
																				sidereniagrothiatext.text = "Logxi bonus: +2";
																				fscv = fscv + 2;
																				toDoDmgText.text = fscv.ToString ();
																		} else {

																				toDoDmgText.text = fscv.ToString ();
																		}


																}

																if (playereksoplismoi.Contains ("Blues_9")) {

																		Text prostateftikosmandiastext = GameObject.Find ("ProtectiveCloakText").GetComponent<Text> ();
																		prostateftikosmandiastext.enabled = false;
																		prostateftikosmandiastext.text = "Prostateftikos Mandias nerf: -1";


																		fscv = fscv - 1;
																		toDoDmgText.text = fscv.ToString ();
																}

																if (!playereksoplismoi.Contains ("Reds_5") && !playereksoplismoi.Contains ("Blues_6")) {
																		//if players has no equip cards that can change the dmg

																		toDoDmgText.text = fscv.ToString (); 
																}


																GameManager.instance.damagePointsCreated = true;	
																if (GameManager.instance.rollForMalburcaAttack) {
																		GameManager.instance.MalburcaDoDamage (toDoDmgText.text);
																}
																GameManager.instance.NewMakeMove ();

																dmgDone = true;
														}

												}
										}
				
				
								}

						}

				} else {

						if (GameManager.instance.rollForGregoryAbility) {

								if (GetComponent<Rigidbody> ().IsSleeping ()) {

										GameManager.instance.GregoryAbilityFinalResult (currentValue);
								}
						}
				}
		}
}
