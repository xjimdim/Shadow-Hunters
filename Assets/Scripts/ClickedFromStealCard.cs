using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromStealCard : MonoBehaviour
{

		public void PlayerClickedFromStealCardPanel ()
		{

				string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
				string id = transform.Find ("PlayerID").GetComponent<Text> ().text;

				GameManager.instance.ShowSelectCardToStealFromPlayer (name, id);
		}
}
