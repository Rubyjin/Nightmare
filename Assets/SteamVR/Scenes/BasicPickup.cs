﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR; //lets us using the steam VR

public class BasicPickup : MonoBehaviour {

	public SteamVR_ControllerManager cm; //assign in Inspector, another way of accessing controller
	Rigidbody currentHeld;

	//this is a "property", like a variable that can call code when you use it
	SteamVR_Controller.Device myDevice{
		get{ return SteamVR_Controller.Input ((int)GetComponent<SteamVR_TrackedObject> ().index); }
	}



	// Use this for initialization
	void Start () {

	}

	//1.Detect if a physics rigidbody pickup thing is within our trigger

	void OnTriggerStay (Collider other){
		//2.detect if we are holding down the controller trigger
		if (myDevice.GetHairTrigger ()) {
		
			//3.turn off physics simulation for the thing we're picking up
			currentHeld = other.GetComponent<Rigidbody>();
			currentHeld.isKinematic = true; //turns off physics simulation for this thing


			//4. parent the object to the controller
			currentHeld.transform.SetParent(this.transform);
		}
	}



	// Update is called once per frame
	void Update () {
		//5. drop a currently held object if we release the trigger
		if (currentHeld != null && myDevice.GetHairTrigger () == false) {
			currentHeld.isKinematic = false; //turn on physics simulation again
			currentHeld.transform.parent = null; //unparent the object
			currentHeld = null; //forget aout this object

		}

	}
}
