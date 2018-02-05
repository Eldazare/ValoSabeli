using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject stickFigurePrefab;


	void Start(){
		DudeManager.Init();
		for (int i = 0; i < 22; i++) {
			SpawnStickFigure ();
		}
	}

	public void SpawnStickFigure(){
		int spawnIndex = CharPathController.GetRandomSpotIndex();
		Vector3 spawnPos = CharPathController.GetNextSpotVector (spawnIndex);
		GameObject stickFig = Instantiate (stickFigurePrefab, spawnPos, Quaternion.identity) as GameObject; 
		stickFig.GetComponent<CharBody> ().Initialize (spawnIndex);
	}
}
