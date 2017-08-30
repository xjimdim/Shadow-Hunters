using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromGregorAbility : MonoBehaviour {

	public void PlayerClickedFromGregorAbility ()
	{
		
		string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
		string id = transform.Find ("PlayerID").GetComponent<Text> ().text;

		GameManager.instance.GregoryAbilityPlayer(name + "_" + id);

		GameManager.instance.ClearGregoryAbilityPanel ();
	}
}
