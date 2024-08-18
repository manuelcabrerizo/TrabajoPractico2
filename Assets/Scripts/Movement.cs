using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private UIPauseManager _pause;
    private float _speed = 10;
    
    [SerializeField] private KeyCode keyUp = KeyCode.UpArrow;
    [SerializeField] private KeyCode keyDown = KeyCode.DownArrow;
    [SerializeField] private KeyCode keyLeft = KeyCode.LeftArrow;
    [SerializeField] private KeyCode keyRight = KeyCode.RightArrow;
    
    private void Awake() 
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _pause = FindObjectOfType<UIPauseManager>();
    }

    private void Update() 
    {
        if ((bool)_pause && _pause.IsActive())
        {
            return;
        }

        Vector3 velocity = Vector3.zero;
        if(Input.GetKey(keyRight))
        {
            velocity.x += 1;
        }
        if(Input.GetKey(keyLeft)) 
        {
            velocity.x -= 1;
        }
        if(Input.GetKey(keyUp)) 
        {
            velocity.y += 1;
        }
        if(Input.GetKey(keyDown)) 
        {
            velocity.y -= 1;
        }

        if (velocity.sqrMagnitude > 0)
        {
            transform.position += velocity.normalized * (_speed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            transform.Rotate(Vector3.forward, 10.0f);
        }
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            transform.Rotate(Vector3.forward, -10.0f);
        }
        if(Input.GetKeyUp(KeyCode.R)) 
        {
            _sprite.color = new Color(Random.Range(0.0f, 1.0f),  Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
}