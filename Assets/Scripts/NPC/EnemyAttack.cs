using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack: MonoBehaviour {

	public int attackDamage = 10;
	//GameObject player;
	//PlayerHealth playerHealth;

	// Start is called before the first frame update
	//void Start() {
	//	player = GameObject.FindGameObjectWithTag( "Player" );
	//	playerHealth = player.GetComponent<PlayerHealth>();
	//}

	void Start() {
		attackDamage = this.gameObject.GetComponentInParent<NPCController>().damage;
	}
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.transform.TryGetComponent( out PlayerHealth playerHealth )) {
			playerHealth.TakeAttack( attackDamage );
		}
	}
}
