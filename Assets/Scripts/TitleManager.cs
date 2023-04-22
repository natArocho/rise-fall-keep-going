using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private bool start = false;
    public bool playBGM;
    public static TitleManager S;

    // Start is called before the first frame update
    void Start()
    {
        if (playBGM && TitleManager.S == null) 
        {
            S = this;
            SoundManager.S.PlayBGM();
            DontDestroyOnLoad(this);
            
        }
    }

    public void startGame()
    {
        SoundManager.S.StopAllSounds();
        start = true;
        StartCoroutine(waitLoad("SampleScene"));
    }

    public void gotToTitle()
    {
        StartCoroutine(waitLoad("Title"));
    }

    public void gotToBonus()
    {
        StartCoroutine(waitLoad("Bonus"));
    }

    public void gotToCredit()
    {
        StartCoroutine(waitLoad("Credits"));
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public IEnumerator waitLoad(string scene)
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(scene);
        if (start) Destroy(this.gameObject);
    }
}
