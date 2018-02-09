using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharAnimations : MonoBehaviour {

	Animator animator;
	ParticleSystem bloodParticles;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>();
		bloodParticles = transform.Find("BloodParticles").GetComponent<ParticleSystem>();
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
		animator.SetBool("Idle", false);
	}

	public void Idle()
	{
		animator.enabled = true;
		animator.SetBool("Grounded", true);
		animator.SetBool("Ragdolled", false);
		animator.SetBool("Walk", false);
		animator.SetBool("Idle", true);
	}

	public void Blood(){
		bloodParticles.Play();
		StartCoroutine(BloodStop());
	}

	IEnumerator BloodStop(){
		yield return new WaitForSeconds(1f);
		var main = bloodParticles.main;
		main.gravityModifier = 0f;
		var vel = bloodParticles.limitVelocityOverLifetime;
		vel.dampen = 1f;
		var col = bloodParticles.collision;
		col.enabled = false;
	}
}
