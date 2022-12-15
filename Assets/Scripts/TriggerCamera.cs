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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(!(moveH || moveV)) CameraFollow.S.MoveCamera(newX, newY, newS);
            if (triggerBGM) SoundManager.S.PlayBGM();
            CameraFollow.S.moveH = moveH;
            CameraFollow.S.moveV = moveV;
            Destroy(this.gameObject);
        }
    }
}
