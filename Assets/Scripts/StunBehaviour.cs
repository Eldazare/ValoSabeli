using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBehaviour : MonoBehaviour {


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DudeBodyPart"))
        {
            other.transform.transform.GetComponentInParent<CharAnimations>().Ragdoll();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
