using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChatInputListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter)) {

			if(!NetworkManager.instance.inGame){
				//call the network manager
				NetworkManager.instance.AddChatMessage(GetComponent<InputField>().text);
				GetComponent<InputField>().text = "";
				//GetComponent<InputField>().
				//GetComponent<InputField>().Select ();
			}
			else{
				//call the game manager
				GameManager.instance.AddChatMessage(GetComponent<InputField>().text);
				GetComponent<InputField>().text = "";
				//GetComponent<InputField>().ActivateInputField ();
				//GetComponent<InputField>().Select ();
			}

		}
	}
}
