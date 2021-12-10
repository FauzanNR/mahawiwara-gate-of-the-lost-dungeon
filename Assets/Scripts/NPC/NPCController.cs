using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent( typeof( NPCMovement ) )]
[RequireComponent( typeof( NavMeshAgent ) )]
[RequireComponent( typeof( AgentLinkMover ) )]
[RequireComponent( typeof( Animator ) )]
[RequireComponent( typeof( NpcAnimation ) )]

public class NPCController: MonoBehaviour {

	private NPC_STATE State;
	NavMeshAgent agent;
	public NPCArea attackArea;
	public NPCArea chaseArea;
	public List<EnemyAttack> attacksPositionList;

	public NPC_STATE enemyState {
		get {
			return State;
		}
		set {
			State = value;
		}
	}

	void Awake() {

		agent = GetComponent<NavMeshAgent>();
	}

	void Update() {
		print( "movement" );
		setState();
		//enemyAttack();
	}

	void setState() {
		var movement = System.Math.Abs( agent.velocity.magnitude / agent.speed );

		if(chaseArea.isTriggered && attackArea.isTriggered) {
			enemyState = NPC_STATE.Attack1;
		} else if(chaseArea.isTriggered) {
			enemyState = NPC_STATE.Run;
		} else {
			if(movement == 0) {
				enemyState = NPC_STATE.Idle;
			} else if(movement > 2) {
				enemyState = NPC_STATE.Jump;
				Debug.Log( "is Jump" );
			} else {
				enemyState = NPC_STATE.Patrol;
			}
		}


		Debug.Log( "state " + enemyState );
	}

	void enemyAttack() {
		if(enemyState == NPC_STATE.Attack1) {
			attacksPositionList.ForEach( area => enabled = true );
		} else
			attacksPositionList.ForEach( area => enabled = false );
	}

}
