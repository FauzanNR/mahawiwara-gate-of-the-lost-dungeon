using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager: MonoBehaviour {
	public GameObject[] weapon;
	void Start() {
		GameManager.Instance.UpdateGameState( GameStates.Lobby );
		PlayerDataManager.Load();
		int level = PlayerDataManager.player.level;
		print( level );
		weapon[0].SetActive( true );
		for(int i = 0; i <= level; i++) {


		}
	}
}

