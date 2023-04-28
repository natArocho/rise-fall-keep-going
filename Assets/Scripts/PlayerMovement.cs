using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    private Rigidbody2D rb;
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
    private bool dead = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rX = new Vector3(0, 0, 0);
        origSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.S.gameState == GameState.playing)
        {
            PlayerUpdate();
        }
        else if (GameManager.S.gameState == GameState.boss)
        {
            if (Input.GetButtonDown("Jump"))
            {
                DialogueManager.S.DisplayNextSentence();
            }
        }
        else if (GameManager.S.gameState == GameState.gameWon)
        {
            if (Input.GetButtonDown("Jump"))
            {
                LevelManager.S.LevelWin();
            }
        }
    }

    private void PlayerUpdate()
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

        if (Input.GetButtonDown("Fire1"))
        {
            if (!dead) { controller.Dash(); }
        }

        else if (Input.GetButtonDown("Fire2"))
        {
            if (!dead) { controller.Attack(); }
        }

        // animation stuff
        float ourSpeed = Input.GetAxis("Horizontal");
        if (crouch) ourSpeed *= 0.36f;//account for crouch speed
        animator.SetFloat("speed", Mathf.Abs(ourSpeed));
        animator.SetBool("grounded", controller.isGrounded());
    }

    public void PlayerDeath()
    {
        if (!dead)
        {
            dead = true;
            SoundManager.S.PlayDeathSound();
            GameManager.S.gameState = GameState.oops;
            GameManager.S.EnableCenterText("Keep Going.");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Rigidbody2D>().isKinematic = true;
            animator.SetTrigger("death");
            GameManager.S.IncrDeathCnt();
            StartCoroutine(Respawn());
        }
    }

    public IEnumerator Respawn()
    {
        bool pMV = false;
        bool pMH = false;
        GameObject resPoint = GameManager.S.RespawnPoint;
        TriggerCamera moveCam = resPoint.GetComponent<TriggerCamera>();
        if (moveCam.moveV || moveCam.moveH)
        {
            pMV = moveCam.moveV;
            pMH = moveCam.moveH;
            CameraFollow.S.MoveCamera(moveCam.newX, moveCam.newY, moveCam.newS);
        }
        yield return new WaitForSeconds(2f);
        if (pMV || pMH)
        {
            CameraFollow.S.moving = false;
            CameraFollow.S.moveV = pMV;
            CameraFollow.S.moveH = pMH;
        }
        GameManager.S.gameState = GameState.playing;
        GameManager.S.DisableCenterText();
        //this.gameObject.SetActive(true);
        animator.SetTrigger("respawn");
        transform.position = resPoint.transform.position;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.freezeRotation = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        dead = false;
        if(!GameManager.S.noEnemies) Enemies.S.RespawnEnemies();
    }

    public void stopPlayer()
    {
        rb.velocity = Vector3.zero; 
    }
    private void FixedUpdate()
    {
        if (GameManager.S.gameState == GameState.playing)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
