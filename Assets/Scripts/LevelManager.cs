using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager S;

    public GameObject RespawnPoint;

    public GameObject CenterText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI DeathText;
    public GameObject DashUI;

    [Header("Level Info")]
    public string levelName;

    [Header("Scene Info")]
    public string nextScene;
    public bool finalScene;

    private void Awake()
    {
        S = this;
        GameManager.S.RespawnPoint = S.RespawnPoint;
        GameManager.S.CenterText = S.CenterText;
        GameManager.S.ScoreText = S.ScoreText;
        GameManager.S.DeathText = S.DeathText;
        GameManager.S.DashUI = S.DashUI;
        GameManager.S.StartGame();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LevelWin()
    {
        if(!finalScene) SceneManager.LoadScene(nextScene);
    }
}
