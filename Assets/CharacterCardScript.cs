using UnityEngine;
using System.Collections;

public class CharacterCardScript : MonoBehaviour {

	public int CharCardID;

	public void CardClicked(){
		Debug.Log ("clicked character");
		
		iTween.MoveTo (gameObject, iTween.Hash ("y", 360, "x", 580, "z", -4.5, "easetype","spring"));
		iTween.RotateTo (gameObject, iTween.Hash ("z", 0, "y", 180, "easetype","spring"));
		iTween.ScaleTo (gameObject, iTween.Hash ("x", -0.6, "y", 0.6, "z", 0.6,  "easetype","spring"));
		
	}
}

