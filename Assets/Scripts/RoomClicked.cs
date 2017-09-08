using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomClicked : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void ClicekdRoom ()
		{

				NetworkManager.instance.JoinThisRoom (this.transform.Find ("PlayerId").GetComponent<Text> ().text);

		}
}
