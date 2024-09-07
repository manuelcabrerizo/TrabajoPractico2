using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerShoot : MonoBehaviour
{
    public bool Active { get; set; }

    [SerializeField] private float projectileSpeed = 20;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private KeyCode keyShoot = KeyCode.Space;

    private bool _projectileShooted;
    private PlayerMovement _movement;
    
    [SerializeField] private GameObject target;
    private GameObject _projectile;
    private Rigidbody2D _projectileBody;
    private Rigidbody2D _targetBody;

    private CircleCollider2D _projectileCollider;
    private PlayerPowerUps _powerUps;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _projectileShooted = false;
        _projectile = Instantiate(projectilePrefab);
        _projectile.SetActive(false);
        _projectileBody = _projectile.GetComponent<Rigidbody2D>();
        _projectileCollider = _projectile.GetComponent<CircleCollider2D>();
        _targetBody = target.GetComponent<Rigidbody2D>();
        _powerUps = GetComponent<PlayerPowerUps>();
    }
    
    private void OnDestroy()
    {
        Destroy(_projectile);
    }

    private void Update()
    {
        if (!Active)
        {
            _projectileShooted = false;
            _projectile.SetActive(false);
            return;
        }
        
        if (!_projectileShooted)
        {
            UpdateProjectileBeforeShoot();
        }
        
        if (Input.GetKeyDown(keyShoot) && !_projectileShooted)
        {
            Shoot();
        }
    }

    private void UpdateProjectileBeforeShoot()
    {
        _projectile.SetActive(true);
        _projectileBody.velocity = new Vector2();
        Vector2 projectilePos = transform.position;
        projectilePos.x += 0.65f * _movement.ImpulseSign;
        _projectile.transform.position = projectilePos;
        _projectileCollider.enabled = false;   
    }

    private void Shoot()
    {
        // shoot the projectile to the ball
        _projectileShooted = true;
        float t = GetTimeOfCollision(_projectile.transform.position, projectileSpeed, target.transform.position, _targetBody.velocity);
        Vector2 shootPosition = target.transform.position;
        if (t >= 0.0f)
        {
            shootPosition = (Vector2)target.transform.position + _targetBody.velocity * t;
        }
        Vector2 shootDirection = (shootPosition - (Vector2)_projectile.transform.position).normalized;
        _projectileBody.velocity = shootDirection * projectileSpeed;
        _projectileCollider.enabled = true;
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
}
