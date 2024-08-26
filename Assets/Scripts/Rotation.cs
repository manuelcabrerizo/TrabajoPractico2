using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private UIPauseManager _pause;
    
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
        
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            transform.Rotate(Vector3.forward, 10.0f);
        }
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            transform.Rotate(Vector3.forward, -10.0f);
        }
    }
}
