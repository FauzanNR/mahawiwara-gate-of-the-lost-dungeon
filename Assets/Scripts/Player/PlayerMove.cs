using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour {
	//Variable
	private float moveSpeed;
	private float transformX;
	private float transformZ;
	private Vector3 move;

	//Reference
	private Rigidbody rb;
	private Animator anim;

	void Start() {

		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
	}
	private void Update() {
		//Input
		transformX = Input.GetAxis( "Horizontal" );
		transformZ = Input.GetAxis( "Vertical" );

		//Set Value
		anim.SetFloat( "VelocityX", transformX );
		anim.SetFloat( "VelocityZ", transformZ );

		move = transform.right * transformX + transform.forward * transformZ; //movement

		if(Input.GetKey( KeyCode.LeftShift )) {
			anim.SetBool( "Dash", true );
			moveSpeed = 5;
		} else if(Input.GetButtonDown( "Jump" )) {
			rb.AddForce( Vector3.up * 200 );
		} else {
			anim.SetBool( "Dash", false );
			moveSpeed = 3;
		}
	}

	void FixedUpdate() {
		transform.Translate( move * moveSpeed * Time.deltaTime, Space.World ); //playermovement

		if(Input.GetButtonDown( "Jump" )) {
			rb.AddForce( Vector3.up * 200 );
		}
	}
}
