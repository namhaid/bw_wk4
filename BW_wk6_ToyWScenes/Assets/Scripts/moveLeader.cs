using UnityEngine;
using System.Collections;

public class moveLeader : MonoBehaviour {
	public float speediness;

	// Use this for initialization
	void Start () {
		
	}
	
	// FixedUpdate is called __ seconds
	void FixedUpdate () {
		// "< >" always calls for a "type"
		transform.rotation.Set (0,0,0,0);
		
		if (Input.GetKey (KeyCode.Space)) {
			GetComponent<Rigidbody> ().AddForce (transform.up * (speediness*2), ForceMode.Acceleration);
		}
		
		if (Input.GetKey (KeyCode.W)) {
			rigidbody.AddForce ( new Vector3(0f, 0f, speediness));
		}
		
		if (Input.GetKey (KeyCode.S)) {
			rigidbody.AddForce ( new Vector3(0f, 0f, -speediness));
		}
		
		if (Input.GetKey (KeyCode.A) ) {
				rigidbody.AddForce ( new Vector3(-speediness, 0f, 0f));
		}
		
		if (Input.GetKey (KeyCode.D) ) {
				rigidbody.AddForce ( new Vector3(speediness, 0f, 0f));
		}
		// equates to
		// GetComponent<Rigidbody> ().AddForce (GetComponent<Transform> ().up * 10f);
		
		// GetComponent<Rigidbody> ().AddForce ( new Vector3(0f, 10f, 0f));
		// equates to
		// rigidbody.AddForce( new Vector3(of, 10f, 0f) );
	}
}