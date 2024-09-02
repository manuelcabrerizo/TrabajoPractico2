using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
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
