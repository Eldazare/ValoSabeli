using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimations : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnInteractableObjectGrabbed(GameObject obj)
	{
		animator.SetBool("Ragdolled", true);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Ground")
		{
			animator.SetBool("Ragdolled", false);
			animator.SetTrigger("Land");
		}
	}
}
