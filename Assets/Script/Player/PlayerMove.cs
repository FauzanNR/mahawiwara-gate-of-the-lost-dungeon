using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //Variable
    public GameObject pedang, tombak;
    private float speed=500;
    private float horizontal;
    private float vertical;
    private Vector3 move;
    private GameObject theDeckWeapon, manager;
    private int level;

    //Reference
    private Rigidbody rb;
    private Animator anim;
    private PlayerManager playerManager;
    private GameManager gameManager;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        theDeckWeapon = GameObject.Find("PlayerManager");
        playerManager = theDeckWeapon.GetComponent<PlayerManager>();

        if(SceneManager.GetActiveScene().name == "Game")
        {
            manager = GameObject.FindGameObjectWithTag("GameManager");
            gameManager = manager.GetComponent<GameManager>();
        }
        
    }

    private void Update()
    {
        //Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        //Set Value
        anim.SetFloat("VelocityX", horizontal);
        anim.SetFloat("VelocityZ", vertical);
        /*move=transform.right*horizontal+transform.forward*vertical;*/

        Vector3 movement = new Vector3(horizontal, -1.0f, vertical);
        movement = transform.TransformDirection(movement);
        rb.velocity = movement * speed * Time.deltaTime;


        if (Input.GetKey(KeyCode.LeftShift)&&vertical>0.9){
            anim.SetBool("Dash",true);
            speed=700;
        }
        else
        {
            anim.SetBool("Dash",false);
            speed=500;
        }

        if (playerManager.weaponPedang == true)
        {
            tombak.SetActive(false);
            pedang.SetActive(true);
        }
        else
        {
            pedang.SetActive(false);
        }

        if (playerManager.weaponTombak == true)
        {
            pedang.SetActive(false);
            tombak.SetActive(true);
        }
        else
        {
            tombak.SetActive(false);
        }

        gameManager.levelAmount = level;
    }

 /*   void FixedUpdate()
    {
        rb.velocity=move*speed*Time.fixedDeltaTime;
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Pedang":
                playerManager.deckWeapon.SetActive(true);
                playerManager.tombak.SetActive(false);
                playerManager.pedang.SetActive(true);
                break;
            case "Tombak":
                playerManager.deckWeapon.SetActive(true);
                playerManager.pedang.SetActive(false);
                playerManager.tombak.SetActive(true);
                break;
            case "Door":
                SceneManager.LoadScene("Game");
                Destroy(this.gameObject);
                break;
            case "Portal":
                level += 1;
                SceneManager.LoadScene("Game");
       /*         Destroy(this.gameObject);*/
                break;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Pedang":
                playerManager.deckWeapon.SetActive(false);
                break;
            case "Tombak":
                playerManager.deckWeapon.SetActive(false);
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
