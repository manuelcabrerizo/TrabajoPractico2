using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandColor : MonoBehaviour
{
    private SpriteRenderer _sprite;
    void Awake()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R)) 
        {
            _sprite.color = new Color(Random.Range(0.0f, 1.0f),  Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }
}
