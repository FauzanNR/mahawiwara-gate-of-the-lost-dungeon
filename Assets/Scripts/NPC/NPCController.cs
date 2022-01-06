using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent( typeof( NPCMovement ) )]
[RequireComponent( typeof( NavMeshAgent ) )]
[RequireComponent( typeof( AgentLinkMover ) )]
[RequireComponent( typeof( Animator ) )]
[RequireComponent( typeof( NPCAnimation ) )]
//[RequireComponent( typeof( NPCHealth ) )] don't forget to add manual

public class NPCController: MonoBehaviour {

	private Rigidbody rigidbody;
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

	}
	void Awake() {
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag( "Player" );
	}

	void Update() => setState();

	void setState() {
		var movement = System.Math.Abs( agent.velocity.magnitude / agent.speed );

		if(chaseArea.isTriggered) {
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
	}

}
