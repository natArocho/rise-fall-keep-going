using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.normalEnemy)
            {
                kieno.setDash(true);
                if (!kieno.isGrounded())
                {
                    rb.velocity = Vector3.zero;
                }
            } 
            else
            {
                rb.velocity = Vector3.zero;
                if (transform.parent.position.x > collision.gameObject.transform.position.x)
                {
                    rb.AddForce(new Vector2(2400, 400));
                } 
                else
                {
                    rb.AddForce(new Vector2(-2400, 400));
                }
            }

            enemy.setDying(true);
        }
    }

    private CharacterController2D kieno;
    private Rigidbody2D rb;

    private void Start()
    {
        kieno = transform.parent.GetComponent<CharacterController2D>();
        rb = transform.parent.GetComponent<Rigidbody2D>();

    }

    private void Update() {}


}
