using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFin : MonoBehaviour
{
    private CircleCollider2D hitbox;

    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hitbox.enabled = false;
            GameManager.S.EnableCenterText("Level Complete!");
            GameManager.S.LevelFinished();
        }
    }
}
