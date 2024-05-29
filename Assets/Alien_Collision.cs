using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien_Collision : MonoBehaviour
{
    private void Start()
    {
        Game_Manager_Script.Instance.onPlay.AddListener(ActivateAlien);
    }
    private void ActivateAlien()
    {
        // Activite
        gameObject.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
    // Check if the collided object has the tag "Obstacle"
    if (other.transform.tag == "Obstacle")
        {
            // Destroy the alien
            gameObject.SetActive(false);
            Game_Manager_Script.Instance.GameOver();
            Obstacle_Spawner_Script.Instance.Reset_Difficulty();

            // Deactivate the specific obstacle that was collided with
            other.gameObject.SetActive(false);

            // Deactivate all other obstacles
            DeactivateAllObstacles();
        }
    }
    private void DeactivateAllObstacles()
    {
        // Find all game objects with the tag "Obstacle"
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        
        // Loop through each obstacle and deactivate it
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
    }
}
