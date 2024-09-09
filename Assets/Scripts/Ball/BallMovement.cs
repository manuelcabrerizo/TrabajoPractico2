using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class BallMovement : MonoBehaviour
{
    [SerializeField] private float MaxSpeed = 50;
    
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void Stop()
    {
        transform.position = new Vector2();
        _rigidbody2D.velocity = new Vector2();
    }

    private void Update()
    {
        Vector2 currentVelocity = _rigidbody2D.velocity;
        if (currentVelocity.SqrMagnitude() > (MaxSpeed * MaxSpeed))
        {
            currentVelocity = currentVelocity.normalized * MaxSpeed;
        }
        _rigidbody2D.velocity = currentVelocity;
    }

    public void Reset()
    {
        transform.position = new Vector2();
        _rigidbody2D.velocity = new Vector2();
        Vector2 force = RandomDirection();
        _rigidbody2D.AddForce(force * 10, ForceMode2D.Impulse);
    }
    
    private Vector2 RandomDirection()
    {
        const float quarterOfPI = (float)Math.PI / 4;
        float sign = (Random.Range(0, 2) == 1) ? 1.0f : -1.0f;
        float angle = Random.Range(-quarterOfPI, quarterOfPI);
        return new Vector2((float)Math.Cos(angle) * sign, (float)Math.Sin(angle));
    }
}
