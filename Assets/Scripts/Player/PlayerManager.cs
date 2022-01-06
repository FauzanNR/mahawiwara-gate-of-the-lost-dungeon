using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PlayerManager: MonoBehaviour {
	public GameObject[] weapon;
	public GameObject berkah;
	public GameObject keyObject;

	private InteractableObject interactable;
	private string weaponName;

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
	}

	void Update() {

		if(interactable) {
			ChoiceWeapon();

			OpenChest();

			OpenDoor();
		}
	}

	void playParticle() {// called in animation event
				   //var position = new Vector3( transform.position.x, transform.position.y, transform.position.z + 1.5f );
				   //var berkahAlam = Instantiate( berkah, position, Quaternion.identity );
		berkah.GetComponent<BerkahAlam>().playParticle();
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
		if(interactable.name == "Chest(Clone)" && interactable.onIneteractedByPlayer() && PlayerDataManager.player.key == false) {
			PlayerDataManager.player.key = true;
			PlayerDataManager.Save();
			keyObject.SetActive( true );
		}
	}

	void OpenDoor() {
		if(interactable.name == "DoorBoss" && interactable.onIneteractedByPlayer() && PlayerDataManager.player.key == true) {
			PlayerDataManager.player.key = false;
			PlayerDataManager.Save();
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