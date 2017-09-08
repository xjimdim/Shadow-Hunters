using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniCardClick : MonoBehaviour
{

		public GameObject MiniCardClickedPanel;


		public void MiniCardClicked ()
		{
				Debug.Log ("MINI CARD CLICKED");
				GameObject MCGO = (GameObject)Instantiate (MiniCardClickedPanel);
				MCGO.transform.Find ("MiniCardClickedPanel").transform.Find ("CharacterImageMiniPanel").GetComponent<Image> ().sprite = this.transform.GetComponent<CardSpriteHandling> ().BackSprite;

				MCGO.transform.SetParent (GameObject.FindGameObjectWithTag ("UICanvas").transform);
				MCGO.GetComponent<RectTransform> ().localPosition = new Vector3 (0f, 175f, 0f);



		}


}
