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

	private bool stunned = false;
	private bool dead = false;
	public float stunTime;
	private Vector3 rigOrigPos;
	public Transform pelvis;

	private float maxheight;

	public void Initialize (int beginningPosIndex){
		rigOrigPos = pelvis.position - transform.position;
		anims = GetComponent<CharAnimations>();
		charNav = GetComponent<NavMeshAgent> ();
		positionIndex = CharPathController.GetNextSpotIndex (beginningPosIndex);
		charNav.SetDestination(CharPathController.GetNextSpotVector(positionIndex));
		anims.Walk();
		maxheight = 0;
		isInitialized = true;
	}

	void Update () {
		if (isInitialized && !waiting && !isGrabbed && !stunned && !dead) {
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
		if (!isGrabbed && !dead && !stunned){
			positionIndex = CharPathController.GetNextSpotIndex (positionIndex);
			charNav.SetDestination (CharPathController.GetNextSpotVector (positionIndex));
			anims.Walk();
		}
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
				Die (DeathType.Fall);
			}
		} else if (col.CompareTag ("FireBall")) {
			Die (DeathType.Burn);
		} else if (col.CompareTag ("Water")) {
			//Drowning animation lol
			Die(DeathType.Drown);
		}
	}

	void Die(DeathType type){
		charNav.enabled = false;
		anims.Blood();
		DudeManager.reportDeath(type);
		dead = true;
		Destroy(gameObject, 15f);
	}

	public void Grabbed(){
		anims.Ragdoll();
		isGrabbed = true;
		charNav.enabled = false;
	}

	public void UnGrabbed(){
		isGrabbed = false;
		StartCoroutine(Stun());
	}

	IEnumerator Stun() {
		stunned = true;
		yield return new WaitForSeconds(Random.Range(0.75f*stunTime, 1.5f * stunTime));
		if (dead)
			yield break;
		transform.position += new Vector3(pelvis.position.x - transform.position.x, 0f, pelvis.position.z - transform.position.z);
		pelvis.position = rigOrigPos;
		charNav.enabled = true;
		if (charNav.isOnNavMesh) {
			positionIndex = CharPathController.GetNextSpotIndex(positionIndex);
			charNav.SetDestination(CharPathController.GetNextSpotVector(positionIndex));
			anims.Walk();
		}
		else {
			Die(DeathType.Fall);
		}
		stunned = false;
	}
}