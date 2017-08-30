using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromMagicForest : MonoBehaviour {



	public void PlayerClickedFromMagicForest(){
		
		string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
		string id = transform.Find ("PlayerID").GetComponent<Text> ().text;

	


		if (GameManager.instance.MagicForestWhatToDo == 0) {
			if (!transform.Find ("ProtectedText").GetComponent<Text> ().enabled) {
				GameManager.instance.DoDamageTo (2, name + '_' + id);
			}
			else {
				//he is protected do something fancy to let the user know that
				transform.Find ("ProtectedText").GetComponent<Text> ().color = Color.red;
				transform.GetComponent<Button> ().interactable = false;
				return;
			}
		} 
		else if (GameManager.instance.MagicForestWhatToDo == 1) {
			GameManager.instance.DoHealTo (1, name+'_'+id);
		} 
		else {
			Debug.LogError ("Didn't select what to do from the magic forest, heal or damage?");
		}



		GameManager.instance.ClearMagicForestPanel ();
	}
}
