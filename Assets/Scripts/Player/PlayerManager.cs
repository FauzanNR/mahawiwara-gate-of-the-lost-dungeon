using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager: MonoBehaviour {

	public GameObject panelText;
	InteractableObject interactable;
	public GameObject pedang, tombak;
	private GameObject player;
	public bool weaponPedang = false, weaponTombak = false;

	//private void Awake() {
	//	GameObject[] objs = GameObject.FindGameObjectsWithTag( "GameManager" );

	//	if(objs.Length > 1) {
	//		Destroy( objs[0] );
	//	}

	//	DontDestroyOnLoad( this.gameObject );
	//}

	public void choiceWeapon() {
		string weaponName = interactable.name;
		if(weaponName == "Pedang") {
			tombak.SetActive( false );
			pedang.SetActive( true );
			weaponTombak = false;
			weaponPedang = true;
			interactable.transform.position = pedang.transform.position;
		} else if(weaponName == "Tombak") {
			pedang.SetActive( false );
			tombak.SetActive( true );
			weaponPedang = false;
			weaponTombak = true;
			interactable.transform.position = pedang.transform.position;

		}
	}

	void Update() {
		//Debug.Log( "pedang : " + weaponPedang );
		//Debug.Log( "tombak : " + weaponTombak );

		if(weaponPedang == true) {
			tombak.SetActive( false );
			pedang.SetActive( true );
		} else {
			pedang.SetActive( false );
		}

		if(weaponTombak == true) {
			pedang.SetActive( false );
			tombak.SetActive( true );
		} else {
			tombak.SetActive( false );
		}
	}


	public string interactableObjectName() {
		return interactable.name;
	}
	//inisialisasi state/kondisi player berinteraksi dengan object yg interacable dengan collider trigger
	void OnTriggerEnter(Collider other) {
		if(other.GetComponent<InteractableObject>() != null) {

			Cursor.lockState = CursorLockMode.None;
			interactable = other.GetComponent<InteractableObject>();
			interactable.onInteracting();

			panelText.SetActive( interactable.onIneteractedByPlayer() );
			panelText.GetComponentInChildren<Text>().text = interactable.GetInformation();

		}
	}

	void OnTriggerExit(Collider other) {
		if(other.GetComponent<InteractableObject>() != null) {
			//interactable = other.GetComponent<InteractableObject>();
			interactable.DeSelected();
			panelText.SetActive( interactable.onIneteractedByPlayer() );
			Cursor.lockState = CursorLockMode.Locked;
		}
	}
}
