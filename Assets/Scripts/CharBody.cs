using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharBody : MonoBehaviour {

	private NavMeshAgent charNav;
	private int positionIndex;
	private bool isInitialized = false;
	private bool waiting = false;

	public void Initialize (int beginningPosIndex){
		charNav = GetComponent<NavMeshAgent> ();
		positionIndex = CharPathController.GetNextSpotIndex (beginningPosIndex);
		charNav.SetDestination(CharPathController.GetNextSpotVector(positionIndex));
		isInitialized = true;
	}

	void Update () {
		if (isInitialized && !waiting) {
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
						}
					}
				}
			}
		}
	}

	IEnumerator WaitAtPlaza()
	{
		waiting = true;
		yield return new WaitForSeconds(5f);
		positionIndex = CharPathController.GetNextSpotIndex (positionIndex);
		charNav.SetDestination (CharPathController.GetNextSpotVector (positionIndex));
		waiting = false;
	}
}
