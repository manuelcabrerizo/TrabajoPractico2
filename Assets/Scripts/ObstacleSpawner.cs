using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnedObstacle
{
    public GameObject gameObject;
    public float time;
}

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    private float time = 0;
    private float timeToSpawn;
    private List<SpawnedObstacle> spawnObstacles;
    private List<SpawnedObstacle> toRemove;


    private void Start()
    {
        spawnObstacles = new List<SpawnedObstacle>();
        toRemove = new List<SpawnedObstacle>(); 
        timeToSpawn = Random.Range(4.0f, 8.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= timeToSpawn)
        {
            SpawnObstacle();
            time = 0;
            timeToSpawn = Random.Range(4.0f, 8.0f);
        }
        time += Time.deltaTime;
        
        ProcessSpawnedOnstacles();
    }

    private void ProcessSpawnedOnstacles()
    {
        foreach (SpawnedObstacle obstacle in spawnObstacles)
        {
            if (obstacle.time <= 0)
            {
                Destroy(obstacle.gameObject);
                toRemove.Add(obstacle);
            }
            else
            {
                obstacle.time -= Time.deltaTime;
            }
        }

        foreach (SpawnedObstacle obstacle in toRemove)
        {
            spawnObstacles.Remove(obstacle);
        }
        toRemove.Clear();
    }

    private void SpawnObstacle()
    {
        SpawnedObstacle obstacle = new SpawnedObstacle();
        Vector2 position = RandomDirection() * Random.Range(0.0f, 5.0f);
        obstacle.gameObject = Instantiate(obstaclePrefab, new Vector3(position.x, position.y, 1), Quaternion.identity);
        obstacle.time = Random.Range(3.0f, 8.0f);
        spawnObstacles.Add(obstacle);
    }
    
    private Vector2 RandomDirection()
    { 
        float angle = Random.Range(0, (float)Math.PI * 2.0f);
        return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
    }


}
