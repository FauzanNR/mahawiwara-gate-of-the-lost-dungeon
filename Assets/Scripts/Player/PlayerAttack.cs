using UnityEngine;

public class PlayerAttack: MonoBehaviour {
	private WeaponStats weaponStat;
	private PlayerManager manager;

	void Start() {
		var player = gameObject;

		weaponStat = player.GetComponent<WeaponStats>();
		manager = player.GetComponent<PlayerManager>();
	}

	public int Damage() {
		return weaponStat.DamageWeaponSkill() != null || manager.berkah.GetComponent<BerkahAlam>().damage != null
			? manager.berkah.GetComponent<BerkahAlam>().damage
			: 0;
	}
}
