using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RollDiceButton : MonoBehaviour {

	public void OnClick(){
		GameManager.instance.waitForPlayerToRoll = false; //player rolled ^^
		GameObject RollButtonGO = GameObject.Find ("RollButton");
		RollButtonGO.GetComponent<Button> ().interactable = false; 
		GetComponent<Button> ().interactable = false;
	}
}