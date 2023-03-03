using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public static Enemies S;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject enemy = transform.GetChild(i).gameObject;
            enemy.GetComponent<Enemy>().SetOrigSpeed();
            if (enemy.GetComponent<Enemy>().normalEnemy) enemy.GetComponent<BoxCollider2D>().enabled = true;
            enemy.GetComponent<CircleCollider2D>().enabled = true;
            //enemy.GetComponent<Rigidbody2D>().isKinematic = false;
            enemy.SetActive(true);
            //enemy.GetComponent<Animator>().SetTrigger("Respawn");

        }
    }
}
