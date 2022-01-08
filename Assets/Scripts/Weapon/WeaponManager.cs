using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager: MonoBehaviour {
	public GameObject[] weapon;
	void Start() {
		GameManager.Instance.UpdateGameState( GameStates.Lobby );
		PlayerDataManager.Load();
		int level = PlayerDataManager.player.level;
		for(int i = 0; i <= level; i++) {
			weapon[i].SetActive( true );
		}
	}
}

