using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharBody : MonoBehaviour {

	// TODO: Tags: Ground Water

	CharAnimations anims;

	public float killHeight = 10.0f;
	private NavMeshAgent charNav;
	private int positionIndex;
	private bool isInitialized = false;
	private bool waiting = false;
	private bool isGrabbed = false;

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



	void OnCollisionEnter (Collision col){
		if (col.collider.CompareTag ("Ground")) {
			anims.HitGround();
			isGrabbed = false;
			charNav.enabled = false;
			if (maxheight - transform.position.y > killHeight) {
				DudeManager.reportDeath (2);
				Destroy (this.gameObject, 0.1f);
			}
		} else if (col.collider.CompareTag ("FireBall")) {
			DudeManager.reportDeath (0);
			Destroy (this, 1.0f);
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Water")) {
			//Drowning animation lol
			DudeManager.reportDeath(1);
			Destroy(this,0.5f);
		}
	}

	void OnInteractableObjectGrabbed(GameObject go){
		anims.Ragdoll();
		isGrabbed = true;
		charNav.enabled = false;
	}

}
