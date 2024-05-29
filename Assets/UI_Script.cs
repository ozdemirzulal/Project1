using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Script : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    Game_Manager_Script myGM;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreUI;
    void Start()
    {
        myGM = Game_Manager_Script.Instance;
        myGM.onGameOver.AddListener(ActivateGameOverUI);
    }
    public void PlayButtonHandler()
    {
        myGM.StartGame();
    }
    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "SCORE: " + myGM.currentScore.ToString("F");
        gameOverHighScoreUI.text = "HIGH SCORE: " +myGM.HighScore.highestScore.ToString("F");
    }
    void Update()
    {
        scoreUI.text = "SCORE: " + myGM.currentScore.ToString("F");
    }

    public void ExitGame()
    {
        Debug.Log("Exit button clicked!");
        Application.Quit();
    }
}
