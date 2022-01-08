using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager: MonoBehaviour {

	public bool isKeyFound = false;
	public GameObject Loading;
	public GameStates gameStates;
	public static GameManager Instance;
	public GameObject[] Berkah;
	public GameObject[] EndRooms;
	public static event Action<GameStates> OnStateChange;

	public int level {
		get {
			return PlayerDataManager.player.level;
		}
	}

	void Awake() {
		if(Instance == null) {
			Instance = this;
		}
		DontDestroyOnLoad( this.gameObject );
	}

	GameObject getEndRoom => EndRooms[level];
	GameObject getBerkah() => Berkah[UnityEngine.Random.Range( 0, Berkah.Length - 1 )];

	public void LaodScene(int sceneIndex) {
		SceneManager.LoadScene( sceneIndex );
	}

	public void UpdateGameState(GameStates newState) {
		gameStates = newState;

		switch(gameStates) {
			case GameStates.MainMenu:

				break;
			case GameStates.Lobby:
				PlayerDataManager.Load();
				PlayerDataManager.Save();
				break;
			case GameStates.Game:
				Loading.SetActive( false );
				break;
			case GameStates.LoadingLevel:
				PlayerDataManager.Load();
				Loading.SetActive( true );
				break;
			case GameStates.Victory:
				PlayerDataManager.player.level += 1;
				break;
			case GameStates.Lose:
				PlayerDataManager.player.level = 0;
				break;
			default: throw new ArgumentOutOfRangeException( nameof( gameStates ), gameStates, null );
		}

		OnStateChange?.Invoke( gameStates );

	}
}

public enum GameStates {
	MainMenu,
	Lobby,
	Game,
	LoadingLevel,
	Victory,
	Lose
}
