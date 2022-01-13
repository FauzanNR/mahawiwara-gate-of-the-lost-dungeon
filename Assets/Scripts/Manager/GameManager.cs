using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager: MonoBehaviour {

	public bool isKeyFound = false;
	public GameObject LoadingCanvas;
	public GameObject VictoryCanvas;
	public GameObject LoseCanvas;
	public GameStates gameStates;
	public List<GameObject> Berkah;
	public List<GameObject> EndRooms;
	public static event Action<GameStates> OnStateChange;
	public static GameManager Instance;
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

	public void Quit() => Application.Quit();
	public GameObject getEndRoom => EndRooms[level];
	public GameObject getBerkah() => Berkah[UnityEngine.Random.Range( 0, Berkah.Count - 1 )];

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

				break;
			case GameStates.Game:
				LoadingCanvas.SetActive( false );
				break;
			case GameStates.LoadingLevel:
				PlayerDataManager.Load();
				LoadingCanvas.SetActive( true );
				VictoryCanvas.SetActive( false );
				LoseCanvas.SetActive( false );
				break;
			case GameStates.Victory:
				VictoryCanvas.SetActive( true );
				PlayerDataManager.player.level += 1;
				break;
			case GameStates.Lose:
				LoseCanvas.SetActive( true );
				PlayerDataManager.player.level = 0;
				break;
			default: throw new ArgumentOutOfRangeException( nameof( gameStates ), gameStates, null );
		}
		PlayerDataManager.Save();
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
