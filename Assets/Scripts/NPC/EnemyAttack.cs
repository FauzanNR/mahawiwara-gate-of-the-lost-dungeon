using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack: MonoBehaviour {

	public int attackDamage = 10;
	GameObject player;
	PlayerHealth playerHealth;

	// Start is called before the first frame update
	void Start() {
		player = GameObject.FindGameObjectWithTag( "Player" );
		playerHealth = player.GetComponent<PlayerHealth>();
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject == player) {
			playerHealth.TakeAttack( attackDamage );
		}
	}
}
