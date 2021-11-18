using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float mouseSensitifity=100;
    public Transform playerBody;
    public Transform aimTarget;
    float xRotation=0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX=Input.GetAxis("Mouse X")*mouseSensitifity*Time.deltaTime;
        float mouseY=Input.GetAxis("Mouse Y")*mouseSensitifity*Time.deltaTime;
        
        xRotation -=mouseY;
        xRotation=Mathf.Clamp(xRotation,-20f,20f);  //batas kamera atas bawah

        aimTarget.localRotation=Quaternion.Euler(xRotation,0f,0f);  //vertical move camera
        playerBody.Rotate(Vector3.up*mouseX);                       //horizontal move camera
    }
}
