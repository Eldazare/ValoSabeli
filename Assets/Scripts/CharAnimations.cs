using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharAnimations : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>();
	}

	public void Ragdoll()
	{
		animator.SetBool("Grounded", false);
		animator.SetBool("Ragdolled", true);
        animator.enabled = false;
	}

	public void HitGround()
	{
		//animator.SetBool("Ragdolled", false);
		//animator.SetTrigger("Land");
		//animator.SetBool("Grounded", true);
	}

	public void Walk()
	{
		animator.enabled = true;
		animator.SetBool("Grounded", true);
		animator.SetBool("Ragdolled", false);
		animator.SetBool("Walk", true);
	}

	public void Idle()
	{
		animator.enabled = true;
		animator.SetBool("Grounded", true);
		animator.SetBool("Ragdolled", false);
		animator.SetBool("Walk", false);
	}
}
