using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCArea: MonoBehaviour {
	public bool isTriggered = false;

	void OnTriggerEnter(Collider otherS) {
		isTriggered = true;
	}

	void OnTriggerExit(Collider other) {
		isTriggered = false;
	}
}
