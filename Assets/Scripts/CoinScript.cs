using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public bool specialCoin; 
    public string coinName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.S.PlayCoinSound();
            if (specialCoin)
            {
                GameManager.S.IncrScore(1000);
                if (coinName == "Blue")
                {
                    GameManager.S.GotBlue();
                } 
                else if(coinName == "Orange")
                {
                    GameManager.S.GotOrange();
                }
                else if (coinName == "Black")
                {
                    GameManager.S.GotBlack();
                }
            }
            else
            {
                GameManager.S.IncrScore(100);
            }
            Destroy(this.gameObject);  
        }
    }
}
