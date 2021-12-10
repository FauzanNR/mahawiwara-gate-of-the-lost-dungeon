using UnityEngine;

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
		print( animationName );
		animator.Play( animationName.ToString() );
	}
}
