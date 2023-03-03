using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    public GameObject RespawnPoint;


    private void Awake()
    {
        if (GameManager.S)
        {
            //singleton exists, delete
            Destroy(this.gameObject);
        }
        else
        {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRespawn(GameObject newRespawn)
    {
        GameObject prevRespawn = GameManager.S.RespawnPoint;
        GameManager.S.RespawnPoint = newRespawn;
        Animator prevAnim = prevRespawn.GetComponent<Animator>();
        prevAnim.SetBool("RespawnEn", false);
    }
}
