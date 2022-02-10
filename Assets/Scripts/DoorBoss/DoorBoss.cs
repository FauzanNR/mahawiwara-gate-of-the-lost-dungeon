using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DoorBoss: InteractableObject {
	private Animator anim1;
	private Animator anim2;
	private bool open = false;
	private bool interacted = false;

	public GameObject Key, NonKey;
	public bool checkIn = false;
	public AreaHelper checkPoint;
	public GameObject door1;
	public GameObject door2;

	private void Start() {
		anim1 = door1.gameObject.GetComponent<Animator>();
		anim2 = door2.gameObject.GetComponent<Animator>();
	}

	public override bool onIneteractedByPlayer() {
		var key = GameManager.Instance.isKeyFound;
		checkIn = checkPoint.isTriggered;
		if(base.isInteracted && key == true && open == false) {
			Key.SetActive( base.isInteracted );
			if(Input.GetKeyDown( KeyCode.F )) {
				Key.SetActive( false );
				open = true;
				anim1.SetBool( "Openn", base.isInteracted );
				anim2.SetBool( "Openn", base.isInteracted );
				return interacted = true;
			}
		} else if(base.isInteracted && key == false && open == false) {
			NonKey.SetActive( base.isInteracted );
			return interacted = false;
		} else if(!base.isInteracted && checkIn == true) {
			NonKey.SetActive( false );
			anim1.SetBool( "Openn", false );
			anim2.SetBool( "Openn", false );
			return interacted = false;
		} else if(!base.isInteracted) {
			NonKey.SetActive( false );
			Key.SetActive( false );
			return interacted = false;
		}
		return interacted;
	}
}
