using UnityEngine;
using System.Collections;

public class HPPlayer : MonoBehaviour {


	public string HPColor;
	public string HPName;
	public Vector3 moveDestination;
	public float moveSpeed = 10f;

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(HPColor);
			stream.SendNext(HPName);
			stream.SendNext(moveDestination);
			
			
		}
		else
		{
			// Network player, receive data
			HPColor  = (string)stream.ReceiveNext();
			HPName = (string)stream.ReceiveNext();
			moveDestination = (Vector3)stream.ReceiveNext ();
		}
	}

	public void CorrectColor(){

		transform.GetComponent<Renderer>().material.color = GameManager.instance.GetColorFromString (HPColor);
		
	}

	public void FixedUpdate(){
		if (Vector3.Distance (moveDestination, transform.position) > 0.1f) {
			transform.position += (moveDestination - transform.position).normalized * moveSpeed * Time.deltaTime;	
			if (Vector3.Distance(moveDestination, transform.position) <= 0.1f) {
				// almost finished moving
				
				transform.position = moveDestination;
			}
		}
	}
}
