using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromGreenCard : MonoBehaviour
{

		public void PlayerClickedFromGreenCard ()
		{
		
				string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
				string id = transform.Find ("PlayerID").GetComponent<Text> ().text;

				GameManager.instance.HandleGreenCardResult (name + "_" + id);

		}
}
