using UnityEngine;

public class NpcHealth: MonoBehaviour {
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

	void getWeapon() => weaponPlayer = GameObject.FindGameObjectWithTag( "WeaponPlayer" );

	void Update() {

		if(currHealth <= 0) {
			if(this.transform.parent != null) {
				Destroy( this.transform.parent.gameObject );
			}
			Destroy( this.gameObject );
		}
	}

	public void getDamage(int amount) {
		currHealth -= amount;
	}

	void OnTriggerEnter(Collider other) {
		var d = weaponPlayer.GetComponent<WeaponStats>().Damage();
		print( "damage = " + d );
		if(other.gameObject == weaponPlayer) {

			getDamage( d );
		}
	}
}
