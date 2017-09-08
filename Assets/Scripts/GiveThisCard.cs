using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GiveThisCard : MonoBehaviour
{

		public string DestinationPlayerName = "none";
		public string DestinationPlayerID = "none";
		public bool isForGreenCard = false;

		public void GiveThisCardClicked ()
		{
				//steal code here
				string spriteName = GetComponent<Image> ().sprite.name;
				string cardtype = "none";
				string cardID = "none";
				string myNameandID = "none";

				if (isForGreenCard) {
						myNameandID = PhotonNetwork.player.name;
				} else {
						//its for pagida
						myNameandID = GameManager.instance.players [GameManager.instance.currentPlayerIndex].PlName;
				}
				string[] myinfoSplitArray = myNameandID.Split (new char[]{ '_' });

				string[] typeSplitArray = spriteName.Split (new char[]{ '_' });
				cardtype = typeSplitArray [0];

				string[] idSplitArray = spriteName.Split (new char[]{ '#' });
				cardID = idSplitArray [0];

				if (DestinationPlayerName != "none" && DestinationPlayerID != "none") {

						GameManager.instance.GetComponent<PhotonView> ().RPC ("RemoveEquipCardFromPlayer_RPC", PhotonTargets.AllBuffered, cardtype, cardID, myinfoSplitArray [0], myinfoSplitArray [1]);

						GameManager.instance.GetComponent<PhotonView> ().RPC ("AddEquipCardToPlayerNetwork_RPC", PhotonTargets.AllBuffered, cardtype, cardID, DestinationPlayerName, DestinationPlayerID);
			

				} else {
						Debug.LogError ("No DestinationPlayer has been set");
				}


				if (isForGreenCard) {
						GameManager.instance.ClearCardToGiveFromGreen ();
						GameObject.FindObjectOfType<GreenCardScript> ().DestroyGreenNet ();
				} else {
						GameManager.instance.ClearTrapPanel ();
				}




		}
}
