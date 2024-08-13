using UnityEngine;

public class Movement : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private UIPause _uiPause;

    [SerializeField] private float speed = 10;
    [SerializeField] private KeyCode keyUp = KeyCode.UpArrow;
    [SerializeField] private KeyCode keyDown = KeyCode.DownArrow;
    [SerializeField] private KeyCode keyLeft = KeyCode.LeftArrow;
    [SerializeField] private KeyCode keyRight = KeyCode.RightArrow;
    
    private void Awake() 
    {
        _uiPause = FindObjectOfType<UIPause>();
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    void Update() 
    {
        if (_uiPause && _uiPause.IsActive())
        {
            return;
        }

        Vector3 position = transform.position;    
        if(Input.GetKey(keyRight))
        {
            position.x += speed * Time.deltaTime;
        }
        if(Input.GetKey(keyLeft)) 
        {
            position.x -= speed * Time.deltaTime;
        }
        if(Input.GetKey(keyUp)) 
        {
            position.y += speed * Time.deltaTime;
        }
        if(Input.GetKey(keyDown)) 
        {
            position.y -= speed * Time.deltaTime;
        }
        transform.position = position;

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
}