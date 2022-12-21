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
    private Vector3 rX;
    private Vector3 nP;


    void Start()
    {
        rX = new Vector3(0, 0, 0);
        origSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = transform.position;
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        speed = controller.isGrounded() ? origSpeed : speed * airAdjust;

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
    }

    public void PlayerDeath()
    {
        SoundManager.S.PlayDeathSound();
        Destroy(this.gameObject);
    }

        private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
