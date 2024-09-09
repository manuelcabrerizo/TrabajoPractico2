using System;
using UnityEngine;

public enum PowerUpType
{
    Height,
    Shoot,
    
    // the count of types in the enum
    // this should be always the last
    // element in the enum
    Count
}

public class PowerUp : MonoBehaviour
{
    public PowerUpType Type { get; set; }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.SendMessage("GrabPowerUp", Type);
        gameObject.SetActive(false);
    }
}
