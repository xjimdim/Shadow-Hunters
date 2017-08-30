#pragma strict

public var faceValue = 0;

function OnTriggerEnter( other : Collider ) {

if(other.GetComponent.<Collider>().gameObject.layer == LayerMask.NameToLayer("tamplo")){

var dieGameObject = GameObject.Find("SixSidedDie(Clone)");

var dieValueComponent = dieGameObject.GetComponent(DieValue);

dieValueComponent.currentValue = faceValue;

}
}