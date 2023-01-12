using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public bool isTriggerByPlayer = false;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (isTriggerByPlayer) {  animator = GetComponent<Animator>(); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isTriggerByPlayer)
            {
                animator.SetTrigger("StartMoving");
            }
        }
    }
}
