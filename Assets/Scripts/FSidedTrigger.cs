using UnityEngine;
using System.Collections;

public class FSidedTrigger : MonoBehaviour
{
		public int faceValue = 0;

	 
		void OnTriggerEnter (Collider other)
		{
				if (other.GetComponent<Collider> ().gameObject.layer == LayerMask.NameToLayer ("tamplo")) {
						GameObject dieGameObject1 = GameObject.Find ("FourSidedDie(Clone)");
			
						FSDieValueNew dieValueComponent1 = dieGameObject1.GetComponent<FSDieValueNew> ();
			
						dieValueComponent1.currentValue = faceValue;
				}

		}
}
