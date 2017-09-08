using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
	
		public Vector2 gridPosition = Vector2.zero;
		public int perioxi;
		public int NumberOfPlayersInArea = 0;

		public GameObject[] positions;
		public bool[] taken = new bool[]{ false, false, false, false, false };
		public string[] PlayersInArea;



		// Use this for initialization
		void Start ()
		{
				PlayersInArea = new string[5];
		}



		[PunRPC]
		void TakePlace_RPC (string name)
		{

				for (int i = 0; i < positions.Length; i++) {
						if (PlayersInArea [i] == name) {
								return;
						}
				}
		
		
				for (int i = 0; i < positions.Length; i++) {
						if (!taken [i]) {
								taken [i] = true;
								PlayersInArea [i] = name;
								break;
						}
				}

		}

		[PunRPC]
		public void LeavePlace_RPC (string name)
		{
				for (int i = 0; i < positions.Length; i++) {
						if (PlayersInArea [i] == name) {
								taken [i] = false;
								PlayersInArea [i] = null;
								break;
						}
				}
		}



		[PunRPC]
		void DecreaseNumOfPlayers_RPC ()
		{
				this.NumberOfPlayersInArea--;
		}

		[PunRPC]
		void IncreaseNumOfPlayers_RPC ()
		{
				this.NumberOfPlayersInArea++;
		}



		public Vector3 getEmptyPlace ()
		{



				Vector3 emptyPos = new Vector3 (-1, -1, -1);
				for (int i = 0; i < positions.Length; i++) {
						if (!taken [i]) {
								emptyPos = positions [i].transform.position;
								Debug.Log ("got position " + i + " in place: " + perioxi);
								return emptyPos;

						}

				}

				if (emptyPos == new Vector3 (-1, -1, -1)) {
						Debug.Log ("Error, players probably more than 5 [see Tile script] perioxi: " + perioxi);		
				}

				return emptyPos;
		}

		//	public void takePlace(GameObject playergo){
		//		//check if player exists *Same place as before
		//		for (int i = 0; i<positions.Length; i++) {
		//			if(PlayersInArea[i] == playergo){
		//				return;
		//			}
		//		}
		//
		//
		//		for (int i = 0; i<positions.Length; i++) {
		//			if(!taken[i]){
		//				taken[i]=true;
		//				PlayersInArea[i] = playergo;
		//				break;
		//			}
		//		}
		//	}

		//	public void leavePlace(GameObject playergo){
		//		for (int i = 0; i<positions.Length; i++) {
		//			if(PlayersInArea[i]==playergo){
		//				taken[i]=false;
		//				PlayersInArea[i] = null;
		//				break;
		//			}
		//		}
		//	}
	

	
}
