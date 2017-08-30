using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StealThisCard : MonoBehaviour {

	public string OwnerName = "none";
	public string OwnerID = "none";

	public void StealThisCardClicked(){
		//steal code here
		string spriteName = GetComponent<Image>().sprite.name;
		string cardtype = "none";
		string cardID = "none";

		string myNameandID = GameManager.instance.players [GameManager.instance.currentPlayerIndex].PlName;
		string[] myinfoSplitArray =  myNameandID.Split(new char[]{'_'});

		string[] typeSplitArray =  spriteName.Split(new char[]{'_'});
		cardtype = typeSplitArray [0];

		string[] idSplitArray = spriteName.Split (new char[]{'#'});
		cardID = idSplitArray [0];

		if (OwnerName != "none" && OwnerID != "none") {

			GameManager.instance.GetComponent<PhotonView>().RPC("RemoveEquipCardFromPlayer_RPC", PhotonTargets.AllBuffered, cardtype, cardID, OwnerName, OwnerID);

			GameManager.instance.GetComponent<PhotonView>().RPC("AddEquipCardToPlayerNetwork_RPC", PhotonTargets.AllBuffered, cardtype, cardID, myinfoSplitArray[0], myinfoSplitArray[1]);


		}
		else {
			Debug.LogError ("No owner has been set");
		}



		GameManager.instance.ClearStealCardPanel ();

	}
}
