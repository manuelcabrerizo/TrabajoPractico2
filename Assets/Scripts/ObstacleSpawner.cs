using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnedObstacle
{
    public PoolObject Obj { get; set; }
    public float Time { get; set; }

    public int PoolIndex { get; set; }

    public void SetPosition(Vector2 pos)
    {
        Obj.SetPosition(pos);
    }
}

public class ObstacleSpawner : MonoBehaviour
{
    private ObjectPool[] _obstaclePools;
    
    private float _time = 0;
    private float _timeToSpawn;
    private List<SpawnedObstacle> _spawnObstacles;
    private List<SpawnedObstacle> _toRemove;
    
    private void Awake()
    {
        _obstaclePools = GetComponents<ObjectPool>();
        _spawnObstacles = new List<SpawnedObstacle>();
        _toRemove = new List<SpawnedObstacle>(); 
        _timeToSpawn = Random.Range(2.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_time >= _timeToSpawn)
        {
            SpawnObstacle();
            _time = 0;
            _timeToSpawn = Random.Range(2.0f, 4.0f);
        }
        RemoveDeadOnstacles();
        _time += Time.deltaTime;
    }
    
    private void SpawnObstacle()
    {
        SpawnedObstacle obstacle = new SpawnedObstacle();
        float x = Random.Range(-4.0f, 4.0f);
        float y = Random.Range(-3.0f, 3.0f);
        obstacle.PoolIndex = Random.Range(0, _obstaclePools.Length);
        obstacle.Obj = _obstaclePools[obstacle.PoolIndex].Alloc();
        obstacle.SetPosition(new Vector2(x, y));
        obstacle.Time = Random.Range(3.0f, 8.0f);
        _spawnObstacles.Add(obstacle);
    }

    private void RemoveDeadOnstacles()
    {
        foreach (SpawnedObstacle obstacle in _spawnObstacles)
        {
            if (obstacle.Time <= 0)
            {
                _toRemove.Add(obstacle);
            }
            else
            {
                obstacle.Time -= Time.deltaTime;
            }
        }

        foreach (SpawnedObstacle obstacle in _toRemove)
        {
            _obstaclePools[obstacle.PoolIndex].Free(obstacle.Obj);
            _spawnObstacles.Remove(obstacle);
        }
        _toRemove.Clear();
    }
}
