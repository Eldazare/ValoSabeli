using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MillRotate : MonoBehaviour {

	public float RotateSpeed = 1f;
	public Vector3 RotateAxis = new Vector3(0,1f,0);
	Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody>();
	}


	void FixedUpdate () {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(RotateSpeed * Time.fixedDeltaTime, 0f, 0f));
	} 
}
