using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed, rotationSpeed;

    private Vector2 rotation;
    private Rigidbody rb;
    private GameManager gameManager;
    private GameObject theDeckWeapon;

    public GameObject pedang, tombak;

    void Start()
    {
        theDeckWeapon = GameObject.Find("GameManager");
        gameManager = theDeckWeapon.GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vertic = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horiz, 0, vertic);
        movement = transform.TransformDirection(movement);

        rb.velocity = new Vector3(movement.x * speed * Time.deltaTime, rb.velocity.y, movement.z * speed * Time.deltaTime);

        rotation.x += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        rotation.y += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        rotation.y = Mathf.Clamp(rotation.y, -10, 10);
        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);

        Debug.Log("pedang : " + gameManager.weaponPedang);
        Debug.Log("tombak : " + gameManager.weaponTombak);

        if(gameManager.weaponPedang == true)
        {
            tombak.SetActive(false);
            pedang.SetActive(true);
        }
        else
        {
            pedang.SetActive(false);
        }

        if (gameManager.weaponTombak == true)
        {
            pedang.SetActive(false);
            tombak.SetActive(true);
        }
        else
        {
            tombak.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Pedang":
                gameManager.deckWeapon.SetActive(true);
                gameManager.tombak.SetActive(false);
                gameManager.pedang.SetActive(true);
                break;
            case "Tombak":
                gameManager.deckWeapon.SetActive(true);
                gameManager.pedang.SetActive(false);
                gameManager.tombak.SetActive(true);
                break;
            case "Door":
                SceneManager.LoadScene("Game");
                Destroy(this.gameObject);
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Pedang":
                gameManager.deckWeapon.SetActive(false);
                break;
            case "Tombak":
                gameManager.deckWeapon.SetActive(false);
                break;
        }
    }
}
