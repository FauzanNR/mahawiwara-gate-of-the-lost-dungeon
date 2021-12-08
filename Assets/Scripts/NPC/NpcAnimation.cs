using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public partial class NpcAnimation: MonoBehaviour {

	NPCController controller;
	Animator animator;

	void Start() {
		animator = GetComponent<Animator>();

		controller = GetComponent<NPCController>();
	}

	void Update() {
		changeAnimations( controller.enemyState );
	}

	void changeAnimations(NPC_STATE animationName) {
		animator.Play( animationName.ToString() );
	}
}
