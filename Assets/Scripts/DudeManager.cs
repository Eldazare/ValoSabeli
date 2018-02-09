using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DudeManager {

	private static int deathsBurn;
	private static int deathsDrown;
	private static int deathsFall;
	private static Spawner spawner;

	public static void Init(){
		spawner = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner>();
		deathsBurn = 0;
		deathsDrown = 0;
		deathsFall = 0;
	}

	public static void reportDeath(DeathType type){
		spawner.SpawnStickFigure ();
		switch ((int)type) {
		case 0:
			deathsBurn += 1;
			break;
		case 1:
			deathsDrown += 1;
			break;
		case 2:
			deathsFall += 1;
			break;
		default:
			break;
		}

		StatsText.instance.UpdateText();
	}

	public static string GetDeathAmountString(){
		return "Falls: " + deathsFall + "\n" +
		"Burns :" + deathsBurn + "\n" +
		"Drowns :" + deathsDrown + "\n";
	}
}

public enum DeathType{
	Burn,
	Drown,
	Fall
}