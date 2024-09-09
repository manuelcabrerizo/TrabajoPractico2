using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedProjectile
{
    public PoolObject Obj { get; set; }
    public float Time { get; set; }
    
    public void SetPosition(Vector2 pos)
    {
        Obj.SetPosition(pos);
    }

    public Vector2 GetPosition()
    {
        return Obj.GetPosition();
    }
}

public class PlayerShootPowerUpState : MonoBehaviour, IState
{
    [SerializeField] private float duration = 4;
    private float _time = 0;
    
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 20;
    [SerializeField] private KeyCode keyShoot = KeyCode.Space;
    
    private PlayerPowerUps _playerPowerUps;
    private PlayerMovement _playerMovement;
    
    private ObjectPool _projectilePool;
    private SpawnedProjectile _spawnedProjectile;
    private List<SpawnedProjectile> _spawnProjectiles;
    private List<SpawnedProjectile> _toRemove;
    
    private Rigidbody2D _targetRigidbody2DBody;
    private Rigidbody2D[] _cachedPRigidbody2Ds;

    private void Awake()
    {
        _playerPowerUps = GetComponent<PlayerPowerUps>();
        _playerMovement = GetComponent<PlayerMovement>();
        
        _projectilePool = GetComponent<ObjectPool>();
        _spawnProjectiles = new List<SpawnedProjectile>();
        _toRemove = new List<SpawnedProjectile>();
        
        _targetRigidbody2DBody = target.GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {       
        // store all the Rigidbody2D components
        // so i dont have to get them every time i spawn a new projectile
        CacheComponents();
    }

    private void Update()
    {
        RemoveDeadProjectiles();
    }

    public void Enter()
    {
        PoolObject poolObject = _projectilePool.Alloc();
        if (poolObject != null)
        {
            _spawnedProjectile = new SpawnedProjectile();
            _spawnedProjectile.Obj = poolObject;
            _spawnedProjectile.Time = duration;
            _spawnProjectiles.Add(_spawnedProjectile);
            _time = duration;
        }
        else
        {
            _playerPowerUps.PowerUpsStateMachine.PopState();
        }
    }

    public void Process(float dt)
    {
        // Check if the projectile still alive
        if (_spawnedProjectile.Obj.Index >= 0)
        {
            Rigidbody2D projectileBody = _cachedPRigidbody2Ds[_spawnedProjectile.Obj.Index];
            projectileBody.velocity = new Vector2();
            Vector2 projectilePos = transform.position;
            projectilePos.x += 0.65f * _playerMovement.ImpulseSign;
            _spawnedProjectile.SetPosition(projectilePos);
            if (Input.GetKeyDown(keyShoot))
            {
                Shoot();
                _playerPowerUps.PowerUpsStateMachine.PopState();
            }
        }

        if (_time <= 0)
        {
            _playerPowerUps.PowerUpsStateMachine.PopState();
        }
        _time -= dt;
    }

    public void Exit()
    {
    }
    
    private void Shoot()
    {
        // shoot the projectile to the ball
        SpawnedProjectile projectile = _spawnProjectiles[^1];
        Rigidbody2D projectileBody = _cachedPRigidbody2Ds[projectile.Obj.Index];
        
        float t = GetTimeOfCollision(projectile.GetPosition(), projectileSpeed, target.transform.position, _targetRigidbody2DBody.velocity);
        Vector2 shootPosition = target.transform.position;
        if (t >= 0.0f)
        {
            shootPosition = (Vector2)target.transform.position + _targetRigidbody2DBody.velocity * t;
        }
        Vector2 shootDirection = (shootPosition - (Vector2)projectile.GetPosition()).normalized;
        projectileBody.velocity = shootDirection * projectileSpeed;
    }
    
    private float GetTimeOfCollision(Vector2 aPos, float aSpeed, Vector2 bPos, Vector2 bVel)
    {
        Vector2 x0p = aPos;
        float sp = aSpeed;

        Vector2 x0w = bPos;
        Vector2 vw = bVel;
            
        float a = Vector2.Dot(vw, vw) - (sp * sp);
        float b = Vector2.Dot((x0w - x0p),vw)*2.0f;
        float c = Vector2.Dot((x0w - x0p),(x0w - x0p));

        float t = -1;

        if (a == 0.0f)
        {
            t = -c / b;
        }
        else
        {
            float discr = (b*b)-4.0f*a*c;
            if (discr < 0.0f)
            {
                return t;
            }
            else
            {
                if (discr == 0.0f)
                {
                    // one solution
                    t = -b / (2.0f * a);
                }
                else
                {
                    // two solutions
                    float x1 = (-b + (float)Math.Sqrt(discr)) / (2.0f * a);
                    float x2 = (-b - (float)Math.Sqrt(discr)) / (2.0f * a);
                    if (x1 < 0.0f)
                    {
                        t = x2;
                    }
                    else if (x2 < 0.0f)
                    {
                        t = x1;
                    }
                    else
                    {
                        t = Math.Min(x1, x2);
                    }
                }
            }
        }
        return t;
    }
    
    private void RemoveDeadProjectiles()
    {
        foreach (SpawnedProjectile projectile in _spawnProjectiles)
        {
            if (projectile.Time <= 0)
            {
                _toRemove.Add(projectile);
            }
            else
            {
                projectile.Time -= Time.deltaTime;
            }
        }

        foreach (SpawnedProjectile projectile in _toRemove)
        {
            _projectilePool.Free(projectile.Obj);
            _spawnProjectiles.Remove(projectile);
        }
        _toRemove.Clear();
    }
    
    void CacheComponents()
    {
        GameObject[] gameObjects = _projectilePool.GetGameObjects();
        _cachedPRigidbody2Ds = new Rigidbody2D[gameObjects.Length];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            _cachedPRigidbody2Ds[i] = gameObjects[i].GetComponent<Rigidbody2D>();
        }
    }
}
