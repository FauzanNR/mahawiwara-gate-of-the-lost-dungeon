using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCMovement: MonoBehaviour {
	NavMeshAgent agent;
	NPCController controller;
	GameObject player;
	public Collider chaseArea;
	public LayerMask avoidanceLayer;
	public int IdleTime = 3;
	PositionHelper randomPosition = new PositionHelper();
	float timer = 0.0f;

	void Start() {
		controller = GetComponent<NPCController>();
		agent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag( "Player" );

	}

	void Update() {
		var movement = System.Math.Abs( agent.velocity.magnitude / agent.speed );
		timer += Time.deltaTime;
		int seconds = ( int )(timer % 60);

		if(controller.enemyState == NPC_STATE.Run) {
			Move( player.transform.position );
		} else if(movement == 0f &&
			controller.enemyState != NPC_STATE.Attack1 &&
			controller.enemyState == NPC_STATE.Idle && seconds % IdleTime == 0) {
			seconds = 0;
			Move( randomPosition.generateRandomPosition( chaseArea, 1f, avoidanceLayer ) );
		} else if(controller.enemyState == NPC_STATE.Attack1) {
			transform.LookAt( player.transform.position );
		}
	}


	void Move(Vector3 destination) {
		transform.LookAt( destination );
		agent.SetDestination( destination );
	}
}
