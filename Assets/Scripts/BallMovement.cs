using System;
using UnityEngine;
using Random = UnityEngine.Random;


struct Plane2D
{
    public Vector2 Point;
    public Vector2 Normal;
    
    public Plane2D(Vector2 point, Vector2 normal)
    {
        Point = point;
        Normal = normal;
    }
}

public class BallMovement : MonoBehaviour
{
    private const float QuarterOfPI = (float)Math.PI / 4;
    
    private float _cameraHalfWidth;
    private float _cameraHalfHeight;
    
    private Vector2 _velocity;
    private float _speed = 10;
    private float _restitution = 0.8f; // TODO: remove this and apply damping
    // Start is called before the first frame update
    
    // TODO: move these to a enviroment class
    private Plane2D _left, _right, _top, _bottom;
    
    private void Start()
    {
        // set a starting random direction for the ball
        _velocity = RandomDirection() * _speed;
        
        //  get the dimensions of the camera for bound checking
        Camera currentCamera = FindObjectOfType<Camera>();
        _cameraHalfWidth = currentCamera.orthographicSize * currentCamera.aspect;
        _cameraHalfHeight = currentCamera.orthographicSize;

        _left = new Plane2D(new Vector2(-_cameraHalfWidth, 0), new Vector2(1, 0));
        _right = new Plane2D(new Vector2(_cameraHalfWidth, 0), new Vector2(-1, 0));
        _top = new Plane2D(new Vector2(0, _cameraHalfHeight), new Vector2(0, -1));
        _bottom = new Plane2D(new Vector2(0, -_cameraHalfHeight), new Vector2(0, 1));
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }

        Vector2 position = transform.position;
        position += _velocity * Time.deltaTime;
        
        ObstaclesCollision(ref position);
        //CirclePlaneCollision(ref position, _left);
        //CirclePlaneCollision(ref position, _right);
        CirclePlaneCollision(ref position, _top);
        CirclePlaneCollision(ref position, _bottom);
        
        transform.position = position;
    }
    
    private void CirclePlaneCollision(ref Vector2 position, Plane2D plane)
    {
        float radio = transform.localScale.x / 2.0f;
        Vector2 relativePosition = position - plane.Point;
        float distToPlane = Vector2.Dot(relativePosition, plane.Normal);
        if (distToPlane <= radio)
        {
            Vector2 contactPoint = position - (plane.Normal * distToPlane);
            ResolveCollision(ref position, new Vector2(), contactPoint, plane.Normal);
        }
    }

    private void ObstaclesCollision(ref Vector2 position)
    {
        float radio = transform.localScale.x / 2.0f;
        Obstacle[] obstacles = FindObjectsByType<Obstacle>(FindObjectsSortMode.None);
        foreach (Obstacle obstacle in obstacles)
        {
            Vector2 obstacleHalfSize = new Vector2(
                obstacle.transform.localScale.x / 2.0f,
                obstacle.transform.localScale.y / 2.0f);
            Vector2 obstaclePosition = obstacle.transform.position;
            // get the position of the ball relative to the obstacle
            Vector2 relPosition = position - obstaclePosition;
            // get the closest point in the obstacle to the circle
            Vector2 closestPoint = obstaclePosition + new Vector2(
                Math.Clamp(relPosition.x, -obstacleHalfSize.x, obstacleHalfSize.x),
                Math.Clamp(relPosition.y, -obstacleHalfSize.y, obstacleHalfSize.y));
            // check if the distance squared to the closest point is less than the radio square
            if ((position - closestPoint).sqrMagnitude <= (radio*radio))
            {
                // calculate the normal of the collision
                Vector2 normal = (position - closestPoint).normalized;
                // Resolve the collision
                ResolveCollision(ref position, obstacle.GetVelocity(), closestPoint, normal);
            }
        }
    }

    private void ResolveCollision(ref Vector2 position, Vector2 otherVelocity, Vector2 contactPoint, Vector2 normal)
    {
        float radio = transform.localScale.x / 2.0f;
        // resolve the interpenetration
        position = contactPoint + (normal * (radio + 0.001f));
        // resolve the velocity
        Vector2 obstacleVelocity = otherVelocity;
        // get the separating velocity between the two objects
        float separatingVelocity = Vector2.Dot(_velocity - obstacleVelocity, normal);
        if (separatingVelocity > 0)
        {
            // the objects are already separating or stationary 
            return;
        }
        float newSeparatingVelocity = -separatingVelocity * _restitution;
        float deltaVelocity = newSeparatingVelocity - separatingVelocity;
        Vector2 impulse = normal * deltaVelocity;
        _velocity += impulse;
    }

    private void Reset()
    {
        transform.position = new Vector2();
        _velocity = RandomDirection() * _speed;
    }

    private Vector2 RandomDirection()
    {
        float sign = (Random.Range(0, 2) == 1) ? 1.0f : -1.0f;
        float angle = Random.Range(-QuarterOfPI, QuarterOfPI);
        return new Vector2((float)Math.Cos(angle) * sign, (float)Math.Sin(angle));
    }
}
