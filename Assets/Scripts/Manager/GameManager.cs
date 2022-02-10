using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager: MonoBehaviour {

	public bool isKeyFound = false;
	public GameObject player;
	public GameObject MainMenuCanvas;
	public GameObject pausePanel;
	public GameObject LoadingCanvas;
	public GameObject VictoryCanvas;
	public GameObject LoseCanvas;
	public GameStates gameStates;
	public List<GameObject> Berkah;
	public List<Room> EndRooms;
	public static event Action<GameStates> OnStateChange;
	public static GameManager Instance;
	private bool isPause = false;

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

	void Start() {
		player.SetActive( false );
	}

	public void Quit() => Application.Quit();
	public Room getEndRoom => EndRooms[level];
	public GameObject getBerkah() => Berkah[UnityEngine.Random.Range( 0, Berkah.Count - 1 )];

	public void LaodScene(int sceneIndex) {
		SceneManager.LoadScene( sceneIndex );
	}

	//void Update() {
	//	if(Input.GetKeyDown( KeyCode.Tab )) {
	//		print( isPause );
	//		if(isPause && gameStates != GameStates.MainMenu
	//		|| gameStates != GameStates.LoadingLevel
	//		|| gameStates != GameStates.Victory
	// 		|| gameStates != GameStates.Lose) {
	//			resumeState();
	//		} else {
	//			Pause();
	//		}
	//	}
	//	pausePanel.SetActive( isPause );
	//}

	//private void Pause() {
	//	isPause = true;
	//	cursorOn();
	//	Time.timeScale = 0f;
	//}

	//private void resumeState() {
	//	cursorOff();
	//	isPause = false;
	//	Time.timeScale = 1f;
	//}


	void cursorOn() {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
	void cursorOff() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void UpdateGameState(GameStates newState) {
		gameStates = newState;
		print( gameStates );
		switch(gameStates) {
			case GameStates.MainMenu:
				MainMenuCanvas.SetActive( true );
				cursorOn();
				LoadingCanvas.SetActive( false );
				VictoryCanvas.SetActive( false );
				LoseCanvas.SetActive( false );
				player.SetActive( false );
				break;
			case GameStates.Lobby:
				cursorOff();
				isKeyFound = false;
				LoadingCanvas.SetActive( false );
				VictoryCanvas.SetActive( false );
				LoseCanvas.SetActive( false );
				if(player != null) {
					player?.SetActive( true );
				}
				if(PlayerDataManager.player.level >= EndRooms.Count) {
					PlayerDataManager.player.level = 0;
				}
				player.transform.position = new Vector3( 77, 19, 60 );
				PlayerDataManager.Load();

				break;
			case GameStates.Game:
				LoadingCanvas.SetActive( false );
				player?.SetActive( true );
				cursorOff();
				break;
			case GameStates.LoadingLevel:
				PlayerDataManager.Load();
				LoadingCanvas.SetActive( true );
				VictoryCanvas.SetActive( false );
				LoseCanvas.SetActive( false );
				player?.SetActive( false );
				break;

			case GameStates.Victory:
				cursorOn();
				VictoryCanvas.SetActive( true );
				print( "level" + level );
				if(PlayerDataManager.player.level >= EndRooms.Count) {
					PlayerDataManager.player.level = 0;
				}
				break;
			case GameStates.Lose:
				cursorOn();
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
