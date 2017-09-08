﻿using UnityEngine;
using System.Collections;

public class PlayerTile : MonoBehaviour
{

		// Use this for initialization

		bool GreenClicked = false;

		void Start ()
		{
				GreenClicked = GameManager.instance.greenClicked;
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnMouseDown ()
		{
				Debug.Log ("Clicked");
				GreenClicked = GameManager.instance.greenClicked;
				if (GreenClicked) {
						GameManager.instance.playerTileSelectedGO = gameObject;
						GameManager.instance.playerTileClicked = true;
				}

		}


}
