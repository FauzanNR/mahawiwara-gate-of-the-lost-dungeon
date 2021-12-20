using UnityEngine;

public class NPCHealth: MonoBehaviour {
	public int startHealth = 100;
	public int currHealth;
	public int attack = 10;

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

		if(currHealth <= 0) {
			if(this.transform.parent != null) {
				Destroy( this.transform.parent.gameObject );
			}
			Destroy( this.gameObject );
		}
	}

	public void getDamage(int damage) {
		currHealth -= damage;
	}

	void OnTriggerEnter(Collider other) {

		var weaponDamage = weaponPlayer.GetComponentsInChildren<WeaponStats>();

		foreach(WeaponStats weapon in weaponDamage) {
			if(other.GetComponent<WeaponStats>() != null) {
				getDamage( weapon.DamageWeaponSkill() );
			}
		}

	}
}
