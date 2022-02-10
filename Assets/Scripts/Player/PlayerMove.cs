using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove: MonoBehaviour {
	[Header( "Variable" )]
	private float moveSpeed = 3;
	private string currentState;
	private float transformX;
	private float transformZ;
	private float jumpForce = 2f;
	private float gravity = -20f;

	//Ground Check
	public Transform groundCheck;
	private float groundDistance = 0.15f;
	public LayerMask groundMask;
	bool isGrounded;

	Vector3 move;
	Vector3 velocity;

	[Header( "Reference" )]
	private Animator anim;
	private CharacterController controller;

	public AudioSource step;

	public void stepAudio() => step.Play();

	void Start() {
		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
	}
	private void Update() {
		//Input
		transformX = Input.GetAxis( "Horizontal" );
		transformZ = Input.GetAxis( "Vertical" );



		//ground check
		isGrounded = Physics.CheckSphere( groundCheck.position, groundDistance, groundMask );
		if(isGrounded && velocity.y < 0) {
			velocity.y = -2f;
			ChangeAnimationState( "Movement" );
		}




		//Gravity setting
		velocity.y += gravity * Time.deltaTime;
		controller.Move( velocity * Time.deltaTime );
		if(!Input.GetButtonDown( "Fire1" ) && (!anim.GetCurrentAnimatorStateInfo( 1 ).IsName( "Attack1" ) && !anim.GetCurrentAnimatorStateInfo( 1 ).IsName( "Attack2" ) && !anim.GetCurrentAnimatorStateInfo( 1 ).IsName( "Attack3" )) && !anim.GetCurrentAnimatorStateInfo( 2 ).IsName( "BerkahAttack" )) {
			Movement();
		}

		if(Input.GetKey( KeyCode.LeftShift )) {
			Dash();
		} else {
			Walk();
		}

		//Set Value animasi
		anim.SetFloat( "VelocityX", transformX );
		anim.SetFloat( "VelocityZ", transformZ );
	}
	private void FixedUpdate() {
		if(Input.GetButton( "Jump" ) && isGrounded) {
			Jump();
		}
	}

	public void Movement() {
		move = transform.right * transformX + transform.forward * transformZ;
		controller.Move( move * moveSpeed * Time.deltaTime );
	}

	public void Dash() {

		anim.SetBool( "Dash", true );
		moveSpeed = 6;
		transformZ *= 2;
	}

	public void Walk() {

		anim.SetBool( "Dash", false );
		moveSpeed = 3;
	}

	public void Jump() {
		velocity.y = Mathf.Sqrt( jumpForce * -2f * gravity );
		ChangeAnimationState( "Jump" );
	}
	void ChangeAnimationState(string newState) {
		if(currentState == newState) return;
		anim.Play( newState );
		currentState = newState;
	}
}