using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    private float mouseSensitifity=100;
    private GameObject theDeckWeapon;
    private PlayerManager playerManager;

    public Transform playerBody;
    float xRotation=0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Lobby")
        {
            theDeckWeapon = GameObject.Find("PlayerManager");
            playerManager = theDeckWeapon.GetComponent<PlayerManager>();

            if (playerManager.deckWeapon.activeSelf)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitifity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitifity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -30f, 10f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitifity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitifity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -30f, 10f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
    }
}
