using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class BallMovement : MonoBehaviour
{
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }
    
    private void Reset()
    {
        transform.position = new Vector2();
        _body.velocity = new Vector2();
        Vector2 force = RandomDirection();
        _body.AddForce(force * 10, ForceMode2D.Impulse);
    }
    
    private Vector2 RandomDirection()
    {
        const float QuarterOfPI = (float)Math.PI / 4;
        float sign = (Random.Range(0, 2) == 1) ? 1.0f : -1.0f;
        float angle = Random.Range(-QuarterOfPI, QuarterOfPI);
        return new Vector2((float)Math.Cos(angle) * sign, (float)Math.Sin(angle));
    }
}
