#pragma strict

public var currentValue = 0;


function Update() {

		var dieTextGameObject = GameObject.Find("DieText");

		var textMeshComponent = dieTextGameObject.GetComponent(TextMesh);
		
		var dieGameObject1 = GameObject.Find("FourSidedDie(Clone)");
		
		if(dieGameObject1.GetComponent.<Rigidbody>().IsSleeping() && GetComponent.<Rigidbody>().IsSleeping()){
			var dieValueComponent1 = dieGameObject1.GetComponent(fDieValue);
			
			var fscv = dieValueComponent1.currentValue;
			
			var tots = fscv + currentValue;
			
			textMeshComponent.text = tots.ToString(); 
		
		}

		


		/*if (transform.position.x > 4.8) {
			transform.position = new Vector3(4.8f, transform.position.y, transform.position.z);
			
		}

		if (transform.position.x < -11.8f) {
			transform.position = new Vector3(-11.8f, transform.position.y, transform.position.z);
			
		}
		
		if (transform.position.z > 7) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 7);
			 		
		}

		if (transform.position.z < -7) {
			transform.position = new Vector3(transform.position.x, transform.position.y, -7);
			 		
		}
		
		if (transform.position.z > 0.8) {
			transform.position = new Vector3(transform.position.x, 0.8, transform.position.z);
			 		
		}*/

}

