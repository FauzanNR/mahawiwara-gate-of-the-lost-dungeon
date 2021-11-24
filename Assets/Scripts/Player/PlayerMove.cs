using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [Header("Variable")]
    private float moveSpeed = 3;
    private float transformX;
    private float transformZ;
    private float jumpForce = 3f;
    private float gravity = -20f;

    //Ground Check
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    private Vector3 move;
    Vector3 velocity;

    [Header("Reference")]
    private Animator anim;
    private CharacterController controller;
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Movement();
        //ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //Gravity setting
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Dash();
        }
        else
        {
            Walk();
        }
        //Input
        transformX = Input.GetAxis("Horizontal");
        transformZ = Input.GetAxis("Vertical");

        //Set Value animasi
        anim.SetFloat("VelocityX", transformX);
        anim.SetFloat("VelocityZ", transformZ);
    }
    private void FixedUpdate()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "StartGame")
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    public void Movement()
    {
        move = transform.right * transformX + transform.forward * transformZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    public void Dash()
    {
        anim.SetBool("Dash", true);
        moveSpeed = 5;
    }

    public void Walk()
    {
        anim.SetBool("Dash", false);
        moveSpeed = 3;
    }

    public void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
    }
}
