using UnityEngine;
using System.Collections;

public class SSidedTrigger : MonoBehaviour {
	public int faceValue = 0;
	
	
	void OnTriggerEnter (Collider other) {
		if(other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("tamplo")){
			GameObject dieGameObject = GameObject.Find("SixSidedDie(Clone)");
			
			SDieValue dieValueComponent = dieGameObject.GetComponent<SDieValue>();
			
			dieValueComponent.currentValue = faceValue;
		}
		
	}
}
