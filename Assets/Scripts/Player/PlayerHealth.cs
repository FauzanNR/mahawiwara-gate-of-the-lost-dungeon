using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth: MonoBehaviour {
	public int startHealth = 100;
	public int currHealth;
	public Slider healthSlider;
	GameObject enemy;

	// Start is called before the first frame update
	void Start() {
		currHealth = startHealth;
	}

	// Update is called once per frame
	void Update() {
		//enemy = GameObject.FindGameObjectWithTag("Enemy");   searching every frame :'(
		healthSlider.value = currHealth;
		if(currHealth <= 0) GameManager.Instance.UpdateGameState( GameStates.Lose );
	}

	public void TakeAttack(int amount) {
		currHealth -= amount;
		healthSlider.value = currHealth;
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Enemy") {
			TakeAttack( 10 );
		}
	}
}
