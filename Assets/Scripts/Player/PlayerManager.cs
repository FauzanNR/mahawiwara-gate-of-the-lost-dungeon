using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class PlayerManager: MonoBehaviour {
	public GameObject[] weapon;
	public GameObject keyObject;
	public GameObject berkah;
	public GameObject fireBlessImg;
	public GameObject earthBlessImg;

	private InteractableObject interactable;
	private string weaponName;
	private Animator animator;

	private bool isPaused;
	public GameObject pausePanel;
	public bool isDead = false;
	void Awake() {
		GameManager.OnStateChange += setOffPlayer;

	}


	private void setOffPlayer(GameStates states) {

		if(states == GameStates.LoadingLevel) {
			isDead = false;
			berkah = GameManager.Instance.getBerkah();
		} else if(states == GameStates.Lobby) {


		} else if(states == GameStates.Lose) {
			isDead = true;
			gameObject.GetComponent<PlayerMove>().enabled = false;
			gameObject.GetComponentInChildren<CameraMove>().enabled = false;
			gameObject.GetComponent<CombatCondition>().enabled = false;
			animator.SetLayerWeight( 0, 0 );
			animator.SetLayerWeight( 1, 0 );

			animator.SetLayerWeight( 2, 1 );
			animator.Play( "player_death", 2 );

			Invoke( nameof( offAnima ), 5 );
		} else if(states == GameStates.MainMenu) {
			//this?.gameObject?.SetActive( false );
		}// else gameObject?.SetActive( true );

	}
	void OnDestroy() {
		print( "fuckyiu" );

		GameManager.OnStateChange -= setOffPlayer;


	}
	void offAnima() {
		animator.enabled = false;
		animator.SetLayerWeight( 2, 0 );
		animator.enabled = true;
		gameObject.GetComponent<PlayerMove>().enabled = true;
		gameObject.GetComponentInChildren<CameraMove>().enabled = true;
		gameObject.GetComponent<CombatCondition>().enabled = true;
	}
	private void Start() {
		animator = GetComponent<Animator>();
		PlayerDataManager.Load();
		weaponName = PlayerDataManager.player.weaponName;
		if(weaponName != null) {
			foreach(GameObject weaponObject in weapon) {
				if(weaponObject.name == weaponName) {
					weaponObject.SetActive( true );
				} else {
					weaponObject.SetActive( false );
				}
			}
		}

		if(berkah.GetComponent<BerkahAlam>().berkahName == "EarthBerkah") {
			earthBlessImg?.SetActive( true );
		} else { fireBlessImg?.SetActive( true ); }
	}

	void Update() {

		if(interactable) {
			ChoiceWeapon();

			OpenChest();

			OpenDoor();
		}
		if(Input.GetKeyDown( KeyCode.Tab )) {
			if(isPaused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	public void Pause() {
		pausePanel.SetActive( true );
		Time.timeScale = 0f;
		isPaused = true;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void Resume() {
		pausePanel.SetActive( false );
		Time.timeScale = 1f;
		isPaused = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void playParticle() {// called in animation event
		var position = new Vector3( transform.position.x, transform.position.y, transform.position.z + 1.5f );
		var berkahAlam = Instantiate( berkah, transform );
		var berkahComponent = berkahAlam.GetComponent<BerkahAlam>();
		if(berkahComponent.berkahName == "EarthBerkah") berkahAlam.transform.parent = null;
		berkahComponent.playParticle();
	}

	void ChoiceWeapon() {
		foreach(GameObject weaponObject in weapon) {
			if(Input.GetKey( KeyCode.F ) && interactable.EquipWeapon()) {
				if(interactable.name == weaponObject.name) {
					weaponName = weaponObject.name;
				}

				if(weaponName == weaponObject.name) {
					weaponObject.SetActive( true );
					PlayerDataManager.player.weaponName = weaponName;
					PlayerDataManager.Save();
				} else {
					weaponObject.SetActive( false );
				}
			}
		}
	}

	void OpenChest() {
		if(interactable.tag == "Chest" && interactable.onIneteractedByPlayer() && !GameManager.Instance.isKeyFound) {
			GameManager.Instance.isKeyFound = true;
			keyObject.SetActive( true );
		}
	}

	void OpenDoor() {
		if(interactable.name == "DoorBoss" && interactable.onIneteractedByPlayer() && GameManager.Instance.isKeyFound == true) {
			GameManager.Instance.isKeyFound = false;
			keyObject.SetActive( false );
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<InteractableObject>() != null) {
			interactable = other.GetComponent<InteractableObject>();
			interactable.onInteracting();
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.GetComponent<InteractableObject>() != null) {
			interactable.DeSelected();
		}
	}
}