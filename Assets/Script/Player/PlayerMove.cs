using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //Variable
    public GameObject pedang, tombak;
    private float speed=100;
    private float horizontal;
    private float vertical;
    private Vector3 move;
    private GameObject theDeckWeapon;

    //Reference
    private Rigidbody rb;
    private Animator anim;
    private GameManager gameManager;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        theDeckWeapon = GameObject.Find("GameManager");
        gameManager = theDeckWeapon.GetComponent<GameManager>();
    }

    private void Update()
    {
        //Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        //Set Value
        anim.SetFloat("VelocityX", horizontal);
        anim.SetFloat("VelocityZ", vertical);
        move=transform.right*horizontal+transform.forward*vertical;


        if(Input.GetKey(KeyCode.LeftShift)&&vertical>0.9){
            anim.SetBool("Dash",true);
            speed=150;
        }
        else
        {
            anim.SetBool("Dash",false);
            speed=100;
        }

        if (gameManager.weaponPedang == true)
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

    void FixedUpdate()
    {
        rb.velocity=move*speed*Time.fixedDeltaTime;
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
                /*      Destroy(this.gameObject);*/
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

    private void OnTriggerEnter(Collider other)
    {
        Transform leftHand = GameObject.FindGameObjectWithTag("LeftHand").transform;
        if (other.gameObject.tag == "Key")
        {
            GameObject key = GameObject.FindGameObjectWithTag("Key");
            key.transform.parent = leftHand;
        }
    }
}
