using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickedFromPagidaPanel : MonoBehaviour {

	public void PlayerClickedFromPagidaPanel(){

		string name = transform.Find ("PlayerName").GetComponent<Text> ().text;
		string id = transform.Find ("PlayerID").GetComponent<Text> ().text;

		GameManager.instance.ShowSelectCardToGiveToPlayer (name, id);
	}
}
