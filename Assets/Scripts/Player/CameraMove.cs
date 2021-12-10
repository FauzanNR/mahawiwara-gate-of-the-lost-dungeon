using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove: MonoBehaviour {
	public float mouseSensitifity = 100;
	public Transform playerBody;
	public Transform aimTarget;
	float xRotation = 0f;
	public CinemachineVirtualCamera cinemachine;
	// Start is called before the first frame update
	void Start() {
		//Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	// Update is called once per frame
	void Update() {
		float axis = Input.GetAxis( "Vertical" );
		float mouseX = Input.GetAxis( "Mouse X" ) * mouseSensitifity * Time.deltaTime;
		float mouseY = Input.GetAxis( "Mouse Y" ) * mouseSensitifity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp( xRotation, -40f, 40f );  //batas kamera atas bawah

		aimTarget.localRotation = Quaternion.Euler( xRotation, 0f, 0f );  //vertical move camera
		playerBody.Rotate( Vector3.up * mouseX );                       //horizontal move camera

		//Field OF View
		if(Input.GetKey( KeyCode.LeftShift ) && cinemachine.m_Lens.FieldOfView <= 50 && axis > 0) {
			cinemachine.m_Lens.FieldOfView += 0.2f;
		} else if(cinemachine.m_Lens.FieldOfView > 40) {
			cinemachine.m_Lens.FieldOfView -= 0.2f;
		}
	}
}