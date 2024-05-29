using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Spawner_Script : MonoBehaviour
{

    public static Obstacle_Spawner_Script Instance;
   

    // Array of obstacle prefabs to spawn
    [SerializeField] private GameObject[] obstaclePrefabs;
    // Time interval between obstacle spawns
    [SerializeField] private float obstacleSpawnTime = 3f;
    // Timer that keeps track of spawning interval
    [SerializeField] private float timeBetweenSpawn = 0f;

    // Obstacle's moving speed
    [SerializeField] private float currentSpeed = 7f;

    // Difficulty adjustment intervals
    [SerializeField] private float difficultyIncreaseInterval = 10f;
    private float timeSinceLastIncrease = 0f;

    // Difficulty adjustment values
    [SerializeField] private float speedIncreaseAmount = 2f;
    [SerializeField] private float spawnTimeDecreaseAmount = 0.1f;

    // Minimum spawn time to avoid excessive spawning
    [SerializeField] private float minimumSpawnTime = 0.1f;

    // List to keep track of spawned obstacles
    private List<GameObject> spawnedObstacles = new List<GameObject>();
    // X position for destroying obstacles
    [SerializeField] private float destroyingPoint = -27f;

    void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        if (Game_Manager_Script.Instance.isPlaying)
        { 
            // Check if it's time to spawn new obstacles
            SpawnLoop();
            // Check and destroy obstacles that have passed the destroying point
            DestroyPassedObstacles();
            IncreaseDifficulty();
        }

    }

    private void SpawnLoop()
    {
        // Increment the time between spawns
        timeBetweenSpawn += Time.deltaTime;

        // Check if it's time to spawn a new obstacle
        if (timeBetweenSpawn >= obstacleSpawnTime)
        {
            Spawn();
            // Call the Spawn method to spawn an obstacle
            timeBetweenSpawn = 0f;
            // Reset the spawn timer to zero 
        }
    }

    private void Spawn()
    {
        // Randomly select an obstacle prefab from the array
        GameObject spawningObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        // Instantiate the selected obstacle at the position of the spawner with no rotation
        GameObject spawnedObstacle = Instantiate(spawningObstacle, transform.position, Quaternion.identity);
        // Get the Rigidbody2D component of the spawned obstacle to control its movement
        Rigidbody2D obstacleRigidibody = spawnedObstacle.GetComponent<Rigidbody2D>();
        // Set the velocity of the spawned obstacle to move left at the current speed
        obstacleRigidibody.velocity = Vector2.left * currentSpeed;
        // Add the spawned obstacle to the list 
        spawnedObstacles.Add(spawnedObstacle);
    }

    private void DestroyPassedObstacles()
    {
        // reate a temporary list to store obstacles to be removed
        List<GameObject> obstaclesToRemove = new List<GameObject>();

        foreach (GameObject obstacle in spawnedObstacles)
        {
            // Check if the obstacle has passed the destroying point
            if (obstacle.transform.position.x < destroyingPoint)
            {
                obstaclesToRemove.Add(obstacle);
                // Add the spawned obstacles to the removing list
            }
        }

        // Iterate through the obstacles to be removed
        foreach (GameObject obstacle in obstaclesToRemove)
        {
            spawnedObstacles.Remove(obstacle); 
            //Remove the spawned obstacle that passed through the destroying point
            Destroy(obstacle);
            //Destroy the spawned obstacle that passed through the destroying point
        }
    }
    private void IncreaseDifficulty()
    {
        // Increment the time since the last difficulty increase
        timeSinceLastIncrease += Time.deltaTime;

        // Check if it's time to increase the difficulty
        if (timeSinceLastIncrease >= difficultyIncreaseInterval)
        {
            // Increase the speed of the obstacles
            currentSpeed += speedIncreaseAmount;

            // Decrease the spawn time
            // Make sure it doesn't go below the minimum spawn time
            obstacleSpawnTime = Mathf.Max(obstacleSpawnTime - spawnTimeDecreaseAmount, minimumSpawnTime);

            // Reset the time since last difficulty increase
            timeSinceLastIncrease = 0f;
        }
    }
    public void Reset_Difficulty()
    {
        currentSpeed = 7f;
        obstacleSpawnTime = 3f;
        timeBetweenSpawn = 0f;
    }
}
