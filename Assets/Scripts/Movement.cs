using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float StartXPos { get; set; }
    
    private Rigidbody2D _body;
    private Camera _camera;
    private float _direction;
    private float _heightLimit;
    private float _currentHitAnimationTime;

    Vector2 Velocity;

    [SerializeField] private float speed = 50;
    [SerializeField] private float damping = 0.001f;

    [SerializeField] private float hitAnimationTime = 0.5f;
    [SerializeField] private float impulseMagnitude = 4;
    [SerializeField] private float impulseSign = 1;

    [SerializeField] private KeyCode keyUp = KeyCode.UpArrow;
    [SerializeField] private KeyCode keyDown = KeyCode.DownArrow;
    [SerializeField] private KeyCode keyAction = KeyCode.LeftArrow;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        
        //  get the dimensions of the camera for bound checking
        _camera = FindObjectOfType<Camera>();
        StartXPos = transform.position.x;
    }
    
    private void Update()
    {
        _heightLimit = _camera.orthographicSize - (transform.localScale.y / 2.0f);
        
        _direction = 0.0f;
        if (Input.GetKey(keyUp))
        {
            _direction += 1;
        }
        if (Input.GetKey(keyDown))
        {
            _direction -= 1;
        }

        if (Input.GetKeyDown(keyAction) && _currentHitAnimationTime == 0.0f)
        {
            _currentHitAnimationTime = hitAnimationTime;
        }
        
        if (_currentHitAnimationTime > 0)
        {
            float halfTime = hitAnimationTime * 0.5f;
            float impulse = impulseSign * impulseMagnitude;
            Velocity.x = Lerp(impulse, -impulse, Step(_currentHitAnimationTime, halfTime));
            _currentHitAnimationTime -= Time.deltaTime;
        }
        else
        {
            Velocity.x = 0;
            transform.position = new Vector2(StartXPos, transform.position.y);
            _currentHitAnimationTime = 0.0f;
        }

        
        Vector2 position = transform.position;
        if (position.y > _heightLimit)
        {
            position.y = _heightLimit;
        }
        else if (position.y < -_heightLimit)
        {
            position.y = -_heightLimit;
        }

        transform.position = position;
    }

    private void FixedUpdate()
    {
        Velocity.y += (speed * _direction) * Time.fixedDeltaTime;

        Vector2 position = _body.position;
        if (position.y > _heightLimit || position.y < -_heightLimit)
        {
            Velocity = new Vector2();
        }

        _body.velocity = Velocity;
        
        Velocity *= (float)Math.Pow(damping, Time.fixedDeltaTime);
    }
    
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
    private float Step(float a, float x)
    {
        return System.Convert.ToSingle((x >= a));
    }

    private float Lerp(float a, float b, float t)
    {
        return t * b + (1 - t) * a;
    }
}