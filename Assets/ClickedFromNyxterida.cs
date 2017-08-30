using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromNyxterida : MonoBehaviour {

	public void PlayerClickedFromNyxterida ()
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
	
		GameManager.instance.ClearNyxteridaPanel ();
	}
}
