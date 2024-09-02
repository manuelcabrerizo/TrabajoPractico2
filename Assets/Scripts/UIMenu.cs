using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    protected UIPauseManager Pause;
    
    public void Awake()
    {
        Pause = FindObjectOfType<UIPauseManager>();
    }

}
