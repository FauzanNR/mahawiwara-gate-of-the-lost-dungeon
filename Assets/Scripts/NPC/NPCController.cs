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

	private NPC_STATE State;
	private GameObject player;
	NavMeshAgent agent;
	public AreaHelper attackArea;
	public AreaHelper chaseArea;
	public List<EnemyAttack> attacksPositionList;
	public NPCHealth health;
	public int damage;
	public float rayYpost;
	public int rayDistance;
	public LayerMask mask;

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
		RaycastHit hit;

		Debug.DrawRay( transform.position + Vector3.up * rayYpost, transform.forward * rayDistance, Color.red );
		Debug.DrawRay( transform.position + Vector3.up * rayYpost, (transform.forward + transform.right).normalized * rayDistance, Color.red );
		Debug.DrawRay( transform.position + Vector3.up * rayYpost, (transform.forward - transform.forward * 2.5f).normalized * rayDistance, Color.red );
		Debug.DrawRay( transform.position + Vector3.up * rayYpost, (transform.right - transform.forward * 1.5f).normalized * rayDistance, Color.red );

		//if(chaseArea.isTriggered && chaseArea.colliderObj()?.transform?.root?.tag == "Player") {
		Debug.DrawRay( transform.position + Vector3.up * rayYpost, (transform.forward - transform.right).normalized * rayDistance, Color.red );
		if(Physics.Raycast( transform.position + Vector3.up * rayYpost, transform.forward, out hit, rayDistance, mask )
		|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.forward + transform.right).normalized, out hit, rayDistance, mask )
		|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.forward - transform.right).normalized, out hit, rayDistance, mask )
	  	|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.forward - transform.right * 0.5f).normalized, out hit, rayDistance, mask )
		|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.forward + transform.right * 0.5f).normalized, out hit, rayDistance, mask )
	  	|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.forward - transform.right * 1.5f).normalized, out hit, rayDistance, mask )
		|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.forward + transform.right * 1.5f).normalized, out hit, rayDistance, mask )
	 	|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.forward - transform.right * 2.6f).normalized, out hit, rayDistance, mask )
		|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.forward + transform.right * 2.6f).normalized, out hit, rayDistance, mask )
	  	|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (transform.right - transform.forward * 1.5f).normalized, out hit, rayDistance, mask )
		|| Physics.Raycast( transform.position + Vector3.up * rayYpost, (-transform.right - transform.forward * 1.5f).normalized, out hit, rayDistance, mask )
	  ) {
			print( hit.collider.name );
			if(hit.collider.gameObject.tag == "Player") {
				var distance = Vector3.Distance( player.transform.position, transform.position );
				if(attackArea.isTriggered || attackArea.colliderObj()?.transform?.root?.tag == "Player") {
					enemyState = NPC_STATE.Attack1;
				} else if(distance > 3f) {
					enemyState = NPC_STATE.Run;
				}

			}

		} else {
			if(movement == 0f) {
				enemyState = NPC_STATE.Idle;
			} else if(movement > 1.5f) {
				enemyState = NPC_STATE.Jump;
			} else {
				enemyState = NPC_STATE.Patrol;
			}


		}

		print( enemyState );
		var rigBody = gameObject.GetComponent<Rigidbody>();
		var colider = gameObject.GetComponent<Collider>();
		if(rigBody == null && colider == null) {
			rigBody = gameObject.GetComponentInChildren<Rigidbody>();
			colider = gameObject.GetComponentInChildren<Collider>();
		}
		if(health.currHealth < 0) {
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
