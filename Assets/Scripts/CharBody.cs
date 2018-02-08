using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharBody : MonoBehaviour {

	// TODO: Tags: Ground Water

	private CharAnimations anims;

	public float killHeight = 10.0f;
	private NavMeshAgent charNav;
	private int positionIndex;
	private bool isInitialized = false;
	private bool waiting = false;
	public bool isGrabbed = false;

	private bool dead = false;

	private float maxheight;

	public void Initialize (int beginningPosIndex){
		anims = GetComponent<CharAnimations>();
		charNav = GetComponent<NavMeshAgent> ();
		positionIndex = CharPathController.GetNextSpotIndex (beginningPosIndex);
		charNav.SetDestination(CharPathController.GetNextSpotVector(positionIndex));
		anims.Walk();
		maxheight = 0;
		isInitialized = true;
	}

	void Update () {
		if (isInitialized && !waiting && !isGrabbed) {
			if (!charNav.pathPending) {
				if (charNav.remainingDistance <= charNav.stoppingDistance) {
					if (charNav.hasPath || charNav.velocity.sqrMagnitude == 0f) {
						if (positionIndex == 0)
						{
							StartCoroutine(WaitAtPlaza());
						}else
						{
							positionIndex = CharPathController.GetNextSpotIndex (positionIndex);
							charNav.SetDestination (CharPathController.GetNextSpotVector (positionIndex));
							anims.Walk();
						}
					}
				}
			}
		}
		if (transform.position.y > maxheight) {
			maxheight = transform.position.y;
		}
	}

	IEnumerator WaitAtPlaza()
	{
		anims.Idle();
		waiting = true;
		yield return new WaitForSeconds(5f);
		positionIndex = CharPathController.GetNextSpotIndex (positionIndex);
		charNav.SetDestination (CharPathController.GetNextSpotVector (positionIndex));
		waiting = false;
	}



	void OnTriggerEnter (Collider col){
		if (dead)
			return;
		
		if (col.CompareTag ("Ground")) {
			anims.HitGround();
			isGrabbed = false;
			charNav.enabled = false;
			if (maxheight - transform.position.y > killHeight) {
				DudeManager.reportDeath (DeathType.Fall);
				dead = true;
				Destroy (this.gameObject, 15f);
			}
		} else if (col.CompareTag ("FireBall")) {
			DudeManager.reportDeath (DeathType.Burn);
			dead = true;
			Destroy (this, 15f);
		} else if (col.CompareTag ("Water")) {
			//Drowning animation lol
			DudeManager.reportDeath(DeathType.Drown);
			dead = true;
			Destroy(this, 15f);
		}
	}

	public void Grabbed(){
		anims.Ragdoll();
		isGrabbed = true;
		charNav.enabled = false;
	}

}
