using UnityEngine;
using System.Collections;

public class CloseThisPanel : MonoBehaviour
{

		public void ClickedToClose ()
		{
				this.gameObject.SetActive (false);
		}
}
