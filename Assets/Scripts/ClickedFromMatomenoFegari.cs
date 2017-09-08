using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromMatomenoFegari : MonoBehaviour
{

		public void PlayerClickedFromMatomenoFegari ()
		{

				string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
				string id = transform.Find ("PlayerID").GetComponent<Text> ().text;


				if (!transform.Find ("ProtectedText").GetComponent<Text> ().enabled) {
						// just move it to 7

						GameManager.instance.SetDamageTo (7, name + '_' + id);
				} else {
						//he is protected do something fancy to let the user know that
						transform.Find ("ProtectedText").GetComponent<Text> ().color = Color.red;
						transform.GetComponent<Button> ().interactable = false;
						return;
				}


				GameManager.instance.ClearBloodMoonPanel ();
		}


}
