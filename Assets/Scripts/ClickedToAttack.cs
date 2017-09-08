using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedToAttack : MonoBehaviour
{
		public bool isEkriksi = false;

		public void PlayerClickedToAttack ()
		{

				if (!transform.Find ("ProtectedText").GetComponent<Text> ().enabled) {
			
						if (isEkriksi == false) {
								string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
								string id = transform.Find ("PlayerID").GetComponent<Text> ().text;

								int pointsOfDmg = int.Parse (GameObject.Find ("DmgToDoText").GetComponent<Text> ().text);

								if (transform.Find ("MandiasPlayerText").GetComponent<Text> ().enabled) {
										//exei prostateftiko mandia


										pointsOfDmg--;
										Debug.Log ("Mandias activated for this player, actual points of dmg : " + pointsOfDmg);
										GameManager.instance.DoDamageFromAttackTo (pointsOfDmg, name + "_" + id);
					

								} else {
										Debug.Log ("Mandias NOT activated for this player, actual points of dmg : " + pointsOfDmg);
										if (!transform.Find ("ProtectedText").GetComponent<Text> ().enabled) {
												GameManager.instance.DoDamageFromAttackTo (pointsOfDmg, name + "_" + id);
												Player myPlayer = GameManager.instance.players [GameManager.instance.currentPlayerIndex];
												if (myPlayer.PlayerCharacter == "Volco" && myPlayer.AbilityActivated && pointsOfDmg > 0) {
														GameManager.instance.DoHealTo (2, myPlayer.PlName);
												}
										} else {
												//he is protected do something fancy to let the user know that
												transform.Find ("ProtectedText").GetComponent<Text> ().color = Color.red;
												transform.GetComponent<Button> ().interactable = false;
												return;
										}

								}



								GameManager.instance.ClearAttackPanel ();
						} else {
								//do the attack
								// TODO: na mporei h Malburca na kanei attackback
								string MyDestinationText = "none";

								string name1 = transform.Find ("PlayerName").GetComponent<Text> ().text;
								string id1 = transform.Find ("PlayerID").GetComponent<Text> ().text;

								foreach (Player pl in GameManager.instance.players) {
										if (pl.PlName == name1 + "_" + id1) {
												MyDestinationText = pl.destinationText;
										}

								}

								foreach (Transform t in GameManager.instance.attackScrollContain) {

										string name = t.Find ("PlayerName").GetComponent<Text> ().text;
										string id = t.Find ("PlayerID").GetComponent<Text> ().text;

										foreach (Player p in GameManager.instance.players) {
												if (p.PlName == name + "_" + id) {

														if (p.destinationText == MyDestinationText) {
																int pointsOfDmg = int.Parse (GameObject.Find ("DmgToDoText").GetComponent<Text> ().text);
																if (!transform.Find ("ProtectedText").GetComponent<Text> ().enabled) {
																		GameManager.instance.DoDamageTo (pointsOfDmg, name + "_" + id);  //TODO: change to DoDamageFromAttackTo
																		Player myPlayer = GameManager.instance.players [GameManager.instance.currentPlayerIndex];
																		if (myPlayer.PlayerCharacter == "Volco" && myPlayer.AbilityActivated && pointsOfDmg > 0) {
																				GameManager.instance.DoHealTo (2, myPlayer.PlName);
																		}

																} else {
																		//he is protected do something fancy to let the user know that
																		transform.Find ("ProtectedText").GetComponent<Text> ().color = Color.red;
																		transform.GetComponent<Button> ().interactable = false;
																		return;
																}
														}

														if (MyDestinationText == "none") {
																Debug.LogError ("could not find selected players destination");
														}

												}
										}

								}

								GameManager.instance.ClearAttackPanel ();

						}
				} else {
						//he is protected do something fancy to let the user know that
						transform.Find ("ProtectedText").GetComponent<Text> ().color = Color.red;
						transform.GetComponent<Button> ().interactable = false;
						return;
				}




		}
}
