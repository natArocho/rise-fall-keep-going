using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCamera : MonoBehaviour
{
    public float newX;
    public float newY;
    public float newS;

    public bool moveH;
    public bool moveV;
    public bool triggerBGM;
    public bool stopBGM;
    public bool enAnim; //enables checkpoint animation

    public bool triggerBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (triggerBoss)
            {
                Boss.S.triggerDia();
            } 
            if (!(moveH || moveV)) CameraFollow.S.MoveCamera(newX, newY, newS);
            if (triggerBGM) SoundManager.S.PlayBGM();
            if (stopBGM) SoundManager.S.StopAllSounds();
            
            CameraFollow.S.moveH = moveH;
            CameraFollow.S.moveV = moveV;

            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<Animator>().SetBool("RespawnEn", enAnim);

            GameManager.S.UpdateRespawn(this.gameObject);
        }
    }
}
