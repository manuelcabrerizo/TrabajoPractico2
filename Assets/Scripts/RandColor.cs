using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandColor : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private UIPauseManager _pause;
    
    void Awake()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _pause = FindObjectOfType<UIPauseManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if ((bool)_pause && _pause.IsActive())
        {
            return;
        }
        
        if(Input.GetKeyUp(KeyCode.R)) 
        {
            _sprite.color = new Color(Random.Range(0.0f, 1.0f),  Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }
}
