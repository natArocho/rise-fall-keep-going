using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Rigidbody2D rb;
    public float speed;
    private float origSpeed;
    public float airAdjust; //move slower in the air
    public float minSpeed;

    private float horizontalMove = 0.0f;
    private bool jump = false;
    private bool crouch = false;    
    private Vector3 rX;
    private Vector3 nP;

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        rX = new Vector3(0, 0, 0);
        origSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = transform.position;
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        float vertical = Input.GetAxisRaw("Vertical");

        speed = controller.isGrounded() ? origSpeed : speed * airAdjust;
        crouch = controller.isGrounded() && (vertical == -1);
        animator.SetBool("crouch", crouch);

        if (!(speed > minSpeed)) speed = minSpeed;

        if (!controller.isGrounded() && rb.velocity.y < 0)
        {
            controller.AirControl();
        }
        else rb.gravityScale = 5;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            controller.Dash();
        }

        // animation stuff
        float ourSpeed = Input.GetAxis("Horizontal");
        if (crouch) ourSpeed *= 0.36f;//account for crouch speed
        animator.SetFloat("speed", Mathf.Abs(ourSpeed));
        animator.SetBool("grounded", controller.isGrounded());
    }

    public void PlayerDeath()
    {
        SoundManager.S.PlayDeathSound();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Rigidbody2D>().isKinematic = true;
        animator.SetTrigger("death");
        Destroy(this.gameObject, 0.5f);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
