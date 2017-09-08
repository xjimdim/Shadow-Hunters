using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RedCardScript : MonoBehaviour
{
	
		public string RedCardID;
		public bool isEnabled = false;
		public string type = "";

		public GameObject OkPanel;
		public GameObject OkButton;
		public GameObject OkButtonNet;
		public GameObject NetText;
		public GameObject EquipCardPrefab;

		public void CardClicked ()
		{


		
				iTween.MoveTo (gameObject, iTween.Hash ("y", 360, "x", 580, "z", -4.5, "easetype", "spring", "oncomplete", "displayOkButton"));
				iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "y", 180, "easetype", "spring"));
				iTween.ScaleTo (gameObject, iTween.Hash ("x", -0.6, "y", 0.6, "z", 0.6, "easetype", "spring"));


		}


		public void CardClickedNetwork (string m)
		{
				string[] splitArray = m.Split (new char[]{ '_' });

				this.GetComponent<Image> ().color = Color.white;
				iTween.MoveTo (gameObject, iTween.Hash ("y", 360, "x", 580, "z", -4.5, "easetype", "spring", "oncomplete", "displayNetText", "oncompleteparams", splitArray [0]));
				iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "y", 180, "easetype", "spring"));
				iTween.ScaleTo (gameObject, iTween.Hash ("x", -0.6, "y", 0.6, "z", 0.6, "easetype", "spring"));

				if (type == "eksoplismos") {
						GameManager.instance.AddEquipCardToPlayerNetwork ("Reds", RedCardID, splitArray [0], splitArray [1]);
				}

		}

		public void displayNetText (string m)
		{
				NetText.SetActive (true);
				NetText.transform.Find ("Text").GetComponent<Text> ().text = m + " picked this card";
				displayOkButtonNetwork ();
		}

		public void RevealAndHealClicked ()
		{
				GameManager.instance.SkoteiniTeletiHealClicked ();

				SkoteiniTeletiCancelClicked ();

				//destroy card
		}

		public void SkoteiniTeletiCancelClicked ()
		{
				GameObject RevealBtn = transform.parent.Find ("RevealAndHealButton").gameObject;
				GameObject CancelBtn = transform.parent.Find ("CancelButtonST").gameObject;


				RevealBtn.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				RevealBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 90f));
				RevealBtn.transform.SetParent (this.transform);

				CancelBtn.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				CancelBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 90f));
				CancelBtn.transform.SetParent (this.transform);

				Destroy (gameObject);

		}

		public void displayOkButton ()
		{

				if (GetComponent<Image> ().sprite.name == "Reds_1#gegonos") {

						Player currPl = GameManager.instance.players [GameManager.instance.currentPlayerIndex];

						if (currPl.PlayerRace == "Vamp") {
								transform.Find ("SkoteiniTeletiButtons").gameObject.SetActive (true);

								if (currPl.isRevealed) {
										//change the text of the reveal and heal to heal 
										transform.Find ("SkoteiniTeletiButtons").Find ("RevealAndHealButton").Find ("Text").GetComponent<Text> ().text = "Heal";
								}

								GameObject RevealBtn = transform.Find ("SkoteiniTeletiButtons").Find ("RevealAndHealButton").gameObject;
								GameObject CancelBtn = transform.Find ("SkoteiniTeletiButtons").Find ("CancelButtonST").gameObject;

								RevealBtn.transform.SetParent (this.transform.parent);
								RevealBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								RevealBtn.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

								CancelBtn.transform.SetParent (this.transform.parent);
								CancelBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								CancelBtn.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);


						} else {
								// not a vamp so not intrested
								OkPanel.SetActive (true);
								OkPanel.transform.SetParent (this.transform.parent);
								OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								OkPanel.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

								OkButton.transform.Find ("Text").GetComponent<Text> ().resizeTextForBestFit = true;
								OkButton.transform.Find ("Text").GetComponent<Text> ().text = "OK/Not Intrested";
			
						}
						return;
				}


				OkPanel.SetActive (true);
				OkPanel.transform.SetParent (this.transform.parent);
				OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				OkPanel.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
		}

		public void displayOkButtonNetwork ()
		{
				OkButtonNet.SetActive (true);
				OkButtonNet.transform.SetParent (this.transform.parent);
				OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				OkButtonNet.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

		}

		public void RedCardOKNet ()
		{
				OkButtonNet.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 90f));
				OkButtonNet.transform.SetParent (this.transform);


				Destroy (gameObject);

		}

	
		public void RedCardOK ()
		{
				Debug.Log ("red ok clicked");





				OkPanel.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 90f));
				OkPanel.transform.SetParent (this.transform);


				if (type == "eksoplismos") {
						GameManager.instance.AddEquipCardToPlayer ("Reds", RedCardID, true);
						Destroy (gameObject);
				} else {
						if (transform.GetComponent<Image> ().sprite.name == "Reds_2#gegonos") {
								GameManager.instance.ShowBatPanel ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Reds_6#gegonos") {
								GameManager.instance.ShowBloodSpiderPanel ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Reds_8#gegonos") {
								GameManager.instance.DoEkriksi ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Reds_10#gegonos") {
								GameManager.instance.DoTrap ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Reds_11#gegonos") {
								GameManager.instance.ShowAmbushPanel ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Reds_3#gegonos" && GameManager.instance.GetEquipmentNumberOfOtherPlayers () > 0) {
								GameManager.instance.StoneCircleClicked ();
						}
						Destroy (gameObject);
				}

				// GameManager.instance.DisableRedCards (true); TODO UNCOMMENT AND DISABLE ALL CARDS
		}




}
