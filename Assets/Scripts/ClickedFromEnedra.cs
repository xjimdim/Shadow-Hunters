using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromEnedra : MonoBehaviour
{

		public void PlayerClickedFromEnedra ()
		{
				//TODO: CODE FOR ENEDRA
				string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
				string id = transform.Find ("PlayerID").GetComponent<Text> ().text;

				GameManager.instance.AmbushPlayer (name + "_" + id);

				GameManager.instance.ClearAmbushPanel ();
		}
}
