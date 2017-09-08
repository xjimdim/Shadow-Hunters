using UnityEngine;
using System.Collections;

public class CharacterOnNetwork : Photon.MonoBehaviour
{

		Vector3 realPosition;
		Quaternion correctPlayerRot;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
				if (!photonView.isMine) {
						transform.position = Vector3.Lerp (transform.position, realPosition, Time.deltaTime * 5);
						transform.rotation = Quaternion.Lerp (transform.rotation, this.correctPlayerRot, Time.deltaTime * 5);
				}
		}

		void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
		{
				if (stream.isWriting) {
						// We own this player: send the others our data
						stream.SendNext (transform.position);
						stream.SendNext (transform.rotation);

			

				} else {
						// Network player, receive data
						realPosition = (Vector3)stream.ReceiveNext ();
						correctPlayerRot = (Quaternion)stream.ReceiveNext ();

				}
		}
}
