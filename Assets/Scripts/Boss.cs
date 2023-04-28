using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss S;

    private DialogueTrigger dt;

    private AudioSource bossBGM;

    public PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        dt = GetComponent<DialogueTrigger>();
        bossBGM = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerDia()
    {
        SoundManager.S.StopAllSounds();
        bossBGM.Play();
        GameManager.S.gameState = GameState.boss;
        dt.TriggerDialogue();
        pm.stopPlayer();
    }
}
