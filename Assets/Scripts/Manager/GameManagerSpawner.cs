using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSpawner: MonoBehaviour {

	public GameManager gameManager;
	void Awake() {
		if(GameObject.FindGameObjectWithTag( "GameController" ) == null) {
			var gm = Instantiate( gameManager );
		} else {
			GameManager.Instance.UpdateGameState( GameStates.MainMenu );
		}
	}
}
