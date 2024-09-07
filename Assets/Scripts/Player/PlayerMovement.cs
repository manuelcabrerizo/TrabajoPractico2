using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // save the star x position for reset the player position
    // after the hit animation
    public float StartXPos { get; private set; }
    
    // use for the velocity calculations
    private float _direction;
    private Vector2 _velocity;
    [SerializeField] private float speed = 50;
    [SerializeField] private float damping = 0.001f;

    // use for the hit animation for hitting the ball
    private bool _hitAnimationActive;
    private int _hitAnimationDirection;
    [SerializeField] private float hitAnimationTime = 0.5f;
    [SerializeField] private float impulseMagnitude = 4;
    [SerializeField] private float impulseSign = 1;
    public float ImpulseSign => impulseSign;

    private Rigidbody2D _rigidbody2D;
    private Camera _camera;
    private float _heightLimit; // use to limit the player position, so it doesnt go out of the screen
    [SerializeField] private KeyCode keyUp = KeyCode.UpArrow;
    [SerializeField] private KeyCode keyDown = KeyCode.DownArrow;
    [SerializeField] private KeyCode keyAction = KeyCode.LeftArrow;

    private void Awake()
    {
        _velocity = new Vector2();
        
        _hitAnimationActive = false;
        _hitAnimationDirection = 0;
        
        //  get the dimensions of the camera for bound checking
        _camera = FindObjectOfType<Camera>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        StartXPos = transform.position.x;
    }
    
    private void Update()
    {
        // update the new height limit in case the player change its size
        _heightLimit = _camera.orthographicSize - (transform.localScale.y / 2.0f);
        
        // set the direction where the player wants to move
        // and play the hit animation 
        ProcessInput();
        
        // Process the Hit animation
        // this animation changes the velocity of the player
        // because when it hits the ball the physics system has to
        // change the ball velocity taking into account the player
        // velocity
        ProcessHitAnimation();
        
        // Clamp the player position so it cant go out of the screen
        ClampPosition();
    }

    private void FixedUpdate()
    {
        IntegrateVelocity(Time.fixedDeltaTime);
        
        // Set velocity to zero if the player hit a wall
        ClampVelocity();

        // set our custom velocity to be the rigid body velocity
        _rigidbody2D.velocity = _velocity;
        
        // Apply damping or drag, so the player dont move forever
        _velocity *= (float)Math.Pow(damping, Time.fixedDeltaTime);
    }

    private void ProcessInput()
    {
        _direction = 0.0f;
        if (Input.GetKey(keyUp))
        {
            _direction += 1;
        }
        if (Input.GetKey(keyDown))
        {
            _direction -= 1;
        }
        if (Input.GetKeyDown(keyAction) && !_hitAnimationActive)
        {
            StartCoroutine(HitAnimation());
        }
    }
    
    IEnumerator HitAnimation()
    {
        _hitAnimationActive = true;
        float halfTime = hitAnimationTime * 0.5f;
        _hitAnimationDirection = 0;
        yield return new WaitForSeconds(halfTime);
        _hitAnimationDirection = 1;
        yield return new WaitForSeconds(halfTime);
        _hitAnimationActive = false;
    }

    private void ProcessHitAnimation()
    {
        if (_hitAnimationActive)
        {
            float impulse = impulseSign * impulseMagnitude;
            _velocity.x = Lerp(impulse, -impulse, _hitAnimationDirection);
        }
        else
        {
            _velocity.x = 0;
            transform.position = new Vector2(StartXPos, transform.position.y);
        }
    }

    private void ClampPosition()
    {
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

    private void ClampVelocity()
    {
        Vector2 position = _rigidbody2D.position;
        if (position.y > _heightLimit || position.y < -_heightLimit)
        {
            _velocity = new Vector2();
        }
    }

    void IntegrateVelocity(float dt)
    {
        _velocity.y += (speed * _direction) * dt;
    }
    
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
    private float Lerp(float a, float b, float t)
    {
        return t * b + (1 - t) * a;
    }
}