using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour 
{
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	private GameObject obj;
	private FixedJoint fJoint;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();

		fJoint = GetComponent<FixedJoint>();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller == null)
			return;

		var device = SteamVR_Controller.Input((int)trackedObj.index);

		if (controller.GetPressDown(triggerButton))
		{
			PickupObject();
		}

		if (controller.GetPressUp(triggerButton))
		{
			DropObject();
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Pickupable"))
		{
			obj = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other)
	{
		obj = null;
	}

	void PickupObject()
	{
		if (obj != null)
		{
			fJoint.connectedBody = obj.GetComponent<Rigidbody>();
		}
		else
		{
			fJoint.connectedBody = null;
		}
	}

	void DropObject()
	{
		fJoint.connectedBody = null;
	}
}
