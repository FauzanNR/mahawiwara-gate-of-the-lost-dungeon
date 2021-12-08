using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCMovement: MonoBehaviour {
	NavMeshAgent agent;
	NPCController controller;
	GameObject player;
	public Collider chaseArea;
	public LayerMask layer;
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
			//StartCoroutine( Move( player.transform.position, 0 ) );

			Move( player.transform.position, 0 );
		} else if(movement == 0f &&
			controller.enemyState != NPC_STATE.Attack1 &&
			controller.enemyState == NPC_STATE.Idle && seconds % 2 == 0) {
			seconds = 0;
			//StartCoroutine( Move( randomPosition.generateRandomPosition( chaseArea, 1f, layer ), 2 ) );
			Move( randomPosition.generateRandomPosition( chaseArea, 1f, layer ), 2 );
		}
	}


	void Move(Vector3 destination, float delay) {
		Vector3 lookPos = destination - transform.position;
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation( lookPos );
		transform.rotation = Quaternion.Slerp( transform.rotation, rotation, 0.5f );

		agent.SetDestination( destination );
	}

	//IEnumerator Move(Vector3 destination, int delay) {
	//	yield return new WaitForSeconds( delay );
	//	Vector3 lookPos = destination - transform.position;
	//	lookPos.y = 0;
	//	Quaternion rotation = Quaternion.LookRotation( lookPos );
	//	transform.rotation = Quaternion.Slerp( transform.rotation, rotation, 0.5f );

	//	agent.SetDestination( destination );

	//}
}
