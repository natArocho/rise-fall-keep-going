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

    }

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        GameManager.S.RespawnPoint = RespawnPoint;
        GameManager.S.CenterText = CenterText;
        GameManager.S.ScoreText = ScoreText;
        GameManager.S.DeathText = DeathText;
        GameManager.S.DashUI = DashUI;
        GameManager.S.StartGame();
    }

    public void LevelWin()
    {
        if(!finalScene) SceneManager.LoadScene(nextScene);
    }
}
