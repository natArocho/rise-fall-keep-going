using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//this tutorial used as reference
//https://www.youtube.com/watch?v=_nRzoTzeyxU

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager S;
    private Queue<string> sentences;

    public TextMeshProUGUI dialogueText;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (DialogueManager.S)
        {
            Destroy(this);
        }
        else
        {
            S = this;
        }

        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        animator.SetBool("IsOpen", true);

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        animator.SetBool("IsOpen", true);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        GameManager.S.gameState = GameState.gameWon;
        GameManager.S.EnableCenterText("Congrats! Press Space to Return to Title");
    }
}
