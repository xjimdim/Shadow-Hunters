using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlueCardScript : MonoBehaviour
{

		public string BlueCardID;
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
						GameManager.instance.AddEquipCardToPlayerNetwork ("Blues", BlueCardID, splitArray [0], splitArray [1]);
				}

		}

		public void displayNetText (string m)
		{
				NetText.SetActive (true);
				NetText.transform.Find ("Text").GetComponent<Text> ().text = m + " picked this card";
				displayOkButtonNetwork ();


		}

		public void displayOkButtonNetwork ()
		{
				OkButtonNet.SetActive (true);
				OkButtonNet.transform.SetParent (this.transform.parent);
				OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				OkButtonNet.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

		}



		public void displayOkButton ()
		{

				if (GetComponent<Image> ().sprite.name == "Blues_10#gegonos") {

						Player currPl = GameManager.instance.players [GameManager.instance.currentPlayerIndex];
						string firstchar = GameManager.instance.GetCharactersFirstLetter (currPl.PlayerCharacter);




						if (firstchar == "A" || firstchar == "O" || firstchar == "E") {
								transform.Find ("DoubleButtons").gameObject.SetActive (true);


								GameObject ApplyBtn = transform.Find ("DoubleButtons").Find ("ApplyButtonBlue").gameObject;
								if (currPl.isRevealed) {
										ApplyBtn.transform.Find ("Text").GetComponent<Text> ().text = "Heal";
								}
								GameObject CancelBtn = transform.Find ("DoubleButtons").Find ("CancelButtonBlue").gameObject;

								ApplyBtn.transform.SetParent (this.transform.parent);
								ApplyBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								ApplyBtn.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

								CancelBtn.transform.SetParent (this.transform.parent);
								CancelBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								CancelBtn.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

								return;
						}


				}

				if (GetComponent<Image> ().sprite.name == "Blues_11#gegonos") {

						Player currPl = GameManager.instance.players [GameManager.instance.currentPlayerIndex];

						if (currPl.PlayerRace == "Lycan") {
								transform.Find ("DoubleButtons").gameObject.SetActive (true);

								if (currPl.isRevealed) {
										//change the text of the reveal and heal to heal 
										transform.Find ("DoubleButtons").Find ("ApplyButtonBlue").Find ("Text").GetComponent<Text> ().text = "Heal";
								}

								GameObject RevealBtn = transform.Find ("DoubleButtons").Find ("ApplyButtonBlue").gameObject;
								GameObject CancelBtn = transform.Find ("DoubleButtons").Find ("CancelButtonBlue").gameObject;

								RevealBtn.transform.SetParent (this.transform.parent);
								RevealBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								RevealBtn.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

								CancelBtn.transform.SetParent (this.transform.parent);
								CancelBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								CancelBtn.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);


						} else {
								// not a lycan so not intrested
								OkPanel.SetActive (true);
								OkPanel.transform.SetParent (this.transform.parent);
								OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								OkPanel.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

								OkButton.transform.Find ("Text").GetComponent<Text> ().resizeTextForBestFit = true;
								OkButton.transform.Find ("Text").GetComponent<Text> ().text = "OK/Not Intrested";

						}
						return;

				}

				if (GetComponent<Image> ().sprite.name == "Blues_12#gegonos") {

						Player currPl = GameManager.instance.players [GameManager.instance.currentPlayerIndex];



						if (currPl.DamagePoints > 0) {
								transform.Find ("DoubleButtons").gameObject.SetActive (true);


								GameObject ApplyBtn = transform.Find ("DoubleButtons").Find ("ApplyButtonBlue").gameObject;
								GameObject CancelBtn = transform.Find ("DoubleButtons").Find ("CancelButtonBlue").gameObject;

								transform.Find ("DoubleButtons").Find ("ApplyButtonBlue").Find ("Text").GetComponent<Text> ().text = "Heal";

								ApplyBtn.transform.SetParent (this.transform.parent);
								ApplyBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								ApplyBtn.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

								CancelBtn.transform.SetParent (this.transform.parent);
								CancelBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
								CancelBtn.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);

								return;
						}


				}

				OkPanel.SetActive (true);
				OkPanel.transform.SetParent (this.transform.parent);
				OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				OkPanel.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);


		}

		public void DoubleButtonsCancelClicked ()
		{
				GameObject RevealBtn = transform.parent.Find ("ApplyButtonBlue").gameObject;
				GameObject CancelBtn = transform.parent.Find ("CancelButtonBlue").gameObject;


				RevealBtn.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				RevealBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 90f));
				RevealBtn.transform.SetParent (this.transform);

				CancelBtn.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				CancelBtn.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 90f));
				CancelBtn.transform.SetParent (this.transform);

				Destroy (gameObject);

		}

		public void DoubleButtonsApplyClicked ()
		{
				//heal code here
				if (GetComponent<Image> ().sprite.name == "Blues_10#gegonos") {
						GameManager.instance.EnergiaClicked ();
				}
				if (GetComponent<Image> ().sprite.name == "Blues_11#gegonos") {
						GameManager.instance.TherapeiaHealClicked ();
				}
				if (GetComponent<Image> ().sprite.name == "Blues_12#gegonos") {
						GameManager.instance.EvlogiaClicked ();
				}

				DoubleButtonsCancelClicked ();

		}




		public void BlueCardOKNet ()
		{
				OkButtonNet.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				OkButtonNet.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 90f));
				OkButtonNet.transform.SetParent (this.transform);


				Destroy (gameObject);

		}


		public void BlueCardOK ()
		{
				Debug.Log ("blue ok clicked");

	
				OkPanel.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);
				OkPanel.transform.rotation = Quaternion.Euler (new Vector3 (0, 180f, 90f));
				OkPanel.transform.SetParent (this.transform);


				if (type == "eksoplismos") {
						GameManager.instance.AddEquipCardToPlayer ("Blues", BlueCardID, true);
						Destroy (gameObject);
				} else {
						if (transform.GetComponent<Image> ().sprite.name == "Blues_1#gegonos") {
								GameManager.instance.ShowBloodMoonPanel ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Blues_2#gegonos") {
								GameManager.instance.DoIeriOrgi ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Blues_4#gegonos") {
								GameManager.instance.addExtraRoundToCurrentPlayer ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Blues_5#gegonos") {
								GameManager.instance.DoApokalipsi ();
						}
						if (transform.GetComponent<Image> ().sprite.name == "Blues_14#gegonos") {
								GameManager.instance.ShowTreatmentFromAfarPanel ();
						}

						Destroy (gameObject);
				}


				//GameManager.instance.DisableBlueCards (true); TODO UNCOMMENT AND DISABLE ALL CARDS

		}



	


}
