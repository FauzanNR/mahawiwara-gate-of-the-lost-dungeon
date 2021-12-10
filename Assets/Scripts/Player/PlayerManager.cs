using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PlayerManager: MonoBehaviour {
	public GameObject[] weapon;
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
		//system pause and resume
		/*if (Input.GetKey(KeyCode.Escape))
		{
		    Time.timeScale = 0;
		}else if (Input.GetKey(KeyCode.U))
		{
		    Time.timeScale = 1;
		}*/

		if(interactable) {
			foreach(GameObject weaponObject in weapon) {
				if(Input.GetKey( KeyCode.F )) {
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
			/* for (int i = 0; i < weapon.Length; i++)
			 {
			     if (Input.GetKey(KeyCode.F))
			     {
				   if (interactable.name == weapon[i].name)
				   {
					 weaponName = interactable.name;
				   }

				   if (weaponName == weapon[i].name)
				   {
					 weapon[i].SetActive(true);
					 PlayerDataManager.player.weaponName = weaponName;
					 PlayerDataManager.Save();
				   }
				   else
				   {
					 weapon[i].SetActive(false);
				   }
			     }

			 }*/
			Debug.Log( "aaaaaa" + interactable.onIneteractedByPlayer() );

			if(interactable.name == "Chest(Clone)" && interactable.onIneteractedByPlayer() && PlayerDataManager.player.key == false) {
				PlayerDataManager.player.key = true;
				PlayerDataManager.Save();
				keyObject.SetActive( true );
			}

			if(interactable.name == "DoorBoss" && interactable.onIneteractedByPlayer() && PlayerDataManager.player.key == true) {
				Debug.Log( "AAAAAAAAa" );
				PlayerDataManager.player.key = false;
				PlayerDataManager.Save();
				keyObject.SetActive( false );
			}
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