using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour {


	Collider myCol;
	// Use this for initialization
	void Awake () {
		myCol = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Pickupable")
		{
			Physics.IgnoreCollision(myCol, col.collider);
		}
	}
}
