using System;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : Obstacle
{
    private UIPauseManager _pause;
    private float _speed = 10;
    private float _cameraHalfHeight;

    private float startXPos = 0;
    private float currentHitAnimationTime = 0;
    [SerializeField] private float hitAnimationTime = 0.25f;
    [SerializeField] private float impulseMagnitude = 4;
    [SerializeField] private float impulseSign = 1;
    
    [SerializeField] private KeyCode keyUp = KeyCode.UpArrow;
    [SerializeField] private KeyCode keyDown = KeyCode.DownArrow;
    [SerializeField] private KeyCode keyAction = KeyCode.Space;
    
    private void Start()
    {
        _pause = FindObjectOfType<UIPauseManager>();
        
        //  get the dimensions of the camera for bound checking
        Camera currentCamera = FindObjectOfType<Camera>();
        _cameraHalfHeight = currentCamera.orthographicSize;

        startXPos = transform.position.x;
    }

    private void Update() 
    {
        if ((bool)_pause && _pause.IsActive())
        {
            return;
        }

        float direction = 0.0f;
        if(Input.GetKey(keyUp)) 
        {
            direction += 1;
        }
        if(Input.GetKey(keyDown)) 
        {
            direction -= 1;
        }

        if (Input.GetKeyDown(keyAction) && currentHitAnimationTime == 0.0f)
        {
            currentHitAnimationTime = hitAnimationTime;
        }

        if (currentHitAnimationTime > 0)
        {
            float halfTime = hitAnimationTime * 0.5f;
            if (currentHitAnimationTime >= halfTime)
            {
                Velocity.x = impulseMagnitude * impulseSign;
            }
            else
            {
                Velocity.x = impulseMagnitude * -impulseSign;
            }
            currentHitAnimationTime -= Time.deltaTime;
        }
        else
        {
            Velocity.x = 0;
            Vector2 pos = new Vector2(startXPos, transform.position.y);
            transform.position = pos;
            currentHitAnimationTime = 0.0f;
        }


        Velocity.y = _speed * direction; 
        float heightLimit = _cameraHalfHeight - (transform.localScale.y / 2.0f);
        Vector2 position = transform.position;
        position += Velocity * Time.deltaTime;
        position.y = Math.Clamp(position.y, -heightLimit, heightLimit);
        transform.position = position;
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
    
}