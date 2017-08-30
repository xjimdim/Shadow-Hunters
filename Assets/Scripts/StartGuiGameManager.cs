using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class StartGuiGameManager : MonoBehaviour {
	public GameObject[] textboxesGOs;
	public GameObject NumOfPlayersGO;
	public GameObject PlayButtonGO;
	// Use this for initialization
	//int num = 0;


	void Start () {
		
	}
	
	// Update is called once per frame
//	void Update () {
//		num = int.Parse(NumOfPlayersGO.GetComponentInChildren <UILabel>().text);
//
//		for (int i = 0; i<num; i++) {
//			textboxesGOs[i].GetComponent<UIInput>().enabled = true;
//			textboxesGOs[i].GetComponent<BoxCollider>().enabled = true;
//			UILabel[] labels = textboxesGOs[i].GetComponentsInChildren<UILabel>();
//			labels[0].color = Color.white;
//			labels[1].color = Color.white;
//		}
//
//		for (int j = num; j<5; j++) {
//			textboxesGOs[j].GetComponent<UIInput>().enabled = false;
//			textboxesGOs[j].GetComponent<BoxCollider>().enabled = false;
//			UILabel[] labels = textboxesGOs[j].GetComponentsInChildren<UILabel>();
//			labels[0].color = Color.gray;
//			labels[1].color = Color.gray;
//
//		}
//
//		if (num < 2) {
//			EnablePlayButton(false);
//			//add code for error message here if needed
//
//		} 
//		else {
//			EnablePlayButton(true);
//		
//		}
//	}
//
//	void EnablePlayButton(bool pb){
//				
//		PlayButtonGO.GetComponent<UIButton>().enabled = pb;
//		PlayButtonGO.GetComponent<UIButtonOffset>().enabled = pb;
//		PlayButtonGO.GetComponent<UIPlaySound>().enabled = pb;
//		PlayButtonGO.GetComponent<UIPlayAnimation>().enabled = pb;
//
//		
//	}
//
//	public void PlayButtonClicked(){
//		Debug.Log ("Clicked");
//
//		//first make em all red
//		foreach (GameObject t in textboxesGOs) {
//			string txt = t.GetComponentInChildren<UILabel>().text;
//			
//			
//			if(t.GetComponent<UIInput>().enabled &&  t.GetComponent<BoxCollider>().enabled){
//				if( txt == "Enter name here"){
//					
//					t.GetComponent<UISprite>().color = Color.red;
//					
//
//				}
//
//			}
//			
//		}
//
//		// then add em to list
//		foreach (GameObject t in textboxesGOs) {
//			string txt = t.GetComponentInChildren<UILabel>().text;
//
//
//			if(t.GetComponent<UIInput>().enabled &&  t.GetComponent<BoxCollider>().enabled){
//				if( txt == "Enter name here"){
// 
//					Debug.Log("player name not changed");
//					return;
//				}
//				else{
//					NetworkManager.instance.PlayerNames.Add (txt);
//					Debug.Log(txt + " added to list");
//				}
//
//			}
//
//		}
//
//		Application.LoadLevel ("project");
//
//	}


}
