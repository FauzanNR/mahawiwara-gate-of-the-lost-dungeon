using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.UIElements;


[RequireComponent( typeof( NPCMovement ) )]
[RequireComponent( typeof( NavMeshAgent ) )]
[RequireComponent( typeof( AgentLinkMover ) )]
[RequireComponent( typeof( Animator ) )]
[RequireComponent( typeof( NPCAnimation ) )]
//[RequireComponent( typeof( NPCHealth ) )] don't forget to add manual

public class NPCController: MonoBehaviour {

	private NPC_STATE State;
	private GameObject player;
	NavMeshAgent agent;
	public AreaHelper attackArea;
	public AreaHelper chaseArea;
	public List<EnemyAttack> attacksPositionList;
	public NPCHealth health;

	public NPC_STATE enemyState {
		get {
			return State;
		}
		set {
			State = value;
		}
	}

	void Start() {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag( "Player" );
	}

	void Update() => setState();

	void setState() {
		var movement = System.Math.Abs( agent.velocity.magnitude / agent.speed );

		if(chaseArea.isTriggered && chaseArea.colliderObj().tag == "Player") {
			var distance = Vector3.Distance( player.transform.position, transform.position );
			if(chaseArea.isTriggered && attackArea.isTriggered) {
				enemyState = NPC_STATE.Attack1;
			} else if(distance > 2.5f) enemyState = NPC_STATE.Run;
		} else {
			if(movement == 0) {
				enemyState = NPC_STATE.Idle;
			} else if(movement > 2) {
				enemyState = NPC_STATE.Jump;
			} else {
				enemyState = NPC_STATE.Patrol;
			}
		}


		var rigBody = gameObject.GetComponent<Rigidbody>();
		var colider = gameObject.GetComponent<Collider>();
		if(rigBody == null && colider == null) {
			rigBody = gameObject.GetComponentInChildren<Rigidbody>();
			colider = gameObject.GetComponentInChildren<Collider>();
		}
		if(health.currHealth <= 0) {
			enemyState = NPC_STATE.Death;
			if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo( 0 ).IsName( "Death" )) {
				rigBody.isKinematic = false;
				rigBody.useGravity = true;
				colider.isTrigger = false;
				//colider.enabled = false;
				var navAgent = gameObject.GetComponent<NavMeshAgent>();
				navAgent.enabled = false;

			}
		}
	}

	void turnOffAnimation() {
		var animator = gameObject.GetComponent<Animator>();
	}

}
