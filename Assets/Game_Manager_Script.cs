using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Game_Manager_Script : MonoBehaviour
{
    // Declare a static instance of the class
    // This is commonly used to implement a singleton pattern
    // **Ensure that only one instance of the class exists and can be accessed globally**
    public static Game_Manager_Script Instance;
    private void Awake()
    {

        // If instance is null, set the instance to the current instance of the class
        // If the instance is null, it means no other instance has been assigned yet
        if (Instance == null)
            Instance = this;
    }

    // Store the current score
    public float currentScore = 0f;
    // Keep track of whether the game is currently being played
    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();

    public HighScore_Script HighScore;

    void Start()
    {
        onPlay.Invoke();
        isPlaying = false;
        HighScore = new HighScore_Script();
    }

    void Update()
    {
        // Check if the game is currently being played
        if (isPlaying)
        {
            // Increment the current score by the time elapsed since the last frame
            currentScore += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0f;
    }
    public void GameOver()
    {
        if (HighScore.highestScore < currentScore)
        {
            HighScore.highestScore = currentScore;
        }
        onGameOver.Invoke();
        isPlaying = false;
    }

}