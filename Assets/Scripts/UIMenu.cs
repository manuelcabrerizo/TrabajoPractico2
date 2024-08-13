using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    protected UIPause uiPause;
    
    public void Start()
    {
        uiPause = FindObjectOfType<UIPause>();
    }

}
