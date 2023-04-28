using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using UnityEngine;

public enum GameState {playing, oops, lvlDone};

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    public GameObject RespawnPoint;

    public GameObject CenterText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI DeathText;
    public GameObject DashUI;

    public GameState gameState;

    private int score = 0;
    private int deathCount = 0;

    private bool haveBlue, haveOrange, haveBlack,
                 haveAll; //acquired all special coins

    public bool noEnemies;


    private void Awake()
    {
        if (GameManager.S)
        {
            //singleton exists, delete
            Destroy(this.gameObject);
        }
        else
        {
            gameState = GameState.playing;
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        haveBlue = false;
        haveOrange = false;
        haveBlack = false;
        haveAll = false;
        CenterText.SetActive(false);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + score;
        DeathText.text = "Deaths: " + deathCount;
        if (haveBlue && haveOrange && haveBlack) haveAll = true; //use this later
    }

    public void StartGame()
    {
        gameState = GameState.playing;
    }

    public void LevelFinished()
    {
        gameState = GameState.lvlDone;
        StartCoroutine(LoadNextLevel());
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2.0f);
        SoundManager.S.StopAllSounds();
        LevelManager.S.LevelWin();
    }

    public int GetScore()
    {
        return score;
    }

    public void IncrScore(int incr)
    {
        score += incr;
    }

    public void IncrDeathCnt()
    {
        deathCount += 1;
    }

    public void UpdateRespawn(GameObject newRespawn)
    {
        GameObject prevRespawn = GameManager.S.RespawnPoint;
        GameManager.S.RespawnPoint = newRespawn;
        Animator prevAnim = prevRespawn.GetComponent<Animator>();
        prevAnim.SetBool("RespawnEn", false);
    }

    //public method to enable middle text for cutscenes/end of level
    public void EnableCenterText(string text)
    {
        CenterText.SetActive(true);
        TextMeshProUGUI txt = CenterText.GetComponent<TextMeshProUGUI>();
        Debug.Log(text);
        txt.text = text;
    }

    public void DisableCenterText()
    {
        CenterText.SetActive(false);
    }

    public void GotBlue()
    {
        haveBlue = true;
    }

    public void GotOrange()
    {
        haveOrange = true;
    }

    public void GotBlack()
    {
        haveBlack = true;
    }

    //For updating the Dash UI game object when we have/don't have a dash
    public void ToggleDashUI(bool haveDash)
    {
        DashUI.SetActive(haveDash);
    }
}
