using UnityEngine;
using System.Collections;

public class NPCHealth: MonoBehaviour {
	public int startHealth = 100;
	public int currHealth;
	public int attack = 10;
	public bool destroyAfterDeath = true;
	/*public Slider healthSlider;*/
	private GameObject weaponPlayer;
	private GameObject player;

	void Start() {

		player = GameObject.FindGameObjectWithTag( "Player" );
		Invoke( "getWeapon", 3 );
		currHealth = startHealth;
	}

	void getWeapon() => weaponPlayer = GameObject.FindGameObjectWithTag( "Player" );

	void Update() {

		if(currHealth <= 0 && destroyAfterDeath) {
			Invoke( nameof( destroy ), 10 );
		}
	}

	void destroy() {
		Destroy( this.transform.root.gameObject );
	}

	public void getDamage(int damage) {
		currHealth -= damage;
	}

	void OnTriggerEnter(Collider other) {

		if(other.TryGetComponent( out WeaponStats weapon )) {
			getDamage( weapon.DamageWeaponSkill() );
		}

		if(other.TryGetComponent( out BerkahAlam berkah )) {
			getDamage( berkah.damage );
			print( "npc + " + other.gameObject.name );
		}
	}
}
