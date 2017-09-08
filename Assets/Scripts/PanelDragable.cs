using UnityEngine;
using System.Collections;

public class PanelDragable : MonoBehaviour
{

		public void OnDrag ()
		{ 
				transform.position = Input.mousePosition; 
		}


}