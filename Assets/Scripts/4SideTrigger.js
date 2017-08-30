#pragma strict

public var faceValue = 0;

function OnTriggerEnter( other : Collider ) {

if(other.GetComponent.<Collider>().gameObject.layer == LayerMask.NameToLayer("tamplo")){
var dieGameObject1 = GameObject.Find("FourSidedDie(Clone)");

var dieValueComponent1 = dieGameObject1.GetComponent(fDieValue);

dieValueComponent1.currentValue = faceValue;


}

}