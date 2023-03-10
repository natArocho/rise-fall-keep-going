using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public bool faceLeft = true;
    private float origSpeed;

    public bool normalEnemy;

    private bool dying = false;

    private CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        origSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalMove = speed * Time.fixedDeltaTime;


        if(faceLeft) { horizontalMove *= -1.0f; }

        controller.Move(horizontalMove, false, false);

        if (dying)
        {
            dying = false;
            GameManager.S.IncrScore(200); //increase score when we kill an enemy
            StartCoroutine(WaitToDie());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().PlayerDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            faceLeft = !faceLeft;
        } else if (collision.gameObject.tag == "Player")
        {
            dying = true;
        }
    }

    public IEnumerator WaitToDie()
    {
        if (normalEnemy) SoundManager.S.PlayEnemyDeath();
        else SoundManager.S.PlayExplodeSound();
        //GetComponent<Rigidbody2D>().isKinematic = true;
        if (normalEnemy) GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        if (normalEnemy) GetComponent<Animator>().SetTrigger("Dying");
        else GetComponent<Animator>().SetTrigger("Explode");
        speed = 0.0f;
        yield return new WaitForSeconds(1.0f);
        this.gameObject.SetActive(false);
    }


    public void SetOrigSpeed()
    {
        speed = origSpeed;
    }

    public void setDying(bool d)
    {
        dying = d;
    }
}
