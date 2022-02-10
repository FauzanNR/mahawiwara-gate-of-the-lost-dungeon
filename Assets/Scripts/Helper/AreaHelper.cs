using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHelper: MonoBehaviour {
	public bool isTriggered = false;
	private Collider obj;

	public Collider colliderObj() => obj;

	void OnTriggerStay(Collider other) {
		obj = other;
	}
	void OnTriggerEnter(Collider other) {
		isTriggered = true;
	}
	void OnTriggerExit(Collider other) {
		isTriggered = false;
	}
}
