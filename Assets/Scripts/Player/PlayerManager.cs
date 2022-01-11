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
	private InteractableObject interactable;
	private string weaponName;

	void Awake() {
		GameManager.OnStateChange += setOffPlayer;

	}


	private void setOffPlayer(GameStates states) {
		print( states );
		if(states == GameStates.LoadingLevel) {
			berkah = GameManager.Instance.getBerkah();
			gameObject.SetActive( false );
		} else if(states == GameStates.Lose) {
			gameObject.GetComponent<PlayerMove>().enabled = false;
			gameObject.GetComponentInChildren<CameraMove>().enabled = false;
			gameObject.GetComponent<CombatCondition>().enabled = false;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		} else gameObject.SetActive( true );

	}

	private void Start() {
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
		DontDestroyOnLoad( this.gameObject );
	}

	void Update() {

		if(interactable) {
			ChoiceWeapon();

			OpenChest();

			OpenDoor();
		}
	}

	void playParticle() {// called in animation event
		var position = new Vector3( transform.position.x, transform.position.y, transform.position.z + 1.5f );
		var berkahAlam = Instantiate( berkah, transform );
		berkahAlam.transform.parent = null;
		berkahAlam.GetComponent<BerkahAlam>().playParticle();
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