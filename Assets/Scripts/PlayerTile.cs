using UnityEngine;
using System.Collections;

public class PlayerTile : MonoBehaviour {

	// Use this for initialization

	bool GreenClicked = false;
	void Start () {
		GreenClicked = GameManager.instance.GreenClicked;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown(){
		Debug.Log ("Clicked");
		GreenClicked = GameManager.instance.GreenClicked;
		if (GreenClicked) {
			GameManager.instance.PlayerTileSelectedGO = gameObject;
			GameManager.instance.PlayerTileClicked = true;
		}

	}


}
