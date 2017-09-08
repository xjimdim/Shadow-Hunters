using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZoomOnHover : MonoBehaviour
{


		Image zoomedIn;
		GameObject zoomedInGo;

		public bool isInPlayerScrollc = false;
		public bool isInMyEquipTamplo = false;

		void Start ()
		{
				zoomedInGo = new GameObject ();
				zoomedInGo.name = "ZoomedInImage";


				zoomedIn = zoomedInGo.AddComponent<Image> ();

				if (isInPlayerScrollc || isInMyEquipTamplo) {
			
						zoomedInGo.transform.SetParent (GameObject.FindGameObjectWithTag ("UICanvas").transform);

		

						if (isInPlayerScrollc) {
								zoomedInGo.transform.GetComponent<RectTransform> ().anchorMin = new Vector2 (1f, 0.5f);
								zoomedInGo.transform.GetComponent<RectTransform> ().anchorMax = new Vector2 (1f, 0.5f);
								zoomedInGo.transform.GetComponent<RectTransform> ().pivot = new Vector2 (0.5f, 0.5f);
								zoomedInGo.transform.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (-348, 66, 0);
						} else {
								zoomedInGo.transform.GetComponent<RectTransform> ().anchorMin = new Vector2 (0.5f, 0);
								zoomedInGo.transform.GetComponent<RectTransform> ().anchorMax = new Vector2 (0.5f, 0);
								zoomedInGo.transform.GetComponent<RectTransform> ().pivot = new Vector2 (0.5f, 0.5f);
								zoomedInGo.transform.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (0, 337, 0);
						}

						zoomedInGo.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (166, 258);
						zoomedInGo.transform.localScale = new Vector3 (1.5f, 1.5f, 1);


				} else {
						zoomedInGo.transform.SetParent (transform);
						zoomedIn.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));

						zoomedInGo.transform.GetComponent<RectTransform> ().localPosition = new Vector3 (-204, 324, 0);
						zoomedInGo.transform.localScale = new Vector3 (2, 2, 1);
						zoomedInGo.transform.GetComponent<RectTransform> ().sizeDelta = new Vector2 (415, 645);

		
		
				}

				zoomedIn.enabled = false;

		}


		public void MouseEnter ()
		{
				zoomedIn.enabled = true;
				zoomedIn.sprite = GetComponent<Image> ().sprite;


		}

		public void MouseExit ()
		{
				zoomedIn.enabled = false;
		}
}
