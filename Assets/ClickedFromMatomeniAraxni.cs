using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromMatomeniAraxni : MonoBehaviour {

	public void PlayerClickedFromMatomeniAraxni ()
	{

		string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
		string id = transform.Find ("PlayerID").GetComponent<Text> ().text;

		if (!transform.Find ("ProtectedText").GetComponent<Text> ().enabled) {
			GameManager.instance.DoDamageTo (2, name + '_' + id);
		}
		else {
			transform.Find ("ProtectedText").GetComponent<Text> ().color = Color.red;
			transform.GetComponent<Button> ().interactable = false;
			return;
		}

		if (!GameManager.instance.players [GameManager.instance.currentPlayerIndex].eksoplismoi.Contains ("Blues_13")) {
			GameManager.instance.DoDamageTo (2, GameManager.instance.players [GameManager.instance.currentPlayerIndex].PlName);
		}

		GameManager.instance.ClearMatomeniAraxniPanel ();
	}
}
