using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsText : MonoBehaviour {
	public static StatsText instance = null;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(gameObject);
	}

	Text text;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
	}

	public void UpdateText () {
		text.text = DudeManager.GetDeathAmountString();
	}
}
