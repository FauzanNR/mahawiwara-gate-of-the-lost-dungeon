using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHelper: MonoBehaviour {
	public bool isTriggered = false;
	private Collider obj;

	public Collider colliderObj() => obj;

	void OnTriggerEnter(Collider other) {
		isTriggered = true;
		obj = other;
	}

	void OnTriggerExit(Collider other) {
		isTriggered = false;
	}
}
