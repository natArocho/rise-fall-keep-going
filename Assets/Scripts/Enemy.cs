using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public bool faceLeft = true;

    private CharacterController2D controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalMove = speed * Time.fixedDeltaTime;

        if(faceLeft) { horizontalMove *= -1.0f; }

        controller.Move(horizontalMove, false, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TurnAround")
        {
            faceLeft = !faceLeft;
        } else if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
