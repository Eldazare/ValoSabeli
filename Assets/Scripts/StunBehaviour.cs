using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBehaviour : MonoBehaviour {
	List<GameObject> hit;

    private void OnTriggerStay(Collider other)
    {
		if (hit == null)
			hit = new List<GameObject>();
		if (other.CompareTag("DudeBodyPart") && !hit.Contains(other.gameObject))
        {
			hit.Add(other.gameObject);
			other.transform.transform.GetComponentInParent<CharBody>().Grabbed();
			other.transform.transform.GetComponentInParent<CharBody>().UnGrabbed();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
