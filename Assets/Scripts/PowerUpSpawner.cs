using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnedPowerUp
{
    public PoolObject Obj { get; set; }
    public float Time { get; set; }
    
    public void SetPosition(Vector2 pos)
    {
        Obj.SetPosition(pos);
    }
}

public class PowerUpSpawner : MonoBehaviour
{
    private ObjectPool _powerUpPool;
    
    private float _time;
    private int _spawCounter;
    private List<SpawnedPowerUp> _spawnPowerUps;
    private List<SpawnedPowerUp> _toRemove;
    
    private Color[] _colors =
    {
        Color.blue, 
        Color.green, 
        Color.magenta,
        Color.red
    };

    private PowerUp[] _cachedPowerUpComponents;
    private SpriteRenderer[] _cachedSpriteRendererComponent;

    [SerializeField] private float timeToSpawn = 10;
    [SerializeField] private float lifeTime = 5;
    [SerializeField] private Movement player1;
    [SerializeField] private Movement player2;
    
    private void Awake()
    {
        _powerUpPool = GetComponent<ObjectPool>();
        _spawnPowerUps = new List<SpawnedPowerUp>();
        _toRemove = new List<SpawnedPowerUp>();
        _spawCounter = 0;
    }

    private void Start()
    {        
        CacheComponents();
    }

    private void Update()
    {
        if (_time >= timeToSpawn)
        {
            SpawnPowerUp();
            _time = 0;
        }
        RemoveDeadPowerUps();
        _time += Time.deltaTime;
    }
    
    private void SpawnPowerUp()
    {
        PoolObject poolObject = _powerUpPool.Alloc();
        if (poolObject != null)
        {
            SpawnedPowerUp powerUp = new SpawnedPowerUp();
            float x = (_spawCounter++ % 2) == 0 ? player1.StartXPos + 1.5f : player2.StartXPos - 1.5f;
            float y = Random.Range(-4.0f, 4.0f);
            
            powerUp.Obj = poolObject;
            powerUp.SetPosition(new Vector2(x, y));
            powerUp.Time = lifeTime;
            
            // Set a random Type
            PowerUp powerUpComponent = _cachedPowerUpComponents[poolObject.Index];
            powerUpComponent.Type = (PowerUpType)Random.Range(0, (int)PowerUpType.Count);
            // Set the type color
            SpriteRenderer sprite = _cachedSpriteRendererComponent[poolObject.Index];
            sprite.color = _colors[(int)powerUpComponent.Type];

            _spawnPowerUps.Add(powerUp);
        }
    }
    
    private void RemoveDeadPowerUps()
    {
        foreach (SpawnedPowerUp powerUp in _spawnPowerUps)
        {
            if (powerUp.Time <= 0)
            {
                _toRemove.Add(powerUp);
            }
            else
            {
                powerUp.Time -= Time.deltaTime;
            }
        }

        foreach (SpawnedPowerUp powerUp in _toRemove)
        {
            _powerUpPool.Free(powerUp.Obj);
            _spawnPowerUps.Remove(powerUp);
        }
        _toRemove.Clear();
    }

    void CacheComponents()
    {
        GameObject[] gameObjects = _powerUpPool.GetGameObjects();
        _cachedPowerUpComponents = new PowerUp[gameObjects.Length];
        _cachedSpriteRendererComponent = new SpriteRenderer[gameObjects.Length];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            _cachedPowerUpComponents[i] = gameObjects[i].GetComponent<PowerUp>();
            _cachedSpriteRendererComponent[i] = gameObjects[i].GetComponent<SpriteRenderer>();
        }
    }
}
