using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharPathController {

	private static List<Vector3> positions;
	private static bool isInitialized = false;
	private static float plazaRadius = 4.0f;

	public static void Initialize(){
		positions = new List<Vector3> (){ };
		GameObject[] houseGOs = GameObject.FindGameObjectsWithTag ("HousePosition");
		foreach (GameObject go in houseGOs) {
			positions.Add (go.transform.position);
		}
		isInitialized = true;
	}
	
	public static int GetNextSpotIndex(int currentIndex){
		if (isInitialized) {
			int randPlaza = Random.Range (0, 3);
			if (randPlaza == 0 && currentIndex != 0) {
				return 0;
			} else {
				int returnIndex = currentIndex;
				while (returnIndex == currentIndex) {
					returnIndex = Random.Range (1, positions.Count);
				}
				return returnIndex;
			}
		} else {
			Initialize ();
			return GetNextSpotIndex (currentIndex);
		}
	}

	public static Vector3 GetNextSpotVector(int nextIndex){
		if (nextIndex == 0) {
			Vector3 randomBonusVec = new Vector3 (Random.Range (-1.0f, 1.0f), Random.Range (-1.0f, 1.0f), 0).normalized * Random.Range (0.0f, plazaRadius);
			return positions [nextIndex] + randomBonusVec;
		} else {
			return positions [nextIndex];
		}
	}

	public static int GetRandomSpotIndex(){
		if (isInitialized) {
			return Random.Range (0, positions.Count);
		} else {
			Initialize ();
			return GetRandomSpotIndex ();
		}
	}

	public static Vector3 GetSpecificSpotVector(int specificInd){
		return positions [specificInd];
	}
}
