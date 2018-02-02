using UnityEngine;
using System.Collections;

public class MAC_Rotate : MonoBehaviour {

	public int RotateSpeed = 1;
	private Vector3 RotateAxis = new Vector3(1,0,0);

	void Update () {
		this.gameObject.transform.Rotate(RotateAxis);
	} 
}
