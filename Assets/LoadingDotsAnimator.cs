using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingDotsAnimator : MonoBehaviour {

	public float letterPause = 0.2f;
	//public AudioClip typeSound1;
	//public AudioClip typeSound2;
	public bool LoadingDone = false;

	string message;
	Text textComp;

	// Use this for initialization
	void Start () {
		textComp = GetComponent<Text>();
		message = textComp.text;
		textComp.text = "";
		StartCoroutine(TypeText ());
	}

	IEnumerator TypeText () {

		foreach (char letter in message.ToCharArray()) {
			textComp.text += letter;
			if (textComp.text == message) {
				textComp.text = "";
				StartCoroutine(TypeText ());
			}
			yield return 0;
			yield return new WaitForSeconds (letterPause);
		}
	}
}
