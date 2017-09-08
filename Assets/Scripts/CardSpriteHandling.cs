using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardSpriteHandling : MonoBehaviour
{
		public Sprite BackSprite;


		public Sprite FrontSprite;


		// Update is called once per frame
		void Start ()
		{

		}

		void Update ()
		{

				if (transform.rotation.eulerAngles.y < 90f || (transform.rotation.eulerAngles.y >= 270f && transform.rotation.eulerAngles.y <= 360f)) {
						transform.GetComponent<Image> ().sprite = BackSprite;
				} else {

						transform.GetComponent<Image> ().sprite = FrontSprite;
				}


		}

		public void Flip ()
		{
				
				// Multiply the player's y local scale by -1
				Vector3 theScale = transform.localScale;
				theScale.x *= -1;

				transform.localScale = theScale;
		}
}
