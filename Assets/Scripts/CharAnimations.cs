using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharAnimations : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Grabbed()
	{
		animator.SetBool("Grounded", false);
		animator.SetBool("Ragdolled", true);
	}

	public void HitGround()
	{
		animator.SetBool("Ragdolled", false);
		animator.SetTrigger("Land");
		animator.SetBool("Grounded", true);
	}

	public void Walk()
	{
		animator.SetBool("Grounded", true);
		animator.SetBool("Walk", true);
	}

	public void Idle()
	{
		animator.SetBool("Walk", false);
	}
}
