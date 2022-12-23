using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager S; //singleton def
    private AudioSource audio;

    public AudioSource bgm;

    public AudioClip playerDeath;
    public AudioClip enemyDeath;
    public AudioClip dashSound;
    public AudioClip coinSound;

    private void Awake()
    {
        S = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeathSound()
    {
        audio.PlayOneShot(playerDeath);
    }

    public void PlayEnemyDeath()
    {
        audio.PlayOneShot(enemyDeath);
    }

    public void PlayDashSound()
    {
        audio.PlayOneShot(dashSound);
    }

    public void PlayCoinSound()
    {
       audio.PlayOneShot(coinSound);
    }


    public void PlayBGM()
    {
        bgm.Play();
    }

    public void StopAllSounds()
    {
        //stop ambient noise
        bgm.Pause();

        //stop all child sounds
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
