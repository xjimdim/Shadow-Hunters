using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromTheApMa : MonoBehaviour {

	public void PlayerClickedFromTherapiaApoMakria ()
	{
		


		string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
		string id = transform.Find ("PlayerID").GetComponent<Text> ().text;
		GameManager.instance.RollForTherapiaApMa (name + "_" + id);



		GameManager.instance.ClearTherapiaApoMakriaPanel ();
	}
}
